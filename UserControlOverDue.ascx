<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlOverDue.ascx.vb" Inherits="dms.UserControlOverDue" %>
<%--<div style="border:solid 1px #939393;background-color: #E9E9E9;width: 98%; margin-left: 1px">--%>
                    <asp:UpdatePanel ID="pnlPending" runat="server" UpdateMode="Conditional">
                       <ContentTemplate>
                      
                    <div class="hdrtable112">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr height="25px">
                                <td align="left"  class="tableHead6_"  style="font-family:verdana;border-bottom:solid 0px gray;font-weight:bold;font-size:14px;border-bottom:solid 1px #D4D4D4;color:RED">                                    
                                    Overdue Tasks</td>    <td align="right" style="font-family:verdana;border-bottom:solid 0px gray;font-weight:bold;font-size:14px;border-bottom:solid 1px #D4D4D4;color:RED"><asp:Image ID="Image2" runat="server" ImageUrl="images/task.png" height="25px" Width="20px"/>&nbsp;</td>                                
                                    
                            </tr>
                            <tr style="border-bottom:solid 0px gray;font-weight:bold;font-size:9px" colspan="2">
                                <td align="left">&nbsp;&nbsp;</td>
                            </tr>
                        </table>
                    </div>      
                    
                    <asp:Repeater ID="Repeater2" runat="server" visible="true">
                        <HeaderTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" width="100%">
                            
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td align="left" colspan="2" style="font-family: Arial; font-size: 10pt; color: #222222">
                                    <asp:ImageButton ID="ImgDetails"  ToolTip='View Approval Status of this document' runat="server" imageUrl="images/plus.jpg" onmouseover="this.src='images/plus.jpg'" onmouseout="this.src='images/plus.JPG'" />    
                                    <asp:ImageButton ID="ImgDetailsMinus" Visible="false"  ToolTip='Hide Approval Status of this document' runat="server" imageUrl="images/minus.jpg" onmouseover="this.src='images/minus.jpg'" onmouseout="this.src='images/minus.JPG'" />    
                                    <asp:Literal ID="lDocId" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DocId"))%>' Visible="false"></asp:Literal>
                                    <asp:Literal ID="lDocType" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DocType"))%>' Visible="false"></asp:Literal>
                                    <asp:Literal ID="lGroupAccessId" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "GroupAccessId"))%>' Visible="false"></asp:Literal>
                                    <asp:Literal ID="lFileName" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "FileName"))%>' Visible="false"></asp:Literal>
                                    
                               <%-- </td>
                                <td >--%>
                                    <asp:ImageButton ID="imgDocType" Tooltip='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "title"))%>' Onclick="ViewDoc2" style="vertical-align:middle" imageurl="" Height="16px" Width="14px" runat="server" />&nbsp;[<span style="font-weight:bold;font-size:8pt;"><asp:Label ID="lDue" runat="server" ToolTip='Due Date' Text='<%#Server.HtmlEncode( DataBinder.Eval(Container.DataItem, "duedate"))%>'></asp:label></span>]&nbsp;<asp:LinkButton ID="lnkDoc" runat="server" ToolTip="Click here to approve the document" OnClick="ViewDoc" class="menu"><asp:Label ID="lTitle" runat="server" Text='<%#Server.HtmlEncode(iif(DataBinder.Eval(Container.DataItem, "remarks")="",DataBinder.Eval(Container.DataItem, "title"),DataBinder.Eval(Container.DataItem, "remarks"))     )%>' Tooltip='<%#"Author: " & Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "userName"))%>'></asp:Label></asp:LinkButton>
                                    
                                </td>                                
                            </tr>                            
                            <tr>
                               
                                <td colspan="2">
                                        <asp:Repeater ID="rptApprovalStatus" runat="server" visible="false">
                                            <HeaderTemplate>
                                                <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;font-family:tahoma;font-size:9pt;background-color:white;border:solid 1px #D4D4D4;margin-left:1px;margin-right:1px;width:97%">
                                                    <tr>                                                        
                                                        <td  class="newtblhdr">Approver Name</td>
                                                        <td  class="newtblhdr">Status</td>                                                        
                                                        <td  class="newtblhdr">Comment</td>
                                                    </tr>
                                                    

                            
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    
                                                    <td style="padding-left:2px;">
                                                        <asp:Literal ID="lApproverName" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Approver"))%>'></asp:Literal>
                                                    </td>
                                                    <td style="padding-left:2px;">
                                                        <asp:Label ID="lStatus" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Statusdesc"))%>'  ToolTip='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "actdate"))%>'></asp:Label>
                                                    </td>                                                    
                                                    <td style="padding-left:2px;">
                                                       <asp:Literal ID="lComment" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "comment"))%>'></asp:Literal>
                                                    </td>
                                                </tr>      
                                                <tr>
                                                    <td class="tbldashed" colspan="3">                                                    
                                                    </td>
                                                    </tr>                                          
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                    <tr>
                                                    <td  class="dashremover" colspan="3">  
                                                    </td>
                                                    </tr>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>                                    

                                </td>
                            </tr>
                            <tr><td></td>
                                <td><asp:panel ID="pcomment" runat="server" Visible="false"> Comment: 
                                    <asp:TextBox ID="tbComment" runat="server"  CssClass="entryfld"></asp:TextBox></asp:panel></td>
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
                    <div>&nbsp;&nbsp;<asp:LinkButton ID="lbMoreApproval" Visible="false" runat="server" onmouseover="this.style.textDecoration = 'underline',this.style.color='#00005E'" onmouseout="this.style.textDecoration = 'none',this.style.color='blue'" style="color:blue;font-style:italic;font-family:arial;font-size:8pt;">More...</asp:LinkButton></div>
                    
                              </ContentTemplate>
</asp:UpdatePanel>
<%--
                </div>--%>