<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlImgViewer2.ascx.vb" Inherits="dms.UserControlImgViewer2" %>
                       
                    <asp:Panel ID="pnlImageDispx" runat="server"  ClientIDMode="Static" style="height:99%;width:99%;min-width:300px;min-height:300px;overflow:auto;border:ridge   2px #D4D4D4;text-align:left;background-color:#FFFFFF; " visible="true">                
                        <asp:Image ID="Image1" ClientIDMode="Static"  visible="true"  runat="server"  imageurl="images/loading.gif"/>
                        <asp:Image ID="imgHandlerx" ClientIDMode="Static"  visible="false"  runat="server"/>
                    </asp:Panel>


