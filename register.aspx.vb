Public Class register
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then




            Try
                If HiddenField1.Value = "going2live" Then
                    lHome.Text = "Registration - Live Version"
                    tbldemo.Style("display") = "none"
                    tblupgrade.Style("display") = "none"
                    tbllive.Style("display") = "inline"
                ElseIf HiddenField1.Value = "upgrade2day" Then
                    lHome.Text = "Registration - Upgrade Version"
                    tbldemo.Style("display") = "none"
                    tbllive.Style("display") = "none"
                    tblupgrade.Style("display") = "inline"
                    pnlup.Visible = True
                    DisableFields()
                Else
                    lHome.Text = "Registration - Demo Version"
                    tbldemo.Style("display") = "inline"
                    tbllive.Style("display") = "none"
                    tblupgrade.Style("display") = "none"
                End If
                RetrieveRegistration()
            Catch ex As Exception
                Throw New Exception("Registration error. Please contact the software administrator")
            End Try
        End If

    End Sub
    Private Sub RetrieveRegistration()
        Dim objCommand As clsSqlConn
        Dim ldata As DataTable
        Dim ec As New crypt
        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = "SELECT col1,col2,col3,col4,col5,col6,col7,col8,col9 FROM dbDocuments"
            ldata = objCommand.Fill()
            If ldata.Rows.Count > 1 Then
                PanelRegistrationForge.Visible = True
            ElseIf ldata.Rows.Count = 1 Then

                If ldata(0)("col9").ToString.Trim <> "" Then
                    PanelRegistrationForge.Visible = True
                    laltered.Text = ldata(0)("col9").ToString.Trim
                Else
                    Dim lsreg As String = ec.Decrypt(ldata(0)("col7").ToString.Trim)
                    DocSession.sRegKey = ec.Decrypt(ldata(0)("col1").ToString)
                    DocSession.sRegType = lsreg.Split("^")(1)
                    DocSession.sRegDemo = lsreg.Split("^")(0)

                    If (lsreg.Split("^")(0) = "Demo" AndAlso HiddenField1.Value = "going2live") OrElse HiddenField1.Value = "upgrade2day" Then
                        PanelRegistration.Visible = True
                        txEmail.Text = ec.Decrypt(ldata(0)("col4").ToString)
                        txFN.Text = ec.Decrypt(ldata(0)("col3").ToString)
                        txLN.Text = ec.Decrypt(ldata(0)("col5").ToString)
                        
                    Else
                        PanelAlreadyRegisted.Visible = True
                    End If
                End If

            Else
                PanelRegistration.Visible = True
            End If
        Catch ex As Exception
        Finally

        End Try
    End Sub

    Private Sub lbDemo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbDemo.Click
        SaveReg("Demo")
    End Sub
    Private Sub lbRegister_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbRegister.Click
        SaveReg("Live")
    End Sub
    Private Sub SaveReg(ByVal asDemo)
        Dim objCommand As clsSqlConn
        Dim ltr As DbTran
        Dim sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sReg As String
        Try
            If txFN.Text.Trim = "" Then
                lbMsg.Text = "First Name is a required field."
                txFN.Focus()
                Exit Sub
            ElseIf txLN.Text.Trim = "" Then
                lbMsg.Text = "Last Name is a required field."
                txLN.Focus()
                Exit Sub

            ElseIf txEmail.Text.Trim = "" Then
                lbMsg.Text = "Email is a required field."
                txEmail.Focus()
                Exit Sub
            ElseIf Not cbTerms.Checked Then
                lbMsg.Text = "Please read and agree with the terms and conditions."
                Exit Sub
            End If

            If rbBasic.Checked Then
                sReg = "bsx"
                lRegType.Text = "Basic"
            ElseIf rbStandard.Checked Then
                sReg = "std"
                lRegType.Text = "Standard"
            ElseIf rbEnterprise.Checked Then
                sReg = "etp"
                lRegType.Text = "Enterprise"
            Else
                lbMsg.Text = "Please select registration type before submitting."
                Exit Sub
            End If
            Dim ocr As New crypt
            Dim ag As Guid
            Dim lsPass As String
            ag = Guid.NewGuid()
            lsPass = txLN.Text.Trim & Left(ag.ToString, 4)
            sCol1 = ocr.Encrypt(Left(ag.ToString, 20))
            sCol2 = ocr.Encrypt(Request.ServerVariables("Remote_addr"))
            sCol3 = ocr.Encrypt(txFN.Text.Trim)
            sCol4 = ocr.Encrypt(txEmail.Text.Trim)
            sCol5 = ocr.Encrypt(txLN.Text.Trim)
            sCol6 = ocr.Encrypt(Date.Now.ToShortDateString & "^" & Date.Now.ToShortTimeString)

            sCol7 = ocr.Encrypt(asDemo & "^" & sReg)
            sCol8 = ""
            sCol9 = ""
            ltr = New DbTran
            objCommand = New clsSqlConn(ltr.pTran)

            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = "DELETE FROM users "
            objCommand.ExecTranNonQuery()
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = "DELETE FROM groups "
            objCommand.ExecTranNonQuery()

            objCommand.CommandType = CommandType.Text
            
            objCommand.CommandText = "INSERT INTO DbDocuments " & _
                                           "(Col1 " & _
                                           ",Col2 " & _
                                           ",Col3 " & _
                                           ",Col4 " & _
                                           ",Col5 " & _
                                           ",Col6 " & _
                                           ",Col7 " & _
                                           ",Col8 " & _
                                           ",Col9) " & _
                                     "VALUES " & _
                                           "(" & _
                                           "'" & sCol1 & "'" & _
                                           ",'" & sCol2 & "'" & _
                                           ",'" & sCol3 & "'" & _
                                           ",'" & sCol4 & "'" & _
                                           ",'" & sCol5 & "'" & _
                                           ",'" & sCol6 & "'" & _
                                           ",'" & sCol7 & "'" & _
                                           ",'" & sCol8 & "'" & _
                                           ",'" & sCol9 & "'" & _
                                           ")"

            objCommand.ExecTranNonQuery()

            Dim oUser As New DocUser
            oUser.pUserId = Left(txFN.Text.Trim, 1) & txLN.Text.Trim
            oUser.pFirstName = txFN.Text.Trim
            oUser.pLastName = txLN.Text.Trim
            oUser.pIPAddress = Request.UserHostAddress
            oUser.pLockAttempt = "3"
            oUser.pLoginUserId = oUser.pUserId
            oUser.pTitle = "Administrator"
            oUser.pUserEmail = txEmail.Text
            oUser.pCanChangePassword = "1"
            oUser.pExpirationDate = DateAdd(DateInterval.Month, 1, Date.Now).ToShortDateString
            oUser.pPassword = lsPass
            oUser.pRole = "A"
            oUser.pGroup = "Adm"
            oUser.AddUser(objCommand)

            Dim ogrp As New DocGroup
            'If Not ogrp.CheckIfGroupCodeExists("Admin") Then
            ogrp.pGroupID = "Adm"
            ogrp.pDesc = "Administrator"
            ogrp.pIPAddress = Request.UserHostAddress
            ogrp.pReportAccess = "0"
            ogrp.pUserId = oUser.pUserId
            ogrp.AddGroup(objCommand)
            'End If

            PanelSuccessful.Visible = True
            PanelRegistration.Visible = False
            ltr.pTran.Commit()
            lRegKey.Text = Left(ag.ToString, 20)

            lAdminUser.Text = Left(txFN.Text.Trim, 1) & txLN.Text.Trim
            lPassword.Text = lsPass
        Catch ex As Exception
            lmsg.Text = ex.Message
            PanelRegistrationError.Visible = True
            PanelRegistration.Visible = False
            ltr.pTran.Rollback()
        Finally

            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If

        End Try
    End Sub
    Private Sub DisableFields()
        txFN.Enabled = False
        txFN.CssClass = "entryflddisabled"
        txLN.Enabled = False
        txLN.CssClass = "entryflddisabled"
        txEmail.Enabled = False
        txEmail.CssClass = "entryflddisabled"
    End Sub
    
    Private Sub lbAgain_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAgain.Click
        Response.Redirect("register.aspx")
    End Sub

    Private Sub rbBasic_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbBasic.CheckedChanged
        tblBasic.Style("background-color") = "#0099CC"
        tblBasic.Style("border") = "solid 1px #000066"
        tblBasic.Style("color") = "#EEEEEE"
        tblStandard.Style("background-color") = ""
        tblStandard.Style("border") = ""
        tblStandard.Style("color") = ""
        tblEnt.Style("background-color") = ""
        tblEnt.Style("border") = ""
        tblEnt.Style("color") = ""
        upReg.Update()

    End Sub

    Private Sub rbEnterprise_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbEnterprise.CheckedChanged
        tblBasic.Style("background-color") = ""
        tblBasic.Style("border") = ""
        tblBasic.Style("color") = ""
        tblStandard.Style("background-color") = ""
        tblStandard.Style("border") = ""
        tblStandard.Style("color") = ""
        tblEnt.Style("background-color") = "#0099CC"
        tblEnt.Style("border") = "solid 1px #000066"
        tblEnt.Style("color") = "#EEEEEE"
        upReg.Update()
    End Sub

    Private Sub rbStandard_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbStandard.CheckedChanged
        tblBasic.Style("background-color") = ""
        tblBasic.Style("border") = ""
        tblBasic.Style("color") = ""
        tblStandard.Style("background-color") = "#0099CC"
        tblStandard.Style("border") = "solid 1px #000066"
        tblStandard.Style("color") = "#EEEEEE"
        tblEnt.Style("background-color") = ""
        tblEnt.Style("border") = ""
        tblEnt.Style("color") = ""
        upReg.Update()
    End Sub

    Private Sub lbUpgrade_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbUpgrade.Click
        Dim objCommand As clsSqlConn

        Dim sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sReg As String
        Try
            
            If txReg.Text <> DocSession.sRegKey Then
                lbMsg.Text = "Registration key doesn't match. Please enter correct registration key to proceed."
                Exit Sub
            End If

            If Not cbTerms.Checked Then
                lbMsg.Text = "Please read and agree with the terms and conditions."
                Exit Sub
            End If

            If rbBasic.Checked Then
                sReg = "bsx"
                lregtype2.Text = "Basic"
            ElseIf rbStandard.Checked Then
                sReg = "std"
                lregtype2.Text = "Standard"
            ElseIf rbEnterprise.Checked Then
                sReg = "etp"
                lregtype2.Text = "Enterprise"
            Else
                lbMsg.Text = "Please select registration type before clicking the upgrade button."
                Exit Sub
            End If

            If sReg = DocSession.sRegType Then
                lbMsg.Text = "Please select another registration type to upgrade your registration."
                Exit Sub
            End If
            Dim ocr As New crypt
            Dim ag As Guid
            Dim lsPass As String
            Dim sVer As String = DocSession.sRegDemo
            ag = Guid.NewGuid()

            sCol1 = ocr.Encrypt(Left(ag.ToString, 20))
            sCol7 = ocr.Encrypt(sVer & "^" & sReg)
            sCol8 = ""
            sCol9 = ""

            objCommand = New clsSqlConn()

            

            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = "UPDATE DbDocuments SET col1='" & sCol1 & "', col7='" & sCol7 & "',col8='',col9=''"
            

            objCommand.ExecNonQuery()


            PanelUpgrade.Visible = True

            PanelRegistration.Visible = False

            lregkey2.Text = Left(ag.ToString, 20)

        Catch ex As Exception
            lmsg.Text = ex.Message
            PanelRegistrationError.Visible = True
            PanelRegistration.Visible = False

        Finally

            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If

        End Try
    End Sub
End Class