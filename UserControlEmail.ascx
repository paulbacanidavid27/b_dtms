<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlEmail.ascx.vb" Inherits="dms.UserControlEmail" %>
 <asp:UpdatePanel ID="pnl" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
           
            
                                                      <div style="width:500px;border:solid 0px blue;background-color:white;">
                                                      
                                                            <div style="width:100%">
                                                                <asp:Label ID="lMsg" runat="server" Text=""></asp:Label>
                                                                </div>
                                                          <table  border="0" width="100%" cellpadding="0" cellspacing="0" style="background-color:white;border-bottom:4px;border-collapse:collapse">
                                                          <asp:Panel ID="pSearchCriteria" Visible="false" runat="server">
                                                          <asp:Panel ID="pTitle" Visible="false" runat="server">
                                                                <tr >
                                                                        <td align="center" colspan="5" height="10px"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        
                                                                        <td class="tableHead" colspan=4>
                                                                            
                                                                            <table cellpadding="0" cellspacing="0" border="0" width="100%"><tr><td>Add More Users To Approve The Document
                </td><td align="right"><asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    
            <ContentTemplate>
            <asp:ImageButton ID="imgHelp" runat="server" ImageUrl="images/question.png" />
                        </ContentTemplate>
                    </asp:UpdatePanel></td></tr></table>
                                                                            </td>
                                                                        <td></td>
                                                                    </tr>
                                                                    
                                                          </asp:Panel>

                                                          

                                                          <tr>
                                                                <td align="center" colspan="5">
                                                          
                                                                </td>
                                                            </tr>
                                                            </asp:Panel>
                                                          </table>
                                                         
                                                         <asp:Panel ID="pSearchCriteria2" Visible="true" runat="server" DefaultButton="imgSearch">
                                                          
                                                                                                                    <table cellpadding="0" cellspacing="0" style="width:100%;background-color:white;border:solid 1px gray; ">
                                                                                                                    <tr><td align="left">
                                                                                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                         <tr>
                                                         <td>
                                                         <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                                            
                                                            <tr><td class="labelFreeForm" align="left" style="padding-left:2px;width:80px;">Search Email: </td>
                                                                <td  align="left" style="padding-left:2px;">
                                                                    <asp:TextBox ID="txtBx" maxlength="150" runat="server" ClientIDMode="Static" 
                                                                        onfocus="tclear(this,'Enter Email Address or User Name...')" 
                                                                        onblur="tblur(this,'Enter Email Address or User Name...')" width="99%" cssclass="entryfld">Enter Email Address or User Name...</asp:TextBox>
                                                                </td>
                                                                <td  align="left" style="padding-left:2px;width:18px;">
                                                                    <asp:ImageButton ID="imgSearch" runat="server" imageurl="images/magni.png" ToolTip="Search Users"/>
                                                                    <asp:Button ID="btSelect" runat="server" CssClass="btnsmall"  ToolTip="Click this to add the selected users as recipient." Text="Select" visible="false" Width="90%"/>
                                                                </td>                                                                
                                                            </tr>
                                                         </table>
                                                         </td><td><td align="right">
                                                                    <asp:ImageButton ID="imgClose" runat="server" Height="16px" Width="16px" imageurl="images/close.png" onmouseover="this.src='images/close_h.png'"  onmouseout="this.src='images/close_h.png'" style="z-index:500001"/>                                                                
                                                                </td></td>
                                                         </tr>
                                                         </table>
                                                         </td>
                                                                                                                    </tr>
                                                                                                                    </table>
                                                         
                                                         <div style="border-bottom:solid 1px gray;border-right:solid 1px gray;width:100%;max-height:200px;overflow:auto;background-color:white;margin:0;padding:0">
                                                             <center><asp:Label ID="lSearchMsg" cssclass="helpnotes" runat="server" Text="Label" Visible="false"></asp:Label></center>                                                          
                                                             <asp:Repeater ID="rptSub" runat="server" visible="true">
                                                                <HeaderTemplate>
                                                                    
                                                                    <table border="0"  cellpadding="0" cellspacing="0" style="width:100%;background-color:white;border-left:solid 1px gray; border-right:solid 1px gray;border-bottom:solid 1px gray;margin-bottom:2px;">
                                                                    
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <tr >
                                                                        <td><asp:ImageButton ID="imgSelect" runat="server"  ImageUrl="images/box.png"  OnClick="fSelect2" tooltip="Add the user from the document routing list"/>
                                                                        <asp:ImageButton ID="ImgSelected" runat="server"  ImageUrl="images/checkbox.png" OnClick="fUnSelect2" visible="false"/>
                                                                        </td>
                                                                        <td align="left" class="labelFreeForm">
                                                                        <asp:Literal ID="lApprover" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "username"))%>'></asp:Literal>
                                                                        </td>
                                                                        <td align="left"  class="labelFreeForm">
                                                                        <asp:Literal ID="lUserId" visible="false" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "UserId"))%>'></asp:Literal>
                                                                        <asp:Literal ID="lEmail" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Email"))%>'></asp:Literal>
                                                                        </td>
                                                                    </tr>       
                                                                                                                                
                                                                </ItemTemplate>
                                                                <FooterTemplate>                                                                    
                                                                    </table>
                                                                    </div>
                                                                </FooterTemplate>

                                                          </asp:Repeater>
                                                          </div>
                                                       </asp:Panel>
                                                       </div>
                              </ContentTemplate>
            </asp:UpdatePanel>