<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Login.Master" CodeBehind="errorpage.aspx.vb" Inherits="dms.errorpage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h3>Error occurred while processing your request.</h3>
<h4>Administrator has been notified. Please try again.</h4>
    <asp:Label ID="errmsg" runat="server" Text=""></asp:Label>
    <br />
    <br />
    <h5>Go to <a href="Default.aspx" style="color:Blue">login</a> page</h5>
</asp:Content>
