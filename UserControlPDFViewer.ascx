<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlPDFViewer.ascx.vb" Inherits="dms.UserControlPDFViewer" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<iframe id="docprint" clientidmode="static" src="" border="0"  visible="true" style="border:ridge   2px #D4D4D4;height:0px" runat="server" width="0px"></iframe>
<asp:Panel ID="PanelViewer1" cssclass="Div1" runat="server" Visible="True"></asp:Panel>
<asp:Panel ID="PanelViewer2" cssclass="Div2" runat="server" Visible="True">
<table cellspacing="0" cellpadding="0" style="width:100%;height:100%;" border="0">
<tr>
    <td valign="middle" align="center" height="100%">                
        <asp:Panel ID="pnlPDF" visible="true" runat="server" ClientIDMode="Static" style="height:90%;width:90%;border:5px solid white">
        <table id="pnlPDFtbl" border="0" cellspacing="0" cellpadding="0" class="popuphdrbox" style="left:0px;top:0px;border-collapse:collapse;width:100%;height: 100%">
            <tr>
            <td valign="top">
                <table cellspacing="0" class="popuphdr" cellpadding="0" border="0" style="width:100%">
                    <tr>
                    <td align="left" valign="middle">&nbsp;<img height="20px" width="18px" src="images/upload.png" />&nbsp;DOCUMENT VIEWER</td><td>
                    <asp:UpdateProgress runat="server" ID="prgLogin"  DynamicLayout="false">
    <ProgressTemplate>    
        <asp:Image ID="Image2" runat="server" src="images/loading.gif" Height="12px" Width="15px"/>    
    </ProgressTemplate>
    </asp:UpdateProgress></td>
                    <td valign="top" align="right">
                            <table>
                                <tr >                                    
                                    <td align="right" valign="top">
                                    <asp:ImageButton ID="imgDownload" Width="20px" Height="20px" style="cursor:hand;" visible="true"  ToolTip="Download Document" ImageUrl="images/dload2.png" runat="server" />    
                                    <asp:ImageButton ID="imgPrint" Width="20px" Height="20px" style="cursor:hand" visible="true"  ToolTip="Print Document" ImageUrl="images/print2.png" runat="server" />                        
                                    
                                    
                                    
                                    </td>
                                </tr>
                            </table>           
                    </td>                                            
                    <td valign="top" align="right">
                            <table >
                                <tr >                                    
                                    <td align="right" valign="top">
                                    
                                    </td>
                                </tr>
                            </table>           
                    </td>                                            
                    <td  align="right" valign="top" width="80px" nowrap><img alt="Maximize" src="images/max.png" onclick="maxscreen()" style="vertical-align:top;display:inline;cursor:hand"  id="maxw" width="18px" Height="18px" title="Maximize"/><img alt="Minimize" title="Minimize" width="18px" Height="18px" src="images/min.png" onclick="minscreen()"  style="vertical-align:top;display:none;cursor:hand" id="minw" />                        
                        <asp:ImageButton ID="imgClose" runat="server" imageurl="images/close_window.gif" onmouseover="this.src='images/close_window.gif'"  onmouseout="this.src='images/close_window.gif'" width="18px" Height="18px"/>
                    </td>
                    </tr>
                </table>
            </td>
            </tr>
            <tr>
                <td align="center" style="height:auto" valign="center">                            
                    <%--<asp:Panel ID="pnlPDFDisp" runat="server" width="98%"  ClientIDMode="Static" style="min-height:300px;overflow:auto;border:ridge   2px #D4D4D4;text-align:left;background-color:#FFFFFF; " visible="true">                --%>
                    
                        <iframe id="docvw" clientidmode="static" src="Pdf2Viewer.ashx?dt=<% DateTime.now() %> %>" border="0"  visible="true" style="border:ridge   2px #D4D4D4;min-height:300px" runat="server" width="98%"></iframe>
                    <%--</asp:Panel>--%>
                </td>
             </tr>
             </table>
        </asp:Panel>        
        <%--<cc1:DropShadowExtender ID="dse2" runat="server" TargetControlID="pnlPDF" Opacity=".5" Rounded="false" TrackPosition="True" />--%>
    </td>
</tr>
</table>

</asp:Panel>
