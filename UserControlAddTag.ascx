<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlAddTag.ascx.vb" Inherits="dms.UserControlAddTag" %>
<asp:UpdatePanel id="upAddTag" runat="server" UpdateMode="Conditional">
<ContentTemplate>

<table width="100%" style="border:solid 1px #D4D4D4">
    <tr>
        <td align="center">
            <asp:TextBox ID="txtTags" runat="server" ClientIDMode="Static" 
                cssclass="entryfld" maxlength="150" onblur="tblur(this,'Add New Tag')" 
                onfocus="tclear(this,'Add New Tag')" Width="99%" TextMode="MultiLine" Height="30px" style="color:#C0C0C0">Add New Tag</asp:TextBox>
        </td>
        </tr>
        <tr>
        <td>
            <asp:Button ID="lbSave" runat="server" CssClass="btnsmall" Text="Save" /><asp:Button ID="lbCancel" runat="server" CssClass="btnsmall" Text="Cancel" />
        </td>
        <tr>
        <td><asp:Label ID="lcheckmsg" cssclass="msg_green" runat="server" Text=""></asp:Label></td>
        </tr>
    </tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>
