'Imports AjaxControlToolkit
Public Class UserControlDocumentIndex
    Inherits System.Web.UI.UserControl
    Dim Action As String
    Dim bAdd As Boolean = True
    Public Property pAddHistory As Boolean
        Get
            Return bAdd
        End Get
        Set(ByVal value As Boolean)
            bAdd = value
        End Set
    End Property
    Dim bEnableFields As Boolean = True
    Public Property pEnableFields As Boolean
        Get
            Return bEnableFields
        End Get
        Set(ByVal value As Boolean)
            bEnableFields = value
        End Set
    End Property
    Dim bDIS As Boolean = False
    Public Property pDIS As Boolean
        Get
            Return bDIS
        End Get
        Set(ByVal value As Boolean)
            bDIS = value
        End Set
    End Property
    Public Sub RetrieveDocIndex(ByVal asDocId As String, ByVal asDocType As String)
        Dim oDoc As New DocIndex
        oDoc.pDocType = asDocType
        oDoc.pDocId = asDocId
        oDoc.pDIS = pDIS
        Using lodata As DataTable = oDoc.RetrieveDocIndex()
            If lodata.Rows.Count > 0 Then

                rptIndex.Visible = True
                rptIndex.DataSource = lodata
                rptIndex.DataBind()


            Else
                rptIndex.Visible = False
            End If
        End Using
    End Sub

    Public Sub RetrieveDocIndex(ByVal asDocType As String)
        Dim oDoc As New DocIndex
        oDoc.pDocType = asDocType
        oDoc.pDIS = pDIS
        Using lodata As DataTable = oDoc.RetrieveDocTypeIndex()
            If lodata.Rows.Count > 0 Then


                rptIndex.Visible = True
                rptIndex.DataSource = lodata
                rptIndex.DataBind()


            Else
                rptIndex.Visible = False
            End If
        End Using
    End Sub

    Public ReadOnly Property rIndex As Repeater
        Get
            Return rptIndex
        End Get
    End Property

    Public ReadOnly Property pAction As String
        Get
            Return Action
        End Get
    End Property

    Private Sub rptIndex_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptIndex.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim tbT As TextBox = DirectCast(e.Item.FindControl("tbColValue"), TextBox)
            tbT.Text = Server.HtmlDecode(DirectCast(e.Item.FindControl("lColValue"), Literal).Text)
            tbT.MaxLength = CInt(DirectCast(e.Item.FindControl("lLen"), Literal).Text)
            'tbT.Width = CInt(DirectCast(e.Item.FindControl("lLen"), Literal).Text)  

            If DirectCast(e.Item.FindControl("lDataType"), Literal).Text = "3" Then
                DirectCast(e.Item.FindControl("tbColValue"), TextBox).Visible = False
                DirectCast(e.Item.FindControl("dlColValue"), DropDownList).Visible = True
                DirectCast(e.Item.FindControl("dlColValue"), DropDownList).SelectedValue = Server.HtmlDecode(DirectCast(e.Item.FindControl("lColValue"), Literal).Text)
            ElseIf DirectCast(e.Item.FindControl("lDataType"), Literal).Text = "5" Then
                DirectCast(e.Item.FindControl("dlList"), DropDownList).Visible = True
                DirectCast(e.Item.FindControl("tbColValue"), TextBox).Visible = False
                Dim oIn As New DocTypes
                oIn.pDocType = DirectCast(e.Item.FindControl("lDocType"), Literal).Text
                oIn.pColumnId = DirectCast(e.Item.FindControl("lColId"), Literal).Text
                Dim dlList As DropDownList = DirectCast(e.Item.FindControl("dlList"), DropDownList)
                Using ldata As DataTable = oIn.RetrieveIndexList()
                    Dim lrow As DataRow
                    lrow = ldata.NewRow
                    lrow(0) = oIn.pDocType
                    lrow(1) = oIn.pColumnId
                    lrow(2) = "0"
                    lrow(3) = ""
                    ldata.Rows.InsertAt(lrow, 0)
                    dlList.DataSource = ldata
                    dlList.DataTextField = "CodeDesc"
                    dlList.DataValueField = "Code"
                    dlList.DataBind()
                    dlList.SelectedValue = Server.HtmlDecode(DirectCast(e.Item.FindControl("lColValue"), Literal).Text)
                End Using
            ElseIf DirectCast(e.Item.FindControl("lDataType"), Literal).Text = "4" Then
                    DirectCast(e.Item.FindControl("tbColValue"), TextBox).Visible = False
                    DirectCast(e.Item.FindControl("tbDateValue"), TextBox).Visible = True
                    DirectCast(e.Item.FindControl("tbDateValue"), TextBox).Text = Server.HtmlDecode(DirectCast(e.Item.FindControl("lColValue"), Literal).Text)
            End If
            If Not pEnableFields Then
                DirectCast(e.Item.FindControl("tbColValue"), TextBox).Enabled = False
                DirectCast(e.Item.FindControl("tbDateValue"), TextBox).Enabled = False
                DirectCast(e.Item.FindControl("dlList"), DropDownList).Enabled = False
                DirectCast(e.Item.FindControl("dlColValue"), DropDownList).Enabled = False
            End If
        End If
    End Sub

    Public Sub SaveIndex(ByVal asDocId As String, ByVal asDocType As String, ByVal objCommand As clsSqlConn)
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        Dim lColId, lDataType, lColName, lColValue As Literal
        'Dim lColValue As TextBox
        'Dim oConn As New SqlClient.SqlConnection(str)
        'Dim ltr As SqlClient.SqlTransaction
        'Dim objCommand As New SqlClient.SqlCommand
        'Dim liCtr As Integer
        Dim oIndex As New DocIndex
        Dim lsValue As String
        Dim lsValue2 As String
        Dim Ohist As New DocHistory
        'liCtr = 0
        Try
            'oConn.Open()
            'ltr = oConn.BeginTransaction
            'objCommand.Transaction = ltr
            'objCommand.Connection = oConn
            For Each ri In rptIndex.Items
                If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then

                    lColId = DirectCast(ri.FindControl("lColId"), Literal)
                    lDataType = DirectCast(ri.FindControl("lDataType"), Literal)
                    If lDataType.Text = "3" Then
                        lsValue = DirectCast(ri.FindControl("dlColValue"), DropDownList).SelectedItem.Text
                        lsValue2 = DirectCast(ri.FindControl("dlColValue"), DropDownList).SelectedValue
                    ElseIf lDataType.Text = "5" Then
                        lsValue = DirectCast(ri.FindControl("dlList"), DropDownList).SelectedItem.Text
                        lsValue2 = DirectCast(ri.FindControl("dlList"), DropDownList).SelectedValue
                    ElseIf lDataType.Text = "4" Then
                        lsValue = DirectCast(ri.FindControl("tbDateValue"), TextBox).Text
                        lsValue2 = DirectCast(ri.FindControl("tbDateValue"), TextBox).Text
                        If lsValue2.Trim <> "" Then
                            If Not IsDate(lsValue2) Then
                                Throw New Exception("Invalid date value for " & DirectCast(ri.FindControl("lColName"), Literal).Text & ". Format should be mm/dd/yyyy.")
                            Else
                                lsValue2 = CDate(lsValue2).ToString("MM/dd/yyyy")
                                lsValue = lsValue2
                                DirectCast(ri.FindControl("tbDateValue"), TextBox).Text = lsValue2
                            End If
                        End If
                    ElseIf lDataType.Text = "2" Then
                        lsValue = DirectCast(ri.FindControl("tbColValue"), TextBox).Text
                        lsValue2 = DirectCast(ri.FindControl("tbColValue"), TextBox).Text
                        If lsValue2.Trim <> "" Then
                            If Not IsNumeric(lsValue2.Replace(",", "")) Then
                                Throw New Exception("Invalid numeric value for " & DirectCast(ri.FindControl("lColName"), Literal).Text & ".")
                            End If
                        End If
                    Else
                        lsValue = DirectCast(ri.FindControl("tbColValue"), TextBox).Text
                        lsValue2 = DirectCast(ri.FindControl("tbColValue"), TextBox).Text
                    End If
                    lColName = DirectCast(ri.FindControl("lColName"), Literal)
                    lColValue = DirectCast(ri.FindControl("lColValue"), Literal)
                    If lColValue.Text.Trim <> "" Then
                        If lColValue.Text.Trim <> lsValue.Trim AndAlso pAddHistory Then

                            Ohist.pTask = "Index"
                            Ohist.pAction = "Updated index " & lColName.Text & " from " & lColValue.Text.Trim & " to " & lsValue
                            Ohist.pUserId = DocSession.sUserId
                            Ohist.pIpAddress = Request.UserHostAddress
                            Ohist.pDocId = DocSession.sDocID
                            Ohist.AddHistory(objCommand)
                        End If
                    Else
                        If lsValue.Trim <> "" AndAlso pAddHistory Then
                            Ohist.pTask = "Index"
                            Ohist.pAction = "Added index " & lColName.Text & " = " & lsValue.Trim
                            Ohist.pUserId = DocSession.sUserId
                            Ohist.pIpAddress = Request.UserHostAddress
                            Ohist.pDocId = DocSession.sDocID
                            Ohist.AddHistory(objCommand)
                        End If
                    End If
                    

                    oIndex.pDocId = asDocId
                    oIndex.pDocType = asDocType
                    oIndex.pColId = lColId.Text
                    oIndex.pColValue = lsValue2
                    oIndex.SaveDocIndexValues(objCommand)
                    If lColName.Text.ToString.ToLower = DocSession.RDSColumn AndAlso DocSession.sDocStatus = "8" Then
                        If lColValue.Text.Trim <> lsValue.Trim Then


                            If lsValue2 <> "" Then
                                If IsDate(lsValue2) Then
                                    oIndex.pRetentionStartDate = lsValue2
                                End If

                                oIndex.pSetRetention = "Y"
                                oIndex.UpdateRetention(objCommand)
                            Else
                                'oIndex.pSetRetention = False
                                oIndex.pSetRetention = ""
                                oIndex.UpdateRetention(objCommand)
                            End If
                        End If

                    End If
                End If

            Next


            'msg.Text = "Document " & tbDocTitle.Text & " has been created"
            'If liCtr >= 1 Then
            '    ltr.pTran.Commit()
            'End If

        Catch ex As Exception

            'ltr.pTran.Rollback()
            Throw New Exception(ex.Message)
        Finally
            'If Not objCommand Is Nothing Then
            '    objCommand.Dispose()
            '    objCommand = Nothing
            'End If
            'If Not oConn Is Nothing Then
            '    oConn.Close()
            '    oConn.Dispose()
            '    oConn = Nothing
            'End If
            'If Not ltr Is Nothing Then
            '    ltr.Dispose()
            '    ltr = Nothing
            'End If


        End Try

    End Sub
End Class
