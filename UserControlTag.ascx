<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlTag.ascx.vb" Inherits="dms.UserControlTag" %>
 <asp:UpdatePanel ID="pnlTag" runat="server" UpdateMode="Conditional">
                                     <ContentTemplate>
<asp:Repeater ID="rptTags" runat="server" Visible="True">
                <HeaderTemplate>                   

                    <table  border="0" width="100%" cellpadding="0" cellspacing="0" style="border:solid 1px #D4D4D4;background-color:white;border-collapse:collapse">
                            <tr>
                                <td class="newtblheader"><img src="images/tag_icon.png" />&nbsp;Tags</td><td class="newtblheader" align="right"><asp:ImageButton ID="imgDeleteTags"  height="15px" Width="14px" tooltip="Delete selected tags." runat="server"   ImageUrl="images/del.png" onclick="DeleteTags"/></td>
                            </tr>    
                   
                </HeaderTemplate>
                <ItemTemplate>
                     
                     <tr>
                        <td style="padding:5px;font-size:13px;color:#5B5B5B"><asp:Literal ID="lTag" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Tags"))%>'></asp:Literal>
                        <asp:Literal ID="lTB" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CreatedBy"))%>' Visible="false"></asp:Literal>
                        </td>
                        
                        <td align="right"><asp:ImageButton ID="imgSelect" runat="server"  ImageUrl="images/box.png"  OnClick="fSelect"/>
                        <asp:ImageButton ID="ImgSelected" runat="server"  ImageUrl="images/checkbox.png" OnClick="fUnSelect" visible="false"/>

                        </td>
                                                                        
                    </tr>                                                                    
                    <tr>                    
                     
                     <td class="tbldashed-gray" colspan="2">By: <asp:Literal ID="Literal1" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "username"))%>'></asp:Literal>&nbsp; (<asp:Label ID="lCreateDate" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CreatedDate"))%>'></asp:Label>)</td>
                     </tr>
                </ItemTemplate>
                <FooterTemplate>
                            <tr>
                                    <td style="border-top:solid 1px #ffffff" colspan="2"></td>
                                </tr>
                                </table>
                </FooterTemplate>

            </asp:Repeater>
            <asp:Panel ID="TagSearch" runat="server" DefaultButton="lbSave">
            <table>
                                                          <tr>
                                                                        <td><asp:TextBox ID="txtTags" maxlength="150" runat="server" ClientIDMode="Static" Width="300px" onfocus="tclear(this,'Add New Tag')" onblur="tblur(this,'Add New Tag')" cssclass="entryfld" style="color:#C0C0C0">Add New Tag</asp:TextBox></td>
                                                                        <td>
                                                                            <asp:Button ID="lbSave" runat="server" CssClass="btnsmall2" Text="Save" /></td>
                                                                    </tr>
                                                                    <tr>
        <td colspan="2"><asp:Label ID="lcheckmsg" cssclass="msg_green" runat="server" Text=""></asp:Label></td>
        </tr>
                                                         </table>
                                                         </asp:Panel>
                                                         </ContentTemplate>
                                                         </asp:UpdatePanel>