<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucDocUpload.ascx.vb" Inherits="dms.ucDocUpload" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<!--start 2//-->
<asp:Panel ID="pUpload1"  cssclass="Div1" runat="server" Visible="true">
</asp:Panel>
<asp:Panel ID="pUpload2" cssclass="Div2" runat="server" Visible="True" >
<table cellspacing="0" cellpadding="0" style="width:100%;height:100%;">
<tr>
    <td valign="middle" align="center">               
            <asp:Panel id="pUpload3" runat="server" Visible="true" style="width:620px;margin-bottom:30px;" defaultbutton="btUpload">            
                <asp:HiddenField ID="hfrefresh" runat="server" Value="n" />
                <asp:HiddenField ID="hfCreatedDate" runat="server" Value="" />
                <asp:HiddenField ID="hfVersion" runat="server" Value="" />
                <asp:HiddenField ID="hfRefNo" runat="server" Value="" />
            <table border="0" cellspacing="0" cellpadding="0" class="popuphdrbox" style="border-collapse:collapse;width:100%;">

            <tr>
            <td>
                <table cellspacing="0" class="popuphdr" cellpadding="0" border="0" style="width:100%">
                    <tr height="30px">
                        <td align="left" valign="middle" colspan="2">&nbsp;<img height="20px" width="18px" src="images/email.png" />&nbsp;Upload 
                            File</td>
                                            
                        <td  align="right" valign="top">
                            <asp:ImageButton ID="imgClose" runat="server" imageurl="images/close_window.gif" onmouseover="this.src='images/close_window.gif'"  onmouseout="this.src='images/close_window.gif'" width="18px" Height="18px"/>
                        </td>
                    </tr>
                </table>
            </td>
            </tr>
            <tr>
                <td align="center">

                <asp:Panel ID="pnlAttach" runat="server" Visible="true"> 
                <table cellspacing="0" cellpadding="0" style="width:500px;height:100%;border-collapse:collapse;background-color:White;>
                <%--<tr>
                    <td colspan="2" align="left">
                    
                    <p class="helpnotes" ><b>Attachment Instruction:</b><br />                                                                    
                                                                    1. Select the updated file to be Uploaded by clicking on Browse button. <br />
                                                                    2. Enter some comments. <br />
                                                                    3. Click Upload to finalize the attachment. <br />                                                                    
                                                                </p>
                                                                
                    </td>
                </tr>    --%>            
                <tr>
                <td align="left">
                    <table>
                    <tr><td align="left">
                    &nbsp;</td></tr>
                        <tr>
                            <td class="labelFreeForm2" align="left">Click browse to select a file to upload:</td><td><asp:FileUpload ID="fileAttachment" runat="server" class="entryfld2"></asp:FileUpload></td>                        
                        <%--<tr>
                            <td title="Maximum of 200 characters" valign="top"  class="labelFreeForm">Enter Comments:</td><td><asp:TextBox ID="tbComments" cssclass="entryfld" TextMode="MultiLine" height="100px" runat="server" Width="300px"></asp:TextBox></td>
                        </tr>--%>
                        
                            <td><asp:Button ID="btUpload" runat="server" CssClass="btnsmall" width="65px" Text="Upload" visible="true"/><asp:Button ID="btClose" runat="server" CssClass="btnsmall" width="65px" Text="Reload" visible="false"/></td>
                        </tr>
                    </table>
                </td>
                </tr>
                <tr><td align="left">
                    &nbsp;<asp:Label ID="lmsg" runat="server" Text="" cssclass="msg"></asp:Label></td></tr>
                </table>               
                
                </asp:Panel>
            </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            </table>
        </asp:Panel>
        <cc1:DropShadowExtender ID="dse2" runat="server" TargetControlID="pUpload3" Opacity=".5" Rounded="false" TrackPosition="True" />
    </td>
</tr>
</table>
                <!--end 2//-->
</asp:Panel>