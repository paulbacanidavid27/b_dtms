<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Login.Master" CodeBehind="AccessDenied.aspx.vb" Inherits="dms.AccessDenied" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >
    <center>
    <div  class="mainDiv2">        
    
    <asp:Image ID="Image1" runat="server" ImageUrl="images/accessdenied.png" 
                    style="vertical-align:middle" Height="58px" Width="66px"></asp:Image>    

<table width="100%" border="0">
        <tr>
            <td align="left">              &nbsp;  
                </td>
        </tr>
        <tr>
            <td align="center">
               You don't have permission to the page you are trying to access. Please login to continue.
            </td>
        </tr>
        <tr>
            <td class="style1">          &nbsp;      
                </td>
        </tr>
       
        <tr>
            <td align="center"> 
                <input type="button"  class="btn"  value="Login" onclick="window.location.replace('default.aspx')" style="cursor: pointer"/>
                <asp:Button ID="btLogin" runat="server" cssclass="btn" Text="Login" Visible="false" />
                
            </td>
        </tr>
        <tr>
            <td align="left">
                &nbsp;</td>
        </tr>
        
        
        </table>
     


</div>
</center>
</asp:Content>