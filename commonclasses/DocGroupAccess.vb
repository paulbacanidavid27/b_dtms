Public Class DocGroupAccess
    Dim GroupAccessId As Integer
    Dim GroupAccess As String
    Dim GroupId As String
    Dim Holiday As String

    Public Sub New()

    End Sub

    Public Property pGroupAccessId() As Integer
        Get
            Return GroupAccessId
        End Get
        Set(ByVal value As Integer)
            GroupAccessId = value
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

    Public Property pHoliday() As String
        Get
            Return Holiday
        End Get
        Set(ByVal value As String)
            Holiday = value
        End Set

    End Property

    Public Function RetrieveGroupAccess() As DataTable
        Dim objCommand As clsSqlConn
        Dim ldata As DataTable
        Try
            'If DocSession.OraClient Then
            Dim osp As New cls_storedproc
            objCommand = New clsSqlConn
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = osp.DMSF_GROUPACCESSGET
            'Else
            'objCommand = New clsSqlConn
            'objCommand.CommandType = CommandType.StoredProcedure

            'objCommand.CommandText = "xMSP_GROUPACCESSGET"
            'End If

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

    Public Function RetrieveGroupAccess2() As DataTable
        Dim objCommand As clsSqlConn
        Dim ldata As DataTable
        Try
            'If DocSession.OraClient Then
            '    Dim osp As New cls_storedproc
            '    osp.pGroupId = pGroupId
            '    objCommand.pCommandType = CommandType.Text
            '    objCommand.pCommandText = osp.DMSF_GROUPACCESSGET2
            'Else
            Dim osp As New cls_storedproc

            osp.pGroupId = pGroupId
            objCommand = New clsSqlConn
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = osp.DMSF_GROUPACCESSGET2_sql

            objCommand.CommandType = CommandType.Text

            'objCommand.CommandText = "xMSP_GROUPGETACCESS2"
            'objCommand.ParametersAddWithValue("@GroupId", pGroupId)
            'End If

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
    Public Function RetrieveGroupDocTypeAccess() As DataTable
        Dim objCommand As clsSqlConn
        Dim ldata As DataTable
        Try
            Dim s_sql As String = ""
            s_sql = "Select gda.GroupId,gda.DocType,dt.docname " & _
            "FROM dbo.GroupDocTypeAccess gda " & _
            "INNER JOIN dbo.Doctype dt " & _
                "ON dt.doctype = gda.docType " & _
            "WHERE GroupId = '" & pGroupId & "' "


            objCommand = New clsSqlConn
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql

            objCommand.CommandType = CommandType.Text

            'objCommand.CommandText = "xMSP_GROUPGETACCESS2"
            'objCommand.ParametersAddWithValue("@GroupId", pGroupId)
            'End If

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
    Public Function RetrieveWorkSchedule() As DataTable
        Dim objCommand As clsSqlConn
        Dim ldata As DataTable
        Try
            'If DocSession.OraClient Then
            '    Dim osp As New cls_storedproc
            '    osp.pGroupId = pGroupId
            '    objCommand.pCommandType = CommandType.Text
            '    objCommand.pCommandText = osp.xDMSF_userlock
            'Else
            Dim s_sql As String = "SELECT GroupId " & _
                  ",Weekday " & _
                  ",StartTime " & _
                  ",EndTime " & _
                  ",CreatedBy " & _
                  ",CreatedDate " & _
              "FROM WorkSchedule " & _
            "WHERE GroupId = '" & pGroupId & "' "

            If DocSession.OraClient Then
                s_sql = s_sql & "and Weekday = TO_CHAR(SYSDATE,'w')  " & _
                        "and (CURRENT_DATE between to_date(TO_CHAR(SYSDATE,'mm/dd/yyyy')||' '||StartTime, 'mm/dd/yyyy hh24:mi:ss') " & _
                        "and to_date(TO_CHAR(SYSDATE,'mm/dd/yyyy')||' '||EndTime, 'mm/dd/yyyy hh24:mi:ss')) "

            Else
                s_sql = s_sql & "and Weekday = datepart(w,getdate()) " & _
                        "and Getdate() between convert(char(10),getdate(),101)+' '+StartTime  " & _
                        "and convert(char(10),getdate(),101)+' '+EndTime  "
            End If

            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql '"xMSP_TIMEVALIDATE"
            'objCommand.ParametersAddWithValue("@GroupId", pGroupId)
            'End If

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

    Public Function IsHoliday() As Boolean
        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = "SELECT groupid FROM GROUPWORKHOLIDAY WHERE groupid = '" & pGroupId & "' AND holiday = '" & pHoliday & "'"
            'objCommand.ParametersAddWithValue("@GroupId", pGroupId)

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

    
End Class
