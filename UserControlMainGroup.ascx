<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlMainGroup.ascx.vb" Inherits="dms.UserControlMainGroup" %>
<asp:UpdatePanel ID="pnlMainGroup" Runat="server" UpdateMode="Conditional">
<ContentTemplate>
<table width="100%">
    <tr>
        <td class="labelFreeForm">Main Group:</td>
        <td>
            <asp:DropDownList ID="dlMainGroups" runat="server" Width="300px"></asp:DropDownList>
        </td>
        <td>
            <asp:ImageButton ID="imgUpdate" ImageUrl="images/edit.png" runat="server" visible="false"/><asp:ImageButton ID="imgDelete" ImageUrl="images/del.png" runat="server" visible="false"/><asp:ImageButton ID="imgAdd" ImageUrl="images/add.png" runat="server" visible="true" ToolTip="Add New MainGroup" height="18px" Width="18px"/>
        </td>
        <td>
        </td>
    </tr>
</table>
<asp:Panel ID="pAddMainGroup" Visible="false" runat="server">
<table width="100%" style="border:1px solid #95DBB8; border-collapse:collapse;" 
        border="0" cellpadding="0" cellspacing="0" >
    <tr>
        <td class="labelFreeForm" colspan="4" style="background-color: #9FFFCF;color: #001A4F;font-family:Calibri">Add Main Group</td>
        <td align="right" style="background-color: #9FFFCF;color: #001A4F;font-family:Calibri"><asp:ImageButton ID="imgClose" runat="server" ImageUrl="images/close_window.gif" /></td>
    </tr>    
    <tr>
    <td colspan="5">&nbsp;
    </td>
    </tr>
    <tr>
        <td class="labelFreeForm">
            MainGroup Code:</td>
        <td>
            <asp:TextBox ID="tbMainGroupId" runat="server" width="50px" CssClass="entryfld" MaxLength="5"></asp:TextBox>
        </td>
        <td class="labelFreeForm">
            Description:</td>
        <td>
            <asp:TextBox ID="tbMainGroupDesc" runat="server" CssClass="entryfld" MaxLength="300"></asp:TextBox>
        </td>
        <td>
            <asp:ImageButton ID="imgSave" runat="server" ImageUrl="images/saveicon.png" />
        </td>
    </tr>
    <tr>
    <td colspan="5">&nbsp;
        <asp:Label ID="lblMsg" runat="server" Text="" CssClass="msg_green"></asp:Label>
    </td>
    </tr>
</table>
</asp:Panel>
<asp:Panel ID="pDeleteMainGroup" Visible="false" runat="server">
<table width="100%" style="border:solid 1px #303030;border-collapse:collapse;" border="0" cellpadding="0" cellspacing="0" >
    <tr>
        <td colspan="2" style="background-color: #9FFFCF;color: #001A4F;font-family:Calibri;padding:20px;">Are you sure you want to delete this Main Group? Please click OK to confirm.</td>        
    </tr>    
    <tr>
        <td class="labelFreeForm" >
            Main Group Code:</td>
        <td align="left" class="labelFreeForm"  style="font-weight:bold">
            <asp:Label ID="lblMainGroupId" runat="server" Text="Label"></asp:Label>
        </td>
        </tr>
        <tr>
        
        <td class="labelFreeForm">
            Description:</td>
        <td align="left" class="labelFreeForm" style="font-weight:bold">
            <asp:Label ID="lblMainGroupDesc" runat="server" Text="Label"></asp:Label>
        </td>
    </tr>
    <tr>
    <td colspan="2" align="center" style="padding:4px;">
        <asp:Button ID="btOk" runat="server" Text="OK" cssclass="btnsmall" />
        <asp:Button ID="btCancel" runat="server" Text="Cancel" cssclass="btnsmall" />
    </td></tr>
</table>
</asp:Panel>
</ContentTemplate></asp:UpdatePanel>