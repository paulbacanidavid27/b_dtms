<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucConfirm.ascx.vb" Inherits="dms.ucConfirm" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Panel ID="PanelEmail1" cssclass="Div1" runat="server" Visible="True"></asp:Panel>
<asp:Panel ID="PanelEmail2" cssclass="Div2" runat="server" Visible="True">
<table cellspacing="0" cellpadding="0" style="width:100%;height:100%">
<tr>
    <td valign="middle" align="center">                
<%--<asp:UpdatePanel ID="pnlConfirm" runat="server" UpdateMode="Conditional">
    <ContentTemplate>  --%>
    <asp:Panel id="pConfirm" runat="server" Visible="true" Width="500px">
    <!-- start - search criteria //-->
    <center>
    
    
        <table border="0" class="popuphdrbox" cellspacing="0" cellpadding="0" style="border: solid 1px #3A5671;border-collapse:collapse;width:100%">

            <tr>
               <td align="center">
                  <table cellspacing="0" class="popuphdr" cellpadding="0" border="0" style="width:100%">
                        <tr height="30px">
                        <td align="left" valign="middle" colspan="2">&nbsp;<img height="25px" width="20px" src="images/question4.png" />&nbsp;Notification</td>
                                            
                        <td  align="right" valign="top">
                            <asp:ImageButton ID="imgSaveCancel" runat="server" imageurl="images/close_window.gif" onmouseover="this.src='images/close_window.gif'"  onmouseout="this.src='images/close_window.gif'" width="18px" Height="18px"/>
                        </td>
                    </tr>
                  </table>
               </td>
            </tr>
            <tr>
            <td style="padding-left:15px">
        
                <table align="left">
                <tr>
                    <td align="left" style="font-family:Tahoma;font-size:11pt;color:#003399; padding:12px;">
                        <asp:Literal ID="ltext" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                      
                        
                        <td align="right">
                            <asp:Literal ID="lsID" runat="server" Visible="false"></asp:Literal>
                            <asp:Literal ID="lsDesc" runat="server" Visible="false"></asp:Literal>
                            <asp:Button ID="btContinue" runat="server" CssClass="btnsmall2" Text="OK" Width="40px" />&nbsp;<asp:Button ID="btSaveCancel" runat="server" CssClass="btnsmall2" Text="Cancel" />
                        </td>                        
                </tr>
                
                
                </table>
                
                </td>
                </tr>
                </table>
                
                </center>
    </asp:Panel>
    <cc1:DropShadowExtender ID="dse2" runat="server" TargetControlID="pConfirm" Opacity=".5" Rounded="false" TrackPosition="False"  />
   <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>  
    </td>
</tr>                
</table>
    </asp:Panel>