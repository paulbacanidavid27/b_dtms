<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlAdminMenu.ascx.vb" Inherits="dms.UserControlAdminMenu" %>
<table cellpadding="0" cellspacing="0" border="0" width="90%" style="border:solid 1px #D4D4D4;border-collapse:collapse">
<tr>
           
<td  id="div_1" runat="server"  class="adminmenu">
<asp:HyperLink ID="HyperLink1" style="color:gray;" 
    onmouseover="ufOverOut(this,'none','#003399','url(\'images/tabs/btn_bg_h.png\')')" 
    onmouseout="ufOverOut(this,'none','#537598','')" runat="server" NavigateUrl="~/user.aspx">
    <asp:Image ID="Image4" runat="server" width="16px" Height="16px" imageurl="images/user2.png" />&nbsp;System Users
</asp:HyperLink>
</td>
</tr>
<tr>           
<td id="div_2" runat="server" class="adminmenu">       
<asp:HyperLink ID="HyperLink2" style="color:gray;"
    onmouseover="ufOverOut(this,'none','#003399','url(\'images/tabs/btn_bg_h.png\')')" 
    onmouseout="ufOverOut(this,'none','#537598','')" runat="server" NavigateUrl="~/groups.aspx">
    <asp:Image ID="Image22" runat="server" width="16px" Height="16px" imageurl="images/group.png" />
    &nbsp;User Groups
</asp:HyperLink>
</td>
</tr>
<tr>           
<td id="div_3" runat="server" class="adminmenu">       
<asp:HyperLink ID="HyperLink3" style="color:gray;"
    onmouseover="ufOverOut(this,'none','#003399','url(\'images/tabs/btn_bg_h.png\')')" 
    onmouseout="ufOverOut(this,'none','#537598','')" runat="server" NavigateUrl="~/doctype.aspx">
    <asp:Image ID="Image5" runat="server" width="16px" Height="16px" imageurl="images/doctype.png" />
    &nbsp;Document Types
</asp:HyperLink>
</td>
</tr>
<tr>
<td id="div_4" runat="server" class="adminmenu">       
    <asp:HyperLink ID="HyperLink4" style="color:gray;"
    onmouseover="ufOverOut(this,'none','#003399','url(\'images/tabs/btn_bg_h.png\')')" 
    onmouseout="ufOverOut(this,'none','#537598','')" runat="server" NavigateUrl="~/import.aspx">
    <asp:Image ID="Image1" runat="server" width="16px" Height="16px" imageurl="images/import.png" />&nbsp;Import Documents</asp:HyperLink>
</td>
</tr>
<tr>
<td id="div_5" runat="server" class="adminmenu">       
    <asp:HyperLink ID="HyperLink5"  style="color:gray;"
    onmouseover="ufOverOut(this,'none','#003399','url(\'images/tabs/btn_bg_h.png\')')" 
    onmouseout="ufOverOut(this,'none','#537598','')" runat="server" NavigateUrl="~/purge.aspx">
    <asp:Image ID="Image6" runat="server" width="16px" Height="16px" imageurl="images/archive_icon.png" />&nbsp;Purge Documents</asp:HyperLink>
</td>
</tr>
<tr>
<td id="div_6" runat="server" class="adminmenu">       
    <asp:HyperLink ID="HyperLink6"  style="color:gray;"
    onmouseover="ufOverOut(this,'none','#003399','url(\'images/tabs/btn_bg_h.png\')')" 
    onmouseout="ufOverOut(this,'none','#537598','')" runat="server" NavigateUrl="~/changelog.aspx">
    <asp:Image ID="Image7" runat="server" width="16px" Height="16px" imageurl="images/changelog_icon.png" />&nbsp;Track Changes</asp:HyperLink>
</td>
</tr>
<tr>
<td id="div_7" runat="server" class="adminmenu">       
    <asp:HyperLink ID="HyperLink7"  style="color:gray;"
    onmouseover="ufOverOut(this,'none','#003399','url(\'images/tabs/btn_bg_h.png\')')" 
    onmouseout="ufOverOut(this,'none','#537598','')" runat="server" NavigateUrl="~/backupdb.aspx">
    <asp:Image ID="Image8" runat="server" width="16px" Height="16px" imageurl="images/backup.png" />&nbsp;Backup Database</asp:HyperLink>
</td>
</tr>
<tr>

<td id="div_8" runat="server" class="adminmenu">       
    <asp:HyperLink ID="HyperLink8"  style="color:gray;"
    onmouseover="ufOverOut(this,'none','#003399','url(\'images/tabs/btn_bg_h.png\')')" 
    onmouseout="ufOverOut(this,'none','#537598','')" runat="server" NavigateUrl="~/holiday.aspx">
    <asp:Image ID="Image13" runat="server" width="16px" Height="16px" imageurl="images/holiday_icon.png" />&nbsp;Holidays</asp:HyperLink>
</td>
       
</tr>
</table>
