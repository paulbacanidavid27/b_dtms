<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucReceipt.ascx.vb" Inherits="dms.ucReceipt" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
            <asp:HiddenField ID="hfAuthor" runat="server" />
      <asp:Panel ID="pUpload1"  cssclass="Div1" runat="server" Visible="true">
</asp:Panel>
<asp:Panel ID="pUpload2" cssclass="Div2" runat="server" Visible="True">
<table border="0" cellspacing="0" cellpadding="0" style="width:100%;height:100%;border-collapse:collapse" >
    <tr>
    <td valign="middle" align="center">   
     <asp:Panel id="pMsg" runat="server" Visible="false" height="100%" width="800px" style="margin-top:3px;">
                            <table border="0" cellspacing="0" class="popuphdrbox" cellpadding="0" style="border-collapse:collapse;width:100%;">
                            <tr>
                             <td colspan="2">
                                <table cellspacing="0" class="popuphdr" cellpadding="0" border="0" style="width:100%">
                                            <tr height="30px">
                                            <td align="left" valign="middle" colspan="2">&nbsp;<img height="20px" width="18px" src="images/print.png" />&nbsp;Print Acknowledgement Receipt</td>
                                            
                                            <td  align="right" valign="top">
                                                <asp:ImageButton ID="imgCloseAck" runat="server" imageurl="images/close_window.gif" onmouseover="this.src='images/close_window.gif'"  onmouseout="this.src='images/close_window.gif'" width="18px" Height="18px"/>
                                            </td>
                                            </tr>
                                </table>                                   
                                    
                             </td>
                             </tr>
            <tr>
               <td align="center">
               
                                <asp:Panel ID="ackreceipt" runat="server" Visible="true" style="width:99%;overflow:auto;max-height:550px;">
                                <br />
                                
                                <table id="printArea" style="background-image:url('images/logo/watermark.png');background-repeat:no-repeat;background-position: center;height:100%;width:100%;font-family:Arial;font-size:10pt">
                                <tr>
                                <td colspan="2" align="center">
                                
                                <table cellpadding="0" cellspacing="0" style="width:100%">
                                <tr>
                                <td align="center">
                                    <table width="100%">
                                    <tr>
                                    <td width="40%"></td>
                                    <td align="center" width="100px"><%--<img alt="" src="images/logo/dbm.png" height="90px" width="90px"/>--%>
                                    
                                        <asp:Image ID="imgLogo" runat="server" Height="90px" Width="90px"/>
                                    </td>
                                    <td width="40%" align="center" valign="top"><div style="border:solid 1px #222222;font-size:8pt;width:200px;">In following-up, pls. cite DMS ref #<br /><asp:Label ID="lrefno" runat="server" style="font-size:12pt;font-family: Arial Black"></asp:Label></div></td>
                                    </tr>
                                    </table>                                    
                                    

                                    
                                </td>
                                </tr>
                                <tr>
                                    <td align="center" style="font-weight:normal;font-family:Book Antiqua;font-size:11pt;">REPUBLIC OF THE PHILIPPINES</td>
                                </tr>
                                <tr>
                                    <td align="center" style="font-weight:bold;font-family:Book Antiqua;font-size:13pt;padding:1px;">
                                    <asp:Literal ID="lTitle" runat="server" Text="DEPARTMENT OF BUDGET AND MANAGEMENT"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="font-weight:normal;font-family:Cambria;font-size:10pt;">
                                    <asp:Literal ID="lAddress" runat="server" Text="General Solano St, San Miguel, Manila"></asp:Literal></td>
                                </tr>
                                <tr>
                                    <td align="center" style="font-size:14px;font-weight:bold;padding:15px;">ACKNOWLEDGEMENT RECEIPT</td>
                                </tr>
                                </table>
                                </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="left" style="padding-top:5px;padding-bottom:15px;"><p style="text-indent:50px;text-align:justify; line-height:20px;">The <b><asp:Literal ID="lTitle2" runat="server" Text="Department of Budget and Management"></asp:Literal></b> hereby acknowledges the receipt of
your letter/request which has been uploaded to the DBM-Document Management System
and routed to the appropriate office/s with the following information:</p></td>
                                </tr>
                                <tr>
                                    <td align="left" style="padding-left:50px;padding-top:10px;" valign="top">Sender: </td>
                                    <td align="left" style="padding-top:10px;" valign="top">
                                        <asp:Literal ID="lSender" runat="server"></asp:Literal></td>
                                </tr>
                                <tr>
                                    <td align="left" style="padding-left:50px;padding-top:10px;" valign="top">Document Title: </td>
                                    <td align="left" style="padding-top:10px;vertical-align:top">
                                        <div style="width:500px;word-wrap:break-word;border:0px;">
                                        <asp:Literal ID="lackTitle" runat="server"></asp:Literal>
                                        </div></td>
                                </tr>
                                <tr>
                                    <td align="left" style="padding-left:50px;padding-top:10px;" nowrap valign="top">Document Reference No: </td>
                                    <td align="left" style="padding-top:10px;" valign="top">
                                        <asp:Literal ID="lackFilename" runat="server"></asp:Literal></td>
                                </tr>
                                <tr>
                                    <td align="left" style="padding-left:50px;padding-top:10px;" valign="top" >Date and Time Uploaded: </td>
                                    <td align="left" style="padding-top:10px;" valign="top">
                                        <asp:Literal ID="lackDate" runat="server"></asp:Literal></td>
                                </tr>
                                <tr>
                                    <td align="left" style="padding-left:50px;padding-top:10px;" valign="top">Uploaded By: </td>
                                    <td align="left" style="padding-top:10px;" valign="top">
                                        <asp:Literal ID="lCreatedBy" runat="server"></asp:Literal></td>
                                </tr>
                                <tr>
                                    <td align="left" style="padding-left:50px;padding-top:10px;" valign="top">Routed To: </td>
                                    <td align="left" style="padding-top:10px;" valign="top">
                                        <asp:Literal ID="lRoutedTo" runat="server"></asp:Literal></td>
                                </tr>
                                <tr>
                                    <td align="right" style="padding-left:50px;padding-top:10px;" valign="top">cc:</td>
                                    <td align="left" style="padding-top:10px;" valign="top"><asp:Literal ID="lCarbon" runat="server"></asp:Literal></td>
                                </tr>
                                <tr>
                                    <td align="left" style="padding-left:50px;padding-top:10px;" valign="top">Total no. of pages received: </td>
                                    <td align="left" style="padding-top:10px;" valign="top"><asp:Literal ID="lCopiesPages" runat="server"></asp:Literal></td>
                                </tr>
                                <asp:Panel ID="pNotes" runat="server" Visible="false">
                                <tr>
                                    <td align="left" style="padding-left:50px;padding-top:10px;" valign="top">Notes: </td>
                                    <td align="left" style="padding-top:10px;" valign="top">
                                    
                                        <asp:Literal ID="lRemarks" runat="server"></asp:Literal>
                                     
                                        </td>
                                </tr>
                                </asp:Panel>   
                                <tr>
                                    <%--<td colspan="2" align="left" style="padding-top:5px;padding-bottom:5px; line-height:20px;">--%>
                                    <td colspan="2" align="left" style="padding-top:20px;padding-bottom:30px; line-height:20px;">
<p style="text-indent:50px; text-align:justify; line-height:20px;">The determination of the completeness of the documentary requirements
submitted, if any, is subject to the evaluation of the technical person in
charge.</p>
<p style="text-indent:50px; line-height:20px;">This receipt is system generated and does not require signature.</p></td>
                                </tr>
                                <tr>
                                <td colspan="2" align="left" style="padding-top:5px">Received by:</td>
                                </tr>
                                <tr>
                                <td colspan="2" align="left" style="padding-top:10px">
                                    
                                    <asp:Image ID="imgBottomlogo" ImageUrl="images/logo/logo.png" runat="server" />
                                    </td>
                                </tr>
                                </table>
                                
                                </asp:Panel>
                        
                                </td></tr>
                                <tr>
                                <td colspan="2" align="center" style="background-color: #D4D4D4;padding:5px;">
                                <asp:Button ID="btPrint" runat="server" OnClientClick="printAck('printArea')" Text="Print Receipt" ToolTip="Print acknowledgement receipt"   CssClass="btnsmall2" style="margin-right:4px"/>
                                    <asp:Button ID="btAdd" runat="server" Text="Upload Document"   CssClass="btnsmall2" ToolTip="Upload Another document" style="margin-right:4px" visible="false"/>
                                    <asp:Button ID="btView" runat="server" Text="View Document"   CssClass="btnsmall2" ToolTip="View document" style="margin-right:4px" visible="false"/>
                                </td></tr>
                                </table>
                    </asp:Panel>        
    </td>
    </tr>
</table>
                    <cc1:DropShadowExtender ID="dse2" runat="server" TargetControlID="pMsg" Opacity=".5" Rounded="false" TrackPosition="true" />
       </asp:Panel>            
           