<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlDocumentAttachIndex.ascx.vb" Inherits="dms.UserControlDocumentAttachIndex" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<div style="display:inline;width:100%;float:left;text-align:left;max-height:300px;overflow:auto">
<table style="border-collapse:collapse;width:80%;margin-left:20px;border:solid 1px #D4D4D4;background-color:white;">
<tr>
<td colspan="2" class="newtblheader" align="left" style="padding-left:4px"><img src="images/index_icon.png" />&nbsp;Document Index</td>
</tr>
<tr>
<td class="labelFreeForm" width="100px">
Attachment Type
</td>
    
<td>
<asp:HiddenField ID="hfAttachId" runat="server" Value="" />
<asp:UpdatePanel id="upDocType" runat="server" UpdateMode="Conditional">
<ContentTemplate>
<asp:DropDownList ID="dlDocType" 
                                                runat="server" 
    cssclass="entryfld2" Width="312px" AutoPostBack="true">
                                            </asp:DropDownList>
                                            </ContentTemplate>
                                            </asp:UpdatePanel>

</td>
</tr>
<tr>
<td colspan="2">
<asp:UpdatePanel id="upIndex" runat="server" UpdateMode="Conditional">
<ContentTemplate>


<asp:Repeater ID="rptIndex" runat="server" Visible="True">
                        <HeaderTemplate>
                                      <table border="0" cellpadding="0" cellspacing="0">
                                                            
                                
                        </HeaderTemplate>
                        <ItemTemplate>
                        
                            
                                <tr>
                                <td width="100px" class="labelFreeForm" align="left" style="padding-left:2px"><asp:Literal ID="lColId" visible="false" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ColumnId"))%>'></asp:Literal>
                                <asp:Literal ID="lDocType" visible="false" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DocType"))%>'></asp:Literal>
                                <asp:Literal ID="lDataType" visible="false" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DataType"))%>'></asp:Literal>
                                <asp:Literal ID="lDocAttachId" visible="false" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DocAttachId"))%>'></asp:Literal>
                                <asp:Literal ID="lDocId" visible="false" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DocId"))%>'></asp:Literal>
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
                                
                                
                                   
                            
                          
                            
                        </ItemTemplate>
                        <FooterTemplate>
                                </table>
                        </FooterTemplate>

                    </asp:Repeater>
                    <asp:Button ID="btSave" runat="server" CssClass="btnsmall" Text="Save" Visible="false" />
</td>
                    </ContentTemplate>
<Triggers>
<asp:AsyncPostBackTrigger ControlID="dlDocType"  EventName="SelectedIndexChanged"/>
</Triggers>
</asp:UpdatePanel>

</tr>
<tr><td colspan="2">
<asp:UpdatePanel id="upMsg" runat="server" UpdateMode="Conditional">
<ContentTemplate>
    <asp:Label ID="lmsg" runat="server" Text="" CssClass="msg"></asp:Label>
</ContentTemplate>
<Triggers>
    <asp:AsyncPostBackTrigger ControlID="btSave" EventName="click" />
</Triggers>
</asp:UpdatePanel>

</td></tr>
</table>
  </div>