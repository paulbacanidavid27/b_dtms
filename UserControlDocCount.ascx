<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlDocCount.ascx.vb" Inherits="dms.UserControlDocCount" %>
<asp:UpdatePanel ID="upDocCount" runat="server"  UpdateMode="Conditional">
<ContentTemplate>

<table width="100%" border="0" cellpadding="0" cellspacing="0" >
<tr>
<td width="25px" style="border-bottom:solid 1px #D4D4D4;"><asp:Image ID="Image4" runat="server" ImageUrl="images/document.png" height="16px" Width="20px"/></td>
<td style="padding-top:5px;font-family:arial;font-weight:bold;font-size:14px;border-bottom:solid 1px #D4D4D4;color:blue">Documents</td>
</tr>
</table>


<table cellpadding="5"><tr>
<td class="labelFreeForm"><asp:Image ID="Image2" runat="server" ImageUrl="images/newdoc.png" />&nbsp;New&nbsp;
    <asp:LinkButton ID="lbnewd" runat="server" class="menu" ToolTip="Documents received today" Text="" style="color:Green"></asp:LinkButton>
    <asp:Label ID="lblnd" runat="server" class="menu" visible="false" ToolTip="Documents received today." Text="0 item(s)" style="color:Green"></asp:Label></td>


<td class="labelFreeForm"><asp:Image ID="Image1" runat="server" ImageUrl="images/inbox.png" />&nbsp;Inbox&nbsp;
    <asp:LinkButton ID="lbyoursd" runat="server" class="menu" ToolTip="Documents uploaded and received." Text="Inbox" style="color:blue"></asp:LinkButton></td>

<td class="labelFreeForm"><asp:Image ID="Image3" runat="server" ImageUrl="images/sent.png" />
    &nbsp;Sent
    <asp:LinkButton ID="lbsentd" runat="server" class="menu" ToolTip="Documents you routed to another user." Text="" style="color:blue"></asp:LinkButton>
    </td>
</tr>
</table>
</ContentTemplate></asp:UpdatePanel>