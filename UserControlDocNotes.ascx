<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlDocNotes.ascx.vb" Inherits="dms.UserControlDocNotes" %>

 <asp:UpdatePanel ID="pnlNotes" runat="server" UpdateMode="Conditional">
                                     <ContentTemplate>
                                         <asp:HiddenField ID="hfCount" runat="server" />
                                                          <asp:Repeater ID="rptNotes" runat="server" Visible="True">
                                                                <HeaderTemplate>
                                                                    
                                                                    <table  border="0" width="100%" cellpadding="0" cellspacing="0" style="border:solid 1px #D4D4D4;background-color:white;border-collapse:collapse">
                            <tr>
                                <td class="newtblheader"><img src="images/note_icon.png" />&nbsp;Notes</td><td class="newtblheader" align="right"><asp:ImageButton ID="imgDeleteNotes"  tooltip="Delete selected Notes." runat="server"  ImageUrl="images/del.png" onclick="DeleteNotes"/></td>
                            </tr>    
                                                                                
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                     
                                                                         <tr>
                                                                            <td style="padding:5px;font-size:13px;color:#5B5B5B" valign="top">
                                                                                <asp:Literal ID="lTag" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Notes"))%>'></asp:Literal>
                                                                                <asp:Literal ID="lBY" Visible="false" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CreatedBy"))%>'></asp:Literal>
                                                                                <asp:Literal ID="lid" Visible="false" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "noteId"))%>'></asp:Literal>
                                                                                <asp:TextBox ID="txtNotes" maxlength="1000" visible="false" runat="server" Height="40px" Width="500px" Text='<%#DataBinder.Eval(Container.DataItem, "Notes")%>' TextMode="MultiLine" cssclass="entryfld"></asp:TextBox>
                                                                                <asp:Button ID="lbSave" runat="server" CssClass="btnsmall2" visible="false" Text="Save" onclick="updateDocNotes" style="vertical-align:top"/>
                                                                            </td>
                        
                                                                            <td align="right" ><asp:ImageButton ID="imgSelect" runat="server"  ImageUrl="images/box.png"  OnClick="fSelect"/>
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
                                                                </FooterTemplate>

                                                          </asp:Repeater>
                                                          <asp:Panel ID="NoteSearch" runat="server" DefaultButton="lbSave">
                                                         <table>
                                                          <tr>
                                                                        <td><asp:TextBox ID="txtNotes" maxlength="1000" runat="server" ClientIDMode="Static" Height="40px" Width="500px" TextMode="MultiLine"  onfocus="tclear(this,'Add New Note (max of 1000 characters)')" onblur="tblur(this,'Add New Note (max of 1000 characters)')" cssclass="entryfld">Add New Note (max of 1000 characters)</asp:TextBox></td>
                                                                        <td valign="top">
                                                                            <asp:Button ID="lbSave" runat="server" CssClass="btnsmall2" Text="Save" /></td>
                                                                    </tr>
                                                         </table>
                                                        </asp:Panel>
                                                          </ContentTemplate>
                                                          </asp:UpdatePanel>
