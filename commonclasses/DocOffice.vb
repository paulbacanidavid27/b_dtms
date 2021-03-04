Public Class DocOffice
    Dim OfcCode As String = ""
    Dim GroupID As String = ""
    Dim AddrCode As String = ""
    Dim OfcDesc As String = ""
    Dim RowsPerPage As String
    Dim Idx As String
    Dim SortOrder As String
    Dim SortCol As String
    Dim IPAddress As String
    Dim bDeleted As String
    Dim PointPersonId As String
    Dim UserId As String
    Dim ShowAsDefault As String
    Dim ModifiedDate As String = DateTime.Now.ToString
    Dim LandLineNo As String
    Dim LocalNo As String

    Public Property pShowAsDefault() As String
        Get
            Return ShowAsDefault
        End Get
        Set(ByVal value As String)
            ShowAsDefault = value
        End Set

    End Property
    Public Property pGroupId() As String
        Get
            Return GroupID
        End Get
        Set(ByVal value As String)
            GroupID = value
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

    Public Property pPointPersonId() As String
        Get
            Return PointPersonId
        End Get
        Set(ByVal value As String)
            PointPersonId = value
        End Set

    End Property

    Public Property pDeleted() As String
        Get
            Return bDeleted
        End Get
        Set(ByVal value As String)
            bDeleted = value
        End Set

    End Property
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


    Public Property pRowsPerPage() As String
        Get
            Return RowsPerPage
        End Get
        Set(ByVal value As String)
            RowsPerPage = value
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
    Public Sub New()

    End Sub
    Public Property pLocalNo() As String
        Get
            Return LocalNo
        End Get
        Set(ByVal value As String)
            LocalNo = value
        End Set

    End Property
    
    Public Property pLandLineNo() As String
        Get
            Return LandLineNo
        End Get
        Set(ByVal value As String)
            LandLineNo = value
        End Set

    End Property
    Public Property pOfcCode() As String
        Get
            Return OfcCode
        End Get
        Set(ByVal value As String)
            OfcCode = value
        End Set

    End Property

    Public Property pAddrCode() As String
        Get
            Return AddrCode
        End Get
        Set(ByVal value As String)
            AddrCode = value
        End Set

    End Property

    Public Property pOfcDesc() As String
        Get
            Return OfcDesc
        End Get
        Set(ByVal value As String)
            OfcDesc = value
        End Set

    End Property

    Public Function RetrievePointPerson(ByVal aOfcCode As String) As DataTable
        Dim ldata As DataTable
        Dim objCommand As clsSqlConn

        Try
            Dim s_sql As String = "select " & _
"u.email, " & _
"u.UserId, " & _
"u.FirstName+' '+u.LastName as Approver,'' as tbRem " & _
"from Office o " & _
"INNER join Users u ON " & _
            "o.pointperson = u.userid " & _
" where o.OfficeCode = '" & aOfcCode & "' and o.ShowAsDefault = 1 "

            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql
            ldata = objCommand.ExecData
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


    Public Sub AddOffice()

        Dim s_sql As String
        Dim objCommand As clsSqlConn
        'Dim ltr As DbTran
        Try
            s_sql = "INSERT INTO Office " & _
           "(OfficeCode " & _
           ",Description,PointPerson,ShowAsDefault,Deleted,AddressCode,LandlineNo,LocalNo) " & _
        "VALUES  " & _
           "('" & Replace(pOfcCode, "'", "''") & "'" & _
           ",'" & Replace(pOfcDesc, "'", "''") & "'" & _
           ",'" & Replace(pPointPersonId, "'", "''") & "'" & _
           "," & pShowAsDefault & " " & _
           ",0" & _
           ",'" & Replace(pAddrCode, "'", "''") & "' " & _
            ",'" & Replace(pLandLineNo, "'", "''") & "' " & _
            ",'" & Replace(pLocalNo, "'", "''") & "') "

            'ltr = New DbTran
            'objCommand = New clsSqlConn(ltr.pTran)
            objCommand = New clsSqlConn()
            

            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql
            objCommand.ExecNonQuery()

        Catch ex As Exception
            If InStr(ex.Message.ToLower, "primary key") > 0 Then
                Throw New Exception("Office Code " & pOfcCode & " already exists.")
            Else
                Throw New Exception(ex.Message)
            End If
        Finally

            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If


        End Try

    End Sub
    Public Sub AddOffice(ByVal objCommand As clsSqlConn)

        Dim s_sql As String

        'Dim ltr As DbTran
        Try
            s_sql = "INSERT INTO Office " & _
           "(OfficeCode " & _
           ",Description,PointPerson,ShowAsDefault,Deleted,AddressCode,LandlineNo,LocalNo) " & _
        "VALUES  " & _
           "('" & Replace(pOfcCode, "'", "''") & "'" & _
           ",'" & Replace(pOfcDesc, "'", "''") & "'" & _
           ",'" & Replace(pPointPersonId, "'", "''") & "'" & _
           "," & pShowAsDefault & " " & _
           ",0" & _
           ",'" & Replace(pAddrCode, "'", "''") & "' " & _
            ",'" & Replace(pLandLineNo, "'", "''") & "' " & _
            ",'" & Replace(pLocalNo, "'", "''") & "') "

            'ltr = New DbTran
            'objCommand = New clsSqlConn(ltr.pTran)
            'objCommand = New clsSqlConn()


            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql
            objCommand.ExecTranNonQuery()

        Catch ex As Exception
            If InStr(ex.Message.ToLower, "primary key") > 0 Then
                Throw New Exception("Office Code " & pOfcCode & " already exists.")
            Else
                Throw New Exception(ex.Message)
            End If
        Finally




        End Try

    End Sub
    Public Sub UpdateOffice()

        Dim s_sql As String
        Dim objCommand As clsSqlConn
        'Dim ltr As DbTran
        Try
            s_sql = "UPDATE Office SET " & _
           "Description = '" & Replace(pOfcDesc, "'", "''") & "' " & _
           ",AddressCode = '" & Replace(pAddrCode, "'", "''") & "' " & _
           ",PointPerson = '" & Replace(pPointPersonId, "'", "''") & "' " & _
           ",LandLineNo = '" & Replace(pLandLineNo, "'", "''") & "' " & _
           ",LocalNo = '" & Replace(pLocalNo, "'", "''") & "' " & _
           ",ShowAsDefault = " & pShowAsDefault & " " & _
           "WHERE OfficeCode = '" & Replace(pOfcCode, "'", "''") & "'"

            objCommand = New clsSqlConn()

            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql
            objCommand.ExecNonQuery()

        Catch ex As Exception
            'ltr.pTran.Rollback()
            Throw New Exception(ex.Message)
        Finally

            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If


        End Try

    End Sub


    Public Sub UpdateOffice(ByVal objCommand As clsSqlConn)

        Dim s_sql As String
        'Dim objCommand As clsSqlConn
        'Dim ltr As DbTran
        Try
            s_sql = "UPDATE Office SET " & _
           "Description = '" & Replace(pOfcDesc, "'", "''") & "' " & _
           ",AddressCode = '" & Replace(pAddrCode, "'", "''") & "' " & _
           ",PointPerson = '" & Replace(pPointPersonId, "'", "''") & "' " & _
           ",LandLineNo = '" & Replace(pLandLineNo, "'", "''") & "' " & _
           ",LocalNo = '" & Replace(pLocalNo, "'", "''") & "' " & _
           ",ShowAsDefault = " & pShowAsDefault & " " & _
           "WHERE OfficeCode = '" & Replace(pOfcCode, "'", "''") & "'"

            'objCommand = New clsSqlConn()

            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql
            objCommand.ExecTranNonQuery()

        Catch ex As Exception
            'ltr.pTran.Rollback()
            Throw New Exception(ex.Message)
        Finally

            


        End Try

    End Sub
    Public Sub DeleteOffice()

        Dim s_sql As String
        Dim objCommand As clsSqlConn
        'Dim ltr As DbTran
        Try
            s_sql = "Update Office Set deleted = 1 " & _
           "WHERE OfficeCode = '" & Replace(pOfcCode, "'", "''") & "'"


           
            objCommand = New clsSqlConn()
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql
            objCommand.ExecNonQuery()

            
        Catch ex As Exception
            'ltr.pTran.Rollback()
            Throw New Exception(ex.Message)
        Finally

            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If


        End Try

    End Sub
    Private Function OrderBy() As String
        If pSortCol = "Office Code" Then
            Return " o.OfficeCode"
        ElseIf pSortCol = "Office Name" Then
            Return " o.Description"
        ElseIf pSortCol = "Point Person" Then
            Return " o.PointPerson"
        
        Else
            Return " o.OfficeCode "
        End If

    End Function
    Public Function RetrieveOffice() As DataTable

        Dim objCommand As clsSqlConn

        Dim ldata As DataTable
        Dim s_order As String = OrderBy()
        Dim lTop As Integer
        lTop = CInt(pIdx) * CInt(pRowsPerPage)
        Try
            objCommand = New clsSqlConn
            Dim sSQL As String
            If DocSession.OraClient Then
                sSQL = "select dtbl.OfficeCode,dtbl.Description,dtbl.pointperson,case when dtbl.ShowAsDefault = 1 then 'Yes' else 'No' end as ShowAsDefault,oa.AddressDesc,u.FirstName+' '+u.LastName as pointpersonname from (SELECT " & _
                          "row_number() over (ORDER BY " & s_order & " " & pSortOrder & ") as rn, " & _
                "o.OfficeCode, " & _
                "o.PointPerson, " & _
                "o.Description, " & _
                "o.AddressCode, " & _
                "NVL(o.ShowAsDefault,0) as ShowAsDefault" & _
                " FROM  Office o " & _
                "     WHERE (deleted is null or deleted = 0) and rownum <= " & lTop.ToString & " " & WhereClause() & " " & _
                ") dtbl " & _
                " LEFT JOIN Users u ON dtbl.pointperson = u.userid " & _
                " LEFT JOIN OfficeAddress oa ON oa.AddressCode = dtbl.AddressCode " & _
                " WHERE dtbl.rn between " & pIdx & " and " & pRowsPerPage & "+" & pIdx
            Else
                sSQL = "select dtbl.OfficeCode,dtbl.LandLineNo,dtbl.LocalNo,case when dtbl.ShowAsDefault = 1 then 'Yes' else 'No' end as ShowAsDefault,dtbl.Description,isnull(dtbl.pointperson,'') as pointperson,isnull(oa.AddressCode,'') as AddressCode,isnull(oa.AddressDesc,'') as AddressDesc,isnull(u.FirstName+' '+u.LastName,'') as pointpersonname from (SELECT TOP " & lTop.ToString & " " & _
                         "rn= row_number() over (ORDER BY " & s_order & " " & pSortOrder & "), " & _
               "o.OfficeCode, " & _
                "o.PointPerson, " & _
                "o.Description, " & _
                "o.AddressCode, " & _
                "isnull(o.LandLineNo,'') as LandLineNo, " & _
                "isnull(o.LocalNo,'') as LocalNo, " & _
                "isnull(o.ShowAsDefault,0) as ShowAsDefault " & _
                " FROM  Office o " & _
               "WHERE (deleted is null or deleted = 0) " & WhereClause() & " " & _
                          ") dtbl " & _
                " LEFT JOIN Users u ON dtbl.pointperson = u.userid " & _
                " LEFT JOIN OfficeAddress oa ON oa.AddressCode = dtbl.AddressCode " & _
                " WHERE dtbl.rn between " & pIdx & " and " & pRowsPerPage & "+" & pIdx
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

    Public Function CountOffice() As Integer

        Dim objCommand As clsSqlConn

        'Dim ldata As DataTable
        Dim s_sql As String

        Try
            s_sql = "SELECT count(*) " & _
                     "FROM  " & _
            "Office o " & _
           "WHERE (deleted is null or deleted = 0) " & WhereClause()

            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql '"XMSP_USERGET"





            Return objCommand.ExecScalar


        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            'If Not ldata Is Nothing Then
            '    ldata.Dispose()
            '    ldata = Nothing
            'End If


            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If

        End Try

    End Function

    Private Function WhereClause() As String
        Dim lswhere As String = ""
        If pOfcCode.Trim <> "" Then
            lswhere = lswhere & " AND o.OfficeCode = '" & Replace(pOfcCode, "'", "''") & "' "
        End If

        If pOfcDesc <> "" Then
            lswhere = lswhere & " AND o.Description like '%" & Replace(pOfcDesc, "'", "''") & "%' "
        End If
        Return lswhere
    End Function

    Public Function RetrieveOfficeDesc() As String
        Dim ldata As DataTable
        Dim objCommand As clsSqlConn

        Try
            Dim s_sql As String = "SELECT Description " & _
                "FROM Office "

            If pOfcCode.Trim <> "" Then
                s_sql = s_sql & " WHERE OfficeCode = '" & Replace(pOfcCode, "'", "''") & "'"
            End If


            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql

            Return objCommand.ExecScalar3


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
    Public Function RetrieveEquivalentOfficeCode(ByVal asCode As String) As DataTable
        'Dim ldata As DataTable
        Dim objCommand As clsSqlConn

        Try
            Dim s_sql As String = "SELECT OfficeCode,Description " & _
                "FROM Office "

            If asCode.Trim <> "" Then
                s_sql = s_sql & " WHERE OfficeCode = '" & Replace(asCode.Trim, "'", "''") & "' OR " & _
                                    "Description = '" & Replace(asCode.Trim, "'", "''") & "'"
            End If


            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql

            Return objCommand.ExecData


        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            'If Not ldata Is Nothing Then
            '    ldata.Dispose()
            '    ldata = Nothing
            'End If


            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If

        End Try

    End Function
    Public Function RetrieveOfficeInfo() As DataTable

        Dim objCommand As clsSqlConn

        Try
            Dim s_sql As String = "SELECT AddressCode,Description " & _
                "FROM Office "

            If pOfcCode.Trim <> "" Then
                s_sql = s_sql & " WHERE OfficeCode = '" & Replace(pOfcCode, "'", "''") & "'"
            End If


            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql

            Return objCommand.ExecData


        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            

            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If

        End Try

    End Function

    Public Sub DeleteRecord(ByVal objcommand As clsSqlConn)

        Try

            Dim s_sql As String = "Update Office Set Deleted = 1 WHERE OfficeCode = '" & pOfcCode & "' "

            objcommand.CommandType = CommandType.Text
            objcommand.CommandText = s_sql
            objcommand.ExecTranNonQuery()

            Dim oHist As New DocHistory
            oHist.pTableName = "Office"
            oHist.pRecordId = Replace(pOfcCode, "'", "''")
            oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
            oHist.pColumnName = ""
            oHist.pModifiedDate = ModifiedDate
            oHist.pOldValue = ""
            oHist.pNewValue = "Deleted Office (" & pOfcDesc & ")"
            oHist.pIpAddress = pIPAddress
            oHist.LogChanges(objcommand)

        Catch ex As Exception
            'Throw New Exception(ex.Message)

        Finally



        End Try

    End Sub

    Public Function CheckIfOfficeInUse(ByVal asId As String) As Boolean


        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            'If DocSession.OraClient Then
            Dim osp As New cls_storedproc

            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = "SELECT TOP 1 OfficeCode FROM DocList WHERE OfficeCode = '" & pOfcCode & "'"

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
    Public Sub DeleteOfficeGroupDocAccess(ByVal objCommand As clsSqlConn)

        Try

            Dim lsSql As String = "DELETE FROM GroupOfficeAccess WHERE OfficeCode = '" & pOfcCode & "'"
            'Dim lsSql As String = "DELETE FROM GroupOfficeAccess WHERE GroupID = '" & pGroupID & "'"

            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = lsSql
            objCommand.ExecTranNonQuery()


        Catch ex As Exception
            Throw New Exception(ex.Message)

        Finally



        End Try
    End Sub
End Class
