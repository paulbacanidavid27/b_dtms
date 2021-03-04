<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucPrompt.ascx.vb" Inherits="dms.ucPrompt" %>
<%--<div id="filtercc" style="background:#CCCCCC;background-repeat:repeat;background:black;filter:alpha(opacity=70);opacity:0.7;position:fixed;width:100%;height:100%;top:0px;left:0px;z-index:20000;visibility:hidden">
</div>
--%>

                 <table width="100%" height="100%" border="1" cellspacing="0" cellpadding="0" style="z-index:20002;">
                 <tr>
                 <td align="center" valign="middle">
                 <center>
                 
                    <div id="div1" style="padding-bottom:10px;background-color:white;width:40%;border-radius:4px;box-shadow: 1px 1px 1px 1px #808080;">
                    <div style="border-radius:4px 4px 0px 0px;background-color:#000080; padding:3px;border-bottom:solid 1px gray;">
                    <table border="0" cellpadding="2" cellspacing="2" style="color: #EAEAEA;font-family: Arial,Calibri;font-weight: bold;font-size: 12pt;background-color:#000080;width: 100%;border-collapse:collapse;border-radius:4px;">
                    <tr >
                                <td align="left" style="color:White;font-weight:bold;">
                                    <asp:Literal ID="lsTitle" runat="server" Text=""></asp:Literal>
                                </td>
                                <td valign="bottom" align="right">
                                    <asp:ImageButton ID="imgClose" runat="server" ImageUrl="images/close_window.gif" /> 
                                </td>
                            </tr>
                            </table>
                            </div>
                        <table border="0" cellpadding="5" cellspacing="2" style="width: 100%;border-collapse:collapse;margin-top:10px;margin-bottom:10px">
                            <tr>
                                <td align="left" >
                                    <asp:Literal ID="lMsg" runat="server"></asp:Literal>
                                </td>
                            </tr>                            
                            <tr>
                                <td align="center">                                    
                                    <asp:Button ID="btOK" runat="server" Text="OK" CssClass="btn" />&nbsp;<asp:Button ID="btClose" runat="server" Text="Cancel" CssClass="btn" /></td>
                            </tr>
                        </table>
                    </div>
                  </center>
                 </td>
                 </tr>
                 </table>
