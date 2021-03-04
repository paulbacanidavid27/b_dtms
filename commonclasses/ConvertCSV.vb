Public Class ConvertCSV
    Dim sComma As String = ","
    Public Sub New()

    End Sub
    Public Property pComma As String
        Get
            Return sComma
        End Get
        Set(ByVal value As String)
            sComma = value
        End Set
    End Property
    Public Function uf_read_csv_dtl(ByVal uploadFileName As System.Web.UI.WebControls.FileUpload) As DataTable
        Dim lo_rows As String()
        Dim li_row_index, li_tbl_count, li_tbl0_end_ind, li_start_ind As Integer
        Dim ls_file_content As String

        Dim ldr As DataRow

        ls_file_content = uf_get_file_content(uploadFileName.FileContent)

        lo_rows = Split(ls_file_content, vbCrLf)
        li_tbl_count = uf_get_table_count(lo_rows, li_tbl0_end_ind)

        If li_tbl_count > 1 Then
            li_start_ind = li_tbl0_end_ind + 1 ' 1 for remarks
        Else
            li_start_ind = 0
        End If

        Using ldt As New DataTable
            If (lo_rows.Length - li_start_ind) > 0 Then
                uf_create_table_columns(uf_get_column_count(lo_rows(li_start_ind)) + 1, ldt)

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

    Public Function uf_filter_values(ByVal as_word As String) As String
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

    Public Sub uf_append_row(ByVal as_line As String, ByVal adr_row As DataRow)
        Dim ls_comma As String = sComma
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

    Public Function uf_get_column_count(ByVal as_line As String) As Integer
        Dim ls_comma As String = sComma
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

    Public Sub uf_create_table_columns(ByVal ai_total_cols As Integer, ByRef adt_table As DataTable)
        Dim li_index As Integer

        For li_index = 0 To ai_total_cols - 1
            If li_index < 0 Then

                adt_table.Columns.Add("Document_Type")
            Else
                adt_table.Columns.Add("col" & CStr(li_index))
            End If

        Next
    End Sub
    Public Function uf_get_table_count(ByVal as_row As String(), ByRef ai_tbl0_end_ind As Integer) As Integer
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

    Public Function uf_get_file_content(ByVal as_file_stream As System.IO.Stream) As String
        Dim ls_file_content As String
        Dim lo_stream_reader As System.IO.StreamReader

        lo_stream_reader = New System.IO.StreamReader(as_file_stream)

        ls_file_content = lo_stream_reader.ReadToEnd

        lo_stream_reader.Close()

        Return ls_file_content
    End Function

End Class
