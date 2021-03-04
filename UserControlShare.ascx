<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlShare.ascx.vb" Inherits="dms.UserControlShare" %>
<%@ Register src="UserControlEmail.ascx" tagname="UserControlEmail" tagprefix="uc" %>    
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Panel ID="PanelEmail1" cssclass="Div1" runat="server" Visible="True"></asp:Panel>
<asp:Panel ID="PanelEmail2" cssclass="Div2" runat="server" Visible="True">
            
<table cellspacing="0" cellpadding="0" style="width:100%;height:100%;">
<tr>
    <td valign="middle" align="center">                
            <asp:Panel id="PanelEmail3" runat="server" Visible="true" style="width:620px;" defaultbutton="BtSend">
            
    
            <table border="0" cellspacing="0" cellpadding="0" class="popuphdrbox" style="border-collapse:collapse;width:100%;">

            <tr>
            <td>
                <table cellspacing="0" class="popuphdr" cellpadding="0" border="0" style="width:100%">
                    <tr height="30px">
                        <td align="left" valign="middle" colspan="2">&nbsp;<img height="20px" width="18px" src="images/email.png" />&nbsp;Share Document</td>
                                            
                        <td  align="right" valign="top">
                            <asp:ImageButton ID="imgClose" runat="server" imageurl="images/close_window.gif" onmouseover="this.src='images/close_window.gif'"  onmouseout="this.src='images/close_window.gif'" width="18px" Height="18px"/>
                        </td>
                    </tr>
                </table>
            </td>
            </tr>
            <tr>
                <td align="center">
                
                <!--start 2//-->
                <asp:Panel ID="PaneEmaill5" runat="server" Visible="True"> 
                <table >
                <%--<tr>
                    <td colspan="2" style="border-bottom:solid 1px gray" align="left">
                    
                    <p class="helpnotes"  ><b>Email Instruction:</b><br />                                                                    
                                                                    1. Recipient - document can be sent to more than one recipient, separate each receipient email with comma. <br />
                                                                    2. Subject - Enter email subject. This is optional.<br />
                                                                    3. Provide details in the body section of the email. <br />
                                                                    4. Click Send button to send the email to the designated recipient(s). <br />                                                                    
                                                                </p>
                                                                
                    </td>
                </tr>                --%>
                <tr>
                <td colspan="2" align="left">
                    <table width="100%" style="border-collapse:collapse" border="0" cellpading="0" cellspacing="0">
                    <tr>
                            <td colspan="2" valign="bottom"  class="labelFreeForm2">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="labelFreeForm2">From</td><td><asp:Label ID="lblFrom" runat="server" Text=""></asp:Label></td>
                        </tr>
                        
                        <tr>
                            <td class="labelFreeForm2" width="100px" align="top">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
                                    <ContentTemplate>
                            Recipient&nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton ID="imgUserEmail" runat="server" ImageUrl="images/user3.png" style="vertical-align:middle"/>
                            </ContentTemplate>
                                    </asp:UpdatePanel>    
                            </td>
                            <td>
                                <asp:UpdatePanel ID="pnlrecipient" runat="server" UpdateMode="Conditional"><ContentTemplate><asp:TextBox ID="tbRecipient" cssclass="entryfldw" runat="server" Width="500px"></asp:TextBox>
                                <cc1:autocompleteextender runat="server" ID="acomplete" TargetControlID="tbRecipient"
                                                 ServiceMethod="getEmail" ServicePath="getUser.asmx" CompletionInterval="1000" EnableCaching="true"
                                                  MinimumPrefixLength="1" 
                                                 completionsetcount="25"  DelimiterCharacters=","  /></ContentTemplate></asp:UpdatePanel>
                            </td>
                            
                        </tr>
                        
                                    
                                    
                        <tr>
                            <td></td>
                            <td>            
                            <asp:UpdatePanel ID="pemail" runat="server" UpdateMode="Conditional" >
                                    <ContentTemplate>
                            <asp:Panel id="pnlUserEmail" style="position:absolute;z-index:500000" runat="server" visible="false">
                                    <uc:UserControlEmail id="ucEmail" runat="server"></uc:UserControlEmail>

                                    </asp:Panel>
                                
                            </ContentTemplate>
                                    </asp:UpdatePanel>    
                                
                                <%--<cc1:DropShadowExtender ID="dse"  runat="server" TargetControlID="pnlUserEmail" Opacity=".5" Rounded="false" TrackPosition="false" />--%>
                                      </td>
                                    </tr>
                                    
                            
                        <tr>
                            <td class="labelFreeForm2">Subject</td><td><asp:TextBox ID="tbSubject" cssclass="entryfldw" runat="server"  MaxLength="500" Width="500px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="labelFreeForm2">Attachment</td><td  class="dataFreeForm2">
                                <asp:Label ID="lblAttachment" style="color:blue" runat="server" Text=""></asp:Label>
                                <asp:Literal ID="lblFile" runat="server" Text="" visible="false"></asp:Literal></td>
                        </tr>
                        <tr>
                            <td colspan="2" valign="bottom"  class="labelFreeForm2">&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="2" valign="bottom"  class="labelFreeForm2">Message</td>
                        </tr>
                        <tr>
                            <td colspan="2"><asp:TextBox ID="tbBody" cssclass="entryfldw" height="150px" TextMode="MultiLine"  runat="server" Width="600px"></asp:TextBox></td>
                        </tr>                        
                        <tr>
                            <td align="left">
                            <div id="csprc" style="display:none ; font-style: italic;font-size:10px;color:Blue;">
                                <img src="images/processing.gif" /> Processing...
                            </div>
                            <div id="csbtn" style="visibility: visible">
                                <asp:Button ID="btSettings" runat="server" CssClass="btnsmall2" width="65px" Text="Save" ToolTip="Save Settings" visible="true" OnClientClick="statusbar('csbtn','csprc')" />
                            </div>
                            </td><td align="right">
                            <span id="csPrcSnd" style="display:none ; font-style: italic;font-size:10px;color:Blue;">
                                <img src="images/processing.gif" /> Processing...
                            </span>
                            <span id="csBtnSnd" style="visibility: visible">
                                <asp:Button ID="BtSend" runat="server" CssClass="btnsmall2" width="65px" Text="Send" visible="true" OnClientClick="statusbar('csBtnSnd','csPrcSnd')"/>
                                <asp:Button ID="btCancelEmail" runat="server" style="margin-left:4px" CssClass="btnsmall2" width="65px" Text="Cancel" visible="true" OnClientClick="statusbar('csBtnSnd','csPrcSnd')"/>
                                </span></td>
                        </tr>
                    </table>
                </td>
                </tr>
                <tr><td colspan="2">
                    <asp:Label ID="lblEmailMsg" runat="server" Text="" cssclass="msg"></asp:Label>                    
                    </td></tr>
                </table>               
                </asp:Panel>
                <!--end 2//-->
                </td>
            </tr>
            </table>
                <asp:HiddenField ID="hfRestorePDFViewer" runat="server" />   
           
            </asp:Panel>
            <cc1:DropShadowExtender ID="dse2" runat="server" TargetControlID="PanelEmail3" Opacity=".5" Rounded="false" TrackPosition="True" />
    </td>
</tr>                
</table>
            
</asp:Panel>