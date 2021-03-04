<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucHeader.ascx.vb" Inherits="dms.ucHeader" %>
<table cellpadding="0" cellspacing="0" border="0" >
                    <tr>
                        <td style="height:21px;width:10px;background-image:url('images/tabs/btnl.png');">
                            </td>
                        <td style="height:21px;background-image:url('images/tabs/btnm.png');padding-left:5px;padding-right:5px;" valign="top">
                            <asp:LinkButton ID="lbAddDoc" runat="server" style="text-decoration:none;font-weight:bold;font-size:8pt;" ><asp:Image ID="ImageButton1" visible="true" runat="server" ImageUrl="images/upload2.png" style="vertical-align:top;height:18px;width:12px;margin-right:4px;"/>Upload Document</asp:LinkButton></td>
                        <td style="height:21px;width:10px;background-image:url('images/tabs/btnr.png');">
                            </td>
                    </tr>
                </table>