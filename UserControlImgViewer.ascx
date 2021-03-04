<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlImgViewer.ascx.vb" Inherits="dms.UserControlImgViewer" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Panel ID="PanelViewer1" cssclass="Div1" runat="server" Visible="True"></asp:Panel>
<asp:Panel ID="PanelViewer2" cssclass="Div2" runat="server" Visible="True">
<table cellspacing="0" cellpadding="0" style="width:100%;height:100%;" border="0">
<tr>
    <td valign="middle" align="center" height="100%" width="100%">                
        <asp:Panel ID="pnlImg" visible="true" runat="server" ClientIDMode="Static" style="height:90%;width:90%;border:5px solid white">
        <table border="0" cellspacing="0" cellpadding="0" class="popuphdrbox" style="border-collapse:collapse;width:100%;height: 100%">
            <tr>
            <td valign="top" width="100%" >
                <table cellspacing="0" class="popuphdr" cellpadding="0" border="0" style="width:100%">
                    <tr>
                    <td align="left" valign="middle">&nbsp;<img height="20px" width="18px" src="images/upload.png" />&nbsp;IMAGE VIEWER</td>
                    <td valign="top">
                            <table>
                                <tr >
                                    <td><asp:Label ID="lZoom" runat="server" Text="Zoom:"></asp:Label></td>
                                    <td>
                                    <asp:DropDownList ID="dlZoom" runat="server" ClientIDMode="Static" onchange="zoom('imgHandler')" CssClass="entryfld2">
                                        <asp:ListItem value="5" Text="500%" />
                                        <asp:ListItem value="3" Text="300%" />
                                        <asp:ListItem value="2" Text="200%" />                            
                                        <asp:ListItem value="1" Text="100%"  Selected="True"/>                            
                                        <asp:ListItem value="0.75" Text="75%" />                            
                                        <asp:ListItem value="0.5" Text="50%" />                            
                                        <asp:ListItem value=".25" Text="25%" />                            
                                    </asp:DropDownList>                        
                                    <asp:HiddenField ID="hfHeight"  ClientIDMode="Static" runat="server"></asp:HiddenField><asp:HiddenField ID="hfWidth" runat="server" ClientIDMode="Static"></asp:HiddenField>
                                    </td>
                                    <td>
                                    <asp:Image ID="imgSrc" Width="20px" Height="20px" style="cursor:hand" visible="false"  ToolTip="Print Document" ImageUrl="images/print.png" runat="server" onclick="imgHandler.print"/>                        
                                    </td>
                                </tr>
                            </table>           
                    </td>                                            
                    <td  align="right" valign="top" width="20px">
                        <asp:ImageButton ID="imgClose" runat="server" imageurl="images/close_window.gif" onmouseover="this.src='images/close_window.gif'"  onmouseout="this.src='images/close_window.gif'" width="18px" Height="18px"/>
                    </td>
                    </tr>
                </table>
            </td>
            </tr>
            <tr>
                <td align="center" style="height:auto;width:auto" valign="top">                            
                    <asp:Panel ID="pnlImageDisp" runat="server" width="98%" Height="98%" ClientIDMode="Static" style="min-width:300px;min-height:300px;overflow:auto;border:ridge   2px #D4D4D4;text-align:left;background-color:#FFFFFF; " visible="true">                
                        <asp:Image ID="imgHandler" ClientIDMode="Static"  visible="true"  runat="server" />
                    </asp:Panel>
                </td>
             </tr>
             </table>
        </asp:Panel>        
        <cc1:DropShadowExtender ID="dse2" runat="server" TargetControlID="pnlImg" Opacity=".5" Rounded="false" TrackPosition="True" />
    </td>
</tr>
</table>

</asp:Panel>
