<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlCarbonCopy.ascx.vb" Inherits="dms.UserControlCarbonCopy" %>
  <%@ Register src="UserControlPager.ascx" tagname="UserControlPager" tagprefix="uc" %> 
 <%@ Register src="UserControlDocRouting.ascx" tagname="UserControlDocRouting" tagprefix="uc" %>
<%--<div style="border:solid 1px #939393;background-color: #E9E9E9;width: 98%; margin-left: 1px">--%>
<asp:Panel ID="pCarbonCopy" runat="server" cssclass="bordertask">         
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" 
                                        style="background-color:#D7F2FF">
                            <tr height="25px" >
                                <td width="25px" style="border-bottom:solid 1px #D4D4D4;">
                                    <asp:Image ID="Image2" runat="server" ImageUrl="images/task.png" height="16px" Width="20px" />
                                </td>
                                <td align="left"  style="padding-top:5px;font-family:arial;font-weight:bold;font-size:14px;border-bottom:solid 1px #D4D4D4;color:RED">                                    
                                    Copy Furnish</td>                                    
                                 <td align="right" style="padding-top:5px;border-bottom:solid 1px #D4D4D4;">
                                            <asp:UpdatePanel ID="pPager" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                            <asp:HiddenField ID="hfSortCol" runat="server" Value=""/>
                                            <asp:HiddenField ID="hfSortOrder" runat="server" Value="Desc"/>
                                            <asp:HiddenField ID="hfCurrent" runat="server" Value="1"/>
                                            <asp:HiddenField ID="hfTotalRows" runat="server" Value="0"/>
                                            <uc:UserControlPager ID="ucPager" runat="server" />
                                            </ContentTemplate>
                                            </asp:UpdatePanel>
                                    </td>   
                            </tr>
                            
                        </table>
                  <%--  </div> --%>     
                    <asp:UpdatePanel ID="pnlPending" runat="server" UpdateMode="Conditional">
                       <ContentTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" style="margin-left:10px;border-collapse: collapse" width="98%">
                                <tr style="font-style:italic;border-bottom:solid 1px #222222;">
                                    <td></td>
                                    <td><asp:LinkButton ID="lbSort1" runat="server" cssclass="sortcol" tooltip="Sort by Reference No" OnClick="sortColumnHeader">Reference No</asp:LinkButton><asp:Image ID="imgSort1" imageurl="images/asc.png" runat="server" visible="false"/></td>
                                    <td><asp:LinkButton ID="lbSort2" runat="server" cssclass="sortcol" tooltip="Sort by Subject" OnClick="sortColumnHeader">Subject</asp:LinkButton><asp:Image ID="imgSort2" imageurl="images/asc.png" runat="server" visible="false"/></td>
                                    <td><asp:LinkButton ID="lbSort3" runat="server" cssclass="sortcol" tooltip="Sort by Date Sent" OnClick="sortColumnHeader">Date Sent</asp:LinkButton><asp:Image ID="imgSort3" imageurl="images/desc.png" runat="server" visible="true"/></td>
                                    <td style="padding-left:4px"><asp:LinkButton ID="lbSort4" runat="server" cssclass="sortcol" tooltip="Sort by Sender" OnClick="sortColumnHeader">Sender</asp:LinkButton><asp:Image ID="imgSort4" imageurl="images/asc.png" runat="server" visible="false"/></td>                                    
                                    <%--<td><asp:LinkButton ID="lbSort5" runat="server" cssclass="sortcol" tooltip="Sort by (Outgoing Action)" OnClick="sortColumnHeader">(Outgoing Action)</asp:LinkButton><asp:Image ID="imgSort5" imageurl="images/asc.png" runat="server" visible="false"/> <asp:LinkButton ID="lbSort6" runat="server" cssclass="sortcol" tooltip="Sort by Remarks" OnClick="sortColumnHeader">Remarks</asp:LinkButton><asp:Image ID="imgSort6" imageurl="images/asc.png" runat="server" visible="false"/></td>--%>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td colspan="8" height="4px"></td>
                                </tr>
                                
                    <asp:Repeater ID="Repeater2" runat="server" visible="true" >
                        <HeaderTemplate>
                            
                        </HeaderTemplate>
                        <ItemTemplate>

                            <tr  id="rowColorMain" runat="server" style="font-family: Arial; font-size: 8pt; color: #222222">
                                <td valign="top" width="14px" align="center">
                                    <asp:Image ID="imgUrgent" height="17px" width="16px" ToolTip='Urgent' runat="server" imageUrl="images/button/icon1.png" onmouseover="this.src='images/button/icon1.png'" onmouseout="this.src='images/button/icon1.png'" visible="false" />
                                </td>
                                <td align="left" valign="top" width="125px"  style="padding:2px">
                                    <asp:ImageButton ID="ImgDetails"  ToolTip='View Approval Status of this document' runat="server" imageUrl="images/plus.jpg" onmouseover="this.src='images/plus.jpg'" onmouseout="this.src='images/plus.JPG'" />    
                                    <asp:ImageButton ID="ImgDetailsMinus" Visible="false"  ToolTip='Hide Approval Status of this document' runat="server" imageUrl="images/minus.jpg" onmouseover="this.src='images/minus.jpg'" onmouseout="this.src='images/minus.JPG'" />    
                                    <asp:Literal ID="lDocId" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DocId"))%>' Visible="false"></asp:Literal>
                                    <asp:Literal ID="lDocType" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DocType"))%>' Visible="false"></asp:Literal>
                                    <asp:Literal ID="lGroupAccessId" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "GroupAccessId"))%>' Visible="false"></asp:Literal>
                                    <asp:Literal ID="lFileName" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "FileName"))%>' Visible="false"></asp:Literal>                                                                        
                                    <asp:Literal ID="lRefno" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "refno"))%>' Visible="true"></asp:Literal>
                                    <asp:Literal ID="lseqno" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "seqno"))%>' Visible="false"></asp:Literal>
                                    <asp:Literal ID="lstatusid" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "statusid"))%>' Visible="false"></asp:Literal>
                                    <asp:Literal ID="lOutStatusId" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "outstatusid"))%>' Visible="false"></asp:Literal>
                                    <asp:Literal ID="lDocStatusId" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Docstatusid"))%>' Visible="false"></asp:Literal>
                                    <asp:Literal ID="lCC" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "cc"))%>' Visible="false"></asp:Literal>
                                    <asp:Literal ID="lUrgent" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "urgent"))%>' Visible="false"></asp:Literal>
                                    <asp:Label ID="lDue" runat="server" style="font-size:8pt" ToolTip='Due Date' Text='<%#Server.HtmlEncode( IIf(DataBinder.Eval(Container.DataItem, "duedate")="01/01/1900","Date not set",cdate(DataBinder.Eval(Container.DataItem, "duedate")).toString("MMM d, yyyy") ) )%>' Visible="false"></asp:label>
                                </td>
                                <td valign="top" style="padding-left:1px;width:auto">                                
                                    <div style="white-space:nowrap;overflow:hidden;width:250px;text-overflow:ellipsis;" Title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "title"))%>'>
                                    
                                    <asp:ImageButton ID="imgDocType"  Onclick="ViewDoc2" style="vertical-align:middle;" imageurl="" Height="16px" Width="14px" runat="server" />                                    
                                    <asp:LinkButton ID="lnkDoc" runat="server" OnClick="ViewDoc" cssclass="menusmall"><asp:Label ID="lTitle" style="font-size:8pt" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "title"))%>' Tooltip=''></asp:Label></asp:LinkButton>
                                    
                                    </div>
                                    
                                </td>
                                <td valign="top"  width="95px" style="padding-left:1px">
                                    <span id="pdue" runat="server">[<asp:Literal ID="lcdate" runat="server" Text='<%#Server.HtmlEncode( IIf(DataBinder.Eval(Container.DataItem, "createddate")="01/01/1900","Date not set",cdate(DataBinder.Eval(Container.DataItem, "createddate")).toString("MMM d, yyyy") ) )%>' Visible="true"></asp:Literal>]</span>
                                </td>
                                <td valign="top"  style="padding-left:4px;width:auto">
                                    <asp:Literal ID="lOrigDue" visible="false" runat="server" text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "duedate"))%>'></asp:Literal>
                                    <asp:Literal ID="lSender" runat="server" text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "sendername"))%>'></asp:Literal>
                                </td>
                                <%--<td valign="top"   width="20%" align="left" style="padding-left:1px;">
                                    <div style="white-space:nowrap;overflow:hidden;width:245px;text-overflow:ellipsis;" Title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "remarks"))%>'>
                                    <asp:Literal ID="lOutStatus" runat="server" text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "OutStatus"))%>'></asp:Literal>
                                    <asp:Literal ID="lremarks" runat="server" text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "remarks"))%>'></asp:Literal>
                                    </div>
                                </td>--%>
                                <td valign="top"  width="14px" align="center">    
                                    <asp:ImageButton ID="imgRoute"  width="12px" Height="12px" ToolTip='Quick Routing' runat="server" imageUrl="images/routing_icon.png" onmouseover="this.src='images/routing_icon.png'" onmouseout="this.src='images/routing_icon.png'" onclick="fQuickRouting" />                                        
                                </td>       
                                <td></td>                         
                            </tr>                            
                            <tr>
                            <td colspan="7">
                                <uc:UserControlDocRouting ID="ucDocRouting" runat="server" />
                            </td>
                            </tr>
                            <tr>
                               
                                <td colspan="7" style="padding-top:2px" align="right">
                                    <asp:Panel ID="pMsg" runat="server" Visible="false">
                                            <center><asp:Label ID="lMsg" runat="server" Text=""></asp:Label></center>
                                    </asp:Panel>
                                        
                                        
                                        
                                        <asp:Repeater ID="rptApprovalStatus" runat="server"  visible="false" OnItemDataBound="fDataBound">
                                            <HeaderTemplate>
                                                <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;font-family:tahoma;font-size:9pt;background-color:white;border:solid 1px #D4D4D4;margin-left:1px;margin-right:1px;width:97%">
                                                    <tr>                                                        
                                                        <td  class="tableHead2">Recipient/s Name</td>                                                        
                                                        <td  class="tableHead2">Action</td>                                                                                                                
                                                        <td  class="tableHead2">Comment</td>
                                                        <td  class="tableHead2">Outgoing Action</td>
                                                    </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr id="rowColor" style="color:#222222">
                                                    
                                                    <td style="padding-left:2px;">
                                                    <asp:Literal ID="lDocId" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DocId"))%>' Visible="false"></asp:Literal>
                                                    <asp:Literal ID="lAppid" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ApproverId"))%>' Visible="false"></asp:Literal>
                                                    <asp:Literal ID="lSeqNo" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "SeqNo"))%>' Visible="false"></asp:Literal>
                                                    <asp:Literal ID="lCCText" runat="server" Text='CC:'  Visible="false"></asp:Literal>
                                                        <asp:Literal ID="lApproverName" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Approver"))%>'></asp:Literal>
                                                    </td>
                                                    
                                                    <td style="padding-left:2px;">
                                                        <asp:Label ID="lStatus" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Statusdesc"))%>'></asp:Label>
                                                        <asp:Literal ID="lStatusID" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "StatusId"))%>'  Visible="false"></asp:Literal>
                                                        <asp:Literal ID="lDocStatusID" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DocStatusId"))%>'  Visible="false"></asp:Literal>
                                                        <asp:Literal ID="lCC" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "cc"))%>'  Visible="false"></asp:Literal>

                                                        
                                                        <asp:DropDownList ID="dlStatus" runat="server" Visible="false">                                                           
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="dlStatus2" runat="server" Visible="false">
                                                            <asp:ListItem></asp:ListItem>
                                                            <asp:ListItem Value="3">Acknowledged</asp:ListItem>                                                            
                                                        </asp:DropDownList>
                                                    </td>                               
                                                                         
                                                    <td style="padding-left:2px;">
                                                       <asp:Literal ID="lComment" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "comment"))%>'></asp:Literal>
                                                       <asp:Panel ID="pQuickAck" runat="server" DefaultButton="imgSave">
                                                        <asp:TextBox ID="tbComment" runat="server"  Visible="false" MaxLength="200"></asp:TextBox>
                                                        <div id="prc" style="display:none ; font-style: italic;font-size:10px;color:Blue;">
                                                                                                                <img src="images/processing.gif" /> Processing...
                                                                                                            </div>
                                                                                                            <div id="pbtn" style="display: inline;">
                                                       <asp:ImageButton ID="imgSave" OnClientClick="document.getElementById('pbtn').style.display='none';document.getElementById('prc').style.display='inline';return true;" style="vertical-align: middle" visible="false" height="20px" Width="20px" runat="server" ToolTip="Submit" ImageUrl="images/save_send.png" onclick="RouteDocument"/>
                                                       </div>
                                                               </asp:Panel>
                                                        
                                                    </td>
                                                    <td>
                                                        <asp:Literal ID="lOutStatusDesc" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "OutStatusDesc"))%>'  Visible="true"></asp:Literal>
                                                        <asp:Literal ID="lOutStatusId" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "OutStatusId"))%>'  Visible="false"></asp:Literal>
                                                    </td>
                                                </tr>      
                                                <tr>
                                                    <td class="tbldashed" colspan="4">                                                    
                                                    </td>
                                                    </tr>                                          
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                    <tr>
                                                    <td  class="dashremover" colspan="4">  
                                                    </td>
                                                    </tr>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>                                    
                                
                                </td>
                            </tr>
                            <tr>
                                <td colspan="7"><asp:panel ID="pcomment" runat="server" Visible="false"> Comment: 
                                    <asp:TextBox ID="tbComment" runat="server"  CssClass="entryfld"></asp:TextBox></asp:panel></td>
                            </tr>
                            <tr>
                                <td colspan="7" height="5px"><%--<uc1:ucHr ID="cld" runat="server" />--%>                                   
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            
                        </FooterTemplate>
                    </asp:Repeater>
                                 </table>     
                              </ContentTemplate>
</asp:UpdatePanel>
</asp:Panel>    
