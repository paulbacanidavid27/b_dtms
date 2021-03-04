<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Login.Master" CodeBehind="Logout.aspx.vb" Inherits="dms.Logout" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >
    <center>
    <div  class="mainDiv2" style="text-align:left; padding-left:10px;">        
    
    <h1>
        <b>Opening DMS Web Site in multiple instance with the same browser is not allowed. This is to avoid data integrity issue.
    </b></h1>
        

<table width="100%" border="0">
        <tr>
            <td align="left">              &nbsp;  
                </td>
        </tr>
        <tr>
            <td align="left">
               
                     In order to open multiple session of DMS web site, you need to open two diffent browsers. Or, if you need the same browser like Chrome, you need to open one of the browser in Incognito mode. For IE, you can use the New Session feature when opening another instance of the browser.
            </td>
        </tr>
        <tr>
        <td><asp:Image ID="Image1" runat="server" ImageUrl="images/browser3.png" style="vertical-align:middle" Height="68px"></asp:Image>     </td></tr>
        <tr>
            <td class="style1">          &nbsp;      
                </td>
        </tr>
       
        <tr>
            <td align="center"> 
                <input type="button"  class="btn2"  value="Go Back to Login Page" onclick="window.location.replace('default.aspx')" style="cursor: pointer"/>
                <asp:Button ID="btLogin" runat="server" cssclass="btn" Text="Go Back to Login Page" Visible="false" />
                
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