Imports System
Imports System.Data.SqlClient
Imports System.IO

Public Class _home
    Inherits System.Web.UI.Page

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        AddHandler ucAddDoc.e_click, AddressOf AddDoc
        AddHandler uapproval.e_ShowMessage, AddressOf ShowMessage
        AddHandler ucUpload.e_ShowMessage, AddressOf ShowUploadMessage
        AddHandler ucUpload.e_exec, AddressOf RefreshCount
        AddHandler uapproval.e_UpdateCount, AddressOf RefreshCount

        Master.SelectTab("Home")
    End Sub
    Private Sub ShowMessage()
        If DocSession.sSendEmail Then
            Master.ShowMessage("Document has been approved and routed to the selected user. Email notification was sent to the approver.")
        Else
            Master.ShowMessage("Document has been approved and routed to the selected user. Email notification is currently disabled. Please notify the approver manually.")
        End If

    End Sub
    Private Sub ShowUploadMessage()
        
        Master.ShowMessage(ucUpload.Message)

    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If DocSession.sUserId Is Nothing OrElse DocSession.sUserId = "" Then
            Response.Redirect("Login.aspx")
        End If
        If Not IsPostBack Then
            Try
                DocSession.sFolderID = ""
                Dim lidays As Integer = DateDiff(DateInterval.Day, CDate(Date.Now.ToShortDateString), CDate(DocSession.sPasswordExpiry))
                Dim lsDays As String = ""
                If lidays <= 0 Then
                    If lidays = 1 Then
                        lsDays = "day"
                    Else
                        lsDays = "days"
                    End If
                    Master.ShowMessage("Your password has expired. Please notify administrator.")
                ElseIf lidays <= 5 Then
                    If lidays = 1 Then
                        lsDays = "day"
                    Else
                        lsDays = "days"
                    End If
                    Master.ShowMessage("Your password will expire in " & CStr(lidays) & " " & lsDays & ". Please notify administrator.")
                End If
                If DocSession.sDocAccess > 2 Then
                    'lbUpload.Visible = True
                    ucAddDoc.Visible = True
                Else
                    'lbUpload.Visible = False
                    ucAddDoc.Visible = False
                End If
                'If DocSession.sUserGroup = "CRD" OrElse DocSession.sUserGroup = "CRV" OrElse DocSession.sUserGroup = "CRR" OrElse DocSession.sUserGroup = "ASC" OrElse DocSession.sUserLogin = "gsotelo" Then
                ucPending.Visible = True
                ucPending.RetrieveApproval()

                    'End If
                    CheckDriveSpace()
            Catch ex As Exception

            End Try
        End If

    End Sub
    Private Sub CheckDriveSpace()
        Try
            If DocSession.sUserRole = "AC" Then
                DocSession.sUserRole = "A"
                Dim lsLoc As String = System.Configuration.ConfigurationManager.AppSettings("fileloc")
                Dim liMinSpace As String = IIf(System.Configuration.ConfigurationManager.AppSettings("DriveSpaceWarning") = "", 512000000, CDbl(System.Configuration.ConfigurationManager.AppSettings("DriveSpaceWarning")))
                Dim ldriveInfo As DriveInfo = New DriveInfo(lsLoc)
                Dim lFreeSpace As Long = ldriveInfo.AvailableFreeSpace
                If lFreeSpace <= liMinSpace Then
                    Master.ShowMessage("The drive space for uploading documents is reaching the minimum limit.Please replace as soon as possible.")
                End If

            End If

        Catch ex As Exception

        End Try

    End Sub
    Private Sub AddDoc()
        ucUpload.ShowAdd()
        ucUpload.Visible = True
    End Sub

    Private Sub RefreshCount()
        ucDocCount.RetrieveDocs()
    End Sub

    

    'Private Sub lbUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbUpload.Click
    '    ucUpload.ShowAdd()
    '    ucUpload.Visible = True
    'End Sub
End Class