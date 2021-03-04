<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucReceiptRoute.ascx.vb" Inherits="dms.ucReceiptRoute" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:HiddenField ID="hfAuthor" runat="server" />
<asp:Panel ID="pUpload1" CssClass="Div1" runat="server" Visible="true">
</asp:Panel>
<asp:Panel ID="pUpload2" CssClass="Div2" runat="server" Visible="True">
    <table border="0" cellspacing="0" cellpadding="0" style="margin-top:25px;width: 100%; height: 100%; border-collapse: collapse">
        <tr>
            <td valign="middle" align="center">
                <asp:Panel ID="pMsg" runat="server" Visible="false" Height="100%" Width="800px" Style="margin-top: 3px;">
                    <table border="0" cellspacing="0" class="popuphdrbox" cellpadding="0" style="border-collapse: collapse; width: 100%;">
                        <tr>
                            <td colspan="2">
                                <table cellspacing="0" class="popuphdr" cellpadding="0" border="0" style="width: 100%">
                                    <tr height="30px">
                                        <td align="left" valign="middle" colspan="2">&nbsp;<img height="20px" width="18px" src="images/print.png" />&nbsp;Print Routing Slip</td>

                                        <td align="right" valign="top">
                                            <asp:ImageButton ID="imgCloseAck" runat="server" ImageUrl="images/close_window.gif" onmouseover="this.src='images/close_window.gif'" onmouseout="this.src='images/close_window.gif'" Width="18px" Height="18px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Panel ID="ackreceipt" runat="server" Visible="true" Style="width: 99%; overflow: auto; max-height: 550px;">
                                    <br />
                                    <table id="printRouteArea" style="background-image: url('images/logo/watermark.png'); background-repeat: no-repeat; background-position: center; height: 100%; width: 100%; font-family: Arial; font-size: 11pt">
                                        <tr>
                                            <td colspan="4" align="center">
                                                <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                                    <tr>
                                                        <td align="center">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td width="40%"></td>
                                                                    <td align="center" width="100px">
                                                                        <asp:Image ID="imgLogo" runat="server" Height="90px" Width="90px" />
                                                                    </td>
                                                                    <td width="40%" align="center" valign="top">
                                                                        <div style="border: solid 1px #222222; font-size: 8pt; width: 200px;">
                                                                            In following-up, pls. cite DMS ref #<br />
                                                                            <asp:Label ID="lrefno" runat="server" Style="font-size: 12pt; font-family: Arial Black"></asp:Label>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" style="font-weight: normal; font-family: Book Antiqua; font-size: 11pt;">REPUBLIC OF THE PHILIPPINES</td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" style="font-weight: bold; font-family: Book Antiqua; font-size: 13pt; padding: 1px;">
                                                            <asp:Literal ID="lTitle" runat="server" Text="DEPARTMENT OF BUDGET AND MANAGEMENT"></asp:Literal>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" style="font-weight: normal; font-family: Cambria; font-size: 10pt;">
                                                            <asp:Literal ID="lAddress" runat="server" Text="General Solano St, San Miguel, Manila"></asp:Literal></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" style="font-size: 14px; font-weight: bold; padding: 15px;">ROUTING SLIP</td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="font-size:8pt;font-weight:bold;">DMS Reference No: </td>
                                            <td align="left"><asp:Literal ID="lRefNo2" runat="server"></asp:Literal></td>
                                            <td align="left" style="font-size:8pt;font-weight:bold;">Total No. of Pages Received: </td>
                                            <td align="left"><asp:Literal ID="lCopiesPages" runat="server"></asp:Literal></td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="font-size:8pt;font-weight:bold;">Uploaded By: </td>
                                            <td align="left"><asp:Literal ID="lCreatedBy" runat="server"></asp:Literal></td>
                                            <td align="left" style="font-size:8pt;font-weight:bold;">Uploaded Date and Time: </td>
                                            <td align="left"><asp:Literal ID="lUploadDate" runat="server"></asp:Literal></td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="font-size:8pt;font-weight:bold;">Notes: </td>
                                            <td align="left" colspan="3"><asp:Literal ID="lNotes" runat="server"></asp:Literal></td>
                                            
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <table width="100%" border="1" style="border-collapse:collapse;font-size:10pt;" cellpadding="5">
                                                    <tr>
                                                        <td colspan="2" style="font-weight:bold;">SENDER
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:Literal ID="lSender" runat="server"></asp:Literal>
                                                        </td>
                                                        
                                                    </tr>
                                                    <tr style="min-height:50px">
                                                        <td colspan="2" style="font-weight:bold;">DOCUMENT TITLE</td>
                                                        <td colspan="2" align="top">
                                                            
                                                                <asp:Literal ID="lackTitle" runat="server"></asp:Literal>
                                                           
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="font-weight:bold;">CC</td>
                                                        <td colspan="2">
                                                            
                                                                <asp:Literal ID="lCarbon" runat="server"></asp:Literal>
                                                            
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color:#cccccc">
                                                        <td style="font-weight:bold;width:70px;">DATE
                                                        </td>
                                                        <td style="font-weight:bold;width:80px;">FROM
                                                        </td>
                                                        <td style="font-weight:bold;width:90px;">TO</td>
                                                        <td style="font-weight:bold;">REMARKS</td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" style="width:70px;font-size:9pt;word-break:break-all;">
                                                            <asp:Literal ID="lackDate" runat="server"></asp:Literal>
                                                        </td>
                                                        <td valign="top" style="width:85px;font-size:9pt;word-break:break-all;">
                                                            <asp:Literal ID="lGroupID" runat="server"></asp:Literal>
                                                        </td>
                                                        <td valign="top" style="width:85px;font-size:9pt;word-break:break-all;">
                                                            <asp:Literal ID="lRoutedTo" runat="server"></asp:Literal>
                                                        </td>
                                                        <td valign="top">
                                                            <asp:Panel ID="pnlRemarks" runat="server" Visible="false" style="width:100%;word-break:break-all;font-size:9pt;">
                                                                <asp:Literal ID="lRemarks" runat="server"></asp:Literal>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr >
                                                        <td>&nbsp;</td><td></td><td></td><td></td>
                                                    </tr>                                                    
                                                    <tr>
                                                        <td>&nbsp;</td><td></td><td></td><td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td><td></td><td></td><td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td><td></td><td></td><td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td><td></td><td></td><td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td><td></td><td></td><td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td><td></td><td></td><td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td><td></td><td></td><td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td><td></td><td></td><td></td>
                                                    </tr>
                                                    <tr >
                                                        <td>&nbsp;</td><td></td><td></td><td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td><td></td><td></td><td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td><td></td><td></td><td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td><td></td><td></td><td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td><td></td><td></td><td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td><td></td><td></td><td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td><td></td><td></td><td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td><td></td><td></td><td></td>
                                                    </tr>                                                    
                                                    <tr>
                                                        <td>&nbsp;</td><td></td><td></td><td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td><td></td><td></td><td></td>
                                                    </tr>
                                                </table>
                                            </td>

                                        </tr>
                                       <tr>
                                           <td colspan="4" style="font-size:7pt;" align="right"><i>1st page</i></td>
                                       </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                         <tr>
                                            <td colspan="4" align="center" style="background-color: #D4D4D4; padding: 5px;">
                                                
                                                <asp:Button ID="btPrintRoute" OnClientClick="printAck('printRouteArea')" runat="server" Text="Print Routing Slip" ToolTip="Print Routing Slip" CssClass="btnsmall2" Style="margin-right: 4px" />
                                                <asp:Button ID="btAdd" runat="server" Text="Upload Document" CssClass="btnsmall2" ToolTip="Upload Another document" Style="margin-right: 4px" Visible="false" />
                                                <asp:Button ID="btView" runat="server" Text="View Document" CssClass="btnsmall2" ToolTip="View document" Style="margin-right: 4px" Visible="false" />
                                            </td>
                                        </tr>
                    </table>
                    </asp:Panel>
                </td>
            </tr>
                </table>
                    <cc1:DropShadowExtender ID="dse2" runat="server" TargetControlID="pMsg" Opacity=".5" Rounded="false" TrackPosition="true" />
                </asp:Panel>
                