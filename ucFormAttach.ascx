<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucFormAttach.ascx.vb" Inherits="dms.ucFormAttach" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!--start 2//-->
<asp:Panel ID="pUpload1" CssClass="Div1" runat="server" Visible="true">
</asp:Panel>
<asp:Panel ID="pUpload2" CssClass="Div2" runat="server" Visible="True">
    <table cellspacing="0" cellpadding="0" style="width: 100%; height: 100%;">
        <tr>
            <td valign="middle" align="center">
                <asp:Panel ID="pUpload3" runat="server" Visible="true" Style="width: 620px; margin-bottom: 30px;" DefaultButton="btUpload">
                    <asp:HiddenField ID="hfrefresh" runat="server" Value="n" />
                    <asp:HiddenField ID="hfCreatedDate" runat="server" />
                    <table border="0" cellspacing="0" cellpadding="0" class="popuphdrbox" style="border-collapse: collapse; width: 100%;">

                        <tr>
                            <td>
                                <table cellspacing="0" class="popuphdr" cellpadding="0" border="0" style="width: 100%">
                                    <tr height="30px">
                                        <td align="left" valign="middle" colspan="2">&nbsp;<img height="20px" width="18px" src="images/email.png" />&nbsp;Form Upload</td>

                                        <td align="right" valign="top">
                                            <asp:ImageButton ID="imgClose" runat="server" ImageUrl="images/close_window.gif" onmouseover="this.src='images/close_window.gif'" onmouseout="this.src='images/close_window.gif'" Width="18px" Height="18px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>

                                <asp:Panel ID="pnlAttach" runat="server" Visible="true" Style="width: 100%;">
                                    <table cellspacing="0" cellpadding="0" style="margin: 10px; width: 90%; height: 90%; border-collapse: collapse; background-color: White;">

                                        <tr>

                                            <td class="labelFreeForm2" align="left">Click browse to select a file to attach.
                            <p class="helpnotes">
                                <b>Notes:</b><br />
                                1. Limit the file size to 30MB.
                                <br />
                                2. Avoid using special character in the file name.<br />
                            </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:FileUpload ID="fileAttachment" runat="server" CssClass="entryfld2" Width="600px"></asp:FileUpload></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="tbDescription" runat="server" placeholder="Description" CssClass="entryfld2" Width="600px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:FileUpload ID="fileAttachment2" runat="server" CssClass="entryfld2" Width="600px"></asp:FileUpload></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="tbDescription2"  placeholder="Description" runat="server"  CssClass="entryfld2" Width="600px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:FileUpload ID="fileAttachment3" runat="server" CssClass="entryfld2" Width="600px"></asp:FileUpload></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="tbDescription3"  placeholder="Description" runat="server" CssClass="entryfld2" Width="600px"></asp:TextBox></td>
                                        </tr>

                                        <%--<tr>
                            <td title="Maximum of 200 characters" valign="top"  class="labelFreeForm">Enter Comments:</td><td><asp:TextBox ID="tbComments" cssclass="entryfld" TextMode="MultiLine" height="100px" runat="server" Width="300px"></asp:TextBox></td>
                        </tr>--%>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btUpload" runat="server" CssClass="btnsmall" Width="65px" Text="Upload" Visible="true" /><asp:Button ID="btClose" runat="server" CssClass="btnsmall" Width="65px" Text="Reload" Visible="false" /></td>
                                        </tr>

                                        <tr>
                                            <td align="left">&nbsp;<asp:Label ID="lmsg" runat="server" Text="" CssClass="msg"></asp:Label></td>
                                        </tr>
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
