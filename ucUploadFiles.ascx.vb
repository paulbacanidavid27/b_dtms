Imports System
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls

Public Class ucUploadFiles
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            getFiles()
        End If

    End Sub

    Protected Shared arrFiles As New ArrayList()
    ' has to be static since Adding and then reusing 
    Protected isUploaded As Integer = 0
    Protected pathToUpload As String = DocSession.UploadLoc

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd.Click
        'Functionality to add the item in the list 
        'At very first add to the array list & simultanously display in the listbox 
        Try
            If Page.IsPostBack Then
                arrFiles.Add(fUpload)
                lstFiles.Items.Add(fUpload.PostedFile.FileName)
            End If
        Catch ex As Exception
            lblMessage.Text = "An error has occured while adding file" + ex.Message
        End Try
    End Sub
    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRemove.Click
        'Functionality to remove files 
        'Veryfirst you have to remove from the arraylist and similarly from the listbox 
        If lstFiles.Items.Count <> 0 Then
            arrFiles.Remove(fUpload)
            lstFiles.Items.Remove(lstFiles.SelectedItem.Text)
        End If
    End Sub
    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpload.Click
        'Very first check if the files are present to upload or Selected to upload 
        If (lstFiles.Items.Count = 0) AndAlso (isUploaded = 0) Then
            lblMessage.Text = "Please specify file name"
        Else
            'Take every element from the arraylist as HTMLInputFile, iterate through 
            'each InputFile and upload the files to the specified location 
            For Each Ipf As System.Web.UI.WebControls.FileUpload In arrFiles
                Try
                    Dim strFileName As String = System.IO.Path.GetFileName(Ipf.PostedFile.FileName)
                    Ipf.PostedFile.SaveAs(pathToUpload + "\" + strFileName)
                    isUploaded = isUploaded + 1
                Catch ex As Exception
                    lblMessage.Text = "An error has occured while uploading your files:<br>" + ex.Message
                End Try
            Next
            If isUploaded = arrFiles.Count Then
                lblMessage.Text = "Files uploaded successfully"
                getFiles()
            End If
            'Empty the arraylist and listbox once the upload process finishes 
            arrFiles.Clear()
            lstFiles.Items.Clear()

        End If
    End Sub

    Private Sub imgClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgClose.Click
        Me.Visible = False
    End Sub
    Public Sub getFiles()

        Dim dirInfo As New System.IO.DirectoryInfo(DocSession.UploadLoc)

        articleList.DataSource = dirInfo.GetFiles()
        articleList.DataBind()
        pFiles.Visible = True

    End Sub

    Private Sub imgHelp_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgHelp.Click
        pNote.Visible = Not pNote.Visible
        pmsg.Update()


    End Sub

    'Private Sub imgViewFiles_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgViewFiles.Click
    '    ShowPanels()
    'End Sub

    'Private Sub ShowPanels()
    '    pUpload.Visible = Not pUpload.Visible
    '    pFiles.Visible = Not pFiles.Visible
    '    imgViewFiles.Visible = Not imgViewFiles.Visible
    '    imgHelp.Visible = Not imgHelp.Visible
    '    imgUploadFiles.Visible = Not imgUploadFiles.Visible
    '    pnlFiles.Update()
    '    'pnlUpload.Update()
    'End Sub

    'Private Sub imgUploadFiles_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgUploadFiles.Click
    '    ShowPanels()

    'End Sub

    Private Sub imgRefresh_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgRefresh.Click
        getFiles()
    End Sub
End Class
