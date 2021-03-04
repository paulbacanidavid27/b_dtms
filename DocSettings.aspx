<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="DocSettings.aspx.vb" Inherits="dms.DocSettings" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register src="UserControlAdminMenuH.ascx" tagname="UserControlAdminMenuH" tagprefix="uc" %>
<%--<%@ Register src="UserControlOffice.ascx" tagname="ucOffice" tagprefix="uc" %>--%>
<%@ Register src="UserControlAddress.ascx" tagname="ucAddress" tagprefix="uc" %>
<%@ Register src="UserControlMainGroup.ascx" tagname="ucMainGroup" tagprefix="uc" %>


<%--menu content start--%>
<asp:Content ID="Content12" runat="server" ContentPlaceHolderID="AdminMenu">
    <uc:UserControlAdminMenuH id="ucMenu" runat="server"></uc:UserControlAdminMenuH>                                       
</asp:Content>
<%--main content end--%>

<%--main headr content start--%>

<%--main headr content end--%>
<asp:Content ID="Content7" runat="server" ContentPlaceHolderID="HeaderMenuContent">
    <table cellpadding="0" cellspacing="0" border="0" width="100%" class="tableheaderGreen">
                   <tr >
                        <td valign="middle">
                        </td>
                        </tr>
                        </table>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="AddContent" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0" class="tableheaderGreen">
        <tr><td></td></tr></table>
</asp:Content>
<%--main headr content end--%>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">    
    <title>System Settings</title>
</asp:Content>
<%--main headr content start--%>
<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="MainHeaderContent">
   
           <table border="0" width="100%" class="tableheaderGreen">
        <tr>
            <td class="tableheader_1"><img  alt="" src="images/advancedsearch_h.png" height="20px" width="20px" style="vertical-align:middle" />&nbsp;System Settings</td>
            
        </tr>        
    </table>    
    
</asp:Content>
<asp:Content ID="Content8" runat="server" ContentPlaceHolderID="MainFooterContent">
    <table cellpadding="0" cellspacing="3" border="0" width="100%" class="tableheaderGreen">
                   <tr >
                        <td valign="middle">
                        </td>
                        </tr>
                        </table>
</asp:Content>
<%--main headr content end--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="pnlMsg" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                             <asp:Panel ID="pSettings" runat="server"  DefaultButton="btSave">
    <div class="mainDiv_"  align="left">
    <table>
    <tr>
    <td valign="top">
    
 <table>
 <tr>
    <td class="labelFreeForm">Page Timeout:</td>
    <td><asp:TextBox ID="tbTimeout" runat="server" Text = "" width="60px" cssclass="entryfldint"></asp:TextBox></td>
 </tr>
 <tr>
    <td class="labelFreeForm">Display Rows Per Page:</td>
    <td><asp:TextBox ID="tbRows" runat="server"  Text = "" width="60px" cssclass="entryfldint"></asp:TextBox></td>
 </tr> 
 <tr>
    <td class="labelFreeForm">Admin Email:</td>
    <td><asp:TextBox ID="tbAdminEmail" runat="server"  Text = "" width="300px" cssclass="entryfld"></asp:TextBox></td>
 </tr>
 <tr>
    <td class="labelFreeForm"><%--Archive Point Person:--%></td>
    <td><asp:TextBox ID="tbArchivePerson" runat="server" visible="false" Text = "" width="300px" cssclass="entryfld"></asp:TextBox></td>
 </tr>
 <tr>
    <td class="labelFreeForm">Purge Point Person:</td>
    <td><asp:TextBox ID="tbPurgePointPerson" runat="server"  Text = "" width="300px" cssclass="entryfld"></asp:TextBox></td>
 </tr>
 <tr>
    <td class="labelFreeForm">Email Host</td>
    <td><asp:TextBox ID="tbEmailHost" runat="server" visible="true" Text = "" width="300px" cssclass="entryfld"></asp:TextBox></td>
 </tr>
 <tr>
    <td class="labelFreeForm">Email Port:</td>
    <td><asp:TextBox ID="tbEmailPort" runat="server"  Text = "" width="300px" cssclass="entryfld"></asp:TextBox></td>
 </tr>
 <tr>
    <td class="labelFreeForm">Email Enable SSL:</td>
    <td><asp:CheckBox ID="cbEmailEnableSSL" clientIdMode="Static" runat="server" /></td>
 </tr>
 <tr>
    <td class="labelFreeForm">Email User Name:</td>
    <td><asp:TextBox ID="tbEmailUserName" runat="server"  Text = "" width="300px" cssclass="entryfld"></asp:TextBox></td>
 </tr>
 <tr>
    <td class="labelFreeForm">Email Password:</td>
    <td><asp:TextBox ID="tbEmailPassword" runat="server" visible="" Text = "" width="300px" cssclass="entryfld"></asp:TextBox></td>
 </tr>  
    <tr><td><asp:Button ID="btSave" runat="server" CssClass="btnsmall2" Text="Save" visible="true"/></td>
    <td align="left">
               
                                <asp:Label ID="msg" runat="server" cssclass="msg_red">&nbsp;</asp:Label>
                           
    </td>
 </tr> 
      
        
 </table>
 </td>
    <td valign="top" style="padding-left:20px">
        <%--<div>
            <uc:ucOffice runat="server" id="uOffice"></uc:ucOffice>
        </div>--%>
        <div>
            <uc:ucAddress runat="server" id="ucAddr"></uc:ucAddress>
        </div>
        <div>
            <uc:ucMainGroup runat="server" id="UcOffice1"></uc:ucMainGroup>
        </div>
        
    </td>
    </tr>
    </table>
 </div>
 </asp:Panel>
  </ContentTemplate>
                </asp:UpdatePanel>
</asp:Content>
