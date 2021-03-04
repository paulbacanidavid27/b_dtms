<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Search.aspx.vb" Inherits="dms.Search" EnableEventValidation="true" %>
<%@ MasterType VirtualPath="~/Site.Master"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register src="UserControlDocumentIndex.ascx" tagname="UserControlDocumentIndex" tagprefix="uc" %>    
<%@ Register src="UserControlUpload.ascx" tagname="UserControlDocumentUpload" tagprefix="uc" %>    
<%@ Register src="ucUploadButton.ascx" tagname="ucBtnUpload" tagprefix="uc" %>    
<%@ Register src="UserControlPager.ascx" tagname="UserControlPager" tagprefix="uc" %>

<asp:Content ID="Content5" runat="server" ContentPlaceHolderID="HeaderMenuContent">
    <table cellpadding="0" cellspacing="0" border="0" width="100%" class="tableheaderWhite">
                   <tr >
                        <td valign="middle">
                        </td>
                        </tr>
                        </table>
</asp:Content>
<asp:Content ID="Content6" runat="server" ContentPlaceHolderID="AddContent">
    <table cellpadding="0" cellspacing="0" border="0" width="100%" class="tableheaderWhite">
                   <tr >
                        <td valign="middle">
                        </td>
                        </tr>
                        </table>
</asp:Content>
<asp:Content ID="Content7" runat="server" ContentPlaceHolderID="MainFooterContent">
    <table cellpadding="0" cellspacing="0" border="0" width="100%" class="tableheaderWhite">
                   <tr >
                        <td valign="middle">
                        </td>
                        </tr>
                        </table>
</asp:Content>

<asp:Content ID="cntUpload" runat="server" ContentPlaceHolderID="cntntUpload">   


    <uc:UserControlDocumentUpload runat="server" id="ucUpload" visible="False"/>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MenuContent" runat="server">
    <br />
    <uc:ucBtnUpload runat="server" id="ucUBtn" visible="true"/>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Home</title>
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainHeaderContent">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="pnlBk" runat="server" UpdateMode="Conditional">
                       <ContentTemplate>
    <%--<asp:Panel ID="idAdvSrch" runat="server" Visible="false"  cssclass="newtblheader2" style="margin-top:0px;height:auto;width:100%;">--%>
                    <table  width="100%" cellspacing="0" cellpadding="0" border="0">
                        
                        <tr>
                            <td  style="vertical-align:top;border-bottom:solid 0px #D4D4D4">
                                <table class="tableheaderGreen" border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr height="25px">
                                        <td align="left">&nbsp;&nbsp;Advanced Search</td>
                                        <td width="50px" align="right" valign="top" class="tableHead27" >
                                        <asp:ImageButton ID="imgBk" runat="server" imageurl="images/hidepanel.png"/></td>
                                    </tr>
                                </table>                            
                            </td>
                         </tr>       
                         
                         <tr>
                            <td align="center" style="background-color:#FFFFFF">
                                <asp:panel runat="server" id="pnlAS" Visible="true" DefaultButton="btSearch" style="text-shadow:0px 0px White">
                                <table border="0"  cellspacing="1" cellpadding="0" width="97%">
                                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                                        <td align="right" style="color:Black"><asp:Panel ID="partialexact" runat="server" Visible="true">
                                    <asp:RadioButton ID="rbPartial" runat="server" GroupName="SearchType" Text="Partial" Checked="true"/><asp:RadioButton ID="rbExact" runat="server" GroupName="SearchType" Text="Exact"/>
                                </asp:Panel></td>
                          </tr>
                                    <tr>
                                        
                                        <td align="right" width="150px" class="labelFreeForm" style="padding-right:10px">
                                            Reference Number
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="tbRefNo" runat="server" cssclass="entryfldw"  Width="170px"></asp:TextBox>
                                        </td>
                                        <td align="right" width="150px"  class="labelFreeForm"  style="padding-right:10px">
                                            Archived By
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="tbArchivedBy" runat="server" cssclass="entryfldw"  Width="175px"></asp:TextBox>
                                            <cc1:autocompleteextender runat="server" ID="Autocompleteextender1" TargetControlID="tbArchivedBy"
                                                 ServiceMethod="getUsers" ServicePath="getUser.asmx" CompletionInterval="800" EnableCaching="true"
                                                  MinimumPrefixLength="1"
                                                 completionsetcount="25" FirstRowSelected ="false" />
                                        </td>
                                        
                                    </tr>
                                    <tr>
                                        
                                        <td align="right" width="150px" class="labelFreeForm" style="padding-right:10px">
                                            Title/Subject
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="tbDocTitle" runat="server" cssclass="entryfldw"  Width="170px"></asp:TextBox>
                                        </td>
                                        <td align="right" width="150px"  class="labelFreeForm"  style="padding-right:10px">
                                            Uploaded By
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="tbAuthor" runat="server" cssclass="entryfldw"  Width="175px"></asp:TextBox>
                                            <cc1:autocompleteextender runat="server" ID="acomplete" TargetControlID="tbAuthor"
                                                 ServiceMethod="getUsers" ServicePath="getUser.asmx" CompletionInterval="800" EnableCaching="true"
                                                  MinimumPrefixLength="1"
                                                 completionsetcount="25" FirstRowSelected ="false" />
                                        </td>
                                        
                                    </tr>
                                    <tr>
                                        <td align="right" width="150px"  class="labelFreeForm"  style="padding-right:10px">
                                            Last Action
                                        </td>
                                        <td align="left">
                                        <asp:DropDownList ID="dlStatus" runat="server" cssclass="entryfldw2" Width="175px"></asp:DropDownList>
                                            
                                        </td>
                                        <td align="right" width="150px"  class="labelFreeForm"  style="padding-right:10px">
                                            Tags
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="tbTags" runat="server" cssclass="entryfldw"  Width="175px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="150px"  class="labelFreeForm"  style="padding-right:10px">
                                            Status
                                        </td>
                                        <td align="left">
                                        <asp:DropDownList ID="dlDocStatus" runat="server" cssclass="entryfldw2" Width="175px">
                                        <asp:ListItem Text="-All-" Value="">
                                        </asp:ListItem>
                                        <asp:ListItem Text="Open" Value="O">
                                        </asp:ListItem>
                                        <asp:ListItem Text="Completed/Closed" Value="C">
                                        </asp:ListItem>
                                        </asp:DropDownList>
                                            
                                        </td>
                                        <td align="right" width="150px"  class="labelFreeForm"  style="padding-right:10px">
                                            Sender
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="tbSender" runat="server" cssclass="entryfldw"  Width="175px"></asp:TextBox>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td align="right" width="150px"  class="labelFreeForm"  style="padding-right:10px">
                                            Type of Request
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="dlRequestType" runat="server" cssclass="entryfldw2" Width="175px"></asp:DropDownList>
                                            
                                        </td>
                                                                                                                   
                                        
                                         <td align="right" width="150px"  class="labelFreeForm"  style="padding-right:10px">
                                            Created Date
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="tbDCFrom" runat="server" cssclass="entryfldw"  Width="67px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="tbDCFrom" />

                                            -
                                            <asp:TextBox ID="tbDCTo" runat="server" cssclass="entryfldw"  Width="67px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="tbDCTo"  />
                                        </td>
                                    </tr>
                                    <tr>
                                    <td align="right" width="150px"  class="labelFreeForm"  style="padding-right:10px">
                                        Classification
                                            
                                        </td>
                                        <td align="left">
                                            
                                            <asp:DropDownList ID="dlClassification" runat="server" cssclass="entryfldw2" Width="175px"  AutoPostBack="false">
                                                <asp:ListItem Value="" Text="" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="0" Text="External" Selected="False"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Internal" Selected="False"></asp:ListItem>
                                            </asp:DropDownList>
                                            
                                        </td>
  
                                    <td align="right" width="150px"  class="labelFreeForm"  style="padding-right:10px">Personnel In-Charge</td><td><asp:TextBox ID="tbPIC" runat="server" cssclass="entryfldw"  Width="250px"></asp:TextBox></td></tr>
                                    
                                    <tr>
                                    <td align="right" width="150px"  class="labelFreeForm"  style="padding-right:10px">
                                        Document Type
                                            
                                        </td>
                                        <td align="left">
                                        <asp:DropDownList ID="dlDocType" runat="server" cssclass="entryfldw2" Width="175px"  AutoPostBack="true"></asp:DropDownList>
                                            
                                        </td>
                                    <td class="labelFreeForm" nowrap>
                                        <asp:Literal ID="lShowPurged" runat="server" Text='Show Only Purged Documents'></asp:Literal></td><td><asp:CheckBox ID="cbPurged" runat="server" /></td></tr>
                                    <tr>
                                        <td colspan="4">
                                        <asp:UpdatePanel runat="server" ID="plIndex" UpdateMode="Conditional">
                                             <ContentTemplate>
                                                <uc:UserControlDocumentIndex id="ucDocIndex" runat="server"></uc:UserControlDocumentIndex>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="text-align:right;padding-bottom:10px;">
                                            <div id="prc" style="display: none ; font-style: italic;font-size:10px;color:Blue;" CssClass="btn">
                                            <img src="images/processing.gif" /> Searching ...
                                        </div>
                                        <div id="pbtn" style=" display: inline">
                                            <asp:Button ID="btClear" runat="server" CssClass="btn" Text="Clear" style="margin:3px 3px 3px 3px" OnClientClick="document.getElementById('pbtn').style.display='none';document.getElementById('prc').style.display='';return true;"/> <asp:Button ID="btSearch" runat="server" CssClass="btn" Text="Search" style="margin:3px 3px 3px 3px" OnClientClick="document.getElementById('pbtn').style.display='none';document.getElementById('prc').style.display='';return true;"/>
                                        </div>
                                        </td>
                                    </tr>
                                    
                                </table>
                                </asp:panel>
                            </td>
                         </tr>                         
                         
                    </table>        
               <%-- </asp:Panel>--%>
                </ContentTemplate>
                    </asp:UpdatePanel>
                    
 
      <asp:UpdatePanel ID="pnlRepeater" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    <asp:Panel id="pRepeater" runat="server" Width="100%" STYLE="border-left:solid 1px #C0C0C0;border-top:solid 1px #C0C0C0;border-right:solid 1px #C0C0C0;text-align:left">  
    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="tblHdr2X">
    <tr>
        <td valign="top">
            <div style="width:100%">
                
                
                <asp:Panel ID="idSrchRslt" runat="server" Visible="false" style="height:auto;width:100%">
                
                    <table width="100%" class="newtblheader" cellspacing="0" cellpadding="0" border="0" >
                        <tr>
                            <td style="padding-left:3px"><asp:Label ID="lNo" runat="server" Text="" Visible="false" style="font-weight:bold ;font-size:10pt;color:#666666"></asp:Label> <asp:Label ID="lNoOfRecord" Visible="false" runat="server" Text=" record(s) found" style="color:#808080;font-style:normal;font-size:8pt; text-transform:none"></asp:Label></td>                                                        
                            <td align="right" >
                                      <table>
                        <tr><td valign="middle"><asp:Label ID="lPageCount" runat="server" Text="" style="font-style:italic;font-size:8pt;font-style:italic;color:blue"></asp:Label></td>
                            <td valign="top">      
                            <asp:UpdatePanel ID="pPager" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                <asp:HiddenField ID="hfCurrent" runat="server" Value=""/>                                            
                                            <asp:HiddenField ID="hfTotalRows" runat="server" Value="0"/>
                                            <asp:HiddenField ID="hfSortCol" runat="server" Value=""/>
                                            <asp:HiddenField ID="hfSortOrder" runat="server" Value=""/>
                                            <uc:UserControlPager ID="ucPager" runat="server" />
                                </ContentTemplate>
                                </asp:UpdatePanel>
                                             </td>
                        </tr>
                        </table>
                            </td>
                         </tr>
                         
                    </table>        
                
                </asp:Panel>
                <div id="tblgrid" style="width:100%">                
                    <table width="100%" border="0" style="border-collapse:collapse" cellpadding="0" cellspacing="0">
                    <tr><td colspan="2" ></td></tr>    
                    <asp:Repeater ID="Repeater1" visible="true" runat="server" >
                        <HeaderTemplate>
                                    
                        </HeaderTemplate>
                        <ItemTemplate>                
                            <tr id="rw1" runat="server" style="background-color:white" >
                                <td class="search0" align="center" valign="top">
                                                               
                            
                                <asp:ImageButton ID="imgUpd" runat="server" height="35px" width="25px" imageurl='' ToolTip='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "refno"))%>'/>
                                <div style="color:#C0C0C0;font-size:7pt;">
                                    <asp:Label ID="Label1" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "rn"))%>' ></asp:Label>
                                    </div>
                                <%--<asp:ImageButton ID="ImageButton1" ToolTip='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "doctypeimg"))%>' runat="server" height="35px" width="25px" imageurl='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "doctypeimg"))%>'/>--%></td>
                                <td class="search0">
                                    <table width="100%">
                                        <tr>
                                               <td class="rowclass" width="80%">
                                               <asp:LinkButton ID="lbBM" runat="server" OnClick="ViewDoc" style="color:Blue;font-style:italic" onmouseover="this.style.textDecoration = 'underline',this.style.color='#00005E'" onmouseout="this.style.textDecoration = 'none',this.style.color='blue'" >
                                                        <asp:Literal ID="lTitle" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "title"))%>'></asp:Literal>
                                               </asp:LinkButton>

                                               </td>
                                                <td width="20%" align="right" valign="top">
                                                    <asp:ImageButton ID="ImgBox"  ToolTip='Select this document' runat="server" imageUrl="images/box.png" onmouseover="this.src='images/box_h.png'" onmouseout="this.src='images/box.png'"/>
                                                    <asp:ImageButton ID="ImgCheckBox"  ToolTip='Unselect this document' runat="server" imageUrl="images/checkbox.png" onmouseover="this.src='images/checkbox_h.png'" onmouseout="this.src='images/checkbox.png'" Visible="false"/>
                                                    <asp:ImageButton ID="ImgTag"  ToolTip='Show Tags' runat="server" imageUrl="images/tag.png" onmouseover="this.src='images/tag_h.png'" onmouseout="this.src='images/tag.png'"/>
                                                    <asp:ImageButton ID="ImgLink"  ToolTip='Show links of this document' runat="server" imageUrl="images/link.png" onmouseover="this.src='images/link_h.png'" onmouseout="this.src='images/link.png'"/>
                                                    <asp:ImageButton ID="ImgUnbookmark" ToolTip='Remove your bookmark for this document' runat="server" imageUrl="images/bookmark_h.png" onmouseover="this.src='images/bookmark.png'" onmouseout="this.src='images/bookmark_h.png'"/>
                                                    <asp:ImageButton ID="ImgBookmark" ToolTip='Bookmark this document' runat="server" imageUrl="images/bookmark.png" onmouseover="this.src='images/bookmark_h.png'" onmouseout="this.src='images/bookmark.png'"/>
                                                    <asp:Literal ID="lDocId" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DocId"))%>' visible="false"></asp:Literal>
                                                    <asp:Literal ID="lPurgedDate" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "PurgedDate"))%>' visible="false"></asp:Literal>                                                            
                                                    <asp:Literal ID="lGroupAccessId" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "GroupAccessId"))%>' visible="false"></asp:Literal>
                                                    <asp:Literal ID="lDocType" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DocType"))%>' visible="false"></asp:Literal>
                                                    <asp:Literal ID="lFileName" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "FileName"))%>' visible="false"></asp:Literal>
                                                    
                                                    <asp:Literal ID="lBookmarked" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Bookmarked"))%>' visible="false"></asp:Literal>                                                
                                                </td>
                                        </tr>
                                        <tr>
                                            <td class="rowclass1" colspan="2"><span class="rsLabel">Filename:</span><span class="rsLabelColGreen"><%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Filename"))%></span><span class="rsLabel">Document Type:</span> <span class="rsLabelColGreen"><%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "docname"))%></span><span class="rsLabel">Status:</span> <span class="rsLabelColGreen"><%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "description"))%></span></td>
                                        </tr>                                        
                                        <tr>                                       
                                            <td class="rowclass1" colspan="2"><span class="rsLabel">Uploaded By:</span><span class="rsLabelColNavy"><%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Originator"))%></span><span class="rsLabel">Created Date:</span><span class="rsLabelColNavy"><%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CreatedDate"))%></span><span class="rsLabel">Classification:</span><span class="rsLabelColNavy"><%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Classification"))%></span></td>
                                        </tr>
                                        <tr>                                       
                                            <td class="rowclass1" colspan="2"><span class="rsLabel">Personnel In-Charge:</span><span class="rsLabelColBlack"><%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "PersonnelInCharge"))%></span></td>
                                        </tr>
                                        <tr>                                       
                                            <td class="rowclass1" colspan="2"><span class="rsLabel">Reference No:</span><span class="rsLabelColBlack"><asp:Literal ID="lrfno" runat="server" text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "refno"))%>'></asp:Literal></span><asp:Panel ID="pBox" visible="false" runat="server" style="display:inline;">
                                                    <asp:Label ID="valueLabel" runat="server" Text="" cssclass="rsLabel"></asp:Label><span class="rsLabelColBlack"><%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "colvalue"))%></span>
                                                </asp:Panel>    
                                        </td>
                                        </tr>                                        
                                        <tr>                                       
                                            <td class="rowclass2"  colspan="2">
                                            <asp:panel ID="pTag" runat="server" Visible="false">
                                            <%--Tag: <asp:TextBox ID="txtTag" runat="server" width="200px" cssclass="entryfldw"></asp:TextBox><asp:Button
                                                ID="btSaveTag" runat="server" Text="Save" class="btnsmall"/>--%>
                                            
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
                                        </asp:panel>
                                            </td>
                                        </tr>
                                        <%--<tr>                                       
                                            <td class="rowclass2"  colspan="2">
                                            <asp:panel ID="pReceiving" runat="server" Visible="false">
                                            Received By: <asp:TextBox ID="txtReceivedBy" runat="server" width="150px" cssclass="entryfldw" Text=''></asp:TextBox>
                                            Received Date/Time: <asp:TextBox ID="txtReceivedDate" runat="server" width="68px" cssclass="entryfldw" Text=''></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtReceivedDate"   />
                                            <asp:TextBox ID="txtReceivedTime" runat="server" width="62px" cssclass="entryfldw"  Text=''></asp:TextBox>
                                            <cc1:MaskedEditExtender id="MeExt" runat="server" TargetControlID="txtReceivedTime"  AcceptAMPM="true" MaskType="Time" Mask="99:99" InputDirection="LeftToRight" AcceptNegative="None"/>
                                            <asp:Button ID="btReceivedBy" runat="server" Text="Save" class="btnsmall"/>
                                            </asp:panel></td>
                                        </tr>--%>
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
                                   <%-- --%>
                                </td>                         
                        
                            </tr>                
                            <tr>
                                <td colspan="2" class="tbldashed"></td>
                            </tr>                                   
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr>
                                <td colspan="2" class="dashremover"></td>
                            </tr>                                   
                        </FooterTemplate>
                    </asp:Repeater>
                    
                     <tr>
                        <td colspan="2" style="text-align:center"><asp:Label ID="lMsg"  visible="true" cssclass="msg_red" runat="server" Text=""></asp:Label></td></tr>           
                            
                     
                    </table>                                
                </div>    
            </div>
        </td>        
    </tr>
    </table>


        </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>
 
</asp:Content>
