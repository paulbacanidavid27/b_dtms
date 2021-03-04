<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlVersion.ascx.vb" Inherits="dms.UserControlVersion" %>
<%@ Register src="UserControlImgViewer.ascx" tagname="UserControlImgViewer" tagprefix="uc" %>
<%@ Register src="UserControlPDFViewer.ascx" tagname="UserControlPDFViewer" tagprefix="uc" %>
<%@ Register src="UserControlDocViewer.ascx" tagname="UserControlDocViewer" tagprefix="uc" %>
<uc:UserControlPDFViewer runat="server" id="ucPDFViewer" visible="False"/>
    <uc:UserControlDocViewer runat="server" id="ucDocViewer" visible="False"/>
    <uc:UserControlImgViewer runat="server" id="ucViewer" visible="False"/>
<asp:HiddenField ID="hfCreatedDate" runat="server" Value=""/>
<asp:Repeater ID="rptFileVersion" runat="server" Visible="True">
                    <HeaderTemplate>                                                                   
                       <table  border="0" width="100%" cellpadding="0" cellspacing="0" style="border:solid 1px #D4D4D4;background-color:white;border-collapse:collapse">
                            <tr><td class="newtblheader"><img src="images/version_icon.png" />&nbsp;Document Version</td>
                            <td class="newtblheader"  align="right">File Size</td>
                            <td class="newtblheader"></td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>                                                                    
                        <tr>
                             <td style="padding-left:5px;">
                            <asp:LinkButton ID="lbDocVersion" runat="server" OnClick="openDoc" cssclass="nb">
                                Version <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "docVersion"))%>.0                            
                            </asp:LinkButton>
                            <asp:Literal ID="lTextVersion" runat="server" Text=' <%# "Version " & Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "docVersion")) & ".0" %>' Visible="false"></asp:Literal>
                            &nbsp;
                            <asp:Label ID="lFileName" runat="server" style="color:green" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "FileName"))%>' Visible="true" ToolTip="Filename"></asp:Label>                            
                            <asp:Literal ID="lvDocId" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DocId"))%>' Visible="false"></asp:Literal>
                            <asp:Literal ID="lVersion" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "docVersion"))%>' Visible="false"></asp:Literal>
                            
                            </td>
                            <td align="right"><asp:Literal ID="lFileSize" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "FileSize"))%>' Visible="true"></asp:Literal></td>
                            <td>
                                <asp:ImageButton ID="imgShare" runat="server" ImageUrl="images/share_doc.png" visible="false"/>
                                <asp:ImageButton ID="imgDownload" runat="server" ImageUrl="images/download_doc.png" visible="false"/>
                                <asp:ImageButton ID="imgShow" runat="server" ImageUrl="images/show.png" visible="False" />
                                <asp:HyperLink ID="hlNewWindow" visible="false" runat="server" ImageUrl="images/show.png"  onclientclick="return false" NavigateUrl='viewfile.aspx?d_id=' Target="_blank"  ToolTip="Open in New Window"></asp:HyperLink>
                                
                                </td>                                            
                        </tr>            
                        <tr>
                            <td style="font-style:italic;padding:5px;" colspan="3">
                            <asp:Literal ID="lComments" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Comments"))%>' Visible="true"></asp:Literal>
                            </td>
                        </tr>                                                        
                       <tr>
                          <td class="tbldashed-gray" colspan="3">Uploaded by: <asp:Literal ID="lUploadedByName" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "uploadedbyName"))%>' Visible="true"></asp:Literal>&nbsp; (<asp:Label ID="lUploadedDate" tooltip="Uploaded Date" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "UploadedDate"))%>' Visible="TRUE"></asp:Label>)</td>
                        </tr>   
                    </ItemTemplate>
                    <FooterTemplate>
                                <tr>
                                    <td style="border-top:solid 1px #ffffff;padding-right:5px;" colspan="3" align="right">
                                    <b><asp:Literal ID="ltotalfilesize" runat="server"></asp:Literal></b></td>
                                </tr>
                                </table>
                    </FooterTemplate>

                </asp:Repeater>    


<%--                <table>
                    <tr>
                        <td class="labelFreeForm" valign="top">Version:</td>
                        <td class="dataFreeForm" valign="top" style="padding-right:10px"><asp:Label ID="lDVVersion" runat="server" Text=""></asp:Label>.0</td>
                        <td class="labelFreeForm" valign="top">Document Name:</td>
                        <td class="dataFreeForm" valign="top" style="padding-right:10px"><asp:Label ID="lDVFileName" runat="server" Text=""></asp:Label></td>
                        
                    </tr>
                    <tr>
                    <td class="labelFreeForm" valign="top">Uploaded by:</td>
                        <td class="dataFreeForm" valign="top" style="padding-right:10px"><asp:Label ID="lDVUploadedBy" runat="server" Text=""></asp:Label></td>
                        <td class="labelFreeForm" valign="top">Uploaded Date:</td>
                        <td class="dataFreeForm" valign="top"><asp:Label ID="lDVUploadedDate" runat="server" Text=""></asp:Label></td>                        
                    </tr>
                    <tr>
                        <td class="labelFreeForm" valign="top">User Comment:</td>
                        <td colspan="3" class="dataFreeForm" valign="top"><asp:Label ID="lDVComments" runat="server" Text=""></asp:Label></td>                                               
                    </tr>
                </table>    --%>