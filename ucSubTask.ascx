<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucSubTask.ascx.vb" Inherits="dms.ucSubTask" %>
<asp:HiddenField ID="hfDocId" runat="server" ClientIDMode="Static" value="" />
<asp:HiddenField ID="hfCount" runat="server" />
<asp:UpdatePanel ID="pSubTask" runat="server" UpdateMode="Conditional">
                                     <ContentTemplate>
<asp:Repeater ID="rptSubTasks" runat="server" Visible="True">
                    <HeaderTemplate>                                                                   
                        <table  border="0" width="100%" cellpadding="0" cellspacing="0" style="border:solid 1px #D4D4D4;background-color:white;border-collapse:collapse">
                                                 
                    </HeaderTemplate>
                    <ItemTemplate>                                                                                           
                     <tr>
                        <td style="padding:5px" width="140px" align="left"><asp:LinkButton ID="lbView" runat="server" cssclass="nb" OnClick="ViewDoc" >
                            <asp:Literal ID="lTB" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "RefNo"))%>'  Visible="true"></asp:Literal>
                            </asp:LinkButton>
                        </td>
                        
                        <td align="left" style="padding:5px">
                            <asp:Literal ID="lTitle" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Title"))%>' Visible="true"></asp:Literal>
                            <asp:Literal ID="lDocId" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "docId"))%>' Visible="false"></asp:Literal>

                            <asp:Literal ID="lcby" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CreatedBy"))%>' Visible="false"></asp:Literal>
                            <asp:Literal ID="lcdt" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CreatedDate"))%>' Visible="false"></asp:Literal>
                        </td>
                                          <td width="20px" align="right" style="padding:5px">
                                              <asp:ImageButton ID="imgDelete" runat="server" imageurl="images/del.png" visible="false" OnClientClick="showWindow('dDelSubTask')"/>
                                          </td>                              
                    </tr>                                                                    
                    <tr><td class="tbldashed-gray" colspan="3" align="left">By: <asp:Literal ID="lCreatedBy" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "createdbyName"))%>'></asp:Literal>&nbsp; (<asp:Label ID="lCreateDate" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "cDate"))%>'></asp:Label>)</td>
                     </tr>
                </ItemTemplate>
                <FooterTemplate>
                            <tr>
                                    <td style="border-top:solid 1px #ffffff" colspan="3"></td>
                                </tr>
                                </table>
                </FooterTemplate>

                </asp:Repeater>
                </ContentTemplate>
                                                         </asp:UpdatePanel>