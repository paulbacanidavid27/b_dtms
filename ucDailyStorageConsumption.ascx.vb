Imports System.Web.UI.DataVisualization.Charting
Imports System.IO
Public Class ucDailyStorageConsumption
    Inherits System.Web.UI.UserControl

    Dim ToDate As String = ""
    Dim FromDate As String = ""
    Dim DocType As String = ""

    Public Property pDocType As String
        Get
            Return DocType
        End Get
        Set(ByVal value As String)
            DocType = value
        End Set
    End Property
    Public Property pFromDate As String
        Get
            Return FromDate
        End Get
        Set(ByVal value As String)
            FromDate = value
        End Set
    End Property

    Public Property pToDate As String
        Get
            Return ToDate
        End Get
        Set(ByVal value As String)
            ToDate = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' RetrieveDocTypes()
            LoadPreviousCriteria()
            RetrieveChartData()

            'RetrieveUploadedDocs()
        End If
    End Sub
    'Private Sub RetrieveDocTypes()
    '    Dim ldata As DataTable
    '    Try

    '        Dim oDoc As New DocTypes
    '        oDoc.pGroupId = DocSession.sUserGroup
    '        ldata = oDoc.GetDocType()

    '        If ldata.Rows.Count > 0 Then
    '            dlDocType.DataTextField = "DocName"
    '            dlDocType.DataValueField = "DocType"
    '            Dim lrow As DataRow = ldata.NewRow
    '            lrow("DocType") = ""
    '            lrow("DocName") = "-All-"
    '            ldata.Rows.InsertAt(lrow, 0)
    '            dlDocType.DataSource = ldata
    '            dlDocType.DataBind()

    '        End If
    '    Catch ex As Exception
    '    Finally
    '        If Not ldata Is Nothing Then
    '            ldata.Dispose()
    '            ldata = Nothing
    '        End If
    '    End Try
    'End Sub
    'Private Sub RetrieveUploadedDocs()
    '    ucDB.pDocType = dlDocType.SelectedValue
    '    ucDB.pFromDate = tbDCFrom.Text
    '    ucDB.pToDate = tbDCTo.Text
    '    ucDB.RetrieveAction()
    'End Sub

    Public Sub RetrieveChartData()
        PopulateChart()
    End Sub
    Private Sub ResetChart()
        For Each z As Series In Chart1.Series
            z.Dispose()
        Next
        Chart1.Series.Clear()
    End Sub
    Private Sub PopulateChart()
        Dim ldata As DataTable
        Try


            Dim oList As New DocList
            Dim currentseriesName As String = ""
            'ResetChart()
            

            ''oList.pDocType = pDocType
            If dlType.SelectedValue = "D" Then
                If tbDCFrom.Text.Trim <> "" Then
                    oList.pCreatedDateFrom = tbDCFrom.Text.Trim
                Else
                    oList.pCreatedDateFrom = ""
                End If
                If tbDCTo.Text.Trim <> "" Then
                    oList.pCreatedDateTo = tbDCTo.Text.Trim
                Else
                    oList.pCreatedDateTo = ""
                End If
                ldata = oList.RetrieveFileSize
            Else

                If dlMonthFrom.SelectedValue <> "" Then
                    oList.pCreatedDateFrom = dlMonthFrom.SelectedValue & "/1/" & tbYearFrom.Text
                Else
                    oList.pCreatedDateFrom = ""
                End If
                If dlMonthTo.SelectedValue <> "" Then
                    oList.pCreatedDateTo = DateAdd(DateInterval.Day, -1, CDate((CInt(dlMonthTo.SelectedValue) + 1).ToString() & "/1/" & tbYearTo.Text))
                Else
                    oList.pCreatedDateTo = ""
                End If
                ldata = oList.RetrieveFileSizeMonthly
            End If


            Dim ltotal As ULong = 0
            Dim ltotalfiles As ULong = 0
            If Not ldata Is Nothing Then
                Dim liCtr As Integer = 0
                For Each lrow As DataRow In ldata.Rows
                    'Chart1.Series("DocCount").Points.AddXY(lrow("description"), CInt(lrow("doccount")))
                    If IsDBNull(lrow("TotalFileSize")) Then
                        ldata.Rows(liCtr)("BytesVal") = ""
                    Else
                        If lrow("TotalFileSize").ToString.Trim <> "" Then
                            ltotal += CInt(lrow("TotalFileSize"))
                            ltotalfiles += CInt(lrow("TotalFileS"))
                            ldata.Rows(liCtr)("BytesVal") = FormatBytes(lrow("TotalFileSize"))

                        End If

                    End If

                    liCtr = liCtr + 1
                Next
                'Chart1.Series("DocCount").LegendText = "Total Documents: " & ltotal.ToString
                'Chart1.Titles(0).Text = "Total Documents Received As of " & pToDate & " - " & ltotal.ToString
            End If
            lTotalBytes.Text = FormatBytes(ltotal)
            lFiles.Text = ltotalfiles.ToString
            Dim lsLoc As String = System.Configuration.ConfigurationManager.AppSettings("fileloc")

            Dim ldriveInfo As DriveInfo = New DriveInfo(lsLoc)
            lFreeSpace.Text = FormatBytes(ldriveInfo.AvailableFreeSpace)
            Dim luAB, luAS As Long
            Dim liTR As Integer = ldata.Rows.Count
            Dim liDL As Long
            If ltotal > 0 AndAlso liTR > 0 Then
                luAB = Math.Round(ltotal / liTR, 2)
            Else
                luAB = 0
            End If
            If ltotalfiles > 0 AndAlso liTR > 0 Then
                luAS = Math.Round(ltotalfiles / liTR, 2)
            Else
                luAS = 0
            End If
            If ldriveInfo.AvailableFreeSpace > 0 AndAlso luAB > 0 Then
                liDL = Math.Round(ldriveInfo.AvailableFreeSpace / luAB, 2)
            Else
                liDL = 0
            End If
            lAveBytes.Text = FormatBytes(luAB)
            lAveFiles.Text = luAS.ToString
            lDaysLeft.Text = CalendarDays(liDL)
            Chart1.DataSource = ldata
            Chart1.Series(0).XValueMember = "UploadDate"
            Chart1.Series(0).YValueMembers = "TotalFileSize"
            Chart1.Series(1).XValueMember = "UploadDate"
            Chart1.Series(1).YValueMembers = "TotalFiles"
            'Chart1.Series(0).Y = "TotalFileSize"
            Chart1.Titles(0).Text = "Total Files and Bytes Uploaded " & dlType.SelectedItem.Text
            Chart1.DataBind()

            '    'Chart1.DataBind()
            '    Chart1.EnableViewState = True
            pTotalDocuments.Update()
        Catch ex As Exception
            Throw New Exception(ex.Message)

        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()

            End If
        End Try
    End Sub


    Private Sub btSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSearch.Click
        RetrieveChartData()
        SetCookies()
    End Sub

    Private Sub SetCookies()
        Dim mycookie As HttpCookie = New HttpCookie("dailyStorageFrom")
        mycookie.Value = tbDCFrom.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("dailyStorageTo")
        mycookie.Value = tbDCTo.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)
        
        mycookie = New HttpCookie("dailyStorageMonthFrom")
        mycookie.Value = dlMonthFrom.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)
        mycookie = New HttpCookie("dailyStorageMonthTo")
        mycookie.Value = dlMonthTo.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)
        mycookie = New HttpCookie("dailyStorageYearFrom")
        mycookie.Value = tbYearFrom.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)
        mycookie = New HttpCookie("dailyStorageYearTo")
        mycookie.Value = tbYearTo.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

    End Sub

    Private Sub LoadPreviousCriteria()
        
        If Not Request.Cookies("dailyStorageFrom") Is Nothing Then
            If Request.Cookies("dailyStorageFrom").Value <> "" Then
                tbDCFrom.Text = Server.HtmlEncode(Request.Cookies("dailyStorageFrom").Value)
            Else
                tbDCFrom.Text = DateAdd(DateInterval.Month, -2, Date.Now).ToShortDateString
            End If
        Else
            tbDCFrom.Text = DateAdd(DateInterval.Month, -2, Date.Now).ToShortDateString
        End If
        If Not Request.Cookies("dailyStorageTo") Is Nothing Then
            If Request.Cookies("dailyStorageTo").Value <> "" Then
                tbDCTo.Text = Server.HtmlEncode(Request.Cookies("dailyStorageTo").Value)
            Else
                tbDCTo.Text = Date.Now.ToShortDateString
            End If
        Else
            tbDCTo.Text = Date.Now.ToShortDateString
        End If

        If Not Request.Cookies("dailyStorageMonthFrom") Is Nothing Then
            If Request.Cookies("dailyStorageMonthFrom").Value <> "" Then
                dlMonthFrom.SelectedValue = Server.HtmlEncode(Request.Cookies("dailyStorageMonthFrom").Value)
            Else
                dlMonthFrom.SelectedValue = Month(Date.Now).ToString
            End If
        Else
            dlMonthFrom.SelectedValue = Month(Date.Now).ToString
        End If
        If Not Request.Cookies("dailyStorageMonthTo") Is Nothing Then
            If Request.Cookies("dailyStorageMonthTo").Value <> "" Then
                dlMonthTo.SelectedValue = Server.HtmlEncode(Request.Cookies("dailyStorageMonthTo").Value)
            Else
                dlMonthTo.SelectedValue = Month(Date.Now).ToString
            End If
        Else
            dlMonthTo.SelectedValue = Month(Date.Now).ToString
        End If

        If Not Request.Cookies("dailyStorageYearFrom") Is Nothing Then
            If Request.Cookies("dailyStorageYearFrom").Value <> "" Then
                tbYearFrom.Text = Server.HtmlEncode(Request.Cookies("dailyStorageYearFrom").Value)
            Else
                tbYearFrom.Text = Year(Date.Now).ToString
            End If
        Else
            tbYearFrom.Text = Year(Date.Now).ToString
        End If
        If Not Request.Cookies("dailyStorageYearTo") Is Nothing Then
            If Request.Cookies("dailyStorageYearTo").Value <> "" Then
                tbYearTo.Text = Server.HtmlEncode(Request.Cookies("dailyStorageYearTo").Value)
            Else
                tbYearTo.Text = Year(Date.Now).ToString
            End If
        Else
            tbYearTo.Text = Year(Date.Now).ToString
        End If

    End Sub

    Public Function FormatBytes(ByVal BytesCaller As ULong) As String
        Dim DoubleBytes As Double
        Try
            Select Case BytesCaller
                Case Is >= 1099511627776
                    DoubleBytes = CDbl(BytesCaller / 1099511627776) 'TB
                    Return FormatNumber(DoubleBytes, 2) & " TB"
                Case 1073741824 To 1099511627775
                    DoubleBytes = CDbl(BytesCaller / 1073741824) 'GB
                    Return FormatNumber(DoubleBytes, 2) & " GB"
                Case 1048576 To 1073741823
                    DoubleBytes = CDbl(BytesCaller / 1048576) 'MB
                    Return FormatNumber(DoubleBytes, 2) & " MB"
                Case 1024 To 1048575
                    DoubleBytes = CDbl(BytesCaller / 1024) 'KB
                    Return FormatNumber(DoubleBytes, 2) & " KB"
                Case 1 To 1023
                    DoubleBytes = BytesCaller ' bytes
                    Return FormatNumber(DoubleBytes, 2) & " bytes"
                Case Else
                    Return ""
            End Select
        Catch
            Return ""
        End Try

    End Function

    Public Function CalendarDays(ByVal CalDay As ULong) As String
        Dim DoubleBytes As Double
        Try
            Select Case CalDay
                Case Is > 365

                    Return Math.Round(CalDay / 365, 0).ToString("#,##0") & " years"

                Case 360 To 365
                    'DoubleBytes = CDbl(BytesCaller / 1073741824) 'GB
                    Return "1 year"
                Case 1
                    'DoubleBytes = CDbl(BytesCaller / 1073741824) 'GB
                    Return CalDay.ToString & " day"
                Case 2 To 27
                    'DoubleBytes = CDbl(BytesCaller / 1073741824) 'GB
                    Return CalDay.ToString & " days"
                Case 28 To 31
                    'DoubleBytes = CDbl(BytesCaller / 1073741824) 'GB
                    Return "1 month"
                Case 31 To 364

                    Return Math.Round(CalDay / 30, 0).ToString & " months"
                
                Case Else
                    Return "Out of Diskspace"
            End Select
        Catch
            Return ""
        End Try

    End Function

    Private Sub btUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btUpdate.Click
        Dim ldata = GetFiles()

        Dim ltotal As Integer = 0
        Try
            If Not ldata Is Nothing Then
                Dim liCtr As Integer = 0
                Dim lsFileSize As String
                For Each lrow As DataRow In ldata.Rows
                    lsFileSize = GetDirectory(lrow("CreatedDate").ToString, lrow("refno").ToString, lrow("DocId").ToString, lrow("DocVersion").ToString, lrow("FileName").ToString)
                    If lsFileSize <> "" Then
                        UpdateFileSize(lsFileSize, lrow("DocId"), lrow("DocVersion"))
                    End If
                Next

            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If

        End Try
    End Sub
    Private Function GetFiles() As DataTable
        Dim s_sql As String = "select dl.createddate,dl.DocId,dl.RefNo,dfv.docVersion, dfv.filename from docfileversion dfv " & _
                                "inner join doclist dl on dfv.docid = dl.docid " & _
                                "where dl.createddate >= '" & tbDCFrom.Text & "' and " & _
                                " dl.createddate < DateAdd(day,1,'" & tbDCTo.Text & "') "
        Dim objCommand As clsSqlConn

        Dim ldata As DataTable

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql
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
    Private Sub UpdateFileSize(ByVal aiSize As String, ByVal aDocId As String, ByVal aiVersion As String)
        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text


            objCommand.CommandText = "UPDATE docfileversion SET FileSize =  " & aiSize & " " & _
"WHERE DocId = " & aDocId & " and docversion = " & aiVersion

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
    Private Function GetDirectory(ByVal aCreatedDate As String, ByVal srefno As String, ByVal asDocId As String, ByVal asVersion As String, ByVal asFileName As String) As String
        Dim sYear As String
        Dim sMonth As String
        Dim CreatedDate As Date = CDate(aCreatedDate)

        sYear = Year(CreatedDate).ToString()
        sMonth = MonthName(Month(CreatedDate))
        Dim lsFile As String = DocSession.FileLoc & sYear & "\" & sMonth & "\" & srefno & "\" & asDocId & "_" & asVersion & "_" & asFileName
        If System.IO.File.Exists(lsFile) Then
            Dim infoReader As New System.IO.FileInfo(lsFile)
            Return infoReader.Length.ToString
        Else
            lsFile = DocSession.FileLoc2 & sYear & "\" & sMonth & "\" & srefno & "\" & asDocId & "_" & asVersion & "_" & asFileName
            If System.IO.File.Exists(lsFile) Then
                Dim infoReader As New System.IO.FileInfo(lsFile)
                Return infoReader.Length.ToString
            Else
                Return ""
            End If
        End If
    End Function

    Private Sub dlType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dlType.SelectedIndexChanged
        If dlType.SelectedValue = "D" Then
            tbDCFrom.Visible = True
            tbDCTo.Visible = True
            dlMonthFrom.Visible = False
            dlMonthTo.Visible = False
            tbYearFrom.Visible = False
            tbYearTo.Visible = False
        ElseIf dlType.SelectedValue = "M" Then
            tbDCFrom.Visible = False
            tbDCTo.Visible = False
            dlMonthFrom.Visible = True
            dlMonthTo.Visible = True
            tbYearFrom.Visible = True
            tbYearTo.Visible = True
        ElseIf dlType.SelectedValue = "Y" Then
            tbDCFrom.Visible = False
            tbDCTo.Visible = False
            dlMonthFrom.Visible = False
            dlMonthTo.Visible = False
            tbYearFrom.Visible = True
            tbYearTo.Visible = True
        End If
        pnlCriteria.Update()
    End Sub
End Class