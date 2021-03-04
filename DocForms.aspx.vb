Imports System
Imports System.Data.SqlClient

Public Class DocForms
    Inherits System.Web.UI.Page
#Region "Page Events"
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'pager: step 4


        AddHandler ucAddDoc.e_click, AddressOf AddDoc
        AddHandler ucPager.eGreaterClick, AddressOf imgGreater_Click
        AddHandler ucPager.eLessClick, AddressOf imgLess_Click
        AddHandler ucPager.eFirstClick, AddressOf imgFirst_Click
        AddHandler ucPager.eLastClick, AddressOf imgLast_Click
        'AddHandler ucFolder.e_ShowMessage, AddressOf ShowFolderMessage
        AddHandler ucAtt.e_refresh, AddressOf SearchDocs

        '01/17/2014
        'AddHandler ucFolder.e_LinkButton, AddressOf SearchDocs

        Master.SelectTab("Forms")
        'ge ucDocRouting.ShowSearch()
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If DocSession.sUserId Is Nothing Or DocSession.sUserId = "" Then
            Response.Redirect("login.aspx")
        End If
        If Not IsPostBack Then

            'If Request.QueryString("c") = "1" Then
            '    hfCurrent.Value = "1"
            '    ResetCookies()
            'Else
            '    LoadCookies()
            'End If


            'GetDocType()


            RetrieveDocs()

            DocSession.DocumentPage = "DocForms.aspx"
        End If
    End Sub

#End Region

#Region "Populator Methods"








    Private Sub RetrieveDocs()

        'Dim ldata As DataTable
        Master.HideMessage()
        Dim loCls As clsDocForms
        Dim loData As DataTable
        Dim lsTotalRows As Integer
        Try
            loCls = New clsDocForms
            loCls.pIdx = hfCurrent.Value
            loCls.pRowsPerPage = CInt(DocSession.RowsPerPage)

            loCls.pDescription = tbDescription.text
            loCls.pFormFileName = tbFileName.text
            loCls.pUploadedFromDate = tbUploadedFromDate.Text
            loCls.pUploadedToDate = tbUploadedToDate.Text

            lsTotalRows = loCls.CountDocForms

            If lsTotalRows > 0 Then
                loCls.pSortCol = hfSortCol.Value
                loCls.pSortOrder = hfSortOrder.Value
                loData = loCls.RetrieveDocForms

                If loData.Rows.Count > DocSession.RowsPerPage Then

                    loData.Rows.RemoveAt(DocSession.RowsPerPage)

                Else

                End If

                If loData.Rows.Count = 0 Then

                    ucPager.Visible = False
                    pPager.Update()
                Else
                    hfTotalRows.Value = lsTotalRows
                    ucPager.Visible = True
                    ucPager.EnableButtons(CInt(hfCurrent.Value), CInt(hfTotalRows.Value))
                    pPager.Update()
                End If

                rptRecordList.DataSource = loData
                rptRecordList.DataBind()
            Else
                Master.ShowMessage("No records retrieved for the specified criteria. Please try another search criteria.")
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not loData Is Nothing Then
                loData.Dispose()
                loData = Nothing
            End If
        End Try

    End Sub


#End Region


#Region "Repeater Events"
    Private Sub Repeater1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptRecordList.ItemCreated
        'Dim imgExp As ImageButton
        Dim imgU As ImageButton
        If e.Item.ItemType = ListItemType.Header Then
            If DocSession.sUserRole = "A" Or DocSession.sOfcCode = "CRD" Then
                DirectCast(e.Item.FindControl("imgDelete"), ImageButton).Visible = True
            Else
                DirectCast(e.Item.FindControl("imgDelete"), ImageButton).Visible = False
            End If

        ElseIf e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                'imgExp = DirectCast(e.Item.FindControl("imgExpand"), ImageButton)
                imgU = DirectCast(e.Item.FindControl("imgUpd"), ImageButton)
            AddHandler DirectCast(e.Item.FindControl("lbtnDoc"), LinkButton).Click, AddressOf fDownloadLink

            AddHandler imgU.Click, AddressOf UpdateDocType

            If DocSession.sUserRole = "A" Or DocSession.sOfcCode = "CRD" Then
                imgU.Visible = True
                DirectCast(e.Item.FindControl("cbxDelete"), CheckBox).Visible = True
            Else
                imgU.Visible = False
                DirectCast(e.Item.FindControl("cbxDelete"), CheckBox).Visible = False
            End If


        End If


    End Sub
    'Private Sub Repeater1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeater1.ItemDataBound
    '    'Dim lblDocName As Literal
    '    '', lblActive As Literal

    '    'Dim tDocName As TextBox
    '    ''Dim cActive As CheckBox


    '    'If cbUpdate.Checked Then
    '    If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
    '        Dim lblDocName As ImageButton = DirectCast(e.Item.FindControl("imgUpd"), ImageButton)
    '        Dim lblActive As Literal = DirectCast(e.Item.FindControl("lFileName"), Literal)
    '        Dim lext As String = DocTypeLogo.DocTypeBitMap(Right(lblActive.Text.Trim, 4))

    '        lblDocName.ImageUrl = lext

    '        If DirectCast(e.Item.FindControl("lUrgent"), Literal).Text = "1" OrElse DirectCast(e.Item.FindControl("lUrgent"), Literal).Text = "True" Then
    '            DirectCast(e.Item.FindControl("imgUrgent"), Image).Visible = True
    '        End If
    '        If DirectCast(e.Item.FindControl("lFlag"), Literal).Text <> "-1" Then
    '            DirectCast(e.Item.FindControl("imgFlag"), Image).Visible = True
    '        End If
    '        If DirectCast(e.Item.FindControl("lOffice"), Literal).Text <> "" Then
    '            DirectCast(e.Item.FindControl("ModifiedBy"), Literal).Text = DirectCast(e.Item.FindControl("ModifiedBy"), Literal).Text & "(" & DirectCast(e.Item.FindControl("lOffice"), Literal).Text & ")"
    '        End If
    '    End If
    '    'End If
    'End Sub

    'Private Sub Repeater2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeater2.ItemDataBound
    '    If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
    '        If DirectCast(e.Item.FindControl("lComment"), Literal).Text = "OK to delete." Then
    '            DirectCast(e.Item.FindControl("rw"), HtmlControls.HtmlTableRow).Attributes("class") = "greenHigh"
    '        End If
    '    End If
    'End Sub
#End Region



#Region "Edit Methods"


    Public Function WithSelected() As Boolean
        Dim lbret As Boolean = False
        For Each rItems As RepeaterItem In rptRecordList.Items
            If DirectCast(rItems.FindControl("cbxDelete"), CheckBox).Checked Then
                lbret = True
                Exit For
            End If
        Next
        Return lbret
    End Function

    Private Sub UpdateDocType(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Using oImg As ImageButton = DirectCast(sender, ImageButton)

            Dim oItem As RepeaterItem = DirectCast(oImg.NamingContainer, RepeaterItem)

            DirectCast(oItem.FindControl("imgSave"), ImageButton).Visible = True
            DirectCast(oItem.FindControl("tbDescription"), TextBox).Visible = True
            DirectCast(oItem.FindControl("lDescription"), Literal).Visible = False
            DirectCast(oItem.FindControl("tbDescription"), TextBox).Text = DirectCast(oItem.FindControl("lDescription"), Literal).Text
        End Using


    End Sub

    Private Sub UpdateDocType2(ByVal sender As Object, ByVal e As System.EventArgs)

        'Dim ri As RepeaterItem
        'ri = DirectCast(DirectCast(sender, LinkButton).NamingContainer, RepeaterItem)
        'Dim lDocType As Literal = DirectCast(ri.FindControl("lDocType"), Literal)
        ''Dim lDocTypeAccess As Literal = DirectCast(ri.FindControl("lDocTypeAccess"), Literal)
        'Dim lDocId As Literal = DirectCast(ri.FindControl("lDocId"), Literal)

        'DocSession.sDocID = lDocId.Text
        'DocSession.sDocType = lDocType.Text
        ''DocSession.sDocTypeAccess = lDocTypeAccess.Text
        'Response.Redirect("view.aspx")

    End Sub

#End Region

#Region "Delete Documents Properties"

    Private Sub DeleteDocList()



    End Sub





#End Region

#Region "Pager Section"
    Public Sub RetAction()
        DocSession.doc_DocCurrentPage = hfCurrent.Value
        RetrieveDocs()
        pnlRepeater.Update()
        'ucDB.pIdx = CInt(hfCurrent.Value)
        'ucDB.RetrieveAction(DocSession.sUserId)
        'ucPager.EnableButtons(CInt(hfCurrent.Value), ucDB.pRetVal)
        'hfTotalRows.Value = CStr(ucDB.pRetVal)
    End Sub

    'pager: step 3
    Private Sub imgLess_Click()
        Dim lIdx As Integer
        lIdx = CInt(hfCurrent.Value) - DocSession.RowsPerPage
        hfCurrent.Value = CStr(lIdx)
        RetAction()



    End Sub

    Private Sub imgGreater_Click()
        Dim lIdx As Integer
        lIdx = CInt(hfCurrent.Value) + DocSession.RowsPerPage
        hfCurrent.Value = CStr(lIdx)
        RetAction()


    End Sub

    Private Sub imgFirst_Click()
        Dim lIdx As Integer
        lIdx = 1
        hfCurrent.Value = CStr(lIdx)
        RetAction()

    End Sub

    Private Sub imgLast_Click()
        Dim lIdx As Integer
        If CInt(hfTotalRows.Value) Mod DocSession.RowsPerPage > 0 Then
            lIdx = (CInt(hfTotalRows.Value) - (CInt(hfTotalRows.Value) Mod DocSession.RowsPerPage)) + 1
        Else
            lIdx = (CInt(hfTotalRows.Value) - DocSession.RowsPerPage) + 1
        End If
        hfCurrent.Value = CStr(lIdx)
        RetAction()


    End Sub

    Public Sub sortColumnHeader(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lbSort As LinkButton = DirectCast(sender, LinkButton)

        Dim img As Image
        hfCurrent.Value = "1"
        DocSession.doc_DocCurrentPage = hfCurrent.Value
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
        'If imgSort5.ID = "imgSort" & Right(lbSort.ID, 1) Then
        '    img = imgSort5
        '    imgSort5.Visible = True
        'Else
        '    imgSort5.Visible = False

        'End If


        'Dim oDocList As New DocList

        If img.Visible Then
            If img.ImageUrl.ToLower = "images/asc.png" Then
                img.ImageUrl = "images/desc.png"
                'oDocList.pSortOrder = "Desc"
                DocSession.doc_SortOrder = "Desc"
            Else
                img.ImageUrl = "images/asc.png"
                'oDocList.pSortOrder = "Asc"
                DocSession.doc_SortOrder = "Asc"
            End If
        Else
            img.ImageUrl = "images/asc.png"
            'oDocList.pSortOrder = "Asc"

            DocSession.doc_SortOrder = "Asc"
            img.Visible = True
        End If
        DocSession.doc_SortCol = lbSort.Text
        hfSortCol.Value = lbSort.Text
        hfSortOrder.Value = DocSession.doc_SortOrder
        'oDocList.pDocId = ""
        'oDocList.pDocType = dlFilterDocType.SelectedValue
        'oDocList.pDocTitle = tbFilterTitle.Text
        'oDocList.pDocStatus = dlFilterDocStatus.SelectedValue
        'oDocList.pGroupId = DocSession.sUserGroup
        'oDocList.pIdx = hfCurrent.Value
        'oDocList.pRowsPerPage = DocSession.RowsPerPage
        'oDocList.pSortCol = lbSort.Text
        'oDocList.pUserId = DocSession.sUserId
        'Dim ldata As DataTable = oDocList.RetrieveDocs

        'If ldata.Rows.Count > DocSession.RowsPerPage Then
        '    'imgGreater.Visible = True
        '    ldata.Rows.RemoveAt(DocSession.RowsPerPage)
        'Else
        '    'imgGreater.Visible = False
        'End If

        'lMsg.Visible = False
        ''hfTotalRows.Value = CStr(retval.pParam.Value)
        'ucPager.EnableButtons(CInt(hfCurrent.Value), CInt(hfTotalRows.Value))

        'Repeater1.DataSource = ldata

        'Repeater1.DataBind()
        'pnlRepeater.Update()

        RetAction()

    End Sub
#End Region

#Region "Upload Functionality"
    Private Sub AddDoc()
        'ucUpload.ShowAdd()
        'ucUpload.Visible = True
        ucAtt.ResetFields
        ucAtt.pCreatedDate = DateTime.Now.ToString ' lCreatedDate.Text
        ucAtt.Visible = True
    End Sub

#End Region

#Region "Filter Records"
    Private Sub imgBk_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBk.Click
        pFilter.Visible = Not pFilter.Visible
        If InStr(imgBk.ImageUrl, "show") > 0 Then
            imgBk.ImageUrl = "images/hidepanel.png"
        Else
            imgBk.ImageUrl = "images/showpanel.png"
        End If
        pnlFilter.Update()
    End Sub
    '01/17/2014
    Protected Sub btSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSearch.Click
        SearchDocs()
    End Sub
    '01/17/2014
    Private Sub SearchDocs()
        hfCurrent.Value = "1"
        SetCookies()
        RetrieveDocs()
        ShowResult()
        'ucFolder.RetrieveUserFolders()
        'RetrieveUserFolders()
    End Sub

    Private Sub ShowResult()

        Master.ShowImageDocument = False
        pnlRepeater.Update()
    End Sub


#End Region

    Public Sub fDownload(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Try


            Dim lImg As ImageButton = DirectCast(sender, ImageButton)
            Dim lsFN As String = DirectCast(lImg.Parent.FindControl("lFormFileName"), Literal).Text
            Dim lsUpDate As String = DirectCast(lImg.Parent.FindControl("lUploadedDate"), Literal).Text
            Dim lsFormID As String = DirectCast(lImg.Parent.FindControl("lFormId"), Literal).Text
            '--new folder
            Dim sYear As String = Year(CDate(lsUpDate))
            Dim sMonth As String = MonthName(Month(CDate(lsUpDate)))
            Dim lsFile As String = DocSession.FileLoc & sYear & "\" & sMonth & "\forms\" & lsFN


            Dim linfo As New System.IO.FileInfo(lsFile)
            If System.IO.File.Exists(lsFile) Then
                LogHistory(lsFormID, "Downloaded " & lsFN)
                'DownLoadPDFFile(lsFN, lsFile)
                Dim lsContentType As String = System.Configuration.ConfigurationManager.AppSettings(linfo.Extension.ToLower)
                DownloadFile(lsFile, lsContentType)
            Else
                Master.ShowMessage("File " & IIf(lsFN.Length > 20, Left(lsFN, 20) & "...", lsFN) & " does not exist.")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Sub fDownloadLink(ByVal sender As Object, ByVal e As System.EventArgs)

        Try


            Dim lImg As LinkButton = DirectCast(sender, LinkButton)
            Dim lsFN As String = DirectCast(lImg.Parent.FindControl("lFormFileName"), Literal).Text
            Dim lsUpDate As String = DirectCast(lImg.Parent.FindControl("lUploadedDate"), Literal).Text
            Dim lsFormID As String = DirectCast(lImg.Parent.FindControl("lFormId"), Literal).Text
            '--new folder
            Dim sYear As String = Year(CDate(lsUpDate))
            Dim sMonth As String = MonthName(Month(CDate(lsUpDate)))
            Dim lsFile As String = DocSession.FileLoc & sYear & "\" & sMonth & "\forms\" & lsFN


            Dim linfo As New System.IO.FileInfo(lsFile)
            If System.IO.File.Exists(lsFile) Then
                LogHistory(lsFormID, "Downloaded " & lsFN)
                'DownLoadPDFFile(lsFN, lsFile)
                Dim lsContentType As String = System.Configuration.ConfigurationManager.AppSettings(linfo.Extension.ToLower)
                DownloadFile(lsFile, lsContentType)
            Else
                Master.ShowMessage("File " & IIf(lsFN.Length > 20, Left(lsFN, 20) & "...", lsFN) & " does not exist.")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub DownLoadPDFFile(ByVal psFN As String, ByVal psFile As String)
        'Dim document As iTextSharp.text.Document = Nothing
        'Dim writer As iTextSharp.text.pdf.PdfWriter = Nothing
        Dim reader As iTextSharp.text.pdf.PdfReader = Nothing
        Dim stamper As iTextSharp.text.pdf.PdfStamper = Nothing
        Dim img As iTextSharp.text.Image = Nothing
        'Dim underContent As iTextSharp.text.pdf.PdfContentByte = Nothing
        Dim overContent As iTextSharp.text.pdf.PdfContentByte = Nothing
        Dim rect As iTextSharp.text.Rectangle = Nothing
        Dim X, Y As Single
        Dim pageCount As Integer = 0
        Dim lsMessage As String = ""
        'watermark variables

        Dim lsPath As String
        'lsFileName = 'context.Request.QueryString("filename") 'DocSession.soldDocFileName
        Dim Encoding As New System.Text.UTF8Encoding()
        'lsPath = context.Request.QueryString("location") & lsFileName
        'lsPath = DocSession.FileLoc & "attachment\" & DocSession.sDocID & "_" & psFN
        lsPath = psFile
        Try
            'Dim lbpermitprint As Boolean
            'If context.Session("s_CanPrint") = "True" Then
            'lbpermitPrint = True 
            'lbpermitprint = True ' always false so printing can be handle on the page only
            'Else
            'lbpermitprint = False
            'End If


            'If lbpermitprint ThenoNew
            Dim stime As DateTime = DateTime.Now
            Dim lss As String = "This is a hard copy of " & DocSession.sReferenceNo & " - " & psFN & ","
            Dim lss2 As String = "printed from the Document Management System " &
                        "on " & stime.ToString("dd/MM/yyyy") & " at " & stime.ToString("HH:MM") &
                " by: P" & stime.ToString("MM") & stime.ToString("HHmm") & stime.ToString("dd") & stime.ToString("yy") & DocSession.sUserId.ToUpper & "."

            'End If


            Using outputStream As System.IO.MemoryStream = New System.IO.MemoryStream()

                reader = New iTextSharp.text.pdf.PdfReader(lsPath)
                'reader.unethicalreading = True

                'Dim output_stream As New System.IO.MemoryStream
                'start watermeark
                img = iTextSharp.text.Image.GetInstance(DocSession.FileLoc & "watermark\watermark.png")
                stamper = New iTextSharp.text.pdf.PdfStamper(reader, outputStream)
                pageCount = reader.NumberOfPages()

                Dim font As iTextSharp.text.pdf.BaseFont = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.TIMES_ROMAN, iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.EMBEDDED)
                Dim backgroundColor As iTextSharp.text.BaseColor = New iTextSharp.text.BaseColor(0, 0, 0)
                Dim fontColor As iTextSharp.text.BaseColor = New iTextSharp.text.BaseColor(0, 0, 0)

                'stamper.SetEncryption(Nothing, Encoding.GetBytes("docuvu"), PdfWriter.AllowScreenReaders, PdfWriter.STRENGTH40BITS)
                'stamper.SetEncryption(Nothing, Nothing, PdfWriter.AllowScreenReaders, PdfWriter.STRENGTH40BITS)
                'stamper.SetEncryption((Nothing, PdfWriter.AllowScreenReaders, PdfWriter.STRENGTH40BITS)
                For ii As Integer = 1 To pageCount
                    rect = reader.GetPageSizeWithRotation(ii)
                    img.ScalePercent(100)
                    img.ScaleToFit(rect.Width, rect.Height)
                    X = (rect.Width - img.ScaledWidth) / 2
                    Y = (rect.Height - img.ScaledHeight) / 2
                    img.SetAbsolutePosition(X, Y)
                    overContent = stamper.GetOverContent(ii)
                    overContent.AddImage(img)
                    overContent.BeginText()
                    overContent.SetFontAndSize(font, 6.0F)
                    overContent.SetColorFill(fontColor)

                    overContent.SetTextMatrix(15, 18)
                    overContent.ShowText(lss)
                    overContent.SetTextMatrix(15, 10)
                    overContent.ShowText(lss2)
                    overContent.EndText()
                    'overContent.SaveState()
                    'overContent.RestoreState()

                Next
                stamper.Close()

                Dim cntnt As Byte() = outputStream.ToArray


                outputStream.Flush()
                outputStream.Close()
                outputStream.Dispose()
                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment; filename=" & psFN.Replace(",", ""))
                ' Add a HTTP header to the output stream that contains the 
                ' content length(File Size). This lets the browser know how much data is being transfered
                Response.AddHeader("Content-Length", cntnt.Length.ToString())
                'Set the HTTP MIME type of the output stream
                Response.ContentType = "application/pdf"
                Response.BinaryWrite(cntnt)
            End Using
        Catch ex As Exception

            If ex.Message = "PdfReader not opened with owner password" Then

                'Master.ShowMessage("Cannot open encrypted file. Please upload pdf file without password.")
            Else
                'Master.ShowMessage(ex.Message)
                'Context.Response.Write("File is missing. Please contact the administrator.")
            End If
        Finally

            If Not reader Is Nothing Then
                reader.Close()
                reader = Nothing
            End If
            If Not stamper Is Nothing Then
                stamper.Close()
                stamper.Dispose()
                stamper = Nothing
            End If

        End Try
    End Sub

    Private Sub DownloadFile(ByVal psPath As String, ByVal asContentType As String)

        Dim tFDload As New System.IO.FileInfo(psPath)
        If System.IO.File.Exists(psPath) Then

            ' clear the current output content from the buffer
            'LogHistory(tFDload.Name)
            'Response.Buffer = True
            Response.Clear()
            Response.ClearContent()
            Response.ClearHeaders()
            'Response.Charset = "iso-8859-1"
            'Response.HeaderEncoding = System.Text.Encoding.GetEncoding("iso-8859-1")
            Response.AddHeader("Content-Disposition", "attachment; filename=" +
        tFDload.Name)

            Response.AddHeader("Content-Length", tFDload.Length.ToString())

            Response.ContentType = asContentType

            Response.WriteFile(tFDload.FullName)

            Response.End()
        Else
        End If




    End Sub

    Private Sub WriteFile(filepath As String, fileName As String, contentType As String)
        Response.Buffer = True
        Response.Clear()
        Response.ContentType = contentType
        Response.Charset = "iso-8859-1"
        Response.HeaderEncoding = System.Text.Encoding.GetEncoding("iso-8859-1")

        Dim extension As String = System.IO.Path.GetExtension(fileName)
        Response.AddHeader("content-disposition", Convert.ToString("attachment; filename=") & fileName)

        Response.WriteFile(filepath)
        Response.Flush()
    End Sub

    Private Sub LogHistory(ByVal asFormId As String, ByVal asAction As String)
        Dim ohist As New FormHistory

        ohist.pAction = asAction
        ohist.pFormId = asFormId
        ohist.pIpAddress = Request.UserHostAddress
        ohist.pUserId = DocSession.sUserId
        ohist.AddHistory()


    End Sub
    Private Sub ucCon_e_ok_click() Handles ucCon.e_ok_click

        Dim cbox As CheckBox

        Dim odoc As clsDocForms
        Dim objcommand As clsSqlConn
        Dim ltr As DbTran
        Dim lsMsg As String = ""
        'Dim ri As RepeaterItem
        Try
            Dim ohist As New FormHistory
            ltr = New DbTran
            odoc = New clsDocForms

            objcommand = New clsSqlConn(ltr.pTran)
            Dim lbOk As Boolean = False
            Dim lsDate As String = DateTime.Now.ToString
            For Each ri As RepeaterItem In rptRecordList.Items
                If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then
                    cbox = CType(ri.FindControl("cbxDelete"), CheckBox)
                    If cbox.Checked Then
                        odoc.pFormId = DirectCast(ri.FindControl("lFormId"), Literal).Text
                        odoc.DeleteDocForms(objcommand)
                        odoc.pUserId = DocSession.sUserId

                        ohist.pAction = "Deleted " & DirectCast(ri.FindControl("lFormFileName"), Literal).Text
                        ohist.pFormId = odoc.pFormId
                        ohist.pIpAddress = Request.UserHostAddress
                        ohist.pUserId = DocSession.sUserId
                        ohist.AddHistory(objcommand)
                        lbOk = True
                    End If
                End If

            Next
            ltr.pTran.Commit()
            If lbOk Then
                Master.ShowMessage("Forms selected successfully deleted.")
            End If

            hfCurrent.Value = "1"
            RetrieveDocs()

            pnlRepeater.Update()
        Catch ex As Exception
            If Not ltr Is Nothing Then
                ltr.pTran.Rollback()
            End If
            Master.ShowMessage("Error occurred while processing your request (" & ex.Message & "). Please try again.")

        Finally
            If Not objcommand Is Nothing Then
                objcommand.Dispose()
                objcommand = Nothing

            End If
            If Not ltr Is Nothing Then
                ltr.Dispose()
                ltr = Nothing
            End If
        End Try
    End Sub


    Private Sub ucCon2_e_ok_click() Handles ucCon2.e_ok_click
        'Dim cbox As UserControlCheckBox

        'Dim odoc As DocList
        'Dim objcommand As clsSqlConn
        'Dim ltr As DbTran
        'Dim lsMsg As String = ""
        ''Dim ri As RepeaterItem
        'Try
        '    ltr = New DbTran
        '    odoc = New DocList
        '    'odoc.pDocId =
        '    'odoc.ExistsInInbox()
        '    objcommand = New clsSqlConn(ltr.pTran)
        '    Dim lsDate As String = DateTime.Now.ToString
        '    For Each ri As RepeaterItem In Repeater1.Items
        '        If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then
        '            cbox = CType(ri.FindControl("cbArchive"), UserControlCheckBox)
        '            If cbox.BoxCheck Then
        '                odoc.pDocId = DirectCast(ri.FindControl("lDocId"), Literal).Text.Trim
        '                odoc.pFolderId = Trim(ucCon2.pID)
        '                odoc.pUserId = DocSession.sUserId
        '                odoc.pSeqNo = "0"
        '                'If lType.Text.Trim = "Sent" Then
        '                '    odoc.DeleteFromOutbox(objcommand)
        '                '    lsMsg = "Document has been deleted from your Sent box."
        '                'Else
        '                odoc.MoveDocFolder(objcommand) ', odoc.pExistsInInbox)
        '                lsMsg = "Document has been moved to " & ucCon2.pDesc & " folder."

        '                'End If

        '            End If
        '        End If

        '    Next
        '    ltr.pTran.Commit()

        '    If lsMsg <> "" Then
        '        'DocSession.sFolderID = ucCon2.pID.Trim
        '        'DocSession.sFolderDesc = ucCon2.pDesc
        '    End If

        '    'Repeater1.Visible = False
        '    'ucPager.Visible = False
        '    'pPager.Update()
        '    hfCurrent.Value = "1"
        '    RetrieveDocs()
        '    'ucFolder.RetrieveUserFolders()

        '    pnlRepeater.Update()
        '    Master.ShowMessage(lsMsg)
        'Catch ex As Exception
        '    Master.ShowMessage("Error occurred while processing your request (" & ex.Message & "). Please try again.")
        '    If Not ltr Is Nothing Then
        '        ltr.pTran.Rollback()
        '    End If
        'Finally
        '    If Not objcommand Is Nothing Then
        '        objcommand.Dispose()
        '        objcommand = Nothing

        '    End If
        '    If Not ltr Is Nothing Then
        '        ltr.Dispose()
        '        ltr = Nothing
        '    End If
        'End Try
    End Sub



    Private Sub LoadCookies()
        Dim filterVisible As Boolean = False

        If Not Request.Cookies("docformsDescription") Is Nothing Then
            If Request.Cookies("docformsDescription").Value <> "" Then

                tbDescription.Text = Request.Cookies("docformsDescription").Value
                filterVisible = True
            End If
        End If
        If Not Request.Cookies("docFormsFileName") Is Nothing Then
            If Request.Cookies("docFormsFileName").Value <> "" Then

                tbFileName.Text = Request.Cookies("docFormsFileName").Value
                filterVisible = True
            End If
        End If
        If Not Request.Cookies("docFormsUploadedToDate") Is Nothing Then
            If Request.Cookies("docFormsUploadedToDate").Value <> "" Then

                tbUploadedToDate.Text = Request.Cookies("docFormsUploadedToDate").Value
                filterVisible = True
            End If
        End If
        If Not Request.Cookies("docFormsUploadedFromDate") Is Nothing Then
            If Request.Cookies("docFormsUploadedFromDate").Value <> "" Then

                tbUploadedFromDate.Text = Request.Cookies("docFormsUploadedFromDate").Value
                filterVisible = True
            End If
        End If
        If Not Request.Cookies("docFormUploadedBy") Is Nothing Then
            If Request.Cookies("docFormUploadedBy").Value <> "" Then

                tbUploadedBy.Text = Request.Cookies("docFormUploadedBy").Value

            End If
        End If
        pFilter.Visible = filterVisible
    End Sub
    Private Sub SetCookies()
        Dim mycookie As HttpCookie = New HttpCookie("docformsDescription")

        mycookie.Value = tbDescription.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        'mycookie = New HttpCookie("sentCurrent")
        'mycookie.Value = hfCurrent.Value
        'mycookie.Expires = DateTime.Now.AddDays(30)
        'Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("docFormsFileName")
        mycookie.Value = tbFileName.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("docFormsUploadedFromDate")
        mycookie.Value = tbUploadedFromDate.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("docFormsUploadedToDate")
        mycookie.Value = tbUploadedToDate.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("docFormUploadedBy")
        mycookie.Value = tbUploadedBy.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)


    End Sub
    Private Sub ResetCookies()

        Dim mycookie As HttpCookie = New HttpCookie("docformsDescription")

        mycookie.Value = ""
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        'mycookie = New HttpCookie("sentCurrent")
        'mycookie.Value = hfCurrent.Value
        'mycookie.Expires = DateTime.Now.AddDays(30)
        'Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("docFormsFileName")
        mycookie.Value = ""
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("docFormsUploadedFromDate")
        mycookie.Value = ""
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("docFormsUploadedToDate")
        mycookie.Value = ""
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("docFormUploadedBy")
        mycookie.Value = ""
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)


    End Sub

    Protected Sub imgDelete_Click(sender As Object, e As ImageClickEventArgs)
        If WithSelected() Then
            ucCon.Visible = True
        Else
            Master.ShowMessage("Please select a record before clicking Delete button.")
        End If
        pnlRepeater.Update()
    End Sub

    Protected Sub imgSave_Click(sender As Object, e As ImageClickEventArgs)
        Using oImg As ImageButton = DirectCast(sender, ImageButton)

            Dim oItem As RepeaterItem = DirectCast(oImg.NamingContainer, RepeaterItem)
            Dim loCls As New clsDocForms
            loCls.pFormId = DirectCast(oItem.FindControl("lFormId"), Literal).Text
            loCls.pDescription = DirectCast(oItem.FindControl("tbDescription"), TextBox).Text
            Try
                loCls.UpdateDocForms()
                Master.ShowMessage("Form has been updated.")
                DirectCast(oItem.FindControl("imgSave"), ImageButton).Visible = False
                DirectCast(oItem.FindControl("tbDescription"), TextBox).Visible = False
                DirectCast(oItem.FindControl("lDescription"), Literal).Visible = True
                DirectCast(oItem.FindControl("lDescription"), Literal).Text = DirectCast(oItem.FindControl("tbDescription"), TextBox).Text

            Catch ex As Exception
                Master.ShowMessage("Error occurred while saving (" & ex.Message & "). Please try again.")
            End Try
        End Using
    End Sub
End Class
