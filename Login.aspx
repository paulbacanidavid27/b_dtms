<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Login.Master" CodeBehind="Login.aspx.vb" Inherits="dms.Login1" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >
    <center>
    <div  class="mainDiv2">        
    
    <h2>
        Session Expired!
    </h2>
        

<table width="100%" border="0">
        <tr>
            <td align="left">              &nbsp;  
                </td>
        </tr>
        <tr>
            <td align="center">
               <asp:Image ID="Image1" runat="server" ImageUrl="images/idle.png" 
                    style="vertical-align:middle" Height="58px" Width="66px"></asp:Image> &nbsp;Your session has expired due to inactivity. Please login to continue.
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