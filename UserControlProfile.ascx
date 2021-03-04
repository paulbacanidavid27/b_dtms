<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlProfile.ascx.vb" Inherits="dms.UserControlProfile" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register src="UserControlCheckBox.ascx" tagname="UserControlCheckBox" tagprefix="uc1" %>
<asp:Panel id="pAddUser" runat="server" Visible="True" style="width:700px" defaultbutton="btSave">
    <!-- start - search criteria //-->
    
    
    
        
    <table border="0" cellpadding="0" cellspacing="0" class="popuphdrbox" 
        style="border: solid 1px #3A5671;border-collapse:collapse;width:100%;">
        <tr>
            <td align="center">
                <table border="0" cellpadding="0" cellspacing="0" class="popuphdr" 
                    style="width:100%">
                    <tr height="30px">
                        <td align="left" colspan="2" valign="middle">
                            &nbsp;<img height="30px" width="23px" src="images/user2.png" />&nbsp;User Profile</td>
                        <td align="right" valign="top">
                            <asp:ImageButton ID="imgClose" runat="server" Height="18px" 
                                imageurl="images/close_window.gif" 
                                onmouseout="this.src='images/close_window.gif'" 
                                onmouseover="this.src='images/close_window.gif'" width="18px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="padding-left:15px">
                <table cellpadding="2px" cellspacing="0" style="border-collapse:collapse">
                    <tr>
                        <td align="left" class="notes" colspan="3">
                            &nbsp;</td>
                        <td align="left" class="notes">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="left" class="notes" colspan="3">
                            * - Required Field</td>
                        <td align="left" class="newtblheader">
                            Profile Picture</td>
                    </tr>
                    <tr>
                        <td align="left" class="labelFreeForm">
                            * User ID:</td>
                        <td align="left">
                            <asp:TextBox ID="tbUserID" runat="server" CssClass="entryflddisabled" 
                                ReadOnly="True"></asp:TextBox>
                            <asp:Literal ID="lUserId" runat="server" Visible="false"></asp:Literal>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td rowspan="14" valign="top">
                            <table style="width:100%;">
                                <tr>
                                    <td style="border:solid 1px #CCCCCC">
                                        <asp:UpdatePanel ID="pnlpic" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:Image ID="imgPic" runat="server" Height="193px" ImageAlign="Middle" 
                                                    Width="136px" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="labelFreeForm">
                                        Upload Picture:</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:FileUpload ID="uploadPic" runat="server" CssClass="entryfld2" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="labelFreeForm" nowrap>
                            * First Name:</td>
                        <td align="left">
                            <asp:TextBox ID="tbFirstName" runat="server" CssClass="entryflddisabled" 
                                ReadOnly="True" Width="300px"></asp:TextBox>
                        </td>
                        <td align="left">
                            &nbsp;</td>
                    </tr>
                    <%--<tr>
                <td align="left" class="labelFreeForm">* Middle Name:</td>        
                <td align="left">
                    
                </td>
                <td>&nbsp;</td>                
            </tr>--%>
                    <tr>
                        <td align="left" class="labelFreeForm" nowrap>
                            * Last Name:</td>
                        <td align="left">
                            <asp:TextBox ID="tbMiddleName" runat="server" CssClass="entryflddisabled" 
                                ReadOnly="True" Visible="false" Width="300px"></asp:TextBox>
                            <asp:TextBox ID="tbLastName" runat="server" CssClass="entryflddisabled" 
                                ReadOnly="True" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="left" class="labelFreeForm">
                            &nbsp;&nbsp;Title:</td>
                        <td align="left">
                            <asp:TextBox ID="tbTitle" runat="server" CssClass="entryflddisabled" 
                                Height="22px" ReadOnly="True" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="left" class="labelFreeForm">
                            * Email:</td>
                        <td align="left">
                            <asp:TextBox ID="tbEmail" runat="server" CssClass="entryflddisabled" 
                                ReadOnly="True" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="left" class="labelFreeForm">
                            * Role:</td>
                        <td align="left">
                            <asp:TextBox ID="tbRole" runat="server" CssClass="entryflddisabled" 
                                ReadOnly="True" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="left" class="labelFreeForm">
                            * Group:</td>
                        <td align="left">
                            <asp:TextBox ID="tbGroup" runat="server" CssClass="entryflddisabled" 
                                ReadOnly="True" Width="300px"></asp:TextBox>
                            <asp:Literal ID="lCanChangePass" runat="server" Visible="false"></asp:Literal>
                            <asp:Literal ID="lEmailNotification" runat="server" Visible="false"></asp:Literal>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="left" class="labelFreeForm">
                            * Office:</td>
                        <td align="left">
                            <asp:TextBox ID="tbOffice" runat="server" CssClass="entryflddisabled" 
                                ReadOnly="True" Width="300px"></asp:TextBox>
                            
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <%--<tr>
                        <td align="left" class="labelFreeForm" colspan="3" valign="top">
                            <uc1:UserControlCheckBox ID="cbEmail" runat="server" />
                            <asp:Literal ID="Literal1" runat="server">Turn-off Email Notification</asp:Literal>
                        </td>
                    </tr>--%>
                    <tr>
                        <td align="left" class="labelFreeForm" colspan="3" valign="top">
                            <asp:ImageButton ID="imgSelect" runat="server" ImageUrl="images/box.png" 
                                style="vertical-align:middle" visible="true" />
                            <asp:ImageButton ID="ImgSelected" runat="server" ImageUrl="images/checkbox.png" 
                                visible="false" />
                            <asp:Literal ID="lPassWord" runat="server">Change Password</asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:UpdatePanel ID="pnlChangePassword" runat="server" UpdateMode="Conditional" 
                                Visible="false">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td align="left" class="notes" colspan="3">
                                                Note: Password should be atleast 8 characters.</td>
                                        </tr>
                                        <tr>
                                            <td align="left" class="labelFreeForm" nowrap>
                                                * Old Password:</td>
                                            <td align="left" class="style4">
                                                <asp:TextBox ID="tbOldPassword" runat="server" CssClass="entryfldw" 
                                                    TextMode="Password" Width="200px"></asp:TextBox>
                                            </td>
                                            <td class="notes" rowspan="4" style="width:100px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" class="labelFreeForm" nowrap>
                                                * New Password:</td>
                                            <td align="left" class="style4">
                                                <asp:TextBox ID="tbNewPassword" runat="server" CssClass="entryfldw" 
                                                    TextMode="Password" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" class="labelFreeForm" nowrap>
                                                * Confirm Password:</td>
                                            <td align="left" class="style4">
                                                <asp:TextBox ID="tbConfirmPassword" runat="server" CssClass="entryfldw" 
                                                    TextMode="Password" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                &nbsp;</td>
                                            <td align="left" class="style4">
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="3">
                            <asp:Button ID="btSave" runat="server" CssClass="btnsmall" Text="Save" />
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="left" colspan="3">
                            <asp:UpdatePanel ID="pnlMsg" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Label ID="msg" runat="server" CssClass="msg_red">&nbsp;</asp:Label>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td align="left">
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
                
                
    </asp:Panel>
    <cc1:DropShadowExtender ID="dse2" runat="server" TargetControlID="pAddUser" Opacity=".5" Rounded="false" TrackPosition="False" />