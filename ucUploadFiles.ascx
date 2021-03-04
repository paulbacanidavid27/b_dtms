<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucUploadFiles.ascx.vb" Inherits="dms.ucUploadFiles" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Panel ID="pUpload1"  cssclass="Div1" runat="server" Visible="true">
</asp:Panel>
<asp:Panel ID="pUpload2" cssclass="Div2" runat="server" Visible="True">
<table border="0" cellspacing="0" cellpadding="0" style="width:100%;height:100%;border-collapse:collapse" >
    <tr>
        <td valign="middle" align="center">  
<asp:Panel id="pUploadDoc" runat="server" Visible="True" style="width:900px;">
    <!-- start - search criteria //-->    
                            
                                <table border="0" cellspacing="0" class="popuphdrbox" cellpadding="0" style="border-collapse:collapse;width:100%;">

                                    <tr>
                                       <td colspan="2">
                                        <table cellspacing="0" class="popuphdr" cellpadding="0" border="0" style="width:100%">
                                            <tr height="30px">
                                            <td align="left" valign="middle" colspan="2">&nbsp;<img height="20px" width="18px" src="images/upload.png" />&nbsp;Upload 
                                                Files</td>
                                            
                                            <td  align="right" valign="top">
                                                <asp:ImageButton ID="imgClose" runat="server" imageurl="images/close_window.gif" onmouseover="this.src='images/close_window.gif'"  onmouseout="this.src='images/close_window.gif'" width="18px" Height="18px"/>
                                            </td>
                                            </tr>
                                       </table>
                                       </td>
                                    </tr>            
                                    <tr>
                                    <td colspan="2" style="padding-left:4px;padding-right:4px">
                                            <table width="100%" border="0">
                                                <tr>
                                                    <td align="left" >
                                                    <asp:panel ID="pUpload" runat="server" Visible="true">
                                            <table width="100%" border="0" style="border:solid 1px #D4D4D4; background-color: #DDFFDD;">
                                                
                                                <tr>
                                                    <td  align="left" colspan="2">
                                                    <asp:UpdatePanel ID="pmsg" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Panel ID="pNote" runat="server" CssClass="notes" Visible="false">
                                                        Notes: <br />1. Click Browse to select a file to upload.
                                                        <br />2.Click Add button to add in the list to be upload.
                                                        <br />3.Click Upload button to upload selected the files.
                                                        <br />4.You can click on Remove button if you need to remove
                                                        <br />some files you don't want to upload.
                                                        </asp:Panel>
                                                        </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                        </td>
                                                </tr>
                                                <tr>
                                                <td colspan="2" align="left">
                                                    <table width="100%"><tr><td  class="labelFreeForm">Select file to upload </td><td align="right"><asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:ImageButton ID="imgHelp" runat="server" imageurl="images/question.png"/>
                                                            </ContentTemplate>
                                                            </asp:UpdatePanel></td></tr></table>
                                                </td>
                                                </tr>
                                                <tr>
                                                    
                                                    <td align="left" colspan="2" >
                                                        <asp:FileUpload ID="fUpload" runat="server" CssClass="entryfld" />
                                                        &nbsp;<asp:Button ID="btnAdd" runat="server" cssclass="entryfldb" 
                                                            OnClick="btnAdd_Click" Text="Add" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" colspan="2" valign="top">
                                                        <asp:ListBox ID="lstFiles" runat="server" CssClass="entryfld" Height="244px" 
                                                            Width="324px"></asp:ListBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Button ID="btnRemove" runat="server" cssclass="btnsmall2" 
                                                            OnClick="btnRemove_Click" Text="Remove" />
                                                        &nbsp;<asp:Button ID="btnUpload" runat="server" cssclass="btnsmall2" 
                                                            OnClick="btnUpload_Click" Text="Upload" />
                                                    </td>
                                                </tr> 
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Label ID="lblMessage" runat="server" cssclass="msg_green" ></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                            </asp:Panel>
                                                    </td>
                                                    <td valign="top" align="left" Width="600px">
                                                    <table width="100%"><tr><td class="labelFreeForm">Files Uploaded</td><td align="right">
                                                        <asp:ImageButton ID="imgRefresh" runat="server" ImageUrl="images/refresh.png" 
                                                            tooltip="Refresh List" visible="true" />
                                                            </td>
                                                            </tr>
                                                            <tr>
                                                            <td colspan="2">
                                                                  <asp:UpdatePanel ID="pnlFiles" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                            <asp:panel ID="pFiles" runat="server" Visible="true">
                                                <asp:DataGrid runat="server" id="articleList" 
    AutoGenerateColumns="False" AlternatingItemStyle-BackColor="#eeeeee" 
    HeaderStyle-BackColor="Navy" HeaderStyle-ForeColor="White"
    HeaderStyle-Font-Size="6pt" HeaderStyle-Font-Bold="True" HeaderStyle-HorizontalAlign="Left"
     ItemStyle-Font-Size="7pt" AllowPaging="True" Font-Names="Verdana" Width="100%" PageSize="20">
                                                            <AlternatingItemStyle BackColor="#EEEEEE" Font-Bold="False" Font-Italic="False" 
                                                                Font-Names="Verdana" Font-Overline="False" Font-Size="10px" 
                                                                Font-Strikeout="False" Font-Underline="False" />
                                                            <Columns>
                                                                <asp:BoundColumn DataField="Name" HeaderText="File name" 
                                                                    ItemStyle-HorizontalAlign="left">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundColumn>
                                                                <asp:BoundColumn DataField="LastWriteTime" DataFormatString="{0:d}" 
                                                                    HeaderText="Last Modified" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundColumn>
                                                                <asp:BoundColumn DataField="Length" DataFormatString="{0:#,### bytes}" 
                                                                    HeaderText="File Size" ItemStyle-HorizontalAlign="Right">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:BoundColumn>
                                                            </Columns>
                                                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" 
                                                                Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" 
                                                                Font-Underline="False" />
                                                            <HeaderStyle BackColor="Navy" Font-Bold="True" Font-Size="10px" 
                                                                ForeColor="White" Font-Italic="False" Font-Names="Verdana" 
                                                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                            <ItemStyle Font-Size="10px" Font-Bold="False" Font-Italic="False" 
                                                                Font-Names="Verdana" Font-Overline="False" Font-Strikeout="False" 
                                                                Font-Underline="False" />
                                                            <PagerStyle Mode="NumericPages" Font-Bold="True" Font-Italic="False" 
                                                                Font-Names="Tahoma" Font-Overline="False" Font-Size="7pt" 
                                                                Font-Strikeout="False" Font-Underline="False" ForeColor="Navy" 
                                                                HorizontalAlign="Right" VerticalAlign="Middle" BackColor="#EEEEEE" />
</asp:DataGrid>  
                                            </asp:panel>
                                             </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                            </td></tr>
                                                            </table>
                                                    </td>
                                                </tr>
                                            
                                            </table>
                        </td>
                        </tr>
                        </table>
                        </asp:Panel>

        </td>
    </tr>
</table>
</asp:Panel>
<cc1:dropshadowextender ID="dse" runat="server" TargetControlID="pUploadDoc" 
    Opacity=".5" Rounded="false" TrackPosition="true" />