<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlHoliday.ascx.vb" Inherits="dms.UserControlHoliday" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

 <asp:UpdatePanel ID="pnlHoliday" runat="server" UpdateMode="Conditional">
                                     <ContentTemplate>    
                                     
                                     <fieldset style="margin:0 0 20px 2px;padding-top:0;font-size:10px;font-family:Calibri;">
                                     <legend style="background-color:white">Filter</legend>                                     
                                     <table width="100%"  border="0" cellpadding="0" cellspacing="0" style="text-align:left; ">
                                        <tr>
                                           <td class="labelFreeForm" width="100px">User Group</td> <td align="left"><asp:DropDownList ID="dlGroup" runat="server" AutoPostBack="true">
                                                                            </asp:DropDownList>
                                       
                                                                    </td><td align="right">
                                                                    <table cellpadding="0">
                                                                    <tr><td align="center" valign="bottom"> 
                                                                        
                                                                        <table cellpadding="0" cellspacing="0" border="0" style="border-collapse:collapse">
                                                                        <tr>
                                                                        <td>
                                                                        <asp:ImageButton ID="imgLeft" runat="server" imageurl="images/larrow.png" onmouseover="this.src='images/larrow_h.png'"  onmouseout="this.src='images/larrow.png'"/>
                                                                        </td>
                                                                        <td>
                                                                        <asp:TextBox ID="txYear" runat="server" Width="30px" cssclass="entryfld" MaxLength="4" style="margin-top:0px"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                        <asp:ImageButton ID="imgRight" runat="server" imageurl="images/rarrow.png" onmouseover="this.src='images/rarrow_h.png'"  onmouseout="this.src='images/rarrow.png'"/>
                                                                        </td>
                                                                        </tr>
                                                                        </table>
                                                                        </td></tr>
                                                         </table>
                                                         </td>
                                        </tr>
                                     </table>
                                     </fieldset>  
                                     
                                                          <asp:Repeater ID="rptHoliday" runat="server" Visible="True">
                                                                <HeaderTemplate>
                                                                    
                                                                    <table  border="0" class="codetbl"  cellpadding="0" cellspacing="0" style="border-collapse:collapse;border:solid 1px #D4D4D4;background-color:white;text-align:left;width:100%">
                                                                            <tr>
                                                                                <td class="newtblheader">Holiday</td>
                                                                                <td class="newtblheader">Description</td>
                                                                                <td class="newtblheader">Created By</td><td class="newtblheader">Created Date</td>
                                                                                <td class="newtblheader" align="center" valign="middle" width="20px"><asp:ImageButton ID="imgDeleteHoliday"  tooltip="Delete selected Holiday." runat="server"  ImageUrl="images/del.png" onclick="DeleteHoliday"/></td>
                                                                            </tr>                                                               
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                     <tr>
                                                                        <td style="padding-left:2px"><asp:Literal ID="lHoliday" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Holiday"))%>'></asp:Literal></td>
                                                                        <td><asp:Literal ID="Literal2" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Description"))%>'></asp:Literal></td>
                                                                        <td><asp:Literal ID="Literal1" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "username"))%>'></asp:Literal></td>
                                                                        <td><asp:Literal ID="lCreateDate" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CreatedDate"))%>'></asp:Literal></td>
                                                                        <td align="center" width="20px"><asp:ImageButton ID="imgSelect" runat="server"  ImageUrl="images/box.png"  OnClick="fSelect"/>
                                                                        <asp:ImageButton ID="ImgSelected" runat="server"  ImageUrl="images/checkbox.png" OnClick="fUnSelect" visible="false"/>
                                                                        </td>
                                                                        
                                                                    </tr>    
                                                                    <tr>
                                                                            <td class="tbldashed" colspan="5"></td>
                                                                        </tr>                                                                                                                                   
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                        <tr>
                                                                            <td style="border-top:solid 1px #ffffff" colspan="5"></td>
                                                                        </tr>
                                                                    </table>
                                                                </FooterTemplate>

                                                          </asp:Repeater>
                                                          
                                                         <table width="100%" cellspacing="0" border="0" cellpadding="0">
                                                         <tr>
                                                               <td style="text-align:center">
                                                                    <asp:Label ID="lMsg"  visible="false" cssclass="msg_red" runat="server" Text=""></asp:Label>
                                                               </td>
                                                         </tr>           
                                                         </table>
                                                        
                                                          </ContentTemplate>
                                                          </asp:UpdatePanel>