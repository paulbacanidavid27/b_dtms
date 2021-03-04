<%@ Page Title="Forgot Password" Language="vb" MasterPageFile="~/Login.Master" AutoEventWireup="false"
    CodeBehind="forgot.aspx.vb" Inherits="dms.forgot" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<center>
<asp:UpdatePanel ID="pnl" runat="server" UpdateMode="Conditional">
<ContentTemplate>
    <fieldset style="border-left:solid 1px #CCCCCC;border-top:solid 1px #CCCCCC;border-right:solid 1px #CCCCCC;border-bottom:solid 1px #CCCCCC;background-color: #F0F0F1; border-top-color: #CCCCCC; border-right-color: #DFDFDF; border-left-color: #CCCCCC;box-shadow:1px 6px 2px -4px"><legend>Forgot Password</legend>
    
<table width="300px" border="0" >
        <tr>
         <td align="left" class="notes" colspan="2">
            Note: <br />To retrieve your password you should know your user id or email address. Click Send button after providing any of these informations and check your email to retrieve your password.
                &nbsp;<br /><br /></td>
        </tr>
        <tr>
            <td align="left" colspan="2">                
                Enter your User ID:</td>
        </tr>
        <tr>
            <td align="left" colspan="2">
                <asp:TextBox ID="tbUserID" runat="server" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">                
                &nbsp;</td>
        </tr>
        <tr>
            <td align="left" colspan="2">                
                Enter your registered email address:</td>
        </tr>
        <tr>
            <td align="left" colspan="2">
                <asp:TextBox ID="tbEmail" runat="server" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">                
                &nbsp;</td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Label ID="msg" runat="server" cssclass="msg_green" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
           
            <td align="center" colspan="2">
                <asp:Button ID="Button1" runat="server" Text="Go back to Login page" 
                    ToolTip="Go back to Login page." Visible="true"  cssclass="btn2"/>
                <asp:Button ID="btLogin" runat="server" Text="Send" cssclass="btn" Visible="true"/>
                <asp:Button ID="Button2" runat="server" cssclass="btn" Text="Login" Visible="false"/>
                <asp:UpdateProgress ID="prgRegister" runat="server" DynamicLayout="false">
                <ProgressTemplate>
                    <asp:Image ID="Image2" runat="server" Height="20px" src="images/emailloading.gif" 
                        Width="30px" />
                </ProgressTemplate>
            </asp:UpdateProgress>
            </td>
        </tr>
    </table>
    
</fieldset>
</ContentTemplate>
</asp:UpdatePanel>
</center>
</asp:Content>
