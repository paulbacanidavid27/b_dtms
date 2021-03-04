<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlGroupDocTypeAccess.ascx.vb" Inherits="dms.UserControlGroupDocTypeAccess" %>
<asp:Literal ID="lGroupId" runat="server" Visible="false"></asp:Literal>
                        <asp:Repeater ID="Repeater1" visible="true" runat="server" >   
                        
                        <HeaderTemplate>
                            <%--<table width="100%"  border="1" style="border-collapse:collapse" cellpadding="0" cellspacing="0">--%>
                            <table  border="0" width="100%" cellpadding="0" cellspacing="0" style="border:solid 1px #D4D4D4;background-color:white;border-collapse:collapse">
                            <tr>
                                <td colspan="6" align="left" style="background-color:#CCCCCC;font-size:10px;font-weight:bold;padding:3px; ">Document Access</td>
                            </tr>
                            <tr >
                            <td class="newtblheader" align="left" width="120px">Document Type</td>
                            
                            </tr>            
                        </HeaderTemplate>
                        <ItemTemplate>                
                            <tr  class="search">
                                <td class="search" align="left" >
                                    <asp:Literal ID="ldocname" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "docname"))%>'></asp:Literal>
                                </td >
                                
                                
                                 
                              </tr>                                                 
                            
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>                                
                        </FooterTemplate>
                    </asp:Repeater>