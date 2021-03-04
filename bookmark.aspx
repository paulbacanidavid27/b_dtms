<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="bookmark.aspx.vb" Inherits="dms.bookmark" %>
<%@ MasterType VirtualPath="~/Site.Master"  %>
<%@ Register src="UserControlUpload.ascx" tagname="UserControlDocumentUpload" tagprefix="uc" %>    
<%@ Register src="ucButton.ascx" tagname="ucButton" tagprefix="uc" %>    
<asp:Content ID="Content4" ContentPlaceHolderID="MenuContent" runat="server">

<uc:ucButton id="ucAddDoc" runat="server" pText="Upload Document" pImage="images/upload2.png"></uc:ucButton>    
</asp:content>
<asp:Content ID="cntUpload" runat="server" ContentPlaceHolderID="cntntUpload">   
    <uc:UserControlDocumentUpload runat="server" id="ucUpload" visible="False"/>
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<title>Document Bookmark</title>
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="MainHeaderContent">

                <table border="0" width="100%" class="tblHdr_1">
        <tr>
            <td class="tableheader_1"><img  alt="" src="images/bookmark_icon.png" height="20px" width="20px" style="vertical-align:middle" />&nbsp;Bookmarked Documents</td>
            <td align="right">
                
            </td>
        </tr>        
    </table>                           
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mainDiv_" align="left">
      <asp:UpdatePanel ID="pnlRepeater" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    <asp:Panel id="pRepeater" runat="server">  
    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="tblHdr2">
    <tr>
        <td valign="top">
            <div style="width:100%;margin-top:2px;margin-left:2px">
               
           
                <asp:Panel ID="idSrchRslt" runat="server" Visible="true" style="height:auto;width:100%">
                
                    <table width="99%" cellspacing="0" cellpadding="0" border="0">
                        <tr>
                            <td class="tableHeadxx"><asp:Label ID="lNo" runat="server" Text="" Visible="false" style="font-style:italic;font-size:8pt"></asp:Label> <asp:Label ID="lNoOfRecord" Visible="false" runat="server" Text=" record(s) found" style="font-style:italic;font-size:8pt"></asp:Label></td>                            
                         </tr>
                         
                    </table>        
                
                </asp:Panel>
                <div id="tblgrid" style="width:100%">                
                    <table width="99%" border="0" style="border-collapse:collapse" cellpadding="0" cellspacing="0">
                    <tr><td colspan="2" ></td></tr>    
                    <asp:Repeater ID="Repeater1" visible="true" runat="server" >
                        <HeaderTemplate>
                                    
                        </HeaderTemplate>
                        <ItemTemplate>                
                            <tr id="rw1" runat="server" style="background-color:white" >
                                <td align="center" valign="top"><asp:ImageButton ID="imgUpd"  ClientIDMode="Static" ToolTip='View Document' runat="server" height="35px" width="25px" imageurl='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "doctypeimg"))%>' /></td>
                                <td>
                                    <table width="100%">
                                        <tr>
                                               <td class="rowclass" width="80%">
                                               <asp:LinkButton ID="lbBM" runat="server" OnClick="ViewDoc" OnClientClick="ufShowIP(this)" style="color:Blue;font-style:italic" onmouseover="this.style.textDecoration = 'underline',this.style.color='#00005E'" onmouseout="this.style.textDecoration = 'none',this.style.color='blue'" >
                                                        <asp:Label ID="lTitle" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "title"))%>' Tooltip="Title"></asp:Label>
                                               </asp:LinkButton>
                                               </td>
                                                <td width="20%" align="right" valign="top">
                                                    <asp:ImageButton ID="ImgBox"  ToolTip='Select this document' runat="server" imageUrl="images/box.png" onmouseover="this.src='images/box_h.png'" onmouseout="this.src='images/box.png'"/>
                                                    <asp:ImageButton ID="ImgCheckBox"  ToolTip='Unselect this document' runat="server" imageUrl="images/checkbox.png" onmouseover="this.src='images/checkbox_h.png'" onmouseout="this.src='images/checkbox.png'" Visible="false"/>
                                                    <asp:ImageButton ID="ImgTag"  ToolTip='Tag this document' runat="server" imageUrl="images/tag.png" onmouseover="this.src='images/tag_h.png'" onmouseout="this.src='images/tag.png'"/>
                                                    <asp:ImageButton ID="ImgLink"  ToolTip='Show links of this document' runat="server" imageUrl="images/link.png" onmouseover="this.src='images/link_h.png'" onmouseout="this.src='images/link.png'"/>
                                                    <asp:ImageButton ID="ImgUnbookmark" ToolTip='Remove your bookmark for this document' runat="server" imageUrl="images/bookmark_h.png" onmouseover="this.src='images/bookmark.png'" onmouseout="this.src='images/bookmark_h.png'"/>
                                                    <asp:ImageButton ID="ImgBookmark" ToolTip='Bookmark this document' runat="server" imageUrl="images/bookmark.png" onmouseover="this.src='images/bookmark_h.png'" onmouseout="this.src='images/bookmark.png'"/>
                                                <asp:Literal ID="lDocId" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DocId"))%>' visible="false"></asp:Literal>
                                                <asp:Literal ID="lGroupAccessId" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "GroupAccessId"))%>' visible="false"></asp:Literal>
                                                <asp:Literal ID="lDocType" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DocType"))%>' visible="false"></asp:Literal>
                                                <asp:Literal ID="lFileName" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "FileName"))%>' visible="false"></asp:Literal>
                                                <asp:Literal ID="lBookmarked" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Bookmarked"))%>' visible="false"></asp:Literal>
                                                
                                                </td>
                                        </tr>
                                        
                                        <tr>                                       
                                            <td class="rowclass1" colspan="2">Uploaded By:<span style="font-style:italic;color:black;font-size:9pt"> <asp:Literal ID="lAuthor" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Originator"))%>'></asp:Literal></span>&nbsp;&nbsp;Created Date:<span style="font-style:italic;color:black;font-size:9pt"> <asp:Literal ID="lCreatedDate" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CreatedDate"))%>'></asp:Literal></span> Status: <span style="font-style:italic;color:black;font-size:9pt"><asp:Literal ID="Literal1" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "description"))%>'></asp:Literal></span> Document Type: <span style="font-style:italic;color:black;font-size:9pt"><asp:Literal ID="Literal2" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "docname"))%>'></asp:Literal></td>
                                        </tr>
                                        <tr>                                       
                                            <td class="rowclass1" colspan="2">Reference No:<span style="font-style:italic;color:black;font-size:9pt"> <asp:Literal ID="lrefno" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "refno"))%>'></asp:Literal></span></td>
                                        </tr>
                                        <tr>                                       
                                            <td class="rowclass2"  colspan="2">
                                            <asp:panel ID="pTag" runat="server" Visible="false">
                                            <asp:Repeater ID="rptTags" visible="true" runat="server" >
                                            <HeaderTemplate>
                                                <table width="100%" border="0" style="border-collapse:collapse" cellpadding="0" cellspacing="0">            
                                                    
                                                    <tr><td colspan="2" valign="top" >Tags:
                                            </HeaderTemplate>
                                            <ItemTemplate>                
                                                    <span><u/><asp:Literal ID="lTags" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "tags"))%>' visible="true"></asp:Literal></u>
                                                        </span>
                                                    
                                                    
                    
                                            </ItemTemplate>
                                            
                                            <FooterTemplate>
                                                </td>
                                                </tr>                
                                                </table>                                
                                            </FooterTemplate>
                                        </asp:Repeater>
                                            Tag: <asp:TextBox ID="txtTag" runat="server" width="200px" cssclass="entryfld"></asp:TextBox><asp:Button
                                                ID="btSaveTag" runat="server" Text="Save" class="btnsmall"/>
                                            </asp:panel></td>
                                        </tr>
                                        <tr>                                       
                                            <td class="rowclass2"  colspan="2">
                                    
                                            <asp:Repeater ID="rptLinks" visible="false" runat="server" >
                                            <HeaderTemplate>
                                                <table width="100%" border="0" style="border-collapse:collapse" cellpadding="0" cellspacing="0">            
                                                    
                                                    <tr><td colspan="2" valign="top" >Links:
                                            </HeaderTemplate>
                                            <ItemTemplate>                
                                                    <span>
                                                            <asp:LinkButton ID="lbDoc" runat="server"  onclick="ViewDoc" cssclass="nb"><%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Title"))%></asp:LinkButton>
                                                            <asp:ImageButton ID="imgLinkDelete" runat="server" imageurl="images/del.png"  visible="false"/>&nbsp;
                                                            <asp:Literal ID="lDocId" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DocId"))%>' visible="false"></asp:Literal>                                                            
                                                            <asp:Literal ID="lLinkDocId" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "LinkDocId"))%>' visible="false"></asp:Literal>                                                            
                                                            <asp:Literal ID="lLocation" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Location"))%>' visible="false"></asp:Literal>
                                                            <asp:Literal ID="lFileName" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Filename"))%>' visible="false"></asp:Literal>
                                                            <asp:Literal ID="lStatus" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "statusdesc"))%>' visible="false"></asp:Literal>                                                            
                                                        </span>
                                                    
                                                    
                    
                                            </ItemTemplate>
                                            
                                            <FooterTemplate>
                                                </td>
                                                </tr>                
                                                </table>                                
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    
                                            </td>
                                        </tr>
                                        
                                    </table>
                                </td>                         
                        
                            </tr>                
                            <tr>
                                <td class="tbldashed" colspan="2">                                   
                                </td>
                                </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr>
            <td style="border-top:solid 1px #ffffff" colspan="2"></td>
        </tr></table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <table>
                     <tr>
                        <td colspan="2" style="text-align:center"><asp:Label ID="lMsg" visible="false" style="font-family: Arial;font-size:12pt;color:red" runat="server" Text="No documents has been bookmark yet. "></asp:Label></td></tr>           
                            
                     <tr>
                        <td colspan="2"></td></tr>                                       
                    </table>                                
                </div>    
            </div>
        </td>        
    </tr>
    </table>


        </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>
</asp:Content>