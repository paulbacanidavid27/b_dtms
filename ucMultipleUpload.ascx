<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucMultipleUpload.ascx.vb" Inherits="dms.ucMultipleUpload" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Panel id="pAddUser" runat="server" Visible="True" style="width:700px">
    <!-- start - search criteria //-->
    
    
    
        <table border="0" class="popuphdrbox" cellspacing="0" cellpadding="0" style="border: solid 1px #3A5671;border-collapse:collapse;width:100%;">

            <tr>
               <td align="center">
                  <table cellspacing="0" class="popuphdr" cellpadding="0" border="0" style="width:100%">
                        <tr height="30px">
                        <td align="left" valign="middle" colspan="2">&nbsp;<img height="30px" width="23px" src="images/user2.png" />&nbsp;User Profile</td>
                                            
                        <td  align="right" valign="top">
                            <asp:ImageButton ID="imgClose" runat="server" imageurl="images/close_window.gif" onmouseover="this.src='images/close_window.gif'"  onmouseout="this.src='images/close_window.gif'" width="18px" Height="18px"/>
                        </td>
                    </tr>
                  </table>
               </td>
            </tr>
            <tr>
            <td style="padding-left:15px">
                <asp:Repeater ID="rptFileUpload" runat="server">
                <HeaderTemplate><table></HeaderTemplate>
                <ItemTemplate>
                <tr><td>
                    <asp:Literal ID="lno" runat="server"></asp:Literal>
                    </td>
                    <td>
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                    </td>
                </tr>
                </ItemTemplate>
                <FooterTemplate></table></FooterTemplate>
                </asp:Repeater>
            </td>
            </tr>
        </table>
                
                
    </asp:Panel>
    <cc1:DropShadowExtender ID="dse2" runat="server" TargetControlID="pAddUser" Opacity=".5" Rounded="false" TrackPosition="False" />