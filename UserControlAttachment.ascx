<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlAttachment.ascx.vb" Inherits="dms.UserControlAttachment" %>
<%--<%@ Register src="UserControlImgViewer.ascx" tagname="UserControlImgViewer" tagprefix="uc" %>
<%@ Register src="UserControlPDFViewer.ascx" tagname="UserControlPDFViewer" tagprefix="uc" %>
<%@ Register src="UserControlDocViewer.ascx" tagname="UserControlDocViewer" tagprefix="uc" %>--%>
<%@ Register src="UserControlDocumentAttachIndex.ascx" tagname="UserControlDocAttachIndex" tagprefix="uc" %>
<%--<uc:UserControlPDFViewer runat="server" id="ucPDFViewer" visible="False"/>
    <uc:UserControlDocViewer runat="server" id="ucDocViewer" visible="False"/>
    <uc:UserControlImgViewer runat="server" id="ucViewer" visible="False"/>  --%>
    <%--<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
                        </asp:ScriptManagerProxy>--%>
<asp:HiddenField ID="hfCreatedDate" runat="server" />
<asp:HiddenField ID="hfCount" runat="server" />
<asp:Repeater ID="rptAttachment" runat="server" Visible="True">
                    <HeaderTemplate>                                                                   
                            
                            <table  border="0" width="100%" cellpadding="0" cellspacing="0" style="border:solid 1px #D4D4D4;background-color:white;border-collapse:collapse">
                            <tr>
                                <td class="newtblheader"><img src="images/clip.png" />&nbsp;Attachment</td><td class="newtblheader" align="right">File Size</td><td class="newtblheader" align="right"><asp:ImageButton ID="iDA"  height="15px" Width="14px" tooltip="Delete selected attachment." runat="server"   ImageUrl="images/del.png" onclick="DeleteAttachment"/></td>
                            </tr>    
                    </HeaderTemplate>
                    <ItemTemplate>                                                                    
                        <tr>
                            <td style="padding:5px;font-size:13px;color:#5B5B5B">
                            <asp:ImageButton ID="imgExpand" runat="server" imageurl="images/plus.jpg" OnClick="ShowIndex"/>
                                <%--<asp:LinkButton ID="lF" runat="server" OnClientClick="set_footer()" OnClick="openDoc" cssclass="nb" visible="false" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DocFileName"))%>'>
                                </asp:LinkButton>--%>
                                <asp:HyperLink ID="hlNewTab" runat="server" Target="_blank" cssclass="nb"   onclientclick="return false" ToolTip="View Attachment" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DocFileName"))%>'>
                                
                            </asp:HyperLink>
                                <asp:Literal ID="lFN" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DocFileName"))%>' Visible="true"></asp:Literal>
                                
                                <asp:UpdatePanel runat="server" ID="pAttachInput" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:TextBox ID="tbFileName" runat="server" Text='' visible="false" style="width:520px;" cssclass="entryfld"   autopostback="true" OnTextChanged="SaveChanges"></asp:TextBox>
                                </ContentTemplate>
                            <Triggers>                            
                            <asp:PostBackTrigger ControlID ="imgSelect" />
                            <asp:PostBackTrigger ControlID ="imgSelected" />
                            </Triggers>
                            
                            </asp:UpdatePanel>
                            </td>
                            <td align="right"><asp:Literal ID="lFileSize" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "FileSize"))%>' Visible="true"></asp:Literal>
                            </td>
                            <td align="right">
                            <asp:UpdatePanel runat="server" ID="pAT" UpdateMode="Conditional">
                            <ContentTemplate>
                            <asp:HyperLink ID="hlNewTab2" runat="server" Target="_blank"   onclientclick="return false" ToolTip="Print Attachment" Visible="false">
                                <asp:Image ID="imgPDFPrint" ImageUrl="images/print.png" runat="server" width="20px" Height="20px"  />
                            </asp:HyperLink>
                                
                            <asp:ImageButton ID="iD" runat="server" ToolTip="Download Attachment" height="14px" imageUrl="images/download_doc.png"  OnClick="fDownload"/>&nbsp;
                            <asp:ImageButton ID="imgSelect" runat="server"  ImageUrl="images/box.png"  OnClick="fSelect"/>
                                <asp:ImageButton ID="ImgSelected" runat="server"  ImageUrl="images/checkbox.png" OnClick="fUnSelect" visible="false"/>                       
                            </ContentTemplate>
                            <Triggers>                            
                            <asp:PostBackTrigger ControlID ="iD" />
                            </Triggers>
                            
                            </asp:UpdatePanel>
                                     <asp:Literal ID="lA" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "AttachedBy"))%>' Visible="false"></asp:Literal>
                                <asp:Literal ID="lAI" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DocAttachId"))%>' Visible="false"></asp:Literal>
                                <asp:Literal ID="lDT" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "AttachType"))%>' Visible="false"></asp:Literal>
                                <asp:Literal ID="lD" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "AttachedDate"))%>' Visible="false"></asp:Literal>

                            </td>
                        </tr>    
                        <tr>
                        <td colspan="3"><uc:UserControlDocAttachIndex runat="server" id="ucIndex" visible="False"/></td></tr>                                                                                        
                       <tr>
                          <td class="tbldashed-gray" colspan="3">Attached by: <asp:Literal ID="lAB" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "username"))%>' Visible="true"></asp:Literal>&nbsp; (<asp:Label ID="lUploadedDate" tooltip="Attached Date" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ADate"))%>' Visible="true"></asp:Label>)</td>
                        </tr>   
                    </ItemTemplate>
                    <FooterTemplate>
                                <tr>
                                    
                                    <td  align="right" style="border-top:solid 1px #ffffff;padding-right:90px;" colspan="3">
                                        <b><asp:Literal ID="ltotalfilesize" runat="server"></asp:Literal></b></td>
                                        
                                </tr>
                                </table>
                    </FooterTemplate>

                </asp:Repeater>    


