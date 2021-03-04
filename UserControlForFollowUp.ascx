<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlForFollowUp.ascx.vb" Inherits="dms.UserControlForFollowUp" %>

<%--<div style="border:solid 1px #939393;background-color: #E9E9E9;width: 98%; margin-left: 1px">--%>
                    
                    <%--  <div style="border:solid 1px #9DB5CD;background-color: #F5F5F5; width: 98%; margin-top: 8px; margin-left: 1px">--%>
                    <div style="border-style: solid; border-width: 1px; border-color: #F1F4F8 #CFDBE7 #81A0C0 #CEDAE8; background-color: #FFFFFF; width: 98%; margin-top: 8px; margin-left: 1px">
                    <asp:UpdatePanel ID="pnlPending" runat="server" UpdateMode="Conditional">
                       <ContentTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" class="newtblheader2" >
                            <tr height="25px">
                                <td align="left" valign="bottom"  style="color:#EEEEEE;font-family:Arial;font-size:10pt;font-weight:bold;font-style:normal;color:#CCCCCC;padding-left:4px;padding-bottom:4px;" title="Only 10 notifications will be displayed">
                                    <img alt="" src="images/notification.png" valign="top"    height="20px" width="20px" />&nbsp;&nbsp;Notifications<asp:Label
                                        ID="lNotification" style="vertical-align:super;color:red;font-weight:bold;font-family:Verdana;font-size:7pt;" runat="server" Text=""></asp:Label></td>
                                    <td width="50px" align="right" valign="top" class="tableHead27" >
                                        <asp:ImageButton ID="imgBk" runat="server" imageurl="images/showpanel.png"/></td>
                            </tr>
                        </table>
                    
                       <asp:Panel runat="server" ID="Pbk" Visible="false"> 
                    <asp:Repeater ID="Repeater2" runat="server" visible="true">
                        <HeaderTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" 
                                style="border-collapse: collapse;margin-top:5px;" width="100%">
                            
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td align="left" colspan="2" style="font-family: Arial; font-size: 10pt; color: #222222">
                                    
                                    <asp:Literal ID="lDocId" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DocId"))%>' Visible="false"></asp:Literal>
                                    <asp:Literal ID="lstatusid" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "StatusId"))%>' Visible="false"></asp:Literal>
                                    <asp:Literal ID="lDocType" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DocType"))%>' Visible="false"></asp:Literal>
                                    <asp:Literal ID="lGroupAccessId" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "GroupAccessId"))%>' Visible="false"></asp:Literal>
                                    
                                </td>
                                <td style="font-size:8pt;">
                                <table>
                                <tr>
                                <td style="color:#222222;padding-left:5px">[<asp:Label ID="Literal1" runat="server" style="font-weight:bold" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "sactiondate"))%>' Tooltip='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "actiondate"))%>'></asp:label>]
                                </td>
                                <td>
                                <div class="xxx" style="width:200px;">
                                    <asp:LinkButton ID="lnkDoc" runat="server" ToolTip="Click here to view the routing status" OnClick="ViewDoc" class="menu">
                                    <asp:Image ID="Image2"  style="vertical-align:middle;margin-right:3px" runat="server" height="18px" width="15px" imageurl='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "doctypeimg"))%>'/>
                                    <asp:Label ID="lTitle" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "title"))%>'></asp:Label></asp:LinkButton>
                                    </div>
                                    
                                </td>
                                <td>
                                <asp:Label ID="lbstat" style="padding:  2px 4px 2px 4px;color:white;border:solid 1px #EEEEEE;background-color:#22D318" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "description"))%>'></asp:Label>&nbsp;&nbsp;by&nbsp;
                                    
                                    <asp:ImageButton ToolTip='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ApproverName"))%>' style="vertical-align:top" ID="imgProfile" OnClick="ViewDoc2" imageurl='<%#"images/avatar/" & Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "profilepic"))%>' Height="16px" Width="16px" runat="server"/>
                                    
                                </td>
                                </tr>
                                </table>
                                    
                                </td>                                
                            </tr>                            
                                                       
                            <tr>
                                <td colspan="2" height="8px"><%--<uc1:ucHr ID="cld" runat="server" />--%>                                   
                                    
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <asp:LinkButton ID="lbMoreApproval" Visible="false" runat="server" onmouseover="this.style.textDecoration = 'underline',this.style.color='#00005E'" onmouseout="this.style.textDecoration = 'none',this.style.color='blue'" style="color:blue;font-style:italic;font-family:arial;font-size:8pt;">More...</asp:LinkButton>
                    </asp:Panel>
                              </ContentTemplate>
</asp:UpdatePanel>
</div>