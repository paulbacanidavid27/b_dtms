<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlDocRouting.ascx.vb" Inherits="dms.UserControlDocRouting" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register src="ucConfirm.ascx" tagname="ucConfirm" tagprefix="uc" %>

 <asp:UpdatePanel ID="pnl" runat="server" UpdateMode="Conditional">
     
            <ContentTemplate>
            <asp:HiddenField runat="server" id="hfSeqNo" value="" />
                <asp:HiddenField ID="hfRefNo" runat="server" value="" />
            <asp:HiddenField runat="server" id="hfMaxSeqNo" value="" />
            <asp:HiddenField runat="server" id="hfTitle" value="" />
            <asp:HiddenField runat="server" id="hfMaxSeqNoToCancel" value="" />
            <asp:HiddenField runat="server" id="hfCCSeqNoToCancel" value=""  ClientIDMode="Static" />
            <asp:HiddenField runat="server" id="hfDocType" value="" /><asp:HiddenField ID="hfDocid" runat="server" />
            <asp:HiddenField runat="server" id="hfOfficeCode" value="" />
            <asp:HiddenField runat="server" id="hfStatusId" value="" />
                                                      <asp:Panel ID="pHeader" Visible="false" runat="server">
                                                      <table cellpadding="0" cellspacing="0" width="100%" border="0" style="border-collapse:collapse;border-top:solid 1px #D4D4D4;border-left:solid 1px #D4D4D4;border-right:solid 1px #D4D4D4">
                                                      <tr>
                                                      <td class="newtblheader"><img src="images/routing_icon.png" />&nbsp;Document Routing</td>
                                                      <td  class="newtblheader"  align="right" style="padding-right:2px" >                                                      
                                                            <asp:ImageButton ID="imgAddApprover" width="20px" Height="20px" tooltip="Add user to approve the document" runat="server" visible="true" ImageUrl="images/add_approver.png" onmouseout="this.src ='images/add_approver.png'" onmouseover="this.src ='images/add_approver.png'" />
                                                            <asp:ImageButton ID="imgAddCC" width="20px" Height="20px" tooltip="Add CC" runat="server" visible="true" ImageUrl="images/cc.png" onmouseout="this.src ='images/cc.png'" onmouseover="this.src ='images/cc.png'" />
                                                            <asp:ImageButton ID="imgSaveDueDate" width="15px" Height="18px" tooltip="Save updated Due Date" runat="server" ImageUrl="images/save3.png" onmouseout="this.src ='images/save3.png'" onmouseover="this.src ='images/save3.png'" visible="false"/>                                                      
                                                      </td>
                                                      </tr></table>
                                                                   
                                                                   </asp:Panel>
                                                           <div style="width:100%">
                                                                <asp:Label ID="lMsg" runat="server" Text=""></asp:Label>
                                                            </div>
                                                            <asp:Panel ID="pConfirm" runat="server" Visible="false">
                                                                    <table align="left">
                <tr>
                    <td align="left" style="font-family:Tahoma;font-size:11pt;color:#003399; padding:12px;">
                        <asp:Literal ID="ltext" runat="server" Text="Are you sure you want to cancel this routing? Please click OK to proceed."></asp:Literal>
                    </td>               
                      
                        
                        <td align="right">
                            <asp:Button ID="btContinue" runat="server" CssClass="btnsmall2" Text="OK" Width="40px" />&nbsp;<asp:Button ID="btSaveCancel" runat="server" CssClass="btnsmall2" Text="Cancel" />
                        </td>                        
                </tr>
                
                
                </table>
                                                            </asp:Panel>
                                                            <asp:Panel ID="Panel1" Visible="true" runat="server" DefaultButton="imgSendApprover">
                                                         <asp:Panel ID="pSearchCriteria" Visible="false" runat="server" DefaultButton="imgQuickApprover">
                                                          <table  border="0" width="100%" cellpadding="0" cellspacing="0" style="background-color:white;border-bottom:4px;border-collapse:collapse">
                                                          
                                                          <asp:Panel ID="pTitle" Visible="false" runat="server">
                                                                
                                                                    <tr>                                                                        
                                                                        <td class="tableheadersmall" colspan="4">                                                                            
                                                                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                                <tr>
                                                                                    <td>Document Routing</td>
                                                                                    <td align="right">
                                                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                                                            <ContentTemplate>
                                                                                                 <div id="prc" style="display:none ; font-style: italic;font-size:10px;color:Blue;">
                                                                                                                <img src="images/processing.gif" /> Processing...
                                                                                                            </div>
                                                                                                            <div id="pbtn" style="display: inline;">
                                                                                   
                                                                                                <asp:ImageButton ID="imgSendApprover" OnClientClick="document.getElementById('pbtn').style.display='none';document.getElementById('prc').style.display='inline';return true;" width="20px" Height="22px" tooltip="Route" runat="server" visible="false" ImageUrl="images/send.png"/>
                                                                                                <asp:ImageButton ID="imgQuickApprover"  OnClientClick="document.getElementById('pbtn').style.display='none';document.getElementById('prc').style.display='inline';return true" width="20px" Height="22px" tooltip="Route" runat="server" visible="false" ImageUrl="images/send.png"/>            
                                                                                                <asp:ImageButton ID="imgHelp" runat="server" ImageUrl="images/question.png" /><asp:ImageButton ID="imgClose" runat="server" ImageUrl="images/close_window.gif" />
            
                                                                                                </div>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                      </td>
                                                                                  </tr>
                                                                               </table>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                    
                                                          </asp:Panel>

                                                          <tr >
                                                            <td align="left">
                                                            <asp:UpdatePanel runat="server" ID="pnlRtInstruction" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                              <asp:Panel ID="RouteInstruction" runat="server" Visible="false">
                                                                <p class="helpnotes" ><b>Routing Instruction:</b><br />
                                                                    1. Use the search criteria section to select a user to approve the document.<br />                                                                    
                                                                    2. You can do partial search. Enter the first letter of the first name or last name of the approver. <br />
                                                                    3. Select the users from the search result to add the user from the list.<br />
                                                                    4. Click on (<img alt="" src="images/del.png" height="20" width="20px" />) button to remove the selected users from the list of additional approver.<br />
                                                                    5. Click the Send icon (<img alt="" src="images/send.png" height="20" width="20px" />) to finalize the process if you are adding more users to approve the document.<br /><br>
                                                                    Note: All the selected users as approvers will be sent an email notification.
                                                                </p>
                                                                </asp:Panel>
                                                                </ContentTemplate>
                                                             </asp:UpdatePanel>
                                                            </td>
                                                          </tr> 

                                                          <tr>
                                                                <td align="center">
                                                                <div style="width:100%;max-height:200px;overflow:auto">
                                                                <table border="0" cellpadding="0" cellspacing="0" style="width:100%;border:solid 1px gray;background-color:white;margin-bottom:2px;">
                                                                    <tr>
                                                                        
                                                                        <td align="left" colspan="3" class="tableHead2">Selected User for Document Routing</td>
                                                                        <td class="tableHead2" align="left">
                                                                            <asp:Literal ID="lOutS" runat="server" Text="Outgoing Status" Visible="false"></asp:Literal></td>
                                                                        <td class="tableHead2" align="left">Remarks</td>                                                                        
                                                                        <td class="tableHead2" align="center" title="Urgent">!</td>                                                                        
                                                                    </tr>                                                                    
                                                                    <tr>
                                                                        <td align="center" colspan="6">
                                                                            <asp:Label ID="lApproverSelected" runat="server" Text="Use the search criteria below to select a user." cssclass="helpnotes"></asp:Label></td>
                                                                            
                                                                    </tr>
                                                                    <asp:Repeater ID="rptList" runat="server" visible="false">
                                                                    <HeaderTemplate>                                                                        
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                    <tr >
                                                                        
                                                                        <td><asp:ImageButton ID="imgSelect" runat="server"  ImageUrl="images/box.png"  OnClick="fSelect"  visible="false"/>
                                                                        <asp:ImageButton ID="ImgSelected" runat="server"  ImageUrl="images/del.png" OnClick="fUnSelect2" ToolTip="Remove user from the list"/>
                                                                        </td>
                                                                        <td align="left" class="labelFreeForm">
                                                                        <asp:Literal ID="lApprover" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Approver"))%>'></asp:Literal>
                                                                        </td>
                                                                        <td align="left" class="labelFreeForm">
                                                                        <asp:Literal ID="lUserId" visible="false" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "UserId"))%>'></asp:Literal>
                                                                        <asp:Literal ID="lEmail" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Email"))%>'></asp:Literal>
                                                                        </td>
                                                                        <td align="left">
                                                                        <asp:DropDownList ID="dlOutgoingStatus" runat="server" Visible="True">                                                           
                                                                        </asp:DropDownList>
                                                                        </td>
                                                                        <%--<td>
                                                                            <asp:TextBox ID="tbDue" runat="server" Width="65px" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "tbDue"))%>'></asp:TextBox>
                                                                            <cc1:CalendarExtender ID="lColValExtender" runat="server" TargetControlID="tbDue"/>
                                                                        </td>--%>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="tbRem" runat="server" Width="200px" MaxLength="200" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "tbRem"))%>'></asp:TextBox>
                                                                            
                                                                        </td>
                                                                        <td><asp:ImageButton ID="imgUrgent2" runat="server"  ImageUrl="images/button/icon1.png" onclick="fUnUrgent"  visible="false"  ToolTip="Click to set as non-urgent!" Width="18px" Height="14px"/>
                                                                        <asp:ImageButton ID="imgUrgent" runat="server"  ImageUrl="images/button/icon2.png" onclick="fUrgent" ToolTip="Click to set this task as urgent!" Width="18px" Height="14px"/>
                                                                        </td>
                                                                    </tr>                                                                                                  
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>                                                                                                                                        
                                                                    </FooterTemplate>
                                                                    </asp:Repeater>
                                                                    </table>
                                                                   </div>
                                                                      
                                                                </td>
                                                            </tr>
                                                            
                                                          </table>
                                                         </asp:Panel>
                                                         </asp:Panel>
                                                         <asp:Panel ID="pSearchCriteria2" Visible="false" runat="server" DefaultButton="imgSearch">
                                                          
                                                                                                                    <table cellpadding="0" cellspacing="0" style="width:100%;background-color:white;border:solid 1px gray; ">
                                                                                                                    <tr><td align="left">
                                                         <table cellpadding="0" cellspacing="0" border="0">
                                                            
                                                            <tr><td class="labelFreeForm" align="left" style="padding-left:2px;">Search 
                                                                <asp:DropDownList ID="dlSearchType" runat="server" AutoPostBack="true"  CssClass="entryfld2">
                                                                <asp:ListItem Value="u">User</asp:ListItem>
                                                                <asp:ListItem Value="g">Group</asp:ListItem>
                                                                </asp:DropDownList> </td>
                                                                <td  align="left" style="padding-left:2px;">
                                                                    <asp:TextBox ID="txtBx" maxlength="150" runat="server" ClientIDMode="Static" Width="300px" placeholder="Enter user name..." cssclass="entryfld"></asp:TextBox>
                                                                    <asp:DropDownList ID="dlGroups" runat="server" Visible="false" CssClass="entryfld2" Width="300px" >
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td  align="left" style="padding-left:2px;">
                                                                    <asp:ImageButton ID="imgSearch" runat="server" imageurl="images/magni.png" ToolTip="Search Users"/>                                                                    
                                                                </td>
                                                                
                                                            </tr>
                                                         </table>
                                                         </td>
                                                                                                                    </tr>
                                                                                                                    </table>
                                                         
                                                         <div style="width:100%;max-height:100px;overflow:auto;background-color:white;margin:0;padding:0">
                                                             <center><asp:Label ID="lSearchMsg" cssclass="helpnotes" runat="server" Text="Label" Visible="false"></asp:Label></center>                                                          
                                                             <asp:Repeater ID="rptSub" runat="server" visible="false">
                                                                <HeaderTemplate>
                                                                    
                                                                    <table border="0"  cellpadding="0" cellspacing="0" style="width:100%;background-color:white;border-left:solid 1px gray; border-right:solid 1px gray;border-bottom:solid 1px gray;margin-bottom:2px;">
                                                                     <tr>
                                                                        <td class="tableHead2">
                                                                        <asp:ImageButton ID="imgSelecth" runat="server"  Width="16px" Height="16px" ImageUrl="images/selectall.png"  visible="true" ToolTip="Select All"/>
                                                                        <asp:ImageButton ID="ImgSelectedh" runat="server"  ImageUrl="images/selectall.png" OnClick="fUnSelectAll" ToolTip="Deselect All" visible="false"/>
                                                                        </td>
                                                                        <td align="left" class="tableHead2">User Name</td>
                                                                        <td align="left" class="tableHead2">Email</td>
                                                                        </tr>
                                                                    
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
                                                          <asp:Panel ID="pCopyFurnish" Visible="false" runat="server" style="width:100%;max-height:200px;overflow:auto">
                                                                     <table border="0" cellpadding="0" cellspacing="0" 
                                                                         style="width:100%;border:solid 1px gray;background-color:#DFDFDF; margin-bottom:2px;">
                                                                    <tr>
                                                                        
                                                                        <td align="left" colspan="2" class="tableHead2">Copy Furnish</td>
                                                                        <td align="right" class="tableHead2">
                                                                        <span id="drPrcSnd" style="display:none ; font-style: italic;font-size:10px;color:Blue;">
                                                                            <img src="images/processing.gif" /> Processing...
                                                                        </span>
                                                                        <span id="drBtnSnd" style="visibility: visible">
                                                                            <asp:ImageButton ID="imgSendCC" OnClientClick="statusbar('drBtnSnd','drPrcSnd')" width="20px" Height="22px" tooltip="Send" runat="server" visible="false" ImageUrl="images/send.png"/>
                                                                            <asp:ImageButton ID="imgCloseCC" runat="server" ImageUrl="images/close_window.gif" />
                                                                        </span>
                                                                        </td>
                                                                        
                                                                    </tr>                                                                    
                                                                    
                                                                    <asp:Repeater ID="rptCopy" runat="server" visible="false">
                                                                    <HeaderTemplate>                                                                        
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                    <tr >
                                                                        <td><asp:ImageButton ID="imgSelect" runat="server"  ImageUrl="images/box.png"  OnClick="fSelect"  visible="false"/>
                                                                        <asp:ImageButton ID="ImgSelected" runat="server"  ImageUrl="images/del.png" OnClick="fUnSelect2" ToolTip="Remove user from the list"/>
                                                                        </td>
                                                                        <td align="left" class="labelFreeForm">
                                                                        <asp:Literal ID="lApprover" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Approver"))%>'></asp:Literal>
                                                                        </td>
                                                                        <td align="left" class="labelFreeForm">
                                                                        <asp:Literal ID="lUserId" visible="false" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "UserId"))%>'></asp:Literal>
                                                                        <asp:Literal ID="lEmail" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Email"))%>'></asp:Literal>
                                                                        <asp:Literal ID="lExist" runat="server" Text='' Visible="false"></asp:Literal>
                                                                        </td>                                                                        
                                                                    </tr>                                                                                                  
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>                                                                                                                                        
                                                                    </FooterTemplate>
                                                                    </asp:Repeater>
                                                                    </table>
                                                                    </asp:Panel>          
                                                          <asp:Panel ID="pnlCC" Visible="False" runat="server" DefaultButton="imgCCSearch">
                                                          
                                                         <table cellpadding="0" cellspacing="0" 
                                                                  style="width:100%;background-color:#DFDFDF; border:solid 1px gray; ">
                                                         <tr><td align="left">
                                                         <table cellpadding="0" cellspacing="0" border="0">
                                                            
                                                            <tr><td class="labelFreeForm" align="left" style="padding-left:2px;">Search 
                                                                <asp:DropDownList ID="dlCCSearchType" runat="server" AutoPostBack="true"  
                                                                    CssClass="entryfld2">
                                                                <asp:ListItem Value="u">User</asp:ListItem>
                                                                <asp:ListItem Value="g">Group</asp:ListItem>
                                                                </asp:DropDownList> </td>
                                                                <td  align="left" style="padding-left:2px;">
                                                                    <asp:TextBox ID="txtCCBx" maxlength="150" runat="server" ClientIDMode="Static" Width="300px" placeholder="Enter user name..." cssclass="entryfld"></asp:TextBox>
                                                                    <asp:DropDownList ID="dlCCGroups" runat="server" Visible="false" 
                                                                        CssClass="entryfld2"  Width="300px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td  align="left" style="padding-left:2px;">
                                                                    <asp:ImageButton ID="imgCCSearch" runat="server" imageurl="images/magni.png" 
                                                                        ToolTip="Search Users"/>
                                                                </td>                                                                
                                                            </tr>
                                                         </table>
                                                         </td>
                                                         </tr>
                                                         </table>
                                                         
                                                         <div style="width:100%;max-height:100px;overflow:auto;background-color:#DFDFDF; margin:0;padding:0">
                                                             <center><asp:Label ID="lCCSearchMsg" cssclass="helpnotes" runat="server" 
                                                                     Text="Label" Visible="False"></asp:Label></center>                                                          
                                                             <asp:Repeater ID="rptCCSub" runat="server" visible="false">
                                                                <HeaderTemplate>
                                                                    
                                                                    <table border="0"  cellpadding="0" cellspacing="0" style="width:100%;background-color:#DFDFDF; border-left:solid 1px gray; border-right:solid 1px gray;border-bottom:solid 1px gray;margin-bottom:2px;">
                                                                     <tr>
                                                                        <td class="tableHead2">
                                                                        <asp:ImageButton ID="imgSelecth" runat="server"  Width="16px" Height="16px" ImageUrl="images/selectall.png"  visible="true" ToolTip="Select All"/>
                                                                        <asp:ImageButton ID="ImgSelectedh" runat="server"  ImageUrl="images/selectall.png" OnClick="fUnSelectAll" ToolTip="Deselect All" visible="false"/>
                                                                        </td>
                                                                        <td align="left" class="tableHead2">User Name</td>
                                                                        <td align="left" class="tableHead2">Email</td>
                                                                        </tr>
                                                                    
                                                                </HeaderTemplate>
                                                               
                                                                <ItemTemplate>
                                                                    <tr >
                                                                        <td><asp:ImageButton ID="imgSelect" runat="server"  ImageUrl="images/box.png"  OnClick="fSelectCC" tooltip="Add the user from the document routing list"/>                                                                        
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
                                                          <asp:Repeater ID="rpt" runat="server" Visible="True">
                                                                <HeaderTemplate>                                                                   
                                                                
                                                                    
                                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" style="border:solid 1px #D4D4D4;background-color:white;border-collapse:collapse">
                                                                </HeaderTemplate>
                                                                <ItemTemplate>                                                                    
                                                                <tr>
                                                                <td style="padding-left:3px;padding-right:3px;padding-bottom:3px;">
                                                                    <table id="row1" runat="server" width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-top:3px;border:solid 1px #D4D4D4;background-color:#D2E8C8;border-collapse:collapse">
                                                                    <tr>
                                                                        <td style="padding-left:5px;color:#696969;font-weight:bold;font-family:Verdana;font-size:10pt;">
                                                                            <asp:Image ID="imgUrg" runat="server"  ImageUrl="images/button/icon1.png" Visible="false"/>
                                                                            <asp:Literal runat="server" ID="ltask" Text="Task" visible="true"></asp:Literal></td>
                                                                            <td><asp:Label ID="lOutStatus" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "OutStatus"))%>' style="font-weight:normal;color:blue;font-style:italic;font-size:8pt;"/></td>
                                                                        <td style="padding-top:2px;padding-right:2px;text-align:right" >
                                                                        <asp:Literal ID="lSeqNo" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "SeqNo"))%>' Visible="false"></asp:Literal>
                                                                        <asp:Literal ID="lStatusId" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "StatusId"))%>' Visible="false"></asp:Literal>
                                                                        <asp:Literal ID="lApproverId" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ApproverId"))%>' Visible="false"></asp:Literal>
                                                                        <asp:Literal runat="server" ID="lApprover" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "approver"))%>' visible="false"></asp:Literal>
                                                                        <asp:Literal runat="server" ID="lAppEmail" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "email"))%>' visible="false"></asp:Literal>
                                                                        <asp:Literal runat="server" ID="lCC" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "cc"))%>' visible="false"></asp:Literal>
                                                                        <asp:Literal runat="server" ID="lUrgent" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "urgent"))%>' visible="false"></asp:Literal>
                                                                                                                                                        
                                                                            <%--<span style="color:#999999">Due Date:</span> <asp:Label ID="lDueDate1" runat="server" Text="["></asp:Label><asp:Label ID="lDueDate" runat="server" tooltip="Due Date" Text='<%#Server.HtmlEncode( IIf(DataBinder.Eval(Container.DataItem, "duedate")="01/01/1900","Date not set",cdate(DataBinder.Eval(Container.DataItem, "duedate")).toString("MMM d, yyyy") ) )%>' cssclass="dateform"/><asp:Label ID="lDueDate2" runat="server" Text="]" />&nbsp;&nbsp;&nbsp;
                                                                            <asp:TextBox ID="txDueDate" runat="server" Visible="false" maxlength="10" Width="65px" Text='<%#Server.HtmlEncode( DataBinder.Eval(Container.DataItem, "duedate"))%>' CssClass="entryfld"></asp:TextBox>--%>
                                                                            <%--<cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txDueDate" />--%>
                                                                            <span style="color:#999999">Sender:</span> <asp:Label ID="lSenderName" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Sendername"))%>' style="font-weight:normal;color:#696969;text-transform:capitalize;" />
                                                                            <asp:Literal ID="lSenderId" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Sender"))%>' visible="false"/>
                                                                            <asp:Literal ID="lOutStatusId" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "OutStatusId"))%>' visible="false"/>
                                                                            <%--<asp:ImageButton ID="imgCancelxx" ToolTip="Cancel Routing" runat="server"  ImageUrl="images/delete_doc.png" Visible="false" Onclick="CancelRouting"/>--%>
                                                                            <asp:ImageButton ID="imgCancel" ToolTip="Cancel Routing" runat="server"  ImageUrl="images/delete_doc.png" Visible="false"  OnClientClick="showWindow('dCancelRoute')"/>
                                                                            </td>
                                                                            </tr>           
                                                                            <tr>
                                                                            <td align="left" colspan="3" style="padding:5px;">
                                                                            
                                                                            <asp:Label ID="lRemarks" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Remarks"))%>' style="font-weight:bold;color:#222222"/>
                                                                            </td>
                                                                            </tr>                                   
                                                                                                          
                                                                            <tr>
                                                                            <td style="padding:5px;border-top:solid 1px #C0C0C0; border-top-color: #D4D4D4;" colspan="3">                                                                            
                                                                            <asp:Panel  ID="pAction" runat="server" DefaultButton="imgSend">           
                                                                            <span style="color:#222222;font-size:8pt;">Action:</span>
                                                                                    <asp:DropDownList ID="dlStatus" runat="server" Visible="false">                                                                                    
                                                                                    </asp:DropDownList>
                                                                                    <%--<asp:RadioButton ID="rbAck" Visible="false" runat="server" Text="Acknowledged" Checked="true" GroupName="rbAct"/> 
                                                                                    <asp:RadioButton Visible="false" ID="rbEval" runat="server" Text="For Evaluation" GroupName="rbAct"/>--%>
                                                                                    <asp:Label id="lbstat" runat="server" style=" letter-spacing:1px;  text-transform:capitalize;font-weight :bold;color:#58954D;">
                                                                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "statusdesc"))%></asp:Label>&nbsp;<asp:Label ID="lActionDate1" runat="server" Text="["></asp:Label><asp:Label ID="lActionDate" runat="server" tooltip="Action Date" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "actdate"))%>'  cssclass="dateform"/><asp:Label ID="lActionDate2" runat="server" Text="]" /><asp:Label runat="server" ID="lUser" Text='<%# "by: " & Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "approver"))%>' style="color:#222222"></asp:Label>
                                                                            
                                                                            <asp:Label ID="lbRemarks" runat="server" Text='Remarks: ' Visible="false"  style="background-color:transparent;color:#222222;font-size:8pt;" tooltip="User's remarks"/><asp:TextBox ID="txtComment" cssclass="entryfldw" runat="server" Visible="false" Width="340px" style="vertical-align:middle;"></asp:TextBox><asp:Label ID="lComment" runat="server" Text='<%# " - " & Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "comment"))%>' Visible="false"  style="background-color:transparent;font-style:italic" tooltip="User's remarks"/><asp:Label ID="lbAction" runat="server" visible="false" Text='' style="background-color:transparent;font-style:italic" tooltip="Click Acknowledged or For Evaluation button"/>
                                                                            <div id="prcsend" style="display:none ; font-style: italic;font-size:10px;color:Blue;">
                                                                                                                <img src="images/processing.gif" /> Processing...
                                                                                                            </div>
                                                                                                            <div id="pbtnsend" style="display: inline;">
                                                                                   <asp:ImageButton ID="ImgSend" OnClientClick="document.getElementById('pbtnsend').style.display='none';document.getElementById('prcsend').style.display='inline';return true;" style="vertical-align: middle " Height="20px" Width="20px" visible="false" runat="server" ToolTip="Submit" ImageUrl="images/save_send.png" />
                                                                                   </div>
                                                                                </asp:Panel>
                                                                                </td>

                                                                    </tr>                                                                    
                                                                    </table>
                                                                 </td>
                                                                 </tr>
                                                                                                                                     

                                                                </ItemTemplate>
                                                                
                                                                <FooterTemplate>
                                                                    
                                                                </table>    
                                                                </FooterTemplate>

                                                          </asp:Repeater>

                                                          <asp:Repeater ID="rptCC" runat="server" Visible="True">
                                                                <HeaderTemplate>                                                                   
                                                                
                                                                    
                                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" style="border:solid 1px #D4D4D4;background-color:white;border-collapse:collapse">
                                                                    <tr>
                                                                        <td style="background-color:Gray;font-size:10pt;padding:3px;color:White;font-weight:bold;font-family:Segoe ui;"> Copy Furnished</td>
                                                                    </tr>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>                                                                    
                                                                <tr>
                                                                <td style="padding-left:3px;padding-right:3px;padding-bottom:3px;">
                                                                    <table id="row1" runat="server" width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-top:3px;border:solid 1px #D4D4D4;background-color:#D2E8C8;border-collapse:collapse">
                                                                    <tr>
                                                                        <td style="padding-left:5px;color:#696969;font-weight:bold;font-family:Verdana;font-size:10pt;">                                                                            
                                                                        <asp:Literal runat="server" ID="ltask" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "approver"))%>' visible="true"></asp:Literal></td>
                                                                        <td style="padding-top:2px;padding-right:2px;text-align:right" >
                                                                        <asp:Literal ID="lSeqNo" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "SeqNo"))%>' Visible="false"></asp:Literal>
                                                                        <asp:Literal ID="lStatusId" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "StatusId"))%>' Visible="false"></asp:Literal>
                                                                        <asp:Literal ID="lApproverId" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ApproverId"))%>' Visible="false"></asp:Literal>
                                                                        <asp:Literal runat="server" ID="lApprover" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "approver"))%>' visible="false"></asp:Literal>
                                                                        <asp:Literal runat="server" ID="lAppEmail" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "email"))%>' visible="false"></asp:Literal>
                                                                        <asp:Literal runat="server" ID="lCC" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "cc"))%>' visible="false"></asp:Literal>
                                                                            
                                                                            <span style="color:#999999">Sender:</span> <asp:Label ID="lSenderName" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Sendername"))%>' style="font-weight:normal;color:#696969;text-transform:capitalize;" />
                                                                            <asp:Literal ID="lSenderId" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Sender"))%>' visible="false"/>
                                                                            <asp:ImageButton ID="imgCancelCC" ToolTip="Cancel Routing" runat="server" ImageUrl="images/delete_doc.png" Visible="false"  OnClientClick="showWindow('filtercc')"/>
                                                                            </td>
                                                                            </tr>           
                                                                            <tr>
                                                                                     <td align="left" colspan="2" style="padding:5px;">
                                                                                     <asp:Label ID="lComment" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "comment"))%>' Visible="false"  style="background-color:transparent;font-style:italic" tooltip="User's remarks"/>
                                                                                     </td>
                                                                            </tr>                      
                                                                                                          
                                                                            <tr>
                                                                            <td style="padding:5px;border-top:solid 1px #C0C0C0; border-top-color: #D4D4D4;" colspan="2">                                                                            
                                                                            <asp:Panel  ID="pAction" runat="server" DefaultButton="imgSend">           
                                                                            <span style="color:#222222;font-size:8pt;">Action: </span>
                                                                            <asp:DropDownList ID="dlStatus" runat="server" Visible="false">                                                                                    
                                                                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                                                                <asp:ListItem Text="Acknowledged" Value="3"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <%--<asp:RadioButton ID="rbAck" Visible="false" runat="server" Text="Acknowledged" Checked="true" GroupName="rbAct"/> 
                                                                            <asp:RadioButton Visible="false" ID="rbEval" runat="server" Text="For Evaluation" GroupName="rbAct"/>--%>
                                                                            <asp:Label id="lbstat" runat="server" style=" letter-spacing:1px;  text-transform:capitalize;font-weight :bold;color:#58954D;">
                                                                            <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "statusdesc"))%>
                                                                            </asp:Label>&nbsp;<asp:Label ID="lActionDate1" runat="server" Text="["></asp:Label>
                                                                            <asp:Label ID="lActionDate" runat="server" tooltip="Action Date" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "actdate"))%>'  cssclass="dateform"/>
                                                                            <asp:Label ID="lActionDate2" runat="server" Text="]" />
                                                                            <asp:Label runat="server" ID="lUser" Text='<%# "by: " & Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "approver"))%>' style="color:#222222" visible="false"></asp:Label>
                                                                            
                                                                            <asp:Label ID="lbRemarks" runat="server" Text='Remarks: ' Visible="false"  style="background-color:transparent;color:#222222;font-size:8pt;" tooltip="User's remarks"/><asp:TextBox ID="txtComment" cssclass="entryfld" runat="server" Visible="false" MaxLength="200" Width="340px" style="vertical-align:middle;"></asp:TextBox><asp:Label ID="lbAction" runat="server" visible="false" Text='' style="background-color:transparent;font-style:italic" tooltip="Click Acknowledged or For Evaluation button"/><asp:ImageButton ID="imgApprove" runat="server" style="vertical-align: middle " ImageUrl="images/approved.png" tooltip="Acknowledged" visible="false" Height="20px" Width="20px"/><asp:ImageButton ID="imgDeny" ToolTip="For Evaluation"  style="vertical-align: middle " ImageUrl="images/denied.png" runat="server" Height="20px" Width="20px" visible="false" />&nbsp;<asp:ImageButton ID="imgRoute" Height="20px" Width="20px" visible="false" runat="server" style="vertical-align: middle " ImageUrl="images/route.png" ToolTip="Route Document" />
                                                                            <div id="prcsendcc" style="display:none ; font-style: italic;font-size:10px;color:Blue;">
                                                                                                                <img src="images/processing.gif" /> Processing...
                                                                                                            </div>
                                                                                                            <div id="pbtnsendcc" style="display: inline;">
                                                                            <asp:ImageButton ID="ImgSend" OnClientClick="document.getElementById('pbtnsendcc').style.display='none';document.getElementById('prcsendcc').style.display='inline';return true;" style="vertical-align: middle " Height="20px" Width="20px" visible="false" runat="server" ToolTip="Submit" ImageUrl="images/save_send.png" />
                                                                            </div>
                                                                                </asp:Panel>
                                                                                </td>

                                                                    </tr>                                                                       
                                                                    </table>
                                                                 </td>
                                                                 </tr>
                                                                                                                                     

                                                                </ItemTemplate>
                                                                
                                                                <FooterTemplate>
                                                                    
                                                                </table>    
                                                                </FooterTemplate>

                                                          </asp:Repeater>
                                                          <%--end--%>
                                                          
                                                           
                              
                              </ContentTemplate>
            </asp:UpdatePanel>
