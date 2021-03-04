Public Class UserControlPager
    Inherits System.Web.UI.UserControl
    Public Event eLessClick()
    Public Event eGreaterClick()
    Public Event eLessClick2(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event eGreaterClick2(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Event eFirstClick()
    Public Event eLastClick()
    Public Event eFirstClick2(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event eLastClick2(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Property pTextColor As String
        Get
            Return lPageCount.Style("color")
        End Get
        Set(ByVal value As String)
            lPageCount.Style("color") = value
        End Set

    End Property
    Public Property pPageCount As String
        Get
            Return lPageCount.Text
        End Get
        Set(ByVal value As String)
            lPageCount.Text = value
        End Set

    End Property

    'Public Property pRecordCount As String
    '    Get
    '        Return lRecordCount.Text
    '    End Get
    '    Set(ByVal value As String)
    '        lRecordCount.Text = value
    '    End Set

    'End Property

    Public Property pImgGreater As Boolean
        Get
            Return imgGreater.Visible
        End Get
        Set(ByVal value As Boolean)
            imgGreater.Visible = value
            imgGreaterD.Visible = Not value
        End Set

    End Property

    Public Property pImgLess As Boolean
        Get
            Return imgLess.Visible
        End Get
        Set(ByVal value As Boolean)
            imgLess.Visible = value
            imgLessD.Visible = Not value
        End Set

    End Property

    Public Property pImgFirst As Boolean
        Get
            Return imgFirst.Visible
        End Get
        Set(ByVal value As Boolean)
            imgFirst.Visible = value
            imgFirstD.Visible = Not value
        End Set

    End Property

    Public Property pImgLast As Boolean
        Get
            Return imgLast.Visible
        End Get
        Set(ByVal value As Boolean)
            imgLast.Visible = value
            imgLastD.Visible = Not value
        End Set

    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Sub EnableButtons(ByVal iCurrVal As Integer, ByVal iRetVal As Integer)
        If iCurrVal > 1 Then
            pImgLess = True
            pImgFirst = True
        Else
            pImgLess = False
            pImgFirst = False
        End If

        If iCurrVal < iRetVal AndAlso (iRetVal - iCurrVal) >= DocSession.RowsPerPage Then
            pImgGreater = True
            pImgLast = True
        Else
            pImgGreater = False
            pImgLast = False
        End If

        'ucPager.pRecordCount = "Total record(s): " & CStr(iRetVal)
        If (iCurrVal + CInt(DocSession.RowsPerPage) - 1) > iRetVal Then
            pPageCount = "Row " & CStr(iCurrVal) & " - " & CStr(iRetVal) & " of " & CStr(iRetVal)
        Else
            pPageCount = "Row " & CStr(iCurrVal) & "-" & CStr(iCurrVal + CInt(DocSession.RowsPerPage) - 1) & " of " & CStr(iRetVal)
        End If
        If iRetVal = 0 Then
            lPageCount.Visible = False
        Else
            lPageCount.Visible = True

        End If
    End Sub
    Public Sub EnableButtons(ByVal iCurrVal As Integer, ByVal iRetVal As Integer, ByVal iRowsPerPage As Integer)
        If iCurrVal > 1 Then
            pImgLess = True
            pImgFirst = True
        Else
            pImgLess = False
            pImgFirst = False
        End If

        If iCurrVal < iRetVal AndAlso (iRetVal - iCurrVal) >= iRowsPerPage Then
            pImgGreater = True
            pImgLast = True
        Else
            pImgGreater = False
            pImgLast = False
        End If

        'ucPager.pRecordCount = "Total record(s): " & CStr(iRetVal)
        If (iCurrVal + CInt(iRowsPerPage) - 1) > iRetVal Then
            pPageCount = "Row " & CStr(iCurrVal) & " - " & CStr(iRetVal) & " of " & CStr(iRetVal)
        Else
            pPageCount = "Row " & CStr(iCurrVal) & "-" & CStr(iCurrVal + CInt(iRowsPerPage) - 1) & " of " & CStr(iRetVal)
        End If
        If iRetVal = 0 Then
            lPageCount.Visible = False
        Else
            lPageCount.Visible = True

        End If
    End Sub
    Private Sub imgGreater_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgGreater.Click
        RaiseEvent eGreaterClick()
        RaiseEvent eGreaterClick2(sender, e)
    End Sub

    Private Sub imgLess_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgLess.Click
        RaiseEvent eLessClick()
        RaiseEvent eLessClick2(sender, e)
    End Sub
    Private Sub imgFirst_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgFirst.Click
        RaiseEvent eFirstClick()
        RaiseEvent eFirstClick2(sender, e)
    End Sub

    Private Sub imgLast_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgLast.Click
        RaiseEvent eLastClick()
        RaiseEvent eLastClick2(sender, e)
    End Sub
End Class