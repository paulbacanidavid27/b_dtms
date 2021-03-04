Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
<System.Web.Script.Services.ScriptService()> _
Public Class getUser
    Inherits System.Web.Services.WebService
    <WebMethod()>
    Public Function getUsers(ByVal prefixText As String, ByVal count As Integer) As String()
        Dim s_sql As String
        Dim objCommand As clsSqlConn
        Dim ldata As DataTable
        Try
            If count = 0 Then
                count = 10
            End If
            s_sql = " SELECT top " & CStr(count) &
                    " username = isnull(u.FirstName,'') + ' ' +isnull(u.LastName,'') " &
                    "FROM users  u " &
                    "WHERE " &
                    "(u.firstname+' '+u.lastname like '" & prefixText & "%') "
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql

            ldata = objCommand.Fill()

            Dim listdata As New List(Of String)
            Dim ictr As Integer
            ictr = 0
            For Each lrow As DataRow In ldata.Rows

                listdata.Add(ldata(ictr)(0))
                ictr = ictr + 1
            Next
            Return listdata.ToArray

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

    Public Function getUploadedBy(ByVal prefixText As String, ByVal count As Integer) As String()
        Dim s_sql As String
        Dim objCommand As clsSqlConn
        Dim ldata As DataTable
        Try
            If count = 0 Then
                count = 10
            End If
            s_sql = " SELECT top " & CStr(count) &
                    " username = isnull(u.FirstName,'') + ' ' +isnull(u.LastName,'') " &
                    "FROM users  u INNER JOIN docForms d ON u.UserId = d.UploadedBy " &
                    "WHERE " &
                    "(u.firstname+' '+u.lastname like '" & prefixText & "%') "
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql

            ldata = objCommand.Fill()

            Dim listdata As New List(Of String)
            Dim ictr As Integer
            ictr = 0
            For Each lrow As DataRow In ldata.Rows

                listdata.Add(ldata(ictr)(0))
                ictr = ictr + 1
            Next
            Return listdata.ToArray

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
    <WebMethod()> _
    Public Function getTitle(ByVal prefixText As String, ByVal count As Integer) As String()
        Dim s_sql As String
        Dim objCommand As clsSqlConn
        Dim ldata As DataTable
        Try
            If count = 0 Then
                count = 10
            End If
            s_sql = " SELECT top " & CStr(count) & _
                    " title " & _
                    "FROM doclist  u " & _
                    "WHERE " & _
                    "(title like '" & prefixText & "%') "
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql

            ldata = objCommand.Fill()

            Dim listdata As New List(Of String)
            Dim ictr As Integer
            ictr = 0
            For Each lrow As DataRow In ldata.Rows

                listdata.Add(ldata(ictr)(0))
                ictr = ictr + 1
            Next
            Return listdata.ToArray

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

    <WebMethod()> _
    Public Function getEmail(ByVal prefixText As String, ByVal count As Integer) As String()
        Dim s_sql As String
        Dim objCommand As clsSqlConn
        Dim ldata As DataTable
        Try
            If count = 0 Then
                count = 10
            End If
            s_sql = " SELECT top " & CStr(count) & _
                    " email " & _
                    "FROM users  u " & _
                    "WHERE " & _
                    "(email like '" & prefixText & "%') "
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql

            ldata = objCommand.Fill()

            Dim listdata As New List(Of String)
            Dim ictr As Integer
            ictr = 0
            For Each lrow As DataRow In ldata.Rows

                listdata.Add(ldata(ictr)(0))
                ictr = ictr + 1
            Next
            Return listdata.ToArray

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
    <WebMethod()> _
    Public Function getLocation(ByVal prefixText As String, ByVal count As Integer) As String()
        Dim s_sql As String
        Dim objCommand As clsSqlConn
        Dim ldata As DataTable
        Try
            If count = 0 Then
                count = 10
            End If
            s_sql = " SELECT top " & CStr(count) & _
                    " Location " & _
                    "FROM crdMonitoringDocLocation " & _
                    "WHERE " & _
                    "(Location like '" & prefixText & "%') "
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql

            ldata = objCommand.Fill()

            Dim listdata As New List(Of String)
            Dim ictr As Integer
            ictr = 0
            For Each lrow As DataRow In ldata.Rows

                listdata.Add(ldata(ictr)(0))
                ictr = ictr + 1
            Next
            Return listdata.ToArray

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
End Class