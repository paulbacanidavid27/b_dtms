Public Class UserControlProfile
    Inherits System.Web.UI.UserControl

    Public Event e_click()


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'RetrieveUserProfile()
    End Sub

    Public Sub RetrieveUserProfile(Optional ByVal aUserId As String = "")
        Dim lodata As DataTable
        Try
            Dim odoc As New DocUser
            If aUserId <> "" Then
                odoc.pUserId = aUserId
            Else
                odoc.pUserId = DocSession.sUserId
            End If

            lodata = odoc.GetUserProfile()


            tbUserID.Text = lodata(0)("UserLogin")
            lUserId.Text = lodata(0)("UserId")
            tbFirstName.Text = lodata(0)("FirstName")
            tbLastName.Text = lodata(0)("LastName")
            tbMiddleName.Text = lodata(0)("MiddleName")
            tbEmail.Text = lodata(0)("Email")
            tbRole.Text = lodata(0)("uRole")
            tbTitle.Text = lodata(0)("Title")
            tbGroup.Text = lodata(0)("GroupName")
            tbOffice.Text = lodata(0)("Office")
            lCanChangePass.Text = lodata(0)("CanChangePass")
            lEmailNotification.Text = lodata(0)("EmailNotificationOff")
            'cbEmail.BoxCheck = IIf(lodata(0)("EmailNotificationOff") = "Yes", True, False)
            If lodata(0)("CanChangePass") = "No" Then
                imgSelect.Visible = False
                lPassWord.Visible = False
            Else
                If DocSession.sFirstTimeUser = "True" Then
                    msg.CssClass = "msg_green"
                    msg.Text = "For first time user, you are required to change your password."
                    'imgClose.Visible = False
                    imgSelect.Visible = False
                    ImgSelected.Visible = True
                    pnlChangePassword.Visible = True
                    pnlChangePassword.Update()
                End If
            End If
            If System.IO.File.Exists(Server.MapPath("") & "\images\avatar\" & lodata(0)("profilepic")) Then
                imgPic.ImageUrl = "images/avatar/" & lodata(0)("profilepic")

            Else
                imgPic.ImageUrl = "images/avatar/default.png"
            End If
        Catch ex As Exception
        Finally
            If Not lodata Is Nothing Then
                lodata.Dispose()
                lodata = Nothing
            End If
        End Try



    End Sub

    Protected Sub imgClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgClose.Click
        RaiseEvent e_click()
    End Sub
    Private Function ValidUserInfo() As Boolean

        If tbFirstName.Text.Trim = "" Then
            msg.CssClass = "msg_red"
            msg.Text = "First Name is a required field."
            pnlMsg.Update()
            Return False
        End If
        If tbLastName.Text.Trim = "" Then
            msg.CssClass = "msg_red"
            msg.Text = "Last Name is a required field."

            pnlMsg.Update()
            Return False
        End If
        If tbEmail.Text.Trim = "" Then
            msg.CssClass = "msg_red"
            msg.Text = "Email is a required field."

            pnlMsg.Update()
            Return False
        End If
        If ImgSelected.Visible Then
            If tbNewPassword.Text.Trim.Length < 8 Then
                msg.CssClass = "msg_red"
                msg.Text = "Password should contain 8 or more characters."
                pnlMsg.Update()
                Return False
            End If
            Dim oDoc As New DocUser
            Dim ec As New crypt
            Dim blnExistsPwd As Boolean = False, strPwdHist As String = "0"
            oDoc.pUserId = DocSession.sUserId
            oDoc.pPassword = ec.Encrypt(tbConfirmPassword.Text)
            strPwdHist = System.Configuration.ConfigurationManager.AppSettings("PasswordHistory")

            If Not strPwdHist Is Nothing Then
                If strPwdHist.Trim() <> "" AndAlso strPwdHist.Trim() <> "0" Then
                    blnExistsPwd = oDoc.ExistsInPwdHist()
                End If
            End If


            If blnExistsPwd Then
                msg.CssClass = "msg_red"
                If strPwdHist = "1" Then
                    msg.Text = "You cannot reuse the last password you used. Please enter different password."
                Else
                    msg.Text = "You cannot reuse the last " & strPwdHist & " passwords you used. Please enter different password."
                End If

                Return False
            End If

            If PasswordNotComplex() Then
                msg.CssClass = "msg_red"
                msg.Text = "Password should contain combination of atleast 1 upper case letter, a small case letter and a number."
                Return False
            End If
        End If
        If uploadPic.HasFile Then
            Dim linfo As New System.IO.FileInfo(uploadPic.PostedFile.FileName)
            If linfo.Extension.ToLower() = ".gif" OrElse linfo.Extension.ToLower() = ".tif" OrElse linfo.Extension.ToLower() = ".tiff" OrElse linfo.Extension.ToLower() = ".png" OrElse linfo.Extension.ToLower() = ".jpg" OrElse linfo.Extension.ToLower() = ".jpeg" Then
            Else
                msg.CssClass = "msg_red"
                msg.Text = "** Invalid file. Only the .gif, .png, .jpeg, .jpg, .tiff,.tif files are accepted."
                pnlMsg.Update()
                Return False
            End If
        End If
        Return True
    End Function

    Protected Sub btSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSave.Click
        Try
            'If (ImgSelected.Visible OrElse uploadPic.HasFile OrElse cbEmail.BoxCheck Or cbEmail.BoxCheck = False) Then
            If (ImgSelected.Visible OrElse uploadPic.HasFile) Then


                If ValidUserInfo() Then


                    Dim oDoc As New DocUser
                    oDoc.pUserId = lUserId.Text 'tbUserID.Text
                    'If cbEmail.BoxCheck Then
                    '    oDoc.pTurnOffEmailNotification = "1"
                    'Else
                    '    oDoc.pTurnOffEmailNotification = "0"
                    'End If
                    If ImgSelected.Visible Then
                        Using ldata As DataTable = oDoc.RetrieveUserInfo
                            Dim ec As New crypt
                            If ec.Decrypt(ldata(0)("password")) = tbOldPassword.Text AndAlso tbConfirmPassword.Text = tbNewPassword.Text Then
                            Else
                                msg.CssClass = "msg_red"
                                msg.Text = "Password doesn't match."
                                Exit Sub
                            End If

                            Dim lsPass = ec.Encrypt(tbConfirmPassword.Text)
                            oDoc.pPassword = lsPass
                            oDoc.pCanChangePassword = "1"
                        End Using

                        If DocSession.sFirstTimeUser = "True" And (tbOldPassword.Text <> tbNewPassword.Text) Then
                            oDoc.pFirstTimeuser = "0"
                            'imgClose.Visible = True
                        End If

                    End If
                    Using ldt As DataTable = oDoc.RetrieveOrigUserInfo()
                        Dim oCrypt As New crypt
                        'setup old value for admin data changes
                        oDoc.pFirstName_o = ldt(0)("FirstName")
                        oDoc.pLastName_o = ldt(0)("LastName")
                        oDoc.pGroup_o = ldt(0)("UserGroup")
                        oDoc.pRole_o = ldt(0)("userRole")
                        oDoc.pExpirationDate_o = ldt(0)("PassExpirationDate")
                        oDoc.pCanChangePassword_o = IIf(ldt(0)("CanChangePass") = "True", "1", "0")
                        oDoc.pTitle_o = ldt(0)("Title")
                        oDoc.pLockAttempt_o = ldt(0)("LockOutAttempts")
                        oDoc.pLocked_o = IIf(ldt(0)("Locked") = "True", "1", "0")
                        oDoc.pUserEmail_o = ldt(0)("Email")

                        oDoc.pPassword_o = oCrypt.Encrypt(ldt(0)("Password"))
                        oDoc.pProfilePic_o = IIf(IsDBNull(ldt(0)("ProfilePic")), "", ldt(0)("ProfilePic"))
                    End Using

                    oDoc.pFirstName = tbFirstName.Text
                    oDoc.pLastName = tbLastName.Text
                    oDoc.pMiddleName = tbMiddleName.Text

                    oDoc.pTitle = tbTitle.Text
                    oDoc.pUserEmail = tbEmail.Text
                    oDoc.pLoginUserId = DocSession.sUserId
                    oDoc.pIPAddress = Request.UserHostAddress
                    'oDoc.pProfilePic = ""
                    'oDoc.pTurnOffEmailNotification = IIf(cbEmail.BoxCheck, "1", "0")

                    If uploadPic.HasFile Then
                        Dim linfo As New System.IO.FileInfo(uploadPic.PostedFile.FileName)
                        'Dim lsPic As String = Server.MapPath("") & "\images\avatar\" & tbUserID.Text.Trim + linfo.Extension.ToLower()

                        Dim lsFN As String = tbUserID.Text.Trim + "_" + linfo.Name
                        Dim lsPic As String = Server.MapPath("") & "\images\avatar\" & lsFN

                        oDoc.pProfilePic = lsFN

                        uploadPic.SaveAs(lsPic)
                        imgPic.ImageUrl = "images/avatar/" & lsFN

                        DirectCast(Parent.FindControl("imgProfile"), Image).ImageUrl = "images/avatar/" & lsFN

                        DocSession.sprofilePic = lsFN
                        pnlpic.Update()
                    End If


                    oDoc.UpdateUser()
                    oDoc.AddPasswordTrack()
                    DocSession.sFirstTimeUser = "False"
                    DocSession.sUserName = tbFirstName.Text.Trim & " " & tbLastName.Text.Trim
                    DirectCast(Parent.FindControl("lUserInfo"), Literal).Text = tbFirstName.Text.Trim & " " & tbLastName.Text.Trim
                    'DirectCast(Parent.FindControl("pPic"), UpdatePanel).Update()
                    msg.CssClass = "msg_green"
                    msg.Text = "Profile has been updated successfully."
                    pnlMsg.Update()
                End If


            End If
        Catch ex As Exception
            msg.CssClass = "msg_red"
            msg.Text = "Error occurred while updating user information (" & ex.Message & "). Please try again."

        End Try
        pnlMsg.Update()
    End Sub

    Private Sub imgSelect_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSelect.Click
        imgSelect.Visible = Not imgSelect.Visible
        ImgSelected.Visible = Not ImgSelected.Visible
        pnlChangePassword.Visible = Not pnlChangePassword.Visible
        pnlChangePassword.Update()

    End Sub

    Private Sub ImgSelected_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgSelected.Click
        imgSelect.Visible = Not imgSelect.Visible
        ImgSelected.Visible = Not ImgSelected.Visible
        pnlChangePassword.Visible = Not pnlChangePassword.Visible
        pnlChangePassword.Update()
    End Sub

#Region "Complex Password"
    Private Function PasswordNotComplex() As Boolean

        'If DocSession.sComplexPassword = "True" Then
        If Regex.IsMatch(tbNewPassword.Text.Trim, "[A-Z]") AndAlso Regex.IsMatch(tbNewPassword.Text.Trim, "[a-z]") AndAlso Regex.IsMatch(tbNewPassword.Text.Trim, "\d") Then
            Return False
        Else
            Return True
        End If
        'End If
        Return False
    End Function
#End Region
End Class