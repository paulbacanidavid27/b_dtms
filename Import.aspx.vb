Public Class Import
    Inherits System.Web.UI.Page

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.SelectTab("Import")
        'UserControlAdminMenuH1.SelectTab("Import")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim oDt As New DocTypes
            oDt.pGroupId = DocSession.sUserGroup
            dlDocType.DataSource = oDt.GetDocType()
            dlDocType.DataTextField = "DocName"
            dlDocType.DataValueField = "doctype"
            dlDocType.DataBind()
            tbLocation.Text = DocSession.DocLoc
            loadStatus()
            GetOffices()
        End If
    End Sub
    Private Sub loadStatus()
        Dim oDoc As New DocTypes
        

        Using ldata As DataTable = oDoc.GetDocStatus

            dlStatus.DataTextField = "description"
            dlStatus.DataValueField = "statusid"
            dlStatus.DataSource = ldata
            dlStatus.SelectedValue = "2"
            dlStatus.DataBind()
        End Using

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
                    ldata.DefaultView.Sort = "col0 asc"


                    Dim drows As DataRow
                    Dim lsDocType As String = ""
                    Dim lsDocType1 As String = "" 'used in validating documents to be imported
                    Dim lsDocType2 As String = ""

                    If rbDocType.SelectedValue = "Yes" Then
                        Dim liCol2 As Integer = 0
                        liCol2 = CInt(IIf(tbDocTypeColNumber.Text.Trim = "", "1", tbDocTypeColNumber.Text)) - 1
                        For Each drows In ldata.Rows
                            lsDocType = ",'" & drows(liCol2).ToString.Trim() & "'"
                            If lsDocType2.IndexOf(lsDocType) < 0 Then

                                lsDocType2 = lsDocType2 & ",'" & drows(liCol2).ToString.Trim() & "'"


                            End If
                        Next
                    Else
                        lsDocType2 = "'" & dlDocType.SelectedValue & "'"
                    End If
                    If Left(lsDocType2, 1) = "," Then
                        lsDocType2 = lsDocType2.Substring(1)
                    End If
                    'step 2 - derive column headers
                    Dim oDocType As New DocTypes
                    oDocType.pDocType = lsDocType2
                    Dim lds As DataSet = oDocType.GetDocTypeIndex()
                    'Dim lsDT As String
                    If lds.Tables.Count > 0 Then
                        If lds.Tables(0).Rows.Count < 1 Then
                            Master.ShowMessage("The Import Column sequence is not properly setup for the following document type(s): " & lsDocType2 & ". Please setup in Document Types screen.")
                            Exit Sub
                        End If

                    End If
                    Dim ldt, ldata2, ldtcolumnid As New DataTable
                    Dim liPrevRow, liDColNo As Integer
                    Dim nrows As DataRow
                    Try


                        liPrevRow = 0
                        If rbDocType.SelectedValue = "Yes" Then
                            liDColNo = CInt(IIf(tbDocTypeColNumber.Text.Trim = "", "0", tbDocTypeColNumber.Text.Trim))

                        Else
                            liDColNo = 0
                        End If

                        'generate table columns
                        Dim lsCol As String
                        lsCol = ""

                        ldata2 = lds.Tables(0)
                        ldtcolumnid = lds.Tables(1)
                        rptDocIndex.DataSource = ldtcolumnid
                        rptDocIndex.DataBind()

                        For Each drows In ldata2.Rows
                            If liDColNo > liPrevRow And liDColNo < CInt(drows(1)) Then
                                If lsCol.IndexOf(",Document Type") >= 0 Then
                                    Master.ShowMessage("Document Type already exists in the import file. Select the option 'Document Type already included in Import File'")
                                    Exit Sub
                                End If

                                ldt.Columns.Add("Document Type", Type.GetType("System.String"))
                                lsCol = lsCol & "," & "Document Type"
                            End If
                            If lsCol.IndexOf("," & CStr(drows(0)).Trim & ",") >= 0 Then
                                Master.ShowMessage("Duplicate '" & drows(0) & "' column. Please check the Document Indexes of all the Document Types you are importing.")
                                Exit Sub
                            End If

                            ldt.Columns.Add(drows(0), Type.GetType("System.String"))

                            liPrevRow = CInt(drows(1))
                            lsCol = lsCol & "," & drows(0)
                        Next




                        'If rbDocType.SelectedValue = "No" Then
                        '    ldt.Columns.Add("Document Type", Type.GetType("System.String"))
                        '    lsCol = lsCol & "," & "Document Type"
                        'End If
                        Dim lsMinusCol As Integer = 0
                        Dim lsOfcCode As String = ""
                        If rbDocType.SelectedValue <> "Yes" Then
                            lsMinusCol += 1
                        End If

                        If IsNumeric(tbOfficeColumnNumber.Text) Then
                            'lsMinusCol += 1
                            ldt.Columns.Add("Reference Number", Type.GetType("System.String"))
                        ElseIf tbOfficeColumnNumber.Text.Trim <> "" Then
                            lsOfcCode = tbOfficeColumnNumber.Text.Trim
                            lsMinusCol += 1
                            ldt.Columns.Add("Reference Number", Type.GetType("System.String"))
                        End If

                        ldt.Columns.Add("File Name", Type.GetType("System.String"))
                        'lsMinusCol += 1
                        ldt.Columns.Add("Comments", Type.GetType("System.String"))
                        lsMinusCol += 1
                        'If GenRefNo() Then

                        'End If


                        If (ldt.Columns.Count - lsMinusCol) <> ldata.Columns.Count Then
                            Master.ShowMessage("The columns in CSV file doesn't match with the number of Document Index columns.")
                            Exit Sub
                        End If
                        Dim liCol, okctr As Integer
                        Dim oDoc As New DocTypes
                        Dim lsFile, lsFileNotImported, lsComments, lsOfficeCode As String
                        'add records to the datatable
                        okctr = 0
                        lsFileNotImported = ""
                        lsOfficeCode = ""
                        Dim liNextRefNo As Integer = CInt(DocSession.CheckNextID(lsOfcCode))
                        For Each drows In ldata.Rows
                            nrows = ldt.NewRow()

                            'If rbDocType.SelectedValue = "No" Then
                            'nrows("Document Type") = dlDocType.SelectedValue
                            'End If
                            liCol = 0
                            For Each dcol As DataColumn In ldt.Columns
                                If liCol < ldata.Columns.Count Then
                                    nrows(dcol.ColumnName) = drows(liCol)
                                End If
                                If dcol.ColumnName = "Comments" Then
                                    If rbDocType.SelectedValue = "No" Then
                                        lsDocType1 = dlDocType.SelectedValue
                                    Else
                                        lsDocType1 = nrows("Document Type")
                                    End If


                                    If lsOfcCode <> "" Then
                                        lsOfficeCode = lsOfcCode
                                        lsFile = drows(CInt(tbFileNameColNumber.Text) - 1)
                                    ElseIf IsNumeric(tbOfficeColumnNumber.Text) Then
                                        lsOfficeCode = drows(CInt(tbOfficeColumnNumber.Text) - 1) 'GetOfficeCode(drows(CInt(tbOfficeColumnNumber.Text) - 1))
                                        lsFile = drows(CInt(tbFileNameColNumber.Text) - 1)
                                    Else
                                        lsFile = drows(CInt(tbFileNameColNumber.Text) - 1)
                                    End If
                                    'lsOfficeCode = GetOfficeCode(drows(CInt(tbOfficeColumnNumber.Text) - 1))
                                    Dim lsTitle As String
                                    Try
                                        lsTitle = lsFile 'nrows("Purpose")
                                        'remove extension if there is
                                        If Left(Right(lsTitle.Trim(), 4), 1) = "." Then  'file extension is only 3 chars likd pdf, gif, jpg
                                            lsTitle = lsTitle.Trim.Substring(0, Len(lsTitle.Trim) - 4)
                                        ElseIf Left(Right(lsTitle.Trim(), 5), 1) = "." Then  'file extension is only 4 chars likd jpeg, tiff, docx, pptx, xlsx
                                            lsTitle = lsTitle.Trim.Substring(0, Len(lsTitle.Trim) - 5)
                                        End If
                                    Catch ex As Exception
                                        'Master.ShowMessage("Purpose column does not exist for the selected Document Type. Please add 'Purpose' column in the Document Type screen to continue.")
                                        Exit Sub
                                    End Try

                                    lsComments = IIf(oDoc.CheckIfDocExists(lsDocType1, lsFile, lsTitle), "Document already exists.", "")
                                    lsComments = lsComments & IIf(DocumentExists(lsFile) = True, "", "File does not exists in the server.")
                                    lsComments = lsComments & IIf(lsOfficeCode <> "", "", "Office/Agency does not exists from the list.")
                                    nrows("File Name") = lsFile
                                    If GenRefNo() Then
                                        nrows("Reference Number") = DocSession.GenerateRefNo(lsOfficeCode, CStr(liNextRefNo)) 'DocSession.getNextID(lsOfcCode))
                                        liNextRefNo += 1
                                    End If

                                    nrows("Comments") = IIf(lsComments.Trim = "", "OK to import.", lsComments & " Document will not be imported.")
                                    If lsComments.Trim = "" Then
                                        okctr += 1
                                    Else
                                        If lsFileNotImported = "" Then
                                            lsFileNotImported = lsFile
                                        Else
                                            lsFileNotImported = lsFileNotImported & ", " & lsFile
                                        End If

                                    End If

                                End If

                                liCol += 1

                            Next
                            ldt.Rows.Add(nrows)
                        Next

                        'Dim pnl As New Panel
                        'Dim tbl As New Table
                        'Dim dgrd As New GridView

                        'dgrd.AutoGenerateColumns = False
                        'dgrd.DataSource
                        'dgrd.DataBind()


                        'Dim rw As TableRow
                        'Dim cll As TableCell
                        'PlaceHolder1.Controls.Add(pnl)
                        'pnl.HorizontalAlign = HorizontalAlign.Center
                        'rw = New TableRow
                        'rw.BorderColor = System.Drawing.Color.Beige
                        'rw.BorderWidth = Unit.Pixel(2)
                        'cll = New TableCell
                        'cll.Text = "one"
                        'rw.Controls.Add(cll)
                        'cll = New TableCell
                        'cll.Text = "two"
                        'rw.Controls.Add(cll)
                        'tbl.Controls.Add(rw)
                        'tbl.GridLines = GridLines.Both
                        'pnl.Controls.Add(tbl)

                        lRecordCount.Text = "No of rows to import: " & ldata.Rows.Count.ToString & "      OK: " & okctr.ToString("#,##0") & vbCrLf & " Files not imported: " & lsFileNotImported
                        rptUpload.Visible = True
                        If rbDocType.SelectedValue = "yes" Then
                            ldt.DefaultView.Sort = "Document Type"
                        End If

                        rptUpload.DataSource = ldt

                        rptUpload.DataBind()
                        'rptUpload.Sort("col0", SortDirection.Ascending)
                        'cbxSelectAll.Checked = True

                        btImport.Visible = True
                        btUpload.Visible = False
                        btCancel.Visible = True
                        step5.Visible = True
                        pnlDocType.Update()
                    Catch ex As Exception
                        Throw New Exception(ex.Message)
                    Finally
                        If Not lds Is Nothing Then
                            lds.Dispose()
                            lds = Nothing
                        End If
                        If Not ldt Is Nothing Then
                            ldt.Dispose()
                            ldt = Nothing

                        End If
                        If Not ldata2 Is Nothing Then
                            ldata2.Dispose()
                            ldata2 = Nothing
                        End If
                        If Not ldtcolumnid Is Nothing Then
                            ldtcolumnid.Dispose()
                            ldtcolumnid = Nothing
                        End If
                    End Try
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


    Private Sub cbDocType_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbDocType.CheckedChanged
        If cbDocType.Checked Then
            tbDocTypeColNumber.ReadOnly = False
            tbDocTypeColNumber.CssClass = "entryfldint"
            tbFileNameColNumber.ReadOnly = False
            tbFileNameColNumber.CssClass = "entryfldint"
        Else
            tbDocTypeColNumber.Text = ""
            tbDocTypeColNumber.ReadOnly = True
            tbDocTypeColNumber.CssClass = "entryfldintdisabled"
            tbFileNameColNumber.Text = ""
            tbFileNameColNumber.ReadOnly = True
            tbFileNameColNumber.CssClass = "entryfldintdisabled"
        End If


    End Sub

    Private Sub MoveDocuments(ByVal asFile As String, ByVal asDocId As String, ByVal asloc As String)
        If System.IO.File.Exists(tbLocation.Text.Trim & asFile) Then

            System.IO.File.Move(tbLocation.Text.Trim & asFile, DocSession.FileLoc & asloc & "\" & asDocId & "_1_" & asFile)
        End If

    End Sub

    Private Function DocumentExists(ByVal asFile As String) As Boolean

        If System.IO.File.Exists(tbLocation.Text.Trim & asFile) Then
            Return True
        Else
            Return False
        End If
    End Function

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
        Return (tbOfficeColumnNumber.Visible AndAlso tbOfficeColumnNumber.Text <> "") OrElse (dlOffice.Visible AndAlso dlOffice.SelectedValue <> "")
    End Function

    Private Sub btImport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btImport.Click
        Dim oIndex As New DocIndex
        Dim oList As DocList
        Dim lsDocType As String
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim lColId As Literal
        'Dim lColValue As TextBox
        'Dim oConn As New SqlClient.SqlConnection(str)
        'Dim ltr As SqlClient.SqlTransaction
        Dim ltr As New DbTran
        Dim objCommand As clsSqlConn
        'Dim liCtr As Integer
        'Dim oIndex As New DocIndex

        'liCtr = 0
        Try
            oIndex = New DocIndex
            oList = New DocList
            'oList.ExistsInInbox()

            objCommand = New clsSqlConn(ltr.pTran)
            Dim lsdate As String = DateTime.Now.ToString
            Dim lsloc As String = Year(CDate(lsdate)).ToString & "\" & MonthName(Month(CDate(lsdate)))
            Dim liDocID, liDocNextId, liNextRefNo As Integer
            Dim lsFile, lsOffice, lsComment, lsColValue As String
            Dim lodata As DataTable
            Dim lbImported As Boolean = False
            'Dim lsSeqNo As String
            lsOffice = ""

            If tbOfficeColumnNumber.Text.Trim <> "" Then
                lsOffice = tbOfficeColumnNumber.Text
            End If
            liNextRefNo = CInt(DocSession.getNextID("refnodoc"))
            liDocNextId = CInt(DocSession.getNextID("docid"))
            For Each grv As GridViewRow In rptUpload.Rows
                lsDocType = IIf(rbDocType.SelectedValue = "Yes", grv.Cells(CInt(tbDocTypeColNumber.Text) - 1).Text, dlDocType.SelectedValue)

                'If GenRefNo() Then
                'lsFile = Server.HtmlDecode(grv.Cells(CInt(tbFileNameColNumber.Text)).Text) ' no need to -1 since I added a reference number
                'Else
                lsFile = Server.HtmlDecode(grv.Cells(CInt(tbFileNameColNumber.Text)).Text) ' no need to -1 since I added a reference number
                'End If
                If IsNumeric(tbOfficeColumnNumber.Text) Then
                    lsOffice = Server.HtmlDecode(grv.Cells(CInt(tbOfficeColumnNumber.Text) - 1).Text)
                End If 'need additional functionality here

                lsComment = Server.HtmlDecode(grv.Cells(grv.Cells.Count - 1).Text)

                If lsFile.Trim <> "" And lsComment = "OK to import." Then
                    lodata = getColumnId(lsDocType)
                    'lsSeqNo = DocSession.getNextID("seqno")
                    oIndex.pApp = ""
                    oIndex.pIPAddress = Request.UserHostAddress
                    oIndex.pDocId = liDocNextId 'DocSession.getNextID("docid")

                    oIndex.pFileName = lsFile.ToLower
                    oIndex.pTitle = lsFile
                    oIndex.pDocType = lsDocType.ToLower
                    oIndex.pStat = dlStatus.SelectedValue
                    oIndex.pPages = "1"
                    oIndex.pCopies = "1"
                    oIndex.pMannerReceipt = "1"
                    liDocID = CInt(oIndex.pDocId)
                    oIndex.pCreatedDate = lsDate
                    oIndex.pSeqNo = ""
                    oIndex.pCCTo = ""
                    oIndex.pOfcCode = lsOffice 'GetOfficeCode(lsOffice)
                    If lsOffice = "" Then
                        oIndex.pRefNo = ""
                    Else
                        oIndex.pRefNo = DocSession.GenerateRefNo(oIndex.pOfcCode, liNextRefNo)
                        liNextRefNo += 1
                    End If

                    oIndex.SaveDocList(objCommand)
                    oIndex.SaveDocListFileVersion(objCommand)

                    oList.pDocId = liDocNextId.ToString
                    oList.pUserId = DocSession.sUserId
                    oList.pSeqNo = "0"
                    oList.AddToInbox(objCommand) ', oList.pExistsInInbox)
                    liDocNextId += 1
                    If Not cbFileName.Checked Then
                        For Each lrow As DataRow In lodata.Rows
                            oIndex.pDocId = liDocID
                            oIndex.pDocType = lsDocType
                            oIndex.pColId = CInt(lrow.Item("ColId"))
                            lsColValue = grv.Cells(CInt(lrow.Item("ColSeq")) - 1).Text
                            oIndex.pColValue = IIf(lsColValue.Trim = "&nbsp;", "", lsColValue)
                            oIndex.SaveDocIndexValues(objCommand)
                        Next

                    End If
                    MoveDocuments(lsFile, CStr(liDocID), lsloc & "\" & oIndex.pRefNo)
                    lbImported = True
                End If
            Next

            'lcheckmsg.Text = "Document Index has been saved successfully."
            'lcheckmsg.CssClass = "msg_green"
            ltr.pTran.Commit()
            DocSession.UpdateNextID("docid", CStr(liDocNextId))
            DocSession.UpdateNextID("refnodoc", CStr(liNextRefNo))
            If lbImported Then
                Master.ShowMessage("All the documents 'ok to import' has been successfully imported.")
            Else
                Master.ShowMessage("No document has been imported. Please check your import file and try again.")
            End If

            rptUpload.Visible = False
            btUpload.Visible = True
            btImport.Visible = False
            step5.Visible = False

        Catch ex As Exception

            If Not ltr Is Nothing Then
                ltr.pTran.Rollback()
            End If
            Master.ShowMessage("There's an error while saving the record (" & ex.Message & "). Please try again.")
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
            If Not oIndex Is Nothing Then
                oIndex = Nothing
            End If
            If Not oList Is Nothing Then
                oList = Nothing
            End If
        End Try
    End Sub

    Private Sub rbDocType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbDocType.SelectedIndexChanged
        If rbDocType.SelectedValue = "Yes" Then
            lSelectDoc.Visible = False
            dlDocType.Visible = False
            lDocTypeColNumber.Visible = True
            tbDocTypeColNumber.Visible = True
        Else
            lSelectDoc.Visible = True
            dlDocType.Visible = True
            lDocTypeColNumber.Visible = False
            tbDocTypeColNumber.Visible = False
        End If
        pnlDocType.Update()
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

    Private Sub btUploadFiles_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btUploadFiles.Click
        ucUploadFiles1.Visible = True
        '  ucUploadFiles1.getFiles()
    End Sub

    Private Sub rbDocLoc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbDocLoc.SelectedIndexChanged
        If rbDocLoc.SelectedItem.Text = "Upload" Then
            ucUploadFiles1.Visible = True
            tbLocation.Text = DocSession.UploadLoc
            btUploadFiles.Visible = True
        Else
            btUploadFiles.Visible = False
            tbLocation.Text = DocSession.DocLoc
        End If
    End Sub

    Private Sub cbFileName_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbFileName.CheckedChanged
        If cbFileName.Checked Then
            lSelectDoc.Visible = True
            dlDocType.Visible = True
            lDocTypeColNumber.Visible = False
            tbDocTypeColNumber.Visible = False
            rbDocType.SelectedValue = "No"
            rbDocType.Enabled = False
            btUpload.Visible = False
            btUploadFileName.Visible = True
            tbFileNameColNumber.Enabled = False
            tbFileNameColNumber.Text = "1"
            dlOffice.Visible = True
            tbOfficeColumnNumber.Visible = False
            lOfficeCode.Visible = True
            lOfficeColumn.Visible = False

        Else
            rbDocType.Enabled = True
            btUpload.Visible = True
            btUploadFileName.Visible = False
            tbFileNameColNumber.Enabled = True
            dlOffice.Visible = False
            tbOfficeColumnNumber.Visible = True
            lOfficeCode.Visible = False
            lOfficeColumn.Visible = True
        End If
    End Sub
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

            dlOffice.DataSource = ldata
            dlOffice.DataValueField = "OfficeCode"
            dlOffice.DataTextField = "Description"
            dlOffice.DataBind()

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
            For i = 0 To dlOffice.Items.Count - 1
                If dlOffice.Items(i).Text.ToLower.Trim = asOffice.ToLower.Trim Then
                    lsOfficeCode = dlOffice.Items(i).Value
                    
                End If
            Next
            
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


                    ''btUpload.Visible = False
                    ''step 1
                    ldata.DefaultView.Sort = "col0 asc"


                    'Dim drows As DataRow
                    Dim lsDocType As String = ""
                    Dim lsDocType1 As String = "" 'used in validating documents to be imported
                    Dim lsDocType2 As String = ""

                    'If rbDocType.SelectedValue = "Yes" Then
                    '    Dim liCol2 As Integer = 0
                    '    liCol2 = CInt(IIf(tbDocTypeColNumber.Text.Trim = "", "1", tbDocTypeColNumber.Text)) - 1
                    '    For Each drows In ldata.Rows
                    '        lsDocType = ",'" & drows(liCol2).ToString.Trim() & "'"
                    '        If lsDocType2.IndexOf(lsDocType) < 0 Then

                    '            lsDocType2 = lsDocType2 & ",'" & drows(liCol2).ToString.Trim() & "'"


                    '        End If
                    '    Next
                    'Else
                    lsDocType2 = "'" & dlDocType.SelectedValue & "'"
                    'End If
                    If Left(lsDocType2, 1) = "," Then
                        lsDocType2 = lsDocType2.Substring(1)
                    End If

                    ''step 2 - derive column headers
                    'Dim oDocType As New DocTypes
                    'oDocType.pDocType = lsDocType2
                    'Dim lds As DataSet = oDocType.GetDocTypeIndex()
                    ''Dim lsDT As String
                    'If lds.Tables.Count > 0 Then
                    'If lds.Tables(0).Rows.Count < 1 Then
                    'Master.ShowMessage("The Import Column sequence is not properly setup for the following document type(s): " & lsDocType2 & ". Please setup in Document Types screen.")
                    'Exit Sub
                    'End If

                End If
                Dim ldt, ldata2, ldtcolumnid As New DataTable
                Dim liPrevRow, liDColNo As Integer
                Dim nrows As DataRow

                liPrevRow = 0
                'If rbDocType.SelectedValue = "Yes" Then
                'liDColNo = CInt(IIf(tbDocTypeColNumber.Text.Trim = "", "0", tbDocTypeColNumber.Text.Trim))

                'Else
                liDColNo = 0
                'End If

                'generate table columns
                Dim lsCol As String
                lsCol = ""

                'ldata2 = lds.Tables(0)
                'ldtcolumnid = lds.Tables(1)
                'rptDocIndex.DataSource = ldtcolumnid
                'rptDocIndex.DataBind()

                'For Each drows In ldata2.Rows
                'If liDColNo > liPrevRow And liDColNo < CInt(drows(1)) Then
                'If lsCol.IndexOf(",Document Type") >= 0 Then
                'Master.ShowMessage("Document Type already exists in the import file. Select the option 'Document Type already included in Import File'")
                'Exit Sub
                'End If

                'ldt.Columns.Add("Document Type", Type.GetType("System.String"))
                'lsCol = lsCol & "," & "Document Type"
                'End If
                'If lsCol.IndexOf("," & CStr(drows(0)).Trim & ",") >= 0 Then
                'Master.ShowMessage("Duplicate '" & drows(0) & "' column. Please check the Document Indexes of all the Document Types you are importing.")
                'Exit Sub
                'End If

                'ldt.Columns.Add(drows(0), Type.GetType("System.String"))

                'liPrevRow = CInt(drows(1))
                'lsCol = lsCol & "," & drows(0)
                'Next

                ldt.Columns.Add("File Name", Type.GetType("System.String"))
                ldt.Columns.Add("Comments", Type.GetType("System.String"))
                'If rbDocType.SelectedValue = "No" Then
                '    ldt.Columns.Add("Document Type", Type.GetType("System.String"))
                '    lsCol = lsCol & "," & "Document Type"
                'End If

                'If (ldt.Columns.Count - 1) <> ldata.Columns.Count Then
                'Master.ShowMessage("The columns in CSV file doesn't match with the number of Document Index columns.")
                'Exit Sub
                'End If
                Dim liCol As Integer
                Dim oDoc As New DocTypes
                Dim lsFile, lsComments As String
                'add records to the datatable
                For Each drows In ldata.Rows
                    nrows = ldt.NewRow()

                    'If rbDocType.SelectedValue = "No" Then
                    'nrows("Document Type") = dlDocType.SelectedValue
                    'End If
                    liCol = 0
                    For Each dcol As DataColumn In ldt.Columns
                        If liCol < ldata.Columns.Count Then
                            nrows(dcol.ColumnName) = drows(liCol)
                        End If
                        If dcol.ColumnName = "Comments" Then

                            lsFile = drows(0)
                            Dim lsTitle As String
                            Try
                                lsTitle = lsFile 'nrows("Purpose")
                                'remove extension if there is
                                If Left(Right(lsTitle.Trim(), 4), 1) = "." Then  'file extension is only 3 chars likd pdf, gif, jpg
                                    lsTitle = lsTitle.Trim.Substring(0, Len(lsTitle.Trim) - 4)
                                ElseIf Left(Right(lsTitle.Trim(), 5), 1) = "." Then  'file extension is only 4 chars likd jpeg, tiff, docx, pptx, xlsx
                                    lsTitle = lsTitle.Trim.Substring(0, Len(lsTitle.Trim) - 5)
                                End If
                            Catch ex As Exception
                                Master.ShowMessage(ex.Message)
                                Exit Sub
                            End Try

                            lsComments = IIf(oDoc.CheckIfDocExists(dlDocType.SelectedValue, lsFile, lsTitle), "Document already exists.", "")
                            lsComments = lsComments & IIf(DocumentExists(lsFile) = True, "", "File does not exists in the server. Document will not be imported.")
                            nrows("Comments") = IIf(lsComments.Trim = "", "OK to import.", lsComments)
                        End If

                        liCol += 1

                    Next
                    ldt.Rows.Add(nrows)
                Next

                lRecordCount.Text = "No of rows to import: " & ldata.Rows.Count.ToString
                rptUpload.Visible = True
                If rbDocType.SelectedValue = "yes" Then
                    ldt.DefaultView.Sort = "Document Type"
                End If

                rptUpload.DataSource = ldt

                rptUpload.DataBind()
                'rptUpload.Sort("col0", SortDirection.Ascending)
                'cbxSelectAll.Checked = True

                btImport.Visible = True
                btUpload.Visible = False
                btCancel.Visible = True
                step5.Visible = True
                pnlDocType.Update()
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

    Private Sub imgViewFiles_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgViewFiles.Click
        ucViewFiles1.Visible = True
    End Sub
End Class