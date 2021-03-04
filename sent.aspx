<%@ Page Title="Document Sent" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="sent.aspx.vb" Inherits="dms.sent" %>
    <%@ MasterType VirtualPath="~/Site.Master" %>
    <%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register src="UserControlBookMark.ascx" tagname="UserControlBk" tagprefix="uc" %>    
<%@ Register src="UserControlUpload.ascx" tagname="UserControlDocumentUpload" tagprefix="uc" %>    
<%--<%@ Register src="ucReply.ascx" tagname="ucReplyDocument" tagprefix="uc" %>    --%>
<%@ Register src="UserControlPager.ascx" tagname="UserControlPager" tagprefix="uc" %>
<%@ Register src="ucHr.ascx" tagname="ucHr" tagprefix="uc1" %>
<%@ Register src="ucButton.ascx" tagname="ucButton" tagprefix="uc" %>    
<%@ Register src="ucConfirm.ascx" tagname="ucConfirm" tagprefix="uc" %>
<%@ Register src="UserControlCheckBox.ascx" tagname="UserControlCheckBox" tagprefix="uc" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">    
    <title>Document Inbox</title>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="MainHeaderContent">

                <table border="0" width="100%">
                   <tr >
                        <td class="tableheader_" valign="middle">
                        
                            <img src="images/doclist.png" ToolTip="Goto Inbox" height="20px" width="20px"/>
                            Document List
                            
                        </td>                 
                        <td>
                        
                        </td>
                        <td align="right">
                        <%--pager: step 2--%>
                
                <asp:UpdatePanel ID="pPager" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    <asp:HiddenField ID="hfCurrent" runat="server" Value="1"/>
                <asp:HiddenField ID="hfTotalRows" runat="server" Value="0"/>
                <asp:HiddenField ID="hfSortCol" runat="server" Value="Created Date"/>
                <asp:HiddenField ID="hfSortOrder" runat="server" Value="Desc"/>
                <uc:UserControlPager ID="ucPager" runat="server" />
                   </ContentTemplate>
    </asp:UpdatePanel>
                        </td>
                   </tr>
                    </table>                               
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <%--<asp:Panel style="padding:5px;border-radius:3px;border:solid 1px #CCCCCC;background-color:White;text-align:left;box-shadow:2px 2px 2px;" ID="PopupMenu" 
        runat="server">
        <asp:UpdatePanel ID="updFolderMenu" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Repeater ID="rptFolder" runat="server" Visible="True">
                <HeaderTemplate>                                       
                    <table  border="0" width="100%" cellpadding="0" cellspacing="0" style="border-collapse: collapse;">       
                        <tr>
                            <td>Select Folder where to move the documents</td>
                        </tr>                                                                
                </HeaderTemplate>
                <ItemTemplate>
                     
                     <tr>
                        <td style="padding:5px;font-size:13px;color:#5B5B5B">
                                    <div  class="xxx" style="width:350px;">
                                        <asp:LinkButton ID="lbFolder" onclientClick="ShowProgressInChild(this,'#Image2')" runat="server"  onclick="MoveDoc" style="color:#6671E7"  onmouseover="this.style.textDecoration = 'underline',this.style.color='BLUE'" onmouseout="this.style.textDecoration = 'none',this.style.color='#6671E7'" >
                                        <asp:Image ID="Image2"  ClientIDMode="Static" runat="server" style="vertical-align:middle;margin-right:3px" imageurl="images/fClose.png"/>
                                        <asp:Literal ID="lFolder" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "FolderDesc"))%>'></asp:Literal>
                                        </asp:LinkButton>
                                    </div> 
                                    <asp:Literal ID="lFolderId" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "FolderId"))%>' Visible="false"></asp:Literal>
                        </td>                        
                        
                    </tr>                                                                                        
                </ItemTemplate>
                <FooterTemplate>
                                </table>
                </FooterTemplate>

            </asp:Repeater>
             </ContentTemplate>
    </asp:UpdatePanel>
                      
    </asp:Panel>--%>
    
    <div class="mainDiv1" align="left">
      
    
    <!-- end - search criteria //-->
    <!-- start - resultset //-->                                                                
    <asp:UpdatePanel ID="pnlCon2" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <uc:ucConfirm id="ucCon2" runat="server" Visible="false" pText="Are you sure you want to move the selected document(s)? Please click OK to proceed."></uc:ucConfirm>             
    </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="pnlRepeater" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    <asp:Panel id="pRepeater" runat="server"> 
    <uc:ucConfirm id="ucCon" runat="server" Visible="false" pText="Are you sure you want to delete the selected document(s)? Please click OK to proceed."></uc:ucConfirm>     
    <!-- 01/17/2014 //-->                                                                        
    
      <%--              <div class="totalno">Total No of records: <asp:Literal ID="lRecordCount" runat="server"></asp:Literal></div>--%>
      
     <table border="0" class="codetbl" cellspacing="0" cellpadding="0" width="100%" style="border-collapse:collapse;z-index:900;border:#D4D4D4 solid 1px">
     
                    <tr >
                        <td class="newtblheader" align="center" nowrap><asp:LinkButton ID="lbSort9" runat="server" class="sortcol" tooltip="Sort by Urgency" OnClick="sortColumnHeader"  style="vertical-align:middle;">!</asp:LinkButton><asp:Image ID="imgSort9" imageurl="images/asc.png" runat="server" visible="false"  style="vertical-align:middle;"/></td>
                        <td class="newtblheader" align="center" nowrap>
                        <asp:LinkButton ID="lbSort8" runat="server" class="sortcol" tooltip="Sort by Flag" OnClick="sortColumnHeader"  style="vertical-align:middle;">F</asp:LinkButton><asp:Image ID="imgSort8" imageurl="images/asc.png" runat="server" visible="false" style="vertical-align:middle;"/>
                        </td>
                        <td class="newtblheader">
                            </td>
                        
                        <td class="newtblheader"><asp:LinkButton ID="lbSort2" runat="server" class="sortcol" tooltip="Sort by Title" OnClick="sortColumnHeader">Title</asp:LinkButton><asp:Image ID="imgSort2" imageurl="images/asc.png" runat="server" visible="false" style="vertical-align:middle;"/></td>
                        <td class="newtblheader"><asp:LinkButton ID="lbSort5" runat="server" class="sortcol" tooltip="Sort by Reference No" OnClick="sortColumnHeader">Reference No</asp:LinkButton><asp:Image ID="imgSort5" imageurl="images/asc.png" runat="server" visible="false" style="vertical-align:middle;"/></td>
                        <td class="newtblheader"><asp:LinkButton ID="lbSort1" runat="server" class="sortcol" tooltip="Sort by Document Type" OnClick="sortColumnHeader">Type</asp:LinkButton><asp:Image ID="imgSort1" imageurl="images/asc.png" runat="server" visible="true" style="vertical-align:middle;"/></td>
                        <td class="newtblheader"><asp:LinkButton ID="lbSort3" runat="server" class="sortcol" tooltip="Sort by Created Date" OnClick="sortColumnHeader">Created Date</asp:LinkButton><asp:Image ID="imgSort3" imageurl="images/asc.png" runat="server" visible="false" style="vertical-align:middle;"/></td>
                        <td class="newtblheader"><asp:LinkButton ID="lbSort6" runat="server" class="sortcol" tooltip="Sort by Status" OnClick="sortColumnHeader">Status</asp:LinkButton><asp:Image ID="imgSort6" imageurl="images/asc.png" runat="server" visible="false" style="vertical-align:middle;"/></td>
                        <td class="newtblheader"><asp:LinkButton ID="lbSort7" runat="server" class="sortcol" tooltip="Sort by Point Person" OnClick="sortColumnHeader">Point Person</asp:LinkButton><asp:Image ID="imgSort7" imageurl="images/asc.png" runat="server" visible="false" style="vertical-align:middle;"/></td>
                        <td class="newtblheader"><asp:LinkButton ID="lbSort4" runat="server" class="sortcol" tooltip="Sort by Location" OnClick="sortColumnHeader">Location(Office)</asp:LinkButton><asp:Image ID="imgSort4" imageurl="images/asc.png" runat="server" visible="false" style="vertical-align:middle;"/></td>
                        
                        <%--<td class="newtblheader"><asp:LinkButton ID="lbSort5" runat="server" class="sortcol" tooltip="Sort by Status" OnClick="sortColumnHeader">Status</asp:LinkButton><asp:Image ID="imgSort5" imageurl="images/asc.png" runat="server" visible="false"/></td>--%>
    <asp:Repeater ID="Repeater1" visible="true" runat="server" >
            <HeaderTemplate>           
               
                        <td  class="newtblheader" valign="middle" nowrap><asp:ImageButton ID="imgDelete" runat="server" ToolTip="Delete this document from your inbox" imageurl="images/del.png" Width="16px" Height="16px"/>
                        <asp:ImageButton ID="imgMove" runat="server" imageurl="images/fmove.png" Width="18px" Height="18px" Visible="false"/>
                        <cc1:HoverMenuExtender ID="hme2" runat="Server"
    TargetControlID="imgMove"
    PopupControlID="PopupMenu"
    HoverCssClass="popupHover"
    PopupPosition="Bottom"
    OffsetX="0"
    OffsetY="0"
    PopDelay="50" />
                        </td>
                    </tr>            
            </HeaderTemplate>
            <ItemTemplate>                
                    <tr>
                    <td class="tbldashed" width="12px" valign="top"  align="center"  style="padding-top:2px;">
                        <asp:Literal ID="lUrgent" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Urgent"))%>' visible="false"></asp:Literal>
                        <asp:Image ID="imgUrgent"  ToolTip='Urgent' runat="server" imageUrl="images/button/icon1.png" onmouseover="this.src='images/button/icon1.png'" onmouseout="this.src='images/button/icon1.png'" height="17px" width="19px" visible="false" style="vertical-align:top"/>
                    </td>
                    <td class="tbldashed" valign="top"  align="center"  style="padding-top:2px;" width="12px">
                        <asp:Image ID="imgFlag"  runat="server" imageUrl="images/button/flag.png" onmouseover="this.src='images/button/flag.png'" onmouseout="this.src='images/button/flag.png'" visible="false" style="vertical-align:top" height="17px" width="16px"/>
                        <asp:Literal ID="lFlag" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "doc_id"))%>' visible="false"></asp:Literal>
                    </td>
                    <td class="tbldashed" valign="top"  align="center" nowrap  style="padding-top:3px;">
                    
                    
                    <asp:ImageButton ID="imgUpd" runat="server" height="20px" width="15px"  imageurl='' ToolTip='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "refno"))%>'/>
                    <%--<asp:Literal ID="lBookmarked" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Bookmarked"))%>' visible="false"></asp:Literal>                                                --%>
                                                                    
                                                                    
                    <%--<asp:ImageButton ID="ImageButton1" runat="server" height="20px" width="15px"  imageurl='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "doctypeimg"))%>' ToolTip='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DocId"))%>'/>--%>
                    </td>
                    <td class="tbldashed" valign="top" width="12px" align="left"  style="padding-top:3px;"><div  class="xxx"  style="width:200px"><asp:ImageButton ID="ImgUnbookmark" ToolTip='Remove your bookmark for this document' runat="server" imageUrl="images/bookmark_h.png" onmouseover="this.src='images/bookmark.png'" onmouseout="this.src='images/bookmark_h.png'"  Visible="false"/>
                                                    <%--<asp:ImageButton ID="ImgBookmark" ToolTip='Bookmark this document' runat="server" imageUrl="images/bookmark.png" onmouseover="this.src='images/bookmark_h.png'" onmouseout="this.src='images/bookmark.png'" Visible="false" />
                                                    <asp:ImageButton ID="ImgReply" ToolTip='Reply to this Document' runat="server" imageUrl="images/reply.png" onmouseover="this.src='images/reply_h.png'" onmouseout="this.src='images/reply.png'" Visible="false"/>--%>
                                                    
                                                    <asp:LinkButton ID="lbtnDoc" runat="server" onmouseover="this.style.textDecoration = 'underline',this.style.color='#00005E'" onmouseout="this.style.textDecoration = 'none',this.style.color='blue'" style="color:blue;font-family:arial;font-size:8pt;" tooltip='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Title"))%>'>
                                                    <asp:Literal ID="DocTitle" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Title"))%>'></asp:Literal>
                                                    </asp:LinkButton>
                                                    </div>
                            
                        </td>
                        <td class="tbldashed" valign="top"  style="padding-top:3px;" nowrap><asp:Literal ID="lrefno" runat="server" visible="true" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "refno"))%>'></asp:Literal></td>
                        <td class="tbldashed" valign="top"   style="padding-top:3px;">
                            <table>
                                <tr>
                                        <td>

                                        <%--<asp:ImageButton ID="imgExpand" runat="server" imageurl="images/plus.jpg" Visible="false" />--%></td>
                                        <td><asp:Literal ID="lDocId" runat="server" visible="false" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DocId"))%>'></asp:Literal>
                                        <%--<asp:Literal ID="lDocTypeAccess" runat="server" visible="false" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "GroupAccessId"))%>'></asp:Literal>--%>
                                        <asp:Literal ID="lDocType" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DocType"))%>' Visible="false"></asp:Literal>
                                        <%--<asp:Literal ID="lIsModified" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "IsBeingModified"))%>' Visible="false"></asp:Literal>--%>
                                        <asp:Literal ID="lDocName" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DocName"))%>'></asp:Literal>
                                        <asp:Literal ID="lFileName" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "FileName"))%>' Visible="false"></asp:Literal></td>
                                </tr>
                            </table>
                        </td>   
                        
                        <td class="tbldashed" valign="top" style="padding-top:3px;"><asp:label ID="ModifiedDate" tooltip='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Originator"))%>' runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CreatedDate"))%>'></asp:Label>
                            
                        </td>
                        <td class="tbldashed" valign="top" style="padding-top:3px;"><asp:Literal ID="lStatus" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Description"))%>'></asp:Literal>
                            
                        </td>
                        <td class="tbldashed" valign="top" style="padding-top:3px;"><asp:Literal ID="lAgency" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "FirstApprover"))%>'></asp:Literal>
                        <%--<td class="tbldashed"><asp:Literal ID="Status" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "StatusDesc"))%>'></asp:Literal>                       --%>
                        </td>
                        <td class="tbldashed" valign="top" style="padding-top:3px;"><asp:Literal ID="lOffice" runat="server" Visible="false" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "office"))%>'></asp:Literal><asp:Literal ID="ModifiedBy" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "assignedto"))%>'></asp:Literal>
                        <%--<td class="tbldashed"><asp:Literal ID="Status" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "StatusDesc"))%>'></asp:Literal>                       --%>
                        </td>                                                               
                        
                        <td class="tbldashed"><uc:usercontrolCheckBox ID="cbArchive" runat="server" /><asp:CheckBox ID="cbxDelete" runat="server" visible="false" /></td>
                        
                    </tr>                
                                 
            </ItemTemplate>
            
            <FooterTemplate>
            <tr>
                                    <td style="border-top:solid 1px #ffffff" colspan="11"></td>
                                </tr>
                    <%--<tr>
                        
                        <td colspan="7" style="> <uc1:ucHr ID="ucHr2" runat="server" /></td>
                    </tr>   --%>
                 </table>                                
            </FooterTemplate>
        </asp:Repeater>
       
                        
        </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>
    <!-- end - resultset //-->     
    </div>
</asp:Content>
<asp:Content ID="mContent" runat="server" ContentPlaceHolderID="menuContent">   
        
    <uc:ucButton id="ucAddDoc" runat="server" pText="Upload Document" pImage="images/upload2.png"></uc:ucButton>    
    <table border="0" width="100%" cellpadding="0" cellspacing="0" style="border-collapse:collapse">
        <tr>
            <td>
                
            </td>
            <td align="left">
            <asp:UpdatePanel ID="pnlPage" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
            </ContentTemplate>
            </asp:UpdatePanel>
            </td>
            <td align="left">
            <table border="0" width="95%">
            <%--<tr>
            <td align="left"><asp:ImageButton ID="imgUpdate" runat="server" ImageUrl="images/img_save_h.jpg" Visible="False"/>
                <img src="images/searchdoc2_h.png" width="20px" Height="24px" alt="Filter Records" onclick="showMenu2('pSearchCriteria')" onmouseover="this.src='images/searchdoc2.png'"  onmouseout="this.src='images/searchdoc2_h.png'" style="cursor:hand" visible="false"/> &nbsp;<span style="color: #537598;font-family:Helvetica;font-size:10pt;">Filter Documents</span>
                <asp:ImageButton ID="imgSearch" runat="server" ImageUrl="images/searchdoc2_h.png"  width="20px" Height="24px" tooltip="Filter Records" onmouseover="this.src='images/searchdoc2.png'" Visible="false"   onmouseout="this.src='images/searchdoc2_h.png'"/></td>
            
            </tr>--%>
            <tr>            
            <td align="left">
                 
                        <!-- start - search criteria //-->
                            <%--<div style="border:solid 1px #9DB5CD;background-color: #F5F5F5; width: 98%; margin-top: 8px; margin-left: 1px">--%>
                            <div style="border-style: solid; border-width: 1px; border-color: #F1F4F8 #CFDBE7 #81A0C0 #CEDAE8; background-color: #FFFFFF; width: 98%; margin-top: 8px; margin-left: 1px">
                                <asp:UpdatePanel ID="pnlFilter" runat="server" UpdateMode="Conditional">
                                   <ContentTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="newtblheader2" >
                                        <tr height="25px">
                                            <td align="left"  class="tableHead27"  style="padding-left:3px">
                                                <img alt="" width="24px" Height="20px" src="images/find.png" />&nbsp;&nbsp;<asp:Label ID="lbBookMrk" runat="server" style="color:#EEEEEE;font-family:Arial;font-size:10pt;font-weight:bold;font-style:normal;color:#CCCCCC">Filter Documents</asp:Label></td>
                                                <td width="50px" align="right" valign="top" class="tableHead27" >
                                                    <asp:ImageButton ID="imgBk" runat="server" imageurl="images/showpanel.png"/></td>
                                        </tr>
                                    </table>
                    
                                <asp:Panel runat="server" ID="pFilter" Visible="false" DefaultButton="btSearch"> 
                                <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                         <tr>
                                            <td align="left"  class="labelFreeForm">
                                                Reference No:</td>
                                            <td align="left">
                                                <asp:TextBox ID="tbFilterRefNo" runat="server"  MaxLength="100" Width="245px"  cssclass="entryfldw"></asp:TextBox>
                                            </td>
            
                                            <td align="left">
                                                &nbsp;</td>
            
                                        </tr>  
                                        <tr>
                                            <td align="left" class="labelFreeForm">
                                                Classification:</td>
                                            <td align="left">                                    
                                                <asp:DropDownList ID="dlClassification" runat="server" cssclass="entryfld2w" Width="250px">
                                                    <asp:ListItem Value="" Text="-All-"></asp:ListItem>
                                                    <asp:ListItem Value="0" Text="External"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Internal"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
            
                                            <td align="left">
                                                &nbsp;</td>
            
                                        </tr>
                                        <tr>
                                            <td align="left" class="labelFreeForm">
                                                Status:</td>
                                            <td align="left">                                    
                                                <asp:DropDownList ID="dlStatus" runat="server" cssclass="entryfld2w" Width="250px"></asp:DropDownList>
                                            </td>
            
                                            <td align="left">
                                                &nbsp;</td>
            
                                        </tr>       
                                                                        
                                        <tr>
                                            <td align="left" class="labelFreeForm">
                                                Document Type:</td>
                                            <td align="left">                                    
                                                <asp:DropDownList ID="dlFilterDocType" runat="server" cssclass="entryfld2w" Width="250px"></asp:DropDownList>
                                            </td>
            
                                            <td align="left">
                                                &nbsp;</td>
            
                                        </tr>
                                        <%--<tr>
                                            <td align="left"  class="labelFreeForm">
                                                Agency:</td>
                                            <td align="left">                                    
                                                <asp:TextBox ID="txtAgency" runat="server"  MaxLength="100" Width="245px"  cssclass="entryfldw"></asp:TextBox>
                                                <asp:DropDownList ID="dlAgency" runat="server" cssclass="entryfld2w" Width="250px" Visible="false"></asp:DropDownList>
                                            </td>
            
                                            <td align="left">
                                                &nbsp;</td>
            
                                        </tr>--%>         
                                         <tr>
                                            <td align="left"  class="labelFreeForm">
                                                Title:</td>
                                            <td align="left">
                                                <asp:TextBox ID="tbFilterTitle" runat="server"  MaxLength="100" Width="245px"  cssclass="entryfldw"></asp:TextBox>
                                            </td>
            
                                            <td align="left">
                                                &nbsp;</td>
            
                                        </tr>                   
                                        <tr>
                                            <td align="left"  class="labelFreeForm">
                                                Uploaded By:</td>
                                            <td align="left">
                                                <asp:TextBox ID="txAuthor" runat="server"  MaxLength="100" Width="245px"  cssclass="entryfldw"></asp:TextBox>
                                            </td>
            
                                            <td align="left">
                                                &nbsp;</td>
            
                                        </tr>  
                                        <tr>
                                            <td align="left"  class="labelFreeForm" title="This is the first person that document was routed to">
                                                Point Person:</td>
                                            <td align="left">
                                                <asp:TextBox ID="tbRoutedTo" runat="server"  MaxLength="100" Width="245px"  cssclass="entryfldw"></asp:TextBox>
                                            </td>
            
                                            <td align="left">
                                                &nbsp;</td>
            
                                        </tr> 
                                        <%--<tr>
                                            <td align="left"  class="labelFreeForm" colspan="3">
                                                Inbox: <asp:CheckBox ID="cbReceived" runat="server"  autopostback="true"/>                                            
                                             </td>            
                                        </tr>  
                                        <tr>
                                            <td align="left"  class="labelFreeForm" colspan="3">
                                                Sent: <asp:CheckBox ID="cbSent" runat="server" autopostback="true"/>
                                            </td>            
                                        </tr>--%>  
                                        <tr>
                                            <td align="left"  class="labelFreeForm">
                                                Created Date:</td>
                                            <td align="left">
                                                <asp:TextBox ID="txDateCreatedFrom" runat="server"  MaxLength="100" Width="67px"  cssclass="entryfldw"></asp:TextBox>-
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txDateCreatedFrom" />
                                                <asp:TextBox ID="txDateCreatedTo" runat="server"  MaxLength="100" Width="67px"  cssclass="entryfldw"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txDateCreatedTo" />
                                            </td>
            
                                            <td align="left">
                                                &nbsp;</td>
            
                                        </tr>  
                                        <tr>
                                            <td align="left" class="labelFreeForm">
                                                Show:</td>
                                            <td align="left">                                    
                                                <asp:DropDownList ID="dlShow" runat="server" cssclass="entryfld2w" Width="250px">
                                                    <asp:ListItem Value="" Text="All"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Confidential"></asp:ListItem>
                                                    <asp:ListItem Value="0" Text="Non-Confidential"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
            
                                            <td align="left">
                                                &nbsp;</td>
            
                                        </tr>
                                         <tr>
                                            <td align="left" class="labelFreeForm">
                                                <asp:Literal ID="lOffice" runat="server" Visible="false" Text="Office:"></asp:Literal></td>
                                            <td align="left">                                    
                                                <asp:DropDownList ID="dlOffice" runat="server" cssclass="entryfld2w" Width="250px"  Visible="false" AutoPostBack="true"></asp:DropDownList>
                                            </td>
            
                                            <td align="left">
                                                </td>
            
                                        </tr>  
                                        <tr>
                                            <td align="left">
                                                &nbsp;</td>
                                            <td align="right">
                                                <asp:Button ID="btSearch" runat="server" CssClass="btnsmall" Text="Filter" />
                                            </td>
            
                                            <td align="left">
                                                &nbsp;</td>
            
                                        </tr>                            
                                    </table>
                                </asp:Panel>
                                </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                       <%--<asp:UpdatePanel ID="pnlUFolder" runat="server" UpdateMode="Conditional">
                                   <ContentTemplate>
                        <uc:ControlFolder id="ucFolder" runat="server"></uc:ControlFolder>
                       </ContentTemplate>
                                </asp:UpdatePanel>--%>
                        <!-- end - search criteria //-->
                        <uc:UserControlBk id="ucb" runat="server"></uc:UserControlBk>
                        <br />

            </td>
           
            </tr>

            </table>
                
                
                
            </td>
        
            
        </tr>    
        
    </table>  
</asp:Content>
                  
<asp:Content ID="cntUpload" runat="server" ContentPlaceHolderID="cntntUpload">   
 
    <uc:UserControlDocumentUpload runat="server" id="ucUpload" visible="False"/>    
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cpPeriod">
<div align="left" style="margin-top:0px;">
<table>
<tr>
<%--<td>
    
    <div id="div1" runat="server" style="padding:2px;border-radius:4px;text-align:center; padding-top:5px; vertical-align:middle; height:20px; width:60px; white-space:nowrap; overflow:hidden; text-overflow:ellipsis; border:solid 1px #C0C0C0; background-color: transparent; margin-right:5px;color:#222222;">
    
    <asp:LinkButton ID="lbInbox" runat="server" Text='Inbox' style="color: inherit"></asp:LinkButton>       
    
</div>
</td>
<td>
    
    <div id="divGrpAll" runat="server" style="padding:2px;border-radius:4px;text-align:center; padding-top:5px; vertical-align:middle; height:20px; width:60px; white-space:nowrap; overflow:hidden; text-overflow:ellipsis; border:solid 1px #C0C0C0; background-color: transparent; margin-right:5px;color:#222222;">
    
    <asp:LinkButton ID="lbNew" runat="server" Text='New' style="color: inherit"></asp:LinkButton>       
    
</div>
</td>

<td>
    
    <div id="div2" runat="server" style="padding:2px;border-radius:4px;text-align:center; padding-top:5px; vertical-align:middle; height:20px; width:60px; white-space:nowrap; overflow:hidden; text-overflow:ellipsis; border:solid 1px #C0C0C0; background-color: transparent; margin-right:5px;color:#222222;">
    
    <asp:LinkButton ID="lbOthers" runat="server" Text='All' tooltip="Contains all documents assigned to your group" style="color: inherit"></asp:LinkButton>       
    
</div>
</td>--%>
<td>
    <asp:RadioButtonList ID="rbSelection" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" Font-Bold>
        <asp:ListItem Selected="False" Text="ALL" Value="A" title="List all documents that you have access. This include all the documents received by your office." ></asp:ListItem>
         <asp:ListItem Selected="False" Text="INBOX" Value="I" title="List all documents that you already received and uploaded."></asp:ListItem>
          <asp:ListItem Selected="True" Text="SENT" Value="S" title="List all documents that you have routed to another user."></asp:ListItem>
           <asp:ListItem Selected="false" Text="NEW" Value="N" title="List all documents that you received for the day."></asp:ListItem>
    </asp:RadioButtonList>
</td>

    <td align="right">
    </td>
</tr>
</table>
</div>
</asp:Content>