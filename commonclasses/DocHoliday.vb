Imports System.Data.SqlClient

Public Class DocHoliday

    Dim UserId As String
    Dim GroupId As String
    Dim OfficeId As String
    Dim CopyGroupId As String
    Dim CopyOfficeId As String
    Dim vYear As Integer
    Dim Holiday As String
    Dim Descr As String

    Public Sub New()

    End Sub

    Public Property pHoliday() As String
        Get
            Return Holiday
        End Get
        Set(ByVal value As String)
            Holiday = value
        End Set

    End Property

    Public Property pDescription() As String
        Get
            Return Descr
        End Get
        Set(ByVal value As String)
            Descr = value
        End Set

    End Property

    Public Property pYear() As Integer
        Get
            Return vYear
        End Get
        Set(ByVal value As Integer)
            vYear = value
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

    Public Property pGroupId() As String
        Get
            Return GroupId
        End Get
        Set(ByVal value As String)
            GroupId = value
        End Set

    End Property

    Public Property pOfficeId() As String
        Get
            Return OfficeId
        End Get
        Set(ByVal value As String)
            OfficeId = value
        End Set

    End Property


    Public Property pCopyGroupId() As String
        Get
            Return CopyGroupId
        End Get
        Set(ByVal value As String)
            CopyGroupId = value
        End Set

    End Property
    Public Property pCopyOfficeId() As String
        Get
            Return CopyOfficeId
        End Get
        Set(ByVal value As String)
            CopyOfficeId = value
        End Set

    End Property

    Public Sub SaveGroupHoliday()


        Dim objCommand As clsSqlConn

        Try
            Dim s_sql As String = "INSERT INTO GroupWorkHoliday " & _
           "(GroupId " & _
           ",Year " & _
           ",Holiday " & _
           ",CreatedBy " & _
           ",Description " & _
           ",CreatedDate) " & _
     "VALUES (" & _
           "'" & Replace(pGroupId, "'", "''") & "' " & _
           "," & pYear & " " & _
           ",'" & pHoliday & "' " & _
           ",'" & Replace(pUserId, "'", "''") & "' " & _
           ",'" & Replace(pDescription, "'", "''") & "' " & _
           "," & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & ") "

            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql '"xMSP_HOLIDAYADD"
            'objCommand.ParametersAddWithValue("@Year", pYear)
            'objCommand.ParametersAddWithValue("@Descr", pDescription)
            'objCommand.ParametersAddWithValue("@Holiday", pHoliday)
            'objCommand.ParametersAddWithValue("@GroupId", pGroupId)
            'objCommand.ParametersAddWithValue("@CreatedBy", pUserId)

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
    Public Sub SaveOfficeHoliday()


        Dim objCommand As clsSqlConn

        Try
            Dim s_sql As String = "INSERT INTO OfficeWorkHoliday " & _
           "(OfficeCode " & _
           ",Year " & _
           ",Holiday " & _
           ",CreatedBy " & _
           ",Description " & _
           ",CreatedDate) " & _
     "VALUES (" & _
           "'" & Replace(pOfficeId, "'", "''") & "' " & _
           "," & pYear & " " & _
           ",'" & pHoliday & "' " & _
           ",'" & Replace(pUserId, "'", "''") & "' " & _
           ",'" & Replace(pDescription, "'", "''") & "' " & _
           "," & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & ") "

            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql '"xMSP_HOLIDAYADD"
            'objCommand.ParametersAddWithValue("@Year", pYear)
            'objCommand.ParametersAddWithValue("@Descr", pDescription)
            'objCommand.ParametersAddWithValue("@Holiday", pHoliday)
            'objCommand.ParametersAddWithValue("@GroupId", pGroupId)
            'objCommand.ParametersAddWithValue("@CreatedBy", pUserId)

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
    Public Sub DeleteGrpHoliday(ByVal objCommand As clsSqlConn)
        Dim s_sql As String = "DELETE FROM GroupWorkHoliday WHERE GroupId = '" & pCopyGroupId & "' and Year = " & pYear
        objCommand.CommandType = CommandType.Text
        objCommand.CommandText = s_sql
        objCommand.ExecTranNonQuery()
    End Sub
    Public Sub DeleteOfcHoliday(ByVal objCommand As clsSqlConn)
        Dim s_sql As String = "DELETE FROM OfficeWorkHoliday WHERE OfficeCode = '" & pCopyOfficeId & "' and Year = " & pYear
        objCommand.CommandType = CommandType.Text
        objCommand.CommandText = s_sql
        objCommand.ExecTranNonQuery()
    End Sub
    Public Sub CopyHoliday()


        Dim objCommand As clsSqlConn
        Dim oTran As DbTran
        Try
            oTran = New DbTran
            objCommand = New clsSqlConn(oTran.pTran)
            DeleteGrpHoliday(objCommand)

            objCommand.CommandType = CommandType.Text


            objCommand.CommandText = "INSERT INTO dbo.GroupWorkHoliday " & _
           "(GroupId " & _
           ",Year " & _
           ",Holiday " & _
           ",CreatedBy " & _
           ",Description " & _
           ",CreatedDate) " & _
     "SELECT '" & pCopyGroupId & "' " & _
           ",Year " & _
           ",Holiday " & _
           ",'" & pUserId & "' " & _
           ",Description " & _
           "," & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & " " & _
           "FROM GroupWorkHoliday " & _
   "WHERE GroupId = '" & pGroupId & "' " & _
   "AND Year = " & CStr(pYear)
            'objCommand.ParametersAddWithValue("@Year", pYear)
            'objCommand.ParametersAddWithValue("@GroupId", pGroupId)
            'objCommand.ParametersAddWithValue("@CopyToGroupId", pCopyGroupId)
            'objCommand.ParametersAddWithValue("@CreatedBy", pUserId)

            objCommand.ExecTranNonQuery()
            oTran.pTran.Commit()
        Catch ex As Exception
            oTran.pTran.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If
            If Not oTran Is Nothing Then
                oTran.Dispose()
                oTran = Nothing
            End If

        End Try

    End Sub

    Public Sub CopyOfficeHoliday()


        Dim objCommand As clsSqlConn
        Dim oTran As DbTran
        Try
            oTran = New DbTran
            objCommand = New clsSqlConn(oTran.pTran)
            DeleteOfcHoliday(objCommand)

            objCommand.CommandType = CommandType.Text


            objCommand.CommandText = "INSERT INTO OfficeWorkHoliday " &
           "(OfficeCode " &
           ",Year " &
           ",Holiday " &
           ",CreatedBy " &
           ",Description " &
           ",CreatedDate) " &
     "SELECT '" & Replace(pCopyOfficeId, "'", "''") & "' " &
           ",Year " &
           ",Holiday " &
           ",'" & pUserId & "' " &
           ",Description " &
           "," & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & " " &
           "FROM OfficeWorkHoliday " &
   "WHERE OfficeCode = '" & pOfficeId & "' " &
   "AND Year = " & CStr(pYear)
            'objCommand.ParametersAddWithValue("@Year", pYear)
            'objCommand.ParametersAddWithValue("@GroupId", pGroupId)
            'objCommand.ParametersAddWithValue("@CopyToGroupId", pCopyGroupId)
            'objCommand.ParametersAddWithValue("@CreatedBy", pUserId)

            objCommand.ExecTranNonQuery()
            oTran.pTran.Commit()
        Catch ex As Exception
            oTran.pTran.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If
            If Not oTran Is Nothing Then
                oTran.Dispose()
                oTran = Nothing
            End If

        End Try

    End Sub

    Public Sub CopyFromHoliday()


        Dim objCommand As clsSqlConn
        Dim oTran As DbTran
        Try
            oTran = New DbTran
            objCommand = New clsSqlConn(oTran.pTran)
            DeleteOfcHoliday(objCommand)

            objCommand.CommandType = CommandType.Text


            objCommand.CommandText = "INSERT INTO OfficeWorkHoliday " &
           "(OfficeCode " &
           ",Year " &
           ",Holiday " &
           ",CreatedBy " &
           ",Description " &
           ",CreatedDate) " &
     "SELECT '" & Replace(pCopyOfficeId, "'", "''") & "' " &
           ",Year+1 " &
           ",DateAdd(year,1,Holiday) " &
           ",'" & pUserId & "' " &
           ",Description " &
           "," & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & " " &
           "FROM OfficeWorkHoliday " &
   "WHERE OfficeCode = '" & pOfficeId & "' " &
   "AND Year = " & CStr(pYear - 1)
            'objCommand.ParametersAddWithValue("@Year", pYear)
            'objCommand.ParametersAddWithValue("@GroupId", pGroupId)
            'objCommand.ParametersAddWithValue("@CopyToGroupId", pCopyGroupId)
            'objCommand.ParametersAddWithValue("@CreatedBy", pUserId)

            objCommand.ExecTranNonQuery()
            oTran.pTran.Commit()
        Catch ex As Exception
            oTran.pTran.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If
            If Not oTran Is Nothing Then
                oTran.Dispose()
                oTran = Nothing
            End If

        End Try

    End Sub
    Public Function RetrieveHoliday() As DataTable


        Dim objCommand As clsSqlConn

        Dim ldata As DataTable

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            If DocSession.OraClient Then
                objCommand.CommandText = "SELECT Year_ as ""Year"" " & _
      ",TO_chAR(Holiday,'mm/dd/yyyy') AS Holiday " & _
      ",wh.CreatedBy " & _
      ",u.FirstName ||' '||u.LastName AS username " & _
      ",TO_chAR(wh.CreatedDate,'mm/dd/yyyy') AS CreatedDate " & _
      ",Description " & _
  "FROM GroupWorkHoliday wh " & _
 "INNER JOIN users u ON u.UserId = wh.CreatedBy " & _
 "WHERE GroupId = '" & pGroupId & "'  and Year_ = " & CStr(pYear) & " " & _
 "ORDER BY Holiday "
            Else
                objCommand.CommandText = "SELECT Year " & _
      ",convert(char(10),Holiday,101) AS Holiday " & _
      ",wh.CreatedBy " & _
      ",u.FirstName +' '+u.LastName AS username " & _
      ",convert(char(10),wh.CreatedDate,101) AS CreatedDate " & _
      ",Description " & _
  "FROM GroupWorkHoliday wh " & _
 "INNER JOIN users u ON u.UserId = wh.CreatedBy " & _
 "WHERE GroupId = '" & pGroupId & "'  and Year = " & CStr(pYear) & " " & _
 "ORDER BY Holiday "
            End If

            '"xMSP_HOLIDAYGET"
            'objCommand.ParametersAddWithValue("@Year", pYear)
            'objCommand.ParametersAddWithValue("@GroupId", pGroupId)
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

    Public Function RetrieveOfficeHoliday() As DataTable


        Dim objCommand As clsSqlConn

        Dim ldata As DataTable

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            If DocSession.OraClient Then

            Else
                objCommand.CommandText = "SELECT Year " & _
      ",convert(char(10),Holiday,101) AS Holiday " & _
      ",wh.CreatedBy " & _
      ",u.FirstName +' '+u.LastName AS username " & _
      ",convert(char(10),wh.CreatedDate,101) AS CreatedDate " & _
      ",Description " & _
  "FROM OfficeWorkHoliday wh " & _
 "INNER JOIN users u ON u.UserId = wh.CreatedBy " & _
 "WHERE OfficeCode = '" & pOfficeId & "'  and Year = " & CStr(pYear) & " " & _
 "ORDER BY Holiday "
            End If

            '"xMSP_HOLIDAYGET"
            'objCommand.ParametersAddWithValue("@Year", pYear)
            'objCommand.ParametersAddWithValue("@GroupId", pGroupId)
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

    Public Sub DeleteOfficeHoliday(ByVal objCommand As clsSqlConn)

        Try

            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = "DELETE FROM OfficeWorkHoliday WHERE OfficeCode = '" & pOfficeId & "' and Holiday = '" & pHoliday & "' "
            '"xMSP_HOLIDAYDELETE"


            'objCommand.ParametersClear()

            'objCommand.ParametersAddWithValue("@Holiday", pHoliday)
            'objCommand.ParametersAddWithValue("@GroupId", pGroupId)
            objCommand.ExecTranNonQuery()

        Catch ex As Exception
            'Throw New Exception(ex.Message)

        Finally


        End Try
    End Sub

    Public Sub DeleteHoliday(ByVal objCommand As clsSqlConn)

        Try

            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = "DELETE FROM GroupWorkHoliday WHERE GroupId = '" & pGroupId & "' and Holiday = '" & pHoliday & "' "
            '"xMSP_HOLIDAYDELETE"


            'objCommand.ParametersClear()

            'objCommand.ParametersAddWithValue("@Holiday", pHoliday)
            'objCommand.ParametersAddWithValue("@GroupId", pGroupId)
            objCommand.ExecTranNonQuery()

        Catch ex As Exception
            'Throw New Exception(ex.Message)

        Finally


        End Try
    End Sub

    Public Function CheckIfGroupHolidayExists() As Boolean


        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = "SELECT * FROM GroupWorkHoliday gwh WHERE gwh.GroupId = '" & pGroupId & "' and Holiday = '" & pHoliday & "'" '"xMSP_HOLIDAYEXISTS"
            'objCommand.ParametersAddWithValue("@Holiday", pHoliday)
            'objCommand.ParametersAddWithValue("@GroupId", pGroupId)
            Return objCommand.ExecHasRow


        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If

        End Try

    End Function
    Public Function CheckIfOfficeHolidayExists() As Boolean


        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = "SELECT * FROM OfficeWorkHoliday gwh WHERE gwh.OfficeCode = '" & pOfficeId & "' and Holiday = '" & pHoliday & "'" '"xMSP_HOLIDAYEXISTS"
            'objCommand.ParametersAddWithValue("@Holiday", pHoliday)
            'objCommand.ParametersAddWithValue("@GroupId", pGroupId)
            Return objCommand.ExecHasRow


        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If

        End Try

    End Function
End Class
