<%@ Page Title="Login" Language="vb" MasterPageFile="~/Login.Master" AutoEventWireup="false"
    CodeBehind="Default.aspx.vb" Inherits="dms._Default" %>
    <%@ MasterType VirtualPath="~/Login.Master" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >

                        
           
    
    
        
    <center>
    <asp:Panel DefaultButton="btLogin" ID="pnllogin" runat="server">
        <asp:UpdatePanel runat="server" ID="pnlMsg" UpdateMode="Conditional" >
            <ContentTemplate>
            <asp:HiddenField ID="hfCtr" value="0" runat="server" />
            <table>
            <tr>
            <td align="left" valign="middle" style="padding-top:20px;padding-bottom:20px">
                <asp:Label ID="msg" runat="server" cssclass="msg_red" Text=""></asp:Label><br />
                <br />
                <asp:HyperLink ID="hlCookies" runat="server" style="color:Blue" visible="false" NavigateUrl="http://www.wikihow.com/Enable-Cookies-in-Your-Internet-Web-Browser">You can check this link on how to enable cookies in your Browser.</asp:HyperLink>
                
            </td>
        </tr>
        </table>
<table border="0" style="border-left:solid 1px #CCCCCC;border-top:solid 1px #CCCCCC;border-right:solid 1px #CCCCCC;border-bottom:solid 1px #CCCCCC;background-color: #F0F0F1; border-top-color: #CCCCCC; border-right-color: #DFDFDF; border-left-color: #CCCCCC;box-shadow:1px 6px 2px -4px">
        <tr>
            <td align="left" style="padding-left:10px;padding-right:10px;color:#222222;font-weight:bold;">                
                
                <img alt="" src="images/userid.png" />
                Username</td>
        </tr>
        <tr>
            <td align="center" style="padding-left:10px;padding-right:10px;">                
                <asp:TextBox ID="tbUserID" runat="server" Width="300px" cssclass="entryfldlogin" AutoComplete="on"></asp:TextBox> 
            </td>
        </tr>
       
        <tr>
            <td align="left" style="padding-left:10px;padding-right:10px;color:#222222;font-weight:bold;">                
                
                <img alt="" src="images/password.png" />
                Password</td>
        </tr>
        <tr>
            <td align="center" style="padding-left:10px;padding-right:10px;">                
                <asp:TextBox ID="tbPassword" runat="server" Width="300px"  cssclass="entryfldlogin" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" style="padding-left:10px;padding-right:10px;">                
                <asp:Button ID="btLogin" runat="server" Text="Login" cssclass="btn"/></td>
        </tr>
        <tr>
        <td align="right" style="padding-left:10px;padding-right:10px;"><asp:HyperLink ID="hlPassword" runat="server" NavigateUrl="~/forgot.aspx" style="color:#000066">Forgot 
                Password?</asp:HyperLink></td>
        </tr>
        
        
        
        </table>
        



</ContentTemplate>
            </asp:UpdatePanel>
</asp:Panel>
            </center>
            </asp:Content>