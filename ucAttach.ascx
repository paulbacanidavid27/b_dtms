<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucAttach.ascx.vb" Inherits="dms.ucAttach" %>
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
                <asp:HiddenField ID="hfCreatedDate" runat="server" />
            <table border="0" cellspacing="0" cellpadding="0" class="popuphdrbox" style="border-collapse:collapse;width:100%;">

            <tr>
            <td>
                <table cellspacing="0" class="popuphdr" cellpadding="0" border="0" style="width:100%">
                    <tr height="30px">
                        <td align="left" valign="middle" colspan="2">&nbsp;<img height="20px" width="18px" src="images/email.png" />&nbsp;Document Attachment</td>
                                            
                        <td  align="right" valign="top">
                            <asp:ImageButton ID="imgClose" runat="server" imageurl="images/close_window.gif" onmouseover="this.src='images/close_window.gif'"  onmouseout="this.src='images/close_window.gif'" width="18px" Height="18px"/>
                        </td>
                    </tr>
                </table>
            </td>
            </tr>
            <tr>
                <td>

                <asp:Panel ID="pnlAttach" runat="server" Visible="true" style="width:100%;"> 
                <table cellspacing="0" cellpadding="0" style="margin:10px;width:90%;height:90%;border-collapse:collapse;background-color:White;">
                           
                <tr>
                
                            <td class="labelFreeForm2" align="left">Click browse to select a file to attach.
                            <p class="helpnotes" ><b>Notes:</b><br />                                                                    
                                                                    1. Limit the attachment file size to 30MB. <br />
                                                                    2. Avoid using special characters in the file name ("&","#","$","@","~",",").<br />                                                                    
                                                                    3. File name should not be more than 200 characters.<br />
                                                                    4. Valid Files (.pdf,.doc,.docx,.xls,.xlsx,.ppt,.pptx,.gif,.png,.jpeg,.jpg,.tiff,.tif)
                                                                    5. File name should only contain one period before the file name extension (e.g. my.file.pdf is invalid, myfile.pdf is valid)
                                                  </p>
                             </td>
                            </tr>
                            <tr>
                                <td><asp:FileUpload ID="fileAttachment"  runat="server" cssclass="entryfld2" Width="600px"></asp:FileUpload></td>                        
                            </tr>
                            <tr>
                                <td><asp:FileUpload ID="fileAttachment2"  runat="server" cssclass="entryfld2" Width="600px"></asp:FileUpload></td>                        
                            </tr>
                            <tr>
                                <td><asp:FileUpload ID="fileAttachment3"  runat="server" cssclass="entryfld2" Width="600px"></asp:FileUpload></td>                        
                            </tr>
                            
                        <%--<tr>
                            <td title="Maximum of 200 characters" valign="top"  class="labelFreeForm">Enter Comments:</td><td><asp:TextBox ID="tbComments" cssclass="entryfld" TextMode="MultiLine" height="100px" runat="server" Width="300px"></asp:TextBox></td>
                        </tr>--%>
                        <tr>
                            <td><asp:Button ID="btUpload" runat="server" CssClass="btnsmall" width="65px" Text="Upload" visible="true"/><asp:Button ID="btClose" runat="server" CssClass="btnsmall" width="65px" Text="Reload" visible="false"/></td>
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