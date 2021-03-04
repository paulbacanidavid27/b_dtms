<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="DocSearch.aspx.vb" Inherits="dms.DocSearch" EnableEventValidation="true" %>
<%@ MasterType VirtualPath="~/Site.Master"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register src="UserControlDocumentIndex.ascx" tagname="UserControlDocumentIndex" tagprefix="uc" %>    

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mainDiv">
      <asp:UpdatePanel ID="pnlRepeater" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    <asp:Panel id="pRepeater" runat="server">  
    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="tblHdr2">
    <tr>
        <td valign="top">
            <div style="width:100%;margin-top:2px;margin-left:2px">
                
                <asp:Panel ID="idAdvSrch" runat="server" Visible="false" style="height:auto;width:99%">
                    <table width="100%" cellspacing="0" cellpadding="0" border="0">
                         <tr>
                            <td class="hdrtitle">&nbsp;Advanced Search</td>
                         </tr>
                         <tr>
                         <td><hr style="width:100%;color:gray" /></td>
                         </tr>
                         <tr>
                            <td align="left">
                                
                                <table border="0"  cellspacing="1" cellpadding="3" width="100%" style="border:solid 1px gray;">
                                    <tr>
                                        
                                        <td align="left" width="150px" class="labelFreeForm" style="padding-left:10">
                                            Document Title:
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="tbDocTitle" runat="server" cssclass="entryfld"  Width="175px"></asp:TextBox>
                                        </td>
                                        <td align="left" width="150px"  class="labelFreeForm"  style="padding-left:10">
                                            Author:
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="tbAuthor" runat="server" cssclass="entryfld"  Width="175px"></asp:TextBox>
                                        </td>
                                        <td align="left" width="150px"  class="labelFreeForm"  style="padding-left:10">
                                            Status:
                                        </td>
                                        <td align="left">
                                        <asp:DropDownList ID="dlStatus" runat="server" cssclass="entryfld2" Width="175px"></asp:DropDownList>
                                            
                                        </td>
                                    </tr>
                                     <tr>
                                        <td align="left" width="150px"  class="labelFreeForm"  style="padding-left:10">
                                        Document Type:
                                            
                                        </td>
                                        <td align="left">
                                        <asp:DropDownList ID="dlDocType" runat="server" cssclass="entryfld2" Width="175px"  AutoPostBack="true"></asp:DropDownList>
                                            
                                        </td>
                                       
                                        <td align="left" width="150px"  class="labelFreeForm"  style="padding-left:10">
                                            Tags:
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="tbTags" runat="server" cssclass="entryfld"  Width="175px"></asp:TextBox>
                                        </td>
                                         <td align="left" width="150px"  class="labelFreeForm"  style="padding-left:10">
                                            Date Created:
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="tbDCFrom" runat="server" cssclass="entryfld"  Width="60px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="tbDCFrom" />

                                            -
                                            <asp:TextBox ID="tbDCTo" runat="server" cssclass="entryfld"  Width="60px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="tbDCTo"  />
                                        </td>
                                    </tr>

                                </table>
                            </td>
                         </tr>
                         <tr>
                            <td >
                            
                            <asp:UpdatePanel runat="server" ID="plIndex" UpdateMode="Conditional">
                                 <ContentTemplate>
                               <uc:UserControlDocumentIndex id="ucDocIndex" runat="server"></uc:UserControlDocumentIndex>
                        </ContentTemplate>
                            </asp:UpdatePanel>
                        
                            </td>
                         </tr>
                         <tr>
                            <td style="text-align:right;"><asp:Button ID="btSearch" runat="server" CssClass="btn" Text="Search" style="margin:3px 3px 3px 3px"/></td>
                         </tr>
                    </table>        
                </asp:Panel>
                <asp:Panel ID="idSrchRslt" runat="server" Visible="false" style="height:auto;width:100%">
                
                    <table width="99%" cellspacing="0" cellpadding="0" border="0" style="border-bottom:solid 1px gray;background-color:#eAEAEA;border-right:solid 1px gray;border-left:solid 1px gray;border-top:solid 1px gray;">
                        <tr>
                            <td>&nbsp;Search Results - <asp:Label ID="lNo" runat="server" Text="" Visible="false" style="font-style:italic;font-size:8pt"></asp:Label> <asp:Label ID="lNoOfRecord" Visible="false" runat="server" Text=" record(s) found" style="font-style:italic;font-size:8pt"></asp:Label></td>                                                        
                            <td align="right" >
                                      <table>
                        <tr>
                            <td valign="top">      <asp:HiddenField ID="hfCurrent" runat="server" Value="1"/>
                            <asp:HiddenField ID="hfTotalValue" runat="server" Value="0"/>
                                            <asp:ImageButton ID="imgLess" runat="server"  ToolTip="Retrieve previous actions" ImageUrl="images/arrow_left_h.png"  onmouseover="this.src='images/arrow_left.png'"  onmouseout="this.src='images/arrow_left_h.png'" Visible="False"/>
                                            <asp:ImageButton ID="imgLessD" runat="server"  style=" cursor:default" ImageUrl="images/arrow_left_h.png" Visible="True"/>
                                            <asp:ImageButton ID="imgALess" runat="server"  ToolTip="Retrieve previous actions" ImageUrl="images/arrow_left_h.png"  onmouseover="this.src='images/arrow_left.png'"  onmouseout="this.src='images/arrow_left_h.png'" Visible="False"/>
                                            <asp:ImageButton ID="imgALessD" runat="server"  style=" cursor:default" ImageUrl="images/arrow_left_h.png" Visible="false"/>                                            
                                            <td valign="middle"><asp:Label ID="lPageCount" runat="server" Text="" style="font-style:italic;font-size:8pt;font-style:italic;color:blue"></asp:Label></td>
                                           <td valign="bottom"> <asp:ImageButton ID="imgGreater" runat="server" ImageUrl="images/arrow_right_h.png" onmouseover="this.src='images/arrow_right.png'"  onmouseout="this.src='images/arrow_right_h.png'" Visible="false"/>                                 
                                            <asp:ImageButton ID="imgGreaterD" runat="server" style=" cursor:default" ImageUrl="images/arrow_right_h.png" Visible="False"/>
                                            <asp:ImageButton ID="imgAGreater" runat="server" ImageUrl="images/arrow_right_h.png" onmouseover="this.src='images/arrow_right.png'"  onmouseout="this.src='images/arrow_right_h.png'" Visible="false"/>                                            
                                            <asp:ImageButton ID="imgAGreaterD" runat="server" style=" cursor:default" ImageUrl="images/arrow_right_h.png"  Visible="false"/>
                                             </td>
                        </tr>
                        </table>
                            </td>
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
                                <td class="search" align="center" valign="top"><asp:ImageButton ID="imgUpd" runat="server" height="35px" width="25px" imageurl=''/>
                                <%--<asp:ImageButton ID="ImageButton1" ToolTip='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "doctypeimg"))%>' runat="server" height="35px" width="25px" imageurl='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "doctypeimg"))%>'/>--%></td>
                                <td class="search">
                                    <table width="100%">
                                        <tr>
                                               <td class="rowclass" width="80%">
                                               <asp:LinkButton ID="lbBM" runat="server" OnClick="ViewDoc" style="color:Blue;font-style:italic" onmouseover="this.style.textDecoration = 'underline',this.style.color='#00005E'" onmouseout="this.style.textDecoration = 'none',this.style.color='blue'" >
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
                                            <td class="rowclass1" colspan="2"><span style="color:#808080">Filename:</span><span style="font-style:italic"> <asp:Literal ID="Literal3" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Filename"))%>'></asp:Literal></span>&nbsp;&nbsp;&nbsp;<span style="color:#808080">Document Type:</span> <span style="font-style:italic"><asp:Literal ID="Literal2" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "docname"))%>'></asp:Literal></span>&nbsp;&nbsp;&nbsp;<span style="color:#808080"> Status:</span> <span style="font-style:italic"><asp:Literal ID="Literal1" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "description"))%>'></asp:Literal></span></td>
                                        </tr>                                        
                                        <tr>                                       
                                            <td class="rowclass1" colspan="2"><span style="color:#808080">Originator:</span><span style="font-style:italic"> <asp:Literal ID="lAuthor" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Originator"))%>'></asp:Literal></span>&nbsp;&nbsp;&nbsp;<span style="color:#808080">Created Date:</span><span style="font-style:italic"> <asp:Literal ID="lCreatedDate" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CreatedDate"))%>'></asp:Literal></span>&nbsp;&nbsp;&nbsp;<span style="color:black">IP:</span> <span style="font-style:italic;color:blue"><asp:Literal ID="Literal5" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "IPAddress"))%>'></asp:Literal></span></td>
                                        </tr>
                                        <tr>                                       
                                            <td class="rowclass2" colspan="2">Tags: <span style="font-style:italic"><asp:Literal ID="ltags" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "doctags"))%>'></asp:Literal></span></td>
                                        </tr>
                                        <tr>                                       
                                            <td class="rowclass2"  colspan="2">
                                            <asp:panel ID="pTag" runat="server" Visible="false">
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
                                                            <asp:LinkButton ID="lbDoc" runat="server" cssclass="nb" onclick="ViewDoc"><%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Title"))%></asp:LinkButton>
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
                                                               
                        </ItemTemplate>
                        <FooterTemplate>
                            
                        </FooterTemplate>
                    </asp:Repeater>
                    
                     <tr>
                        <td colspan="2" style="text-align:center"><asp:Label ID="lMsg"  visible="true" cssclass="msg_red" runat="server" Text=""></asp:Label></td></tr>           
                            
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
