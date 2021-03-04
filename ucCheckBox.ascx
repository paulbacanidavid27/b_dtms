<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucCheckBox.ascx.vb" Inherits="dms.ucCheckBox" %>

<asp:UpdatePanel ID="cbBox" runat="server" UpdateMode="Conditional">
<ContentTemplate>
<asp:ImageButton ID="imgSelect" runat="server" ImageUrl="images/cbx_n.png" style="vertical-align:middle" height="18px" width="18px"/>
<asp:ImageButton ID="imgSelected" runat="server" ImageUrl="images/cbx_y.png" visible="false" style="vertical-align:middle" height="18px" width="18px"/>
</ContentTemplate>
</asp:UpdatePanel>

