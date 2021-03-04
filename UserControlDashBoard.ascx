<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlDashBoard.ascx.vb" Inherits="dms.UserControlDashBoard" %>
<%@ Register src="UserControlPager.ascx" tagname="UserControlPager" tagprefix="uc2" %>

<asp:Repeater ID="rptDashboard" runat="server">
<HeaderTemplate>
    <table width="100%" cellpadding="0" cellspacing="6" border="0" style="border-collapse:separate;border-bottom:solid 1px #D4D4D4;border-right:solid 1px #D4D4D4">
        
</HeaderTemplate>
<ItemTemplate>
        <tr id="trow" runat="server" style="background-color:#D1D3D4; color:#222222;">
            <td class="tbldashedx" style="padding:4px;width:100%">
            <div>
                <asp:Label style="font-weight:bold;font-size:8pt;" ID="lblAdate" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "adate"))%>'></asp:Label>
                <asp:Label ID="lblTask" runat="server" Text=' <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "task"))%> ' style="font-style:normal;font-size:9pt;font-weight:bold;text-transform:  capitalize;letter-spacing:2px;color:#939598"></asp:Label>
                <asp:LinkButton ID="lbProfile" CssClass="menusmall" onclick="showprofile" runat="server"><asp:Label ID="lblUser" runat="server" Text='<%# "by: " & Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "uname"))%>' style="font-weight:normal;font-size:10pt;"></asp:Label></asp:LinkButton>
            </div>
            <div style="word-wrap:break-word;width:850px;margin-left:20px;">
                <asp:Literal ID="lDocId" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "docid"))%>' Visible="false"></asp:Literal>
                <asp:Literal ID="lPPID" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "userid"))%>' Visible="false"></asp:Literal>
                <asp:Label ID="Label1" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "action"))%>'></asp:Label>&nbsp;<asp:LinkButton ID="lbDoc" runat="server" OnClick="ViewDoc" cssclass="menu" Visible="false"><%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "doctitle"))%></asp:LinkButton>
            </div>
            </td>                
        </tr>
       
</ItemTemplate>


<FooterTemplate>        
    </table>
</FooterTemplate>
</asp:Repeater>

