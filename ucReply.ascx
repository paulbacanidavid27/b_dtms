<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucReply.ascx.vb" Inherits="dms.ucReply" %>
<%@ Register src="ucCheckBox.ascx" tagname="ucBox" tagprefix="uc" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>            
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="HTMLEditor" %>
      <asp:Panel ID="pReply1"  cssclass="Div1" runat="server" Visible="true">
</asp:Panel>
<asp:Panel ID="pReply" cssclass="Div2" runat="server" Visible="True">
<asp:TextBox ID="tbAdditionalDoc1" runat="server" Visible="false"></asp:TextBox>
<asp:TextBox ID="tbAdditionalDoc5" runat="server" Visible="false"></asp:TextBox>
<asp:TextBox ID="tbAdditionalDoc2" runat="server" Visible="false"></asp:TextBox>
<asp:TextBox ID="tbAdditionalDoc3" runat="server" Visible="false"></asp:TextBox>
<asp:TextBox ID="tbAdditionalDoc4" runat="server" Visible="false"></asp:TextBox>
<table border="0" cellspacing="0" cellpadding="0" style="width:100%;height:100%;border-collapse:collapse" >
    <tr>
    <td valign="middle" align="center">   
     <asp:Panel id="pMsgx" runat="server" Visible="true" height="100%" width="800px">
     <table cellspacing="0" class="popuphdr" cellpadding="0" border="0" style="width:100%">
                                            <tr height="30px">
                                            <td align="left" valign="middle" colspan="2">&nbsp;<img height="20px" width="18px" src="images/print.png" />&nbsp;Print 
                                                Reply Document</td>
                                            
                                            <td  align="right" valign="top">
                                                <asp:ImageButton ID="imgCloseAck" runat="server" 
                                                    imageurl="images/close_window.gif" 
                                                    onmouseover="this.src='images/close_window.gif'"  
                                                    onmouseout="this.src='images/close_window.gif'" width="16px" Height="18px"/>
                                            </td>
                                            </tr>
                                </table>                                   
     <asp:UpdatePanel runat="server" ID="pSaveInfo" UpdateMode="Conditional" >
            <ContentTemplate>
<asp:HiddenField ID="hnextid" Value="" runat="server" />
<asp:HiddenField ID="hprevid" Value="" runat="server" />
<asp:HiddenField ID="hcurrid" Value="" runat="server" />
                            <table border="0" cellspacing="0" class="popuphdrbox" cellpadding="0" style="border-collapse:collapse;width:100%;">
                            <tr>
                             <td colspan="2">
                                
                                    
                             </td>
                             </tr>
            <tr>
               <td align="center" style="padding-left:20px;">
                    
              
                                <asp:Panel ID="pnlRepDoc" ClientIDMode="Static" runat="server" Visible="true" style="width:100%;overflow:auto;max-height:550px;margin:0px;padding:0px;">
                                <table border="0" cellspacing="0" id="printArea" style="background-image:url('images/logo/watermark.png');background-repeat:no-repeat;background-position: center;height:100%;width:100%;font-family:Arial;font-size:10pt;color:Black;margin-top:0px;">
                                <tr>
                                <td align="center">
                                
                                <table border="0" cellspacing="0" style="width:100%">
                                <tr>
                                    <td align="center" style="margin:0px;padding:0px;">                                                                    
                                        <%--<img alt="" src="images/logo/dbm.png" height="90px" width="90px"/>--%>
                                        <asp:Image ID="imgLogo" runat="server" Height="90px" Width="90px"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="font-weight:normal;font-family:Book Antiqua;font-size:11pt;padding:0px;">REPUBLIC OF THE PHILIPPINES</td>
                                </tr>
                                <tr>
                                    <td align="center" style="font-weight:bold;font-family:Book Antiqua;font-size:13pt;padding:1px;"><asp:Literal ID="lTitle" runat="server" Text="DEPARTMENT OF BUDGET AND MANAGEMENT"></asp:Literal></td>
                                </tr>
                                <tr>
                                    <td align="center" style="font-weight:normal;font-family:Cambria;font-size:10pt;padding:0px;margin:0px">
                                        <asp:Literal ID="lAddress" runat="server" Text="General Solano St, San Miguel, Manila"></asp:Literal></td>
                                </tr>                                    
                                <tr>
                                    <td align="center" style="font-weight:bold;padding:10px;padding:5px;font-size:10pt">REPLY DOCUMENT</td>
                                </tr>
                                </table>
                                </td>
                                </tr>
                                <tr>
                                    <td align="right" style="font-weight:bold;">DMS Reference No: 
                                        <asp:TextBox ID="tbRefNo" runat="server" style="width:150px;border: none;border-bottom: solid 1px #222222;" MaxLength="50"></asp:TextBox></td>
                                </tr>
                                <tr>
                                <td align="left" style="padding:5px;padding-left:0px;"> 
                                    <table border="0">
                                    <tr>
                                    <td align="left" style="font-weight: normal;padding:3px;">Date:</td> 
                                    <td><asp:TextBox ID="tbReplyDate" runat="server" style="border: none;border-bottom: solid 1px #222222;padding-left:10px;padding-right:10px;" MaxLength="10"></asp:TextBox>
                                    <cc1:CalendarExtender ID="cl0" runat="server" TargetControlID="tbReplyDate"/>
                                        <asp:Literal ID="ldocid" runat="server" Visible="false"></asp:Literal>
                                        <asp:Literal ID="Literal1" runat="server" Visible="false"></asp:Literal>
                                        <asp:Literal ID="lPby" runat="server" Visible="false"></asp:Literal>
                                        <asp:Literal ID="lPdate" runat="server" Visible="false"></asp:Literal>
                                        <asp:Literal ID="lreplyid" runat="server" Visible="False"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="font-weight: normal;padding:3px;">Client Agency: </td>
                                        <td><asp:TextBox ID="tbClientAgency" runat="server" style="border: none;border-bottom: solid 1px #222222;padding-left:10px;padding-right:10px;" 
                                            MaxLength="150" Width="500px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="left" style="font-weight: normal;padding:3px;">Address: </td>
                                        <td><asp:TextBox ID="tbAddress" runat="server" style="border: none;border-bottom: solid 1px #222222;padding-left:10px;padding-right:10px;" MaxLength="150" 
                                            Width="500px"></asp:TextBox></td>
                                </tr>
                                    </table>
                                </td></tr>                               
                                <tr>
                                    <td align="left" style="padding-left:3px;padding-top:5px;">Dear <b>Sir/Madam:</b></td>
                                </tr>
                               
                                    <tr>
                                        <td align="left" style="padding-left:3px;padding-top:5px;">
                                            <asp:TextBox ID="tbThisPertains" visible="false" runat="server" cssclass="entryfld2" 
                                                MaxLength="200" Width="650px" textmode="MultiLine" Rows="2" Text="This pertains to the request submitted to this Office dated"></asp:TextBox>
                                            <asp:Literal ID="lThisPertains" runat="server" Text="This pertains to the request submitted to this Office dated"></asp:Literal>
                                            <asp:TextBox ID="tbSubmittedDate" runat="server" style="border: none;border-bottom: solid 1px #222222;padding-left:10px;padding-right:10px;" 
                                                MaxLength="10" Width="90px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="cl1" runat="server" 
                                                TargetControlID="tbSubmittedDate" />
                                            
                                            <asp:TextBox ID="tbWhichWas"  visible="false"  runat="server" cssclass="entryfld2" 
                                                MaxLength="200" Width="150px" Text="which was received on"></asp:TextBox>
                                            <asp:Literal ID="lWhichWas" runat="server" Text="which was received on"></asp:Literal>
                                            <asp:TextBox ID="tbReceivedDate" runat="server" style="border: none;border-bottom: solid 1px #222222;padding-left:10px;padding-right:10px;" 
                                                MaxLength="10" Width="90px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="cl2" runat="server" 
                                                TargetControlID="tbReceivedDate" />
                                            .</td>
                                    </tr>
                                <tr>
                                    <td align="left">
                                    <table cellpadding="5px">
                                    <tr>
                                    <td valign="middle"><uc:ucBox ID="imgcbxAdditional" runat="server" /></td>
                                    <td valign="middle" style="font-weight:bold;">REQUIRE ADDITIONAL SUPPORTING DOCUMENTS</td> 
                                    </tr>    
                                    </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="padding:5px">
                                        <asp:TextBox ID="tbInOrder" visible="false" runat="server" cssclass="entryfld2" 
                                             textmode="MultiLine" Rows="3"   MaxLength="300" Width="650px" Text="In order for us to proceed with the processing of the request, may we request for submission of the following additional documents (attach additional list if necessary):"></asp:TextBox>
                                             <asp:Literal ID="lInOrder" runat="server" Text="In order for us to proceed with the processing of the request, may we request for submission of the following additional documents (attach additional list if necessary):"></asp:Literal>
                                        </td>
                                </tr>
                                <tr>
                                    <td align="left" style="padding-left:20px;padding-bottom:2px;">
                                        <HTMLEditor:Editor ID="EditorAddDoc" runat="server" 
        Height="300px" 
        Width="700px"
        AutoFocus="true"
/>
                                        <asp:Literal ID="pContentAddDoc" runat="server" Visible="false"> </asp:Literal>
                            </td>
                                </tr>
                                <tr>
                                    <td align="left" style="padding:5px">
                                        <asp:TextBox ID="tbPleaseIndicate" visible="false" runat="server" cssclass="entryfld2" 
                                             textmode="MultiLine" Rows="3"   MaxLength="300" Width="650px" Text="Please indicate the DMS Reference Number in the cover page/transmittal sheet and submit the same to our <<office>> not later than"></asp:TextBox>
                                        <asp:Literal ID="lPleaseIndicate" runat="server" text="Please indicate the DMS Reference Number in the cover page/transmittal sheet and submit the same to our Administrative Service, Central Records Division not later than"></asp:Literal>                                       
                                         <asp:TextBox ID="tbSubmitDate" runat="server" style="border: none;border-bottom: solid 1px #222222;padding-left:10px;padding-right:10px;width:90px;" 
                                            MaxLength="10"></asp:TextBox><cc1:CalendarExtender ID="CalendarExtender1" runat="server" 
                                                TargetControlID="tbSubmitDate" />
                                        .</td>
                                </tr>                                   
                                <tr>
                                    <td align="left" valign="middle" style="padding-top:2px;padding-bottom:2px;">
                                        <table cellpadding="4px">
                                        <tr>
                                        <td valign="middle"><uc:ucBox id="imgcbxReturn" runat="server"></uc:ucBox></td>
                                        <td valign="middle" style="font-weight:bold;">RETURN THE DOCUMENTS TO CLIENT AGENCY CONCERNED</td> 
                                        </tr>    
                                        </table>
                                        
                                     </td>
                                </tr>
                                <tr>
                                    <td align="left" valign="middle" style="padding-left:70px;padding-top:2px;padding-bottom:2px;">
                                        <table cellpadding="4px">
                                        <tr>
                                        <td valign="middle"><uc:ucBox ID="imgcbxComplete" runat="server" /></td>
                                        <td valign="middle" style="font-weight:bold;">COMPLETE</td>
                                        <td valign="middle"><uc:ucBox ID="imgcbxPartial" runat="server" /></td>
                                        <td valign="middle" style="font-weight:bold;">PARTIAL</td>
                                        <td></td><td valign="middle"><b>No of Pages:</b></td>
                                        <td valign="middle"><asp:TextBox ID="tbNoPages" runat="server" Text="" ToolTip="Input number here" style="border: none;border-bottom: solid 1px #222222;"></asp:TextBox>
                                        </td>
                                        </tr>
                                        </table>                                                                                     
                                    </td>
                                </tr>
                               
                                    <tr>
                                        <td align="left" valign="middle" style="padding:7px">
                                            <asp:TextBox ID="tbBasedOn" visible="false" runat="server" class="entryfld2"
                                             textmode="MultiLine" Rows="2"   MaxLength="300" Width="650px" Text="Based on our review, it is necessary to return the documents you have submitted due to the following reason:"></asp:TextBox>
                                            <asp:Literal ID="lBasedOn" runat="server" Text="Based on our review, it is necessary to return the documents you have submitted due to the following reason:"></asp:Literal>
                                        </td>
                                    </tr>
                                <tr>
                                    <td align="left" style="padding-left:50px;" valign="middle">
                                        <table cellpadding="5px">
                                        <tr>
                                        <td valign="middle"><uc:ucBox ID="imgcbxDeficiency" runat="server" /></td>
                                        <td valign="middle">Deficiency In The Document Submitted</td>
                                        </tr>
                                        </table>
                                     </td>
                                </tr>
                                <tr>
                                    <td align="left" style="padding-left:50px;">
                                        Specifics (attach additional documents if necessary):</td>
                                </tr>
                                <tr>
                                    <td align="left" style="padding-left:55px;padding-bottom:2px;">
                                        <asp:TextBox ID="tbSpecificDoc1" runat="server" Width="688px" MaxLength="120" visible="false"
                                            style="border: none;border-bottom: solid 1px #222222;padding-left:10px;padding-right:10px;"></asp:TextBox>
                                            <asp:TextBox ID="tbSpecificDoc2" runat="server" visible="false" Width="688px" MaxLength="120" 
                                            style="border: none;border-bottom: solid 1px #222222;padding-left:10px;padding-right:10px;"></asp:TextBox>
                                            <asp:TextBox ID="tbSpecificDoc3" runat="server" visible="false" style="border: none;border-bottom: solid 1px #222222;padding-left:10px;padding-right:10px;" 
                                            MaxLength="120" Width="688px"></asp:TextBox>
                                            <HTMLEditor:Editor ID="EditorSpecific" runat="server" Height="100px" Width="650px" AutoFocus="true" />
                                            <asp:Literal ID="pContentSpecific" runat="server" Visible="false"> </asp:Literal>
                                    </td>
                                </tr>                                       
                                <tr>
                                    <td align="left" valign="middle" align="left" style="padding-left:50px;">
                                         <table cellpadding="5px">
                                            <tr>
                                            <td valign="middle"><uc:ucBox ID="imgcbxWithdrawal" runat="server" /></td>
                                            <td>Withdrawal of Request</td><td>&nbsp;&nbsp;&nbsp;Reference:</td>
                                            <td><asp:TextBox ID="tbWReferenceNo" runat="server" Text="" style="border: none;border-bottom: solid 1px #222222;padding-left:10px;padding-right:10px;"></asp:TextBox></td>
                                            </tr>
                                         </table>
                                    </td>
                                </tr>
                                 
                                 <tr>
                                    <td align="left" style="padding-left:55px;padding-bottom:2px;">
                                         <HTMLEditor:Editor ID="EditorWithdrawal" runat="server" AutoFocus="true" 
                                             Height="100px" Width="650px" />
                                             <asp:Literal ID="pContentWithdrawal" runat="server" Visible="false"> </asp:Literal>
                                    </td>
                                </tr>
                                    <tr>
                                        <td align="left" style="padding:7px;padding-left:0px;" valign="middle">
                                            Thank you.
                                        </td>
                                    </tr>
                                 <tr>
                                    <td align="left" valign="middle" style="padding:7px;padding-left:0px;">
                                         <asp:TextBox ID="tbBureauHead" runat="server" style="border: none;border-bottom: solid 1px #222222;padding-left:10px;padding-right:10px;" Width="300px"></asp:TextBox>
                                    </td>
                                </tr>                         
                                    <tr>
                                        <td align="left" valign="middle">
                                            <b>Head of Bureau/Service/Office</b>
                                        </td>
                                    </tr>
                                </table>
                                
                                </asp:Panel>
                        
                                </td></tr>
                                <tr><td>
                                
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                <td align="left" style="background-color: #D4D4D4;padding:5px;">
                                
                                <asp:Button ID="btPrint" runat="server" OnClientClick="printAck('printArea')" 
                                        Text="Print Reply" ToolTip="Print acknowledgement receipt"   
                                        CssClass="btnsmall2" style="margin-right:4px" visible="false"/>
                                   <asp:Button ID="btPost" runat="server" 
                                        Text="Save Info" ToolTip="Save Info In Order to Print the Document Reply"  
                                        CssClass="btnsmall2" style="margin-right:4px"  visible="false" />
                                        </td>
                                        <td align="right" style="background-color: #D4D4D4;padding:5px;">
                                    <asp:Button ID="btEditReply" runat="server" Text="Edit Reply Content"   CssClass="btnsmall2" 
                                        ToolTip="Edit Document Reply Content" style="margin-right:4px;" 
                                        visible="true"/>
                                    <asp:Button ID="btSaveReply" runat="server" Text="Save Reply Changes"   CssClass="btnsmall2" 
                                        ToolTip="Save Changes to the Document Reply Content" style="margin-right:4px" 
                                        visible="false"/>
                                    <asp:Button ID="btEmailReply" runat="server" Text="Email Reply"   
                                        CssClass="btnsmall2" ToolTip="View document" style="margin-right:4px" 
                                        visible="false"/>
                                        <asp:Label ID="lmsg" runat="server" CssClass="msg_red"></asp:Label>
                                </td>
                                <td align="right" style="background-color: #D4D4D4;padding:5px;">
                                    <asp:ImageButton ID="imgLeft" runat="server" visible="false" ImageUrl="images/larrow_h.png" /><asp:ImageButton ID="imgRight"
                                        runat="server"  visible="false" ImageUrl="images/rarrow_h.png"/>
                                </td>
                                </tr>
                                </table>
                       
                                </td></tr>
                                </table>
                                         </ContentTemplate>
     </asp:UpdatePanel>
                    </asp:Panel>        
    </td>
    </tr>
</table>
           
       </asp:Panel>            
           