<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="backupdb.aspx.vb" Inherits="dms.backupdb" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register src="ucHr.ascx" tagname="ucHr" tagprefix="uc1" %>
<%@ Register src="UserControlPager.ascx" tagname="UserControlPager" tagprefix="uc2" %>
<%@ Register src="UserControlAdminMenuH.ascx" tagname="UserControlAdminMenuH" tagprefix="uc" %>



<%--menu content start--%>
<asp:Content ID="Content12" runat="server" ContentPlaceHolderID="AdminMenu">
<uc:UserControlAdminMenuH id="ucMenu" runat="server"></uc:UserControlAdminMenuH>                                       
</asp:Content>
<%--main content end--%>

<%--main headr content start--%>

<asp:Content ID="Content8" runat="server" ContentPlaceHolderID="MainFooterContent">
    <table cellpadding="0" cellspacing="3" border="0" width="100%" class="tableheaderGreen">
                   <tr >
                        <td valign="middle"><div class="notes2">&nbsp;<asp:Literal ID="lRecordCount" runat="server"></asp:Literal></div>
                        </td>
                        </tr>
                        </table>
</asp:Content>
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
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">    
    <title>Backup Database</title>
</asp:Content>
<%--main headr content start--%>
<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="MainHeaderContent">
   
           <table border="0" width="100%" class="tableheaderGreen">
        <tr>
            <td class="tableheader_1"><img  alt="" src="images/backup.png" height="20px" width="20px" style="vertical-align:middle" />&nbsp;Backup Database</td>
            
        </tr>        
    </table>    
    
</asp:Content>
<%--main headr content end--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="mainDiv_"  align="left">
    <asp:Panel ID="imprt" runat="server" DefaultButton="btBackup">
    <table>
    <tr>
    <td class="labelFreeForm">Backup File Name:</td>
    <td><asp:TextBox ID="tbFileName" runat="server" Text = "" width="300px" cssclass="entryfldw"></asp:TextBox></td>
 </tr>
 <tr>
    <td class="labelFreeForm">Current location of database backup:</td>
    <td><asp:TextBox ID="tbLocation" runat="server"  ReadOnly="true" Text = "" width="300px" cssclass="entryflddisabled"></asp:TextBox></td>
 </tr> 
 
    <tr><td width="250px"><asp:Button ID="btBackup" runat="server" CssClass="btn" Text="Backup" visible="true"/></td>
    <td align="left">
                <asp:UpdatePanel ID="pnlMsg" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Label ID="msg" runat="server" cssclass="msg_red">&nbsp;</asp:Label>
                            </ContentTemplate>
                </asp:UpdatePanel>
    </td>
 </tr> 
    </table>
    </asp:panel>
         <table width="100%">
         <tr>
                <td align="right">
                <asp:HiddenField ID="hfCurrent" runat="server" Value="1"/>
                <asp:HiddenField ID="hfTotalRows" runat="server" Value="0"/>
                <uc2:UserControlPager ID="ucPager" runat="server" />
            </td>
         </tr>
         </table>
<table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-collapse:collapse">

 
 <tr>
    <td colspan="2" style="padding: 0 0 0 0">
        <table border="0" class="codetbl" cellspacing="0" cellpadding="1" style="border-collapse:collapse;width:100%;z-index:900;border:solid 1px #D4D4D4">
                    <tr>                                               
                        <td class="newtblheader"><asp:LinkButton ID="lbSort2" runat="server" class="sortcol" tooltip="Sort by Backup Date" OnClick="sortColumnHeader">Backup Date</asp:LinkButton><asp:Image ID="imgSort2" imageurl="images/desc.png" runat="server" visible="True"/></td>
                        <td class="newtblheader"><asp:LinkButton ID="lbSort1" runat="server" class="sortcol" tooltip="Sort by File Name" OnClick="sortColumnHeader">File Name</asp:LinkButton><asp:Image ID="imgSort1" imageurl="images/asc.png" runat="server" visible="False"/></td>
                        <td class="newtblheader"><asp:LinkButton ID="lbSort3" runat="server" class="sortcol" tooltip="Sort by Backup Location" OnClick="sortColumnHeader">Backup Location</asp:LinkButton><asp:Image ID="imgSort3" imageurl="images/asc.png" runat="server" visible="false"/></td>
                        <td class="newtblheader"><asp:LinkButton ID="lbSort4" runat="server" class="sortcol" tooltip="Sort by Backup By" OnClick="sortColumnHeader">Backup By</asp:LinkButton><asp:Image ID="imgSort4" imageurl="images/asc.png" runat="server" visible="false"/></td>
                    </tr>    
    <asp:Repeater ID="Repeater1" visible="true" runat="server" >
            <HeaderTemplate>
            </HeaderTemplate>
            <ItemTemplate>                
                    <tr>    
                        
                        <td><%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "BackupDate"))%></td>                                                               
                        <td><%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DbBackupFilename"))%></td>                                                               
                        <td><%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "BackupLocation"))%></td>                                                               
                        <td><%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "BackupBy"))%></td>                                                               
                    </tr>   
                    <tr>
                    <td class="tbldashed" colspan="4"></td>
                    </tr>             
                    
            </ItemTemplate>
            <FooterTemplate>  
                           
            </FooterTemplate>
        </asp:Repeater>
            <tr>
               <td style="border-top:solid 1px #ffffff" colspan="4"></td>
            </tr>       
            </table>            
           <%-- <table width="100%">
        <tr>
                        
                        <td align="center">
                            <asp:Label ID="lmsg" runat="server" Text="" CssClass="msg_red" Visible="false"></asp:Label></td>
                    </tr>
                    </table>    --%>                 
    </td>
 </tr>
      
        
 </table>

 </div>

</asp:Content>
