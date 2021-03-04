Public Class clsDocForms
    Dim FormId As String
    Dim FormFileName As String
    Dim Description As String
    Dim UploadedBy As String
    Dim UploadedDate As String
    Dim UploadedFromDate As String
    Dim UploadedToDate As String
    Dim FileSize As String
    Dim UserId As String

    Dim SortOrder As String
    Dim SortCol As String
    Dim IPAddress As String
    Dim ModifiedDate As String = DateTime.Now.ToString
    Dim RowsPerPage As String
    Dim Idx As String
#Region "Creator"
    Public Sub New()

    End Sub
#End Region
#Region "Default Properties"
    Public Property pSortOrder() As String
        Get
            Return SortOrder
        End Get
        Set(ByVal value As String)
            SortOrder = value
        End Set

    End Property

    Public Property pSortCol() As String
        Get
            Return SortCol
        End Get
        Set(ByVal value As String)
            SortCol = value
        End Set

    End Property


    Public Property pIPAddress() As String
        Get
            Return IPAddress
        End Get
        Set(ByVal value As String)
            IPAddress = value
        End Set

    End Property


    Public Property pIdx() As String
        Get
            Return Idx
        End Get
        Set(ByVal value As String)
            Idx = value
        End Set

    End Property
    Public Property pUserId() As String
        Get
            Return UserId
        End Get
        Set(ByVal value As String)
            UserId = value
        End Set

    End Property
    Public Property pRowsPerPage() As String
        Get
            Return RowsPerPage
        End Get
        Set(ByVal value As String)
            RowsPerPage = value
        End Set

    End Property
#End Region
#Region "Table Columns"
    Public Property pFormId() As String
        Get
            Return FormId
        End Get
        Set(ByVal value As String)
            FormId = value
        End Set
    End Property
    Public Property pFormFileName() As String
        Get
            Return FormFileName
        End Get
        Set(ByVal value As String)
            FormFileName = value
        End Set
    End Property
    Public Property pDescription() As String
        Get
            Return Description
        End Get
        Set(ByVal value As String)
            Description = value
        End Set
    End Property
    Public Property pUploadedBy() As String
        Get
            Return UploadedBy
        End Get
        Set(ByVal value As String)
            UploadedBy = value
        End Set
    End Property
    Public Property pUploadedDate() As String
        Get
            Return UploadedDate
        End Get
        Set(ByVal value As String)
            UploadedDate = value
        End Set
    End Property
    Public Property pUploadedFromDate() As String
        Get
            Return UploadedFromDate
        End Get
        Set(ByVal value As String)
            UploadedFromDate = value
        End Set
    End Property
    Public Property pUploadedToDate() As String
        Get
            Return UploadedToDate
        End Get
        Set(ByVal value As String)
            UploadedToDate = value
        End Set
    End Property
    Public Property pFileSize() As String
        Get
            Return FileSize
        End Get
        Set(ByVal value As String)
            FileSize = value
        End Set
    End Property
#End Region
#Region "CUD Methods"

    Public Sub DeleteDocForms()

        Dim objCommand As clsSqlConn
        Try
            objCommand = New clsSqlConn
            Dim s_sql As String = "UPDATE DocForms SET deleted = 1 WHERE FormId =" & Replace(pFormId, "'", "''") & ""

            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = s_sql
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
    Public Sub DeleteDocForms(objCommand As clsSqlConn)


        Try

            Dim s_sql As String = "UPDATE DocForms SET deleted = 1 WHERE FormId =" & Replace(pFormId, "'", "''") & ""

            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = s_sql
            objCommand.ExecTranNonQuery()


        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally


        End Try

    End Sub
    Public Sub AddDocForms()
        Dim objCommand As clsSqlConn
        Try
            objCommand = New clsSqlConn


            Dim lsSql As String = " INSERT INTO DocForms " &
           "( " &
"FormFileName" &
",Description" &
",UploadedBy" &
",UploadedDate" &
",FileSize" &
            ") VALUES " &
            "(" &
"'" & Replace(pFormFileName, "'", "''") & "'" &
",'" & Replace(pDescription, "'", "''") & "'" &
",'" & Replace(pUploadedBy, "'", "''") & "'" &
",'" & Replace(pUploadedDate, "'", "''") & "'" &
",'" & Replace(pFileSize, "'", "''") & "'" &
           ") "
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = lsSql
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
    Public Sub UpdateDocForms()
        Dim objCommand As clsSqlConn
        Try
            objCommand = New clsSqlConn


            Dim lsSql As String = "UPDATE DocForms " &
            "SET Description='" & Replace(pDescription, "'", "''") & "'"

            If pFormFileName <> "" Then
                lsSql = lsSql & ",FormFileName='" & Replace(pFormFileName, "'", "''") & "'"
            End If

            If pUploadedBy <> "" Then
                lsSql = lsSql & ",UploadedBy='" & Replace(pUploadedBy, "'", "''") & "'"
            End If

            If pUploadedDate <> "" Then
                lsSql = lsSql & ",UploadedDate='" & Replace(pUploadedDate, "'", "''") & "'"
            End If

            If pFileSize <> "" Then
                lsSql = lsSql & ",FileSize='" & Replace(pFileSize, "'", "''") & "'"
            End If

            lsSql = lsSql & " WHERE FormId ='" & Replace(pFormId, "'", "''") & "'"
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = lsSql
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


#End Region
    Private Function WhereClause() As String
        Dim lswhere As String = ""
        If pFormId <> "" Then
            lswhere = lswhere & " " & "AND b.FormId = '" & Replace(pFormId, "'", "''") & "'"
        End If
        If pFormFileName <> "" Then
            lswhere = lswhere & " " & "AND b.FormFileName Like '%" & Replace(pFormFileName, "'", "''") & "%'"
        End If
        If pDescription <> "" Then
            lswhere = lswhere & " " & "AND b.Description Like '%" & Replace(pDescription, "'", "''") & "%'"
        End If
        If pUploadedBy <> "" Then
            lswhere = lswhere & " " & "AND u.FirstName+' '+u.LastName Like '%" & Replace(pUploadedBy, "'", "''") & "%'"
        End If
        If pUploadedFromDate <> "" Then
            lswhere = lswhere & " " & "AND b.UploadedDate >= '" & Replace(pUploadedDate, "'", "''") & " 00:00:00'"
        End If
        If pUploadedToDate <> "" Then
            lswhere = lswhere & " " & "AND b.UploadedDate <= '" & Replace(pUploadedToDate, "'", "''") & " 23:59:59 '"
        End If
        If pFileSize <> "" Then
            lswhere = lswhere & " " & "AND b.FileSize = '" & Replace(pFileSize, "'", "''") & "'"
        End If
        Return lswhere
    End Function
    Public Function CountDocForms() As Integer

        Dim objCommand As clsSqlConn

        Dim ldata As DataTable
        Dim s_sql As String

        Try
            s_sql = "SELECT count(*) " &
                  "FROM  " &
           "DocForms b inner join users u on b.UploadedBy = u.UserId  " &
          "WHERE (Deleted is null or Deleted=0) " & WhereClause()

            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql

            Return objCommand.ExecScalar

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

    End Function
    Public Function CheckIfDocFormsIdExists() As Boolean


        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn


            objCommand.pCommandType = CommandType.Text

            objCommand.pCommandText = "SELECT TOP 1 FormId FROM DocForms WHERE FormId = " & pFormId & ""


            Return objCommand.ExecHasRow()


        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally

            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If

        End Try

    End Function

#Region "Retrieval Methods"
    Public Function RetrieveDocForms() As DataTable
        Dim objCommand As clsSqlConn
        Dim ldata As DataTable
        Dim s_order As String = OrderBy()
        Dim lTop As Integer
        lTop = CInt(pIdx) * CInt(pRowsPerPage)
        Try
            Dim sSQL, sWHERE As String
            sSQL = "select * from ( SELECT "
            sSQL = sSQL & " TOP " & lTop.ToString & " "
            sSQL = sSQL & " (row_number() over (ORDER BY " & s_order & " " & pSortOrder & ")) AS rn " &
           ",b.FormId" &
           ",b.FormFileName" &
           ",b.Description" &
           ",b.UploadedBy" &
           ",u.FirstName+' '+u.LastName as UploadedByName" &
           ",b.UploadedDate" &
           ",b.FileSize" &
                       " FROM DocForms b Inner Join Users u on u.UserId =  b.UploadedBy WHERE (b.deleted is null or b.deleted = 0) "
            sWHERE = WhereClause()
            If sWHERE <> "" Then
                sSQL = sSQL & " " & sWHERE
            End If
            sSQL = sSQL & ") dtbl " &
           " WHERE dtbl.rn between " & pIdx & " and " & pRowsPerPage & "+" & pIdx & " "

            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = sSQL
            ldata = objCommand.Fill
            Return ldata
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
    End Function
    Public Function RetrieveDocFormsLookup() As DataTable
        Dim objCommand As clsSqlConn
        Dim ldata As DataTable
        Try
            objCommand = New clsSqlConn
            Dim sSQL, sWHERE As String
            sSQL = "SELECT " &
"b.FormId" &
",b.FormFileName" &
",b.Description" &
",b.UploadedBy" &
",b.UploadedDate" &
",b.FileSize" &
            " FROM DocForms b "
            sWHERE = WhereClause()
            If sWHERE <> "" Then
                sSQL = sSQL & " WHERE " & sWHERE
            End If

            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = sSQL
            ldata = objCommand.Fill

            Return ldata

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

    End Function

    Private Function OrderBy() As String
        If pSortCol = "FormId" Then
            Return " b.FormId "
        ElseIf pSortCol = "File Name" Then
            Return " b.FormFileName "
        ElseIf pSortCol = "Description" Then
            Return " b.Description "
        ElseIf pSortCol = "Uploaded By" Then
            Return " b.UploadedBy "
        ElseIf pSortCol = "Uploaded Date" Then
            Return " b.UploadedDate "
        ElseIf pSortCol = "FileSize" Then
            Return " b.FileSize "
        Else
            Return " b.FormId "
        End If
    End Function

#End Region
End Class