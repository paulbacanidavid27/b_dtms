<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlDocumentIndex.ascx.vb" Inherits="dms.UserControlDocumentIndex" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Repeater ID="rptIndex" runat="server" Visible="True">
                        <HeaderTemplate>
                                <table  border="0" width="100%" cellpadding="0" cellspacing="0" style="border:solid 1px #D4D4D4;background-color:white;border-bottom:4px;border-collapse:collapse">
                            <tr>
                                <td class="newtblheader" align="left" style="padding-left:4px"><img src="images/index_icon.png" />&nbsp;Document Index</td>
                            </tr>            
                        <tr>
                        <td  class="search">                                  
                                
                        </HeaderTemplate>
                        <ItemTemplate>
                        
                            <div style="display:inline;width:100%;float:left;text-align:left;max-height:300px;overflow:auto">
                                <table cellpadding="0" cellspacing="0" border="0"><tr>
                                <td width="100px" class="labelFreeForm" align="left" style="padding-left:2px"><asp:Literal ID="lColId" visible="false" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ColumnId"))%>'></asp:Literal>
                                <asp:Literal ID="lDocType" visible="false" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DocType"))%>'></asp:Literal>
                                <asp:Literal ID="lDataType" visible="false" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DataType"))%>'></asp:Literal>
                                <asp:Literal ID="lLen" visible="false" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DataLength"))%>'></asp:Literal>
                                <asp:Literal ID="lColValue" visible="false" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "colValue"))%>'></asp:Literal>
                                <asp:Literal ID="lColName" visible="true" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ColumnName"))%>'></asp:Literal>
                                </td>
                                <td align="left"><asp:TextBox ID="tbColValue"  CssClass="entryfld" runat="server" Text="" Width="300px"></asp:TextBox>
                                <asp:TextBox ID="tbDateValue"  CssClass="entryfld" runat="server" Text="" Visible="false" width="65px" MaxLength="10"></asp:TextBox>
                                <cc1:CalendarExtender ID="lColValExtender" runat="server" TargetControlID="tbDateValue"/>
                                    <asp:DropDownList ID="dlColValue" runat="server" Visible="false" CssClass="entryfld2">
                                        <asp:ListItem Value=""></asp:ListItem>
                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                        <asp:ListItem Value="No">No</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="dlList" runat="server" Visible="false" CssClass="entryfld2" style=" min-width:300px;">                                        
                                    </asp:DropDownList>
                                </td>
                                </tr>
                                </table>
                                
                                   
                            
                            </div>
                            
                        </ItemTemplate>
                        <FooterTemplate>
                        </td>
                        </tr>
                                               </table>
                        </FooterTemplate>

                    </asp:Repeater>