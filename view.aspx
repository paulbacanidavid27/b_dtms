<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="view.aspx.vb" Inherits="dms.view"  EnableEventValidation="False" %>
<%@ Register src="UserControlDocRouting.ascx" tagname="UserControlDocRouting" tagprefix="uc" %>
<%@ Register src="UserControlDocNotes.ascx" tagname="UserControlDocNotes" tagprefix="uc" %>
<%--<%@ Register src="UserControlImgViewer.ascx" tagname="UserControlImgViewer" tagprefix="uc" %>
<%@ Register src="UserControlPDFViewer.ascx" tagname="UserControlPDFViewer" tagprefix="uc" %>
<%@ Register src="UserControlDocViewer.ascx" tagname="UserControlDocViewer" tagprefix="uc" %>--%>
<%@ Register src="UserControlDocLink.ascx" tagname="UserControlDocLink" tagprefix="uc" %>
<%@ Register src="UserControlAttachment.ascx" tagname="UserControlAttach" tagprefix="uc" %>
<%@ Register src="ucAttach.ascx" tagname="uAttach" tagprefix="uc" %>
<%@ Register src="ucDocUpload.ascx" tagname="uDocUpload" tagprefix="uc" %>
<%@ Register src="UserControlVersion.ascx" tagname="UserControlVersion" tagprefix="uc" %>
<%--<%@ Register src="UserControlDocHistory.ascx" tagname="UserControlDocHistory" tagprefix="uc" %>--%>
<%@ Register src="UserControlCheckBox.ascx" tagname="UserControlCheckBox" tagprefix="uc1" %>
<%@ Register src="ucHr.ascx" tagname="ucHr" tagprefix="uc1" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register src="UserControlDocumentIndex.ascx" tagname="UserControlDocumentIndex" tagprefix="uc" %>    
<%@ Register src="UserControlDashBoard.ascx" tagname="UserControlDashBoard" tagprefix="uc" %>
<%@ Register src="ucTrackStatus.ascx" tagname="ucTrackStatus" tagprefix="uc" %>
<%@ Register src="UserControlTag.ascx" tagname="UserControlTag" tagprefix="uc" %>    
<%@ Register src="UserControlAddTag.ascx" tagname="UserControlAddTag" tagprefix="uc" %>    
<%@ Register src="UserControlBookMark.ascx" tagname="UserControlBk" tagprefix="uc" %>    
<%@ Register src="UserControlUpload.ascx" tagname="UserControlDocumentUpload" tagprefix="uc" %>    
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register src="UserControlShare.ascx" tagname="UserControlShare" tagprefix="uc" %>
<%@ Register src="UserControlPager.ascx" tagname="UserControlPager" tagprefix="uc2" %>
<%@ Register src="ucButton.ascx" tagname="ucButton" tagprefix="uc" %>    
<%@ Register src="ucReceipt.ascx" tagname="ucReceipt" tagprefix="uc" %>
<%@ Register src="ucReceiptRoute.ascx" tagname="ucReceiptRoute" tagprefix="uc" %>
<%@ Register src="ucReceiptRouteBlank.ascx" tagname="ucBlankRoute" tagprefix="uc" %>
<%@ Register src="ucReceiptRouteBlank2.ascx" tagname="ucBlankRoute2" tagprefix="uc" %>
<%@ Register src="ucReply.ascx" tagname="ucReply" tagprefix="uc" %>
<%@ Register src="ucSubTask.ascx" tagname="ucSTask" tagprefix="uc" %>
<%@ Register src="ucConfirm.ascx" tagname="ucConfirm" tagprefix="uc" %>
<%@ Register src="ucPrompt.ascx" tagname="ucPrompt" tagprefix="uc" %>
<%@ Register src="ucPromptEmail.ascx" tagname="ucPEmail" tagprefix="uc" %>
<%@ Register src="ucRetention.ascx" tagname="ucRetention" tagprefix="uc" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">    
    <title>View Document</title>
</asp:Content>

<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="MenuContent">      
    <uc:ucReceipt id="uReceipt" runat="server" Visible="false"></uc:ucReceipt>
    <uc:ucReceiptRoute id="uReceiptRoute" runat="server" Visible="false"></uc:ucReceiptRoute>
    <uc:ucBlankRoute id="uBlankRoute" runat="server" Visible="false"></uc:ucBlankRoute>
    <uc:ucBlankRoute2 id="uBlankRoute2" runat="server" Visible="false"></uc:ucBlankRoute2>
    <uc:ucReply id="uReply" runat="server" Visible="false"></uc:ucReply>   
    <uc:ucButton id="ucAddDoc" runat="server" pText="Upload Document" pImage="images/upload2.png"></uc:ucButton>    
    <uc:ucConfirm id="ucCon" runat="server" Visible="false" pText="Are you sure you want to delete this document? Please click OK to proceed"></uc:ucConfirm>           
    <uc:ucConfirm id="ucCancel" runat="server" Visible="false" pText="Are you sure you want to cancel this routing? Please click OK to proceed"></uc:ucConfirm>           
    <uc:ucConfirm id="ucResetRet" runat="server" Visible="false" pText="Are you sure you want to reset retention period of this document? Please click OK to proceed"></uc:ucConfirm>           
    <uc:ucConfirm id="ucRemoveRet" runat="server" Visible="false" pText="Are you sure you want to remove retention period of this document? Please click OK to proceed"></uc:ucConfirm>           
    <uc:ucConfirm id="ucCreateSubTask" runat="server" Visible="false" pText="Are you sure you want to create subtask of this document? Please click OK to proceed"></uc:ucConfirm>    
    <table>
    <asp:Panel ID="pDownload" runat="server" Visible="false">
    <tr>
    <td>
        <asp:ImageButton ID="btDownload" style="vertical-align:bottom" imageUrl="images/download_doc.png" onmouseover="this.src='images/download_doc.png'"  onmouseout="this.src='images/download_doc.png';" Tooltip="Download current version of document" runat="server" Visible="false" />
    </td>
    <td><asp:LinkButton ID="lbDownload" runat="server" class="menu"  Visible="false" >Download Document</asp:LinkButton></td>
    </tr>
    </asp:Panel>
    
    <asp:Panel ID="pEdit" runat="server">
    <tr>
    <td>
        <asp:ImageButton ID="imgSave" ClientIDMode="Static" style="vertical-align:bottom" imageUrl="images/edit_doc.png" onmouseover="this.src='images/edit_doc.png';"  onmouseout="this.src='images/edit_doc.png';" Tooltip="Edit Document Information" runat="server" Visible="True" />                                
    </td>
    <td ><asp:LinkButton ID="lbEdit" runat="server" class="menu" OnClientClick="ShowProgress(this,'#imgSave')">Edit Properties</asp:LinkButton></td>
    </tr>
    </asp:Panel>
    <asp:Panel ID="pDelDoc" runat="server" Visible="false">
    <tr>
    <td>
        <asp:ImageButton ID="imgDelDoc" style="vertical-align:bottom" imageUrl="images/del.png" Width="18px" Height="18px" onmouseover="this.src='images/del.png';"  onmouseout="this.src='images/del.png';" Tooltip="Delete Document" runat="server" Visible="True" />                                
    </td>
    <td ><asp:LinkButton ID="lbDelDoc" runat="server" class="menu" >Delete Document</asp:LinkButton></td>
    </tr>
    </asp:Panel>
    <asp:Panel ID="pArchiveTrue" runat="server" Visible="false">
    <tr>
    <td>
        <asp:ImageButton ID="imgArchiveTrue" style="vertical-align:bottom" imageUrl="images/archive_icon.png" Width="18px" Height="18px" onmouseover="this.src='images/archive_icon.png';"  onmouseout="this.src='images/archive_icon.png';" Tooltip="Archive Document" runat="server" Visible="True" />                                
    </td>
    <td ><asp:LinkButton ID="lbArchiveTrue" runat="server" class="menu" >Archive Document</asp:LinkButton></td>
    </tr>
    </asp:Panel>
    <asp:Panel ID="pReceipt" runat="server" Visible="false">
    <tr>
    <td>
        <asp:ImageButton ID="imgReceipt" style="vertical-align:bottom" imageUrl="images/print.png" onmouseover="this.src='images/print.png';" Width="16px" Height="16px"  onmouseout="this.src='images/print.png';" Tooltip="Print Receipt" runat="server" Visible="True" />                                
    </td>
    <td ><asp:LinkButton ID="lbReceipt" runat="server" class="menu" >Print Receipt</asp:LinkButton></td>
    </tr>
    </asp:Panel>
    <asp:Panel ID="pBlankReceipt" runat="server" Visible="false">
    <tr>
    <td>
        <asp:ImageButton ID="imgBReceipt" style="vertical-align:bottom" imageUrl="images/print.png" onmouseover="this.src='images/print.png';" Width="16px" Height="16px"  onmouseout="this.src='images/print.png';" Tooltip="Print Receipt" runat="server" Visible="True" />                                
    </td>
    <td ><asp:LinkButton ID="lbBReceipt" runat="server" class="menu" >Print Blank Receipt</asp:LinkButton></td>
    </tr>
    </asp:Panel>
        <asp:Panel ID="pRouteSlip" runat="server" Visible="false">
    <tr>
    <td>
        <asp:ImageButton ID="imgRouteSlip" style="vertical-align:bottom" imageUrl="images/print.png" onmouseover="this.src='images/print.png';" Width="16px" Height="16px"  onmouseout="this.src='images/print.png';" Tooltip="Print Route Slip" runat="server" Visible="True" />                                
    </td>
    <td ><asp:LinkButton ID="lbRouteSlip" runat="server" class="menu" >Print Route Slip</asp:LinkButton></td>
    </tr>
    </asp:Panel>
    <asp:Panel ID="pBRouteSlip" runat="server" Visible="false">
    <tr>
    <td>
        <asp:ImageButton ID="imgBRouteSlip" style="vertical-align:bottom" imageUrl="images/print.png" onmouseover="this.src='images/print.png';" Width="16px" Height="16px"  onmouseout="this.src='images/print.png';" Tooltip="Print Additional Route Slip" runat="server" Visible="True" />                                
    </td>
    <td ><asp:LinkButton ID="lbBRouteSlip" runat="server" class="menu" >Print Additional Route Slip</asp:LinkButton></td>
    </tr>
    </asp:Panel>
        <asp:Panel ID="pBRouteSlip2" runat="server" Visible="false">
    <tr>
    <td>
        <asp:ImageButton ID="imgBRouteSlip2" style="vertical-align:bottom" imageUrl="images/print.png" onmouseover="this.src='images/print.png';" Width="16px" Height="16px"  onmouseout="this.src='images/print.png';" Tooltip="Print Blank Route Slip" runat="server" Visible="True" />                                
    </td>
    <td ><asp:LinkButton ID="lbBRouteSlip2" runat="server" class="menu" >Print Blank Route Slip</asp:LinkButton></td>
    </tr>
    </asp:Panel>
    <asp:Panel ID="pReply" runat="server" Visible="true">
    <tr>
    <td>
        <asp:ImageButton ID="imgReply" ClientIDMode="Static" style="vertical-align:bottom" imageUrl="images/reply.png" onmouseover="this.src='images/reply.png';" Width="16px" Height="16px"  onmouseout="this.src='images/reply.png';" Tooltip="Reply Document" runat="server" Visible="True" />                                
    </td>
    <td ><asp:LinkButton ID="lbReply" runat="server" class="menu" OnClientClick="ShowProgress(this,'#imgReply')" >Reply Document</asp:LinkButton></td>
    </tr>
    </asp:Panel>
    <asp:Panel ID="pCheckOut" runat="server" Visible="false">
    <tr>
    <td>
        <asp:ImageButton ID="imgCheckout" style="vertical-align:bottom" imageUrl="images/checkout_doc.png" onmouseover="this.src='images/checkout_doc.png';" onmouseout="this.src='images/checkout_doc.png';" runat="server" ToolTip="Checkout current version of document"></asp:ImageButton>  
    </td>
    <td><asp:LinkButton ID="lbCheckOut" runat="server" style="color:#537598" class="menu">Check-Out Document</asp:LinkButton></td>
    </tr>
    </asp:Panel>
    <asp:Panel ID="pCheckIn" runat="server"  Visible="false">
    <tr>
    <td>
        <asp:ImageButton ID="imgCheckIn" style="vertical-align:bottom" imageUrl="images/checkin_doc.png" onmouseover="this.src='images/checkin_doc.png';" onmouseout="this.src='images/checkin_doc.png';"  runat="server" ToolTip="Checkin"></asp:ImageButton>                                                    
    </td>
    <td>
        <asp:LinkButton ID="lbCheckin" runat="server" class="menu">Check-In/Cancel Check-Out</asp:LinkButton></td>
    </tr>
    </asp:Panel>
    <asp:Panel ID="pShare" runat="server" visible="false">
    <tr>
    <td>
        <asp:ImageButton ID="imgEmail" style="vertical-align:top" imageUrl="images/share_doc.png" onmouseover="this.src='images/share_doc.png'" onmouseout="this.src='images/share_doc.png';" runat="server" Visible="true" ToolTip="Email current version of document"></asp:ImageButton>      
    </td>
    <td><asp:LinkButton ID="lbShare" runat="server" class="menu" OnClientClick="ShowProgress(this)">Share Document</asp:LinkButton></td>
    </tr>
    </asp:Panel>
    </table>
    <uc:UserControlBk id="ucb" runat="server"></uc:UserControlBk>
    
    
</asp:Content>

       
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainHeaderContent">
       <table border="0" cellpadding="0" cellspacing="0" width="100%" class="tableheaderLG">
            <tr>
            <td colspan="2" align="right">
            <table width="100%" style="border-bottom:solid 1px #C0C0C0">
                                    <tr>
                                    <td valign="middle"><b style="font-weight:normal;font-style:normal; font-size:12pt;">Reference No: </b><span><asp:Label ID="lrefno" runat="server" Visible="true"></asp:Label></span>
                            
                            <asp:Literal ID="lCreatedDate" runat="server" Visible="false"></asp:Literal> </td>
                                        <td valign="middle" align="right" width="30%">
                       <asp:UpdatePanel ID="upnlBkMark" runat="server" UpdateMode="Conditional">
    <ContentTemplate>  
                                            <asp:ImageButton ID="imgBack" runat="server" imageurl="images/doclists.png" tooltip="Go to Document list"/>
                                                         
    <asp:ImageButton ID="imgBook" runat="server" imageurl="images/bookmark.png" tooltip="Bookmark Document"/>
    <asp:ImageButton ID="imgBookM" runat="server" imageurl="images/bookmark_h.png" tooltip="Remove Bookmark" Visible="false"/>
     </ContentTemplate>
    </asp:UpdatePanel> 
                                        
                                        </td>
                                    </tr>
                                </table>
            </td>
            </tr>            
            <tr>
                <td style="padding:10px;" width="100px" >
                   <asp:HyperLink ID="hlNewTab" runat="server" Target="_blank"   onclientclick="return false" ToolTip="View Document">
                       <asp:Image ID="hlOpen" runat="server" width="100px" Height="120px"  />
                   </asp:HyperLink>
                   <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                    
                    <asp:LinkButton ID="lbUpload" runat="server" class="menu4" Visible="false"><asp:Image ID="ImageButton1" visible="true" runat="server" ImageUrl="images/upload2.png" style="vertical-align:middle;height:12px;width:12px;margin-right:4px;"/>Upload File</asp:LinkButton>
                    </ContentTemplate>
                    
                    <Triggers>
                    
                    <asp:PostBackTrigger ControlID="lbUpload" />
                    </Triggers>
                    </asp:UpdatePanel>
                    <asp:HyperLink ID="hlNewTab2" runat="server" Target="_blank"   onclientclick="return false" ToolTip="Print Document">
                       <asp:Image ID="imgPDFPrint" ImageUrl="images/print.png" runat="server" width="20px" Height="20px"  />
                   </asp:HyperLink>
                    <asp:Label ID="lFileSize" runat="server" text="" Visible="false" style="font-size:8pt;font-weight:normal;"></asp:Label>
                </td>
                <td valign="top">
                     <asp:UpdatePanel ID="pDocInfo" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>          
        <asp:Panel ID="pnlDocInfo" runat="server" Visible="true" DefaultButton="" 
        style="background-image:none;background-repeat: no-repeat;    
    background-position: center; ">
                <table border="0" cellspacing="2" cellpadding="2" class="workarea" width="100%" style="font-style:normal">
                    
                    <tr>                                                    
                        <td class="labelFreeForms" width="100px"  valign="top">Title</td>
                        <td class="dataFreeForms" valign="top" colspan="3">
                            <div style=" word-wrap: break-word; white-space: normal ;width:600px">
                            
                            <asp:TextBox ID="tbTitle" runat="server" class="entryfldw" MaxLength="300" Width="300px" Visible="false"></asp:TextBox>
                            <asp:Literal ID="lTitle" runat="server"></asp:Literal>
                            </div>
                        </td>                                                    
                        
                        
                    </tr>                    
                    <tr>
                        <td class="labelFreeForms"  valign="top">File Name</td>
                        <td class="dataFreeForms"  valign="top" colspan="3">
                                <table cellpadding="0" cellspacing="0">
                        <tr>
                        <td valign="top">
                            <asp:Label ID="lFileName" runat="server" Text="" ></asp:Label>&nbsp;<asp:Label ID="lVersionShow" runat="server" Text=""></asp:Label>
                            <asp:Label ID="lVersion" runat="server" Text="" Visible="false"></asp:Label>
                        </td>
                        <td>&nbsp;
                        </td>
                        </tr>
                        </table>
                        </td>
                    </tr>
                    <tr>
                    <td class="labelFreeForms"  valign="top" nowrap>
                            Document Type
                        </td>
                        <td class="dataFreeForms"  valign="top">                            
                            <asp:Literal ID="lDocName" runat="server"></asp:Literal>
                            <asp:DropDownList ID="dlDocType" runat="server" cssclass="entryfldw2" Width="175px" visible="false"></asp:DropDownList>                            
                        </td> 
                        <td class="labelFreeForms" valign="top" width="60px">
                            Date Uploaded
                        </td>
                        <td class="dataFreeForms"  valign="top">
                            <asp:Literal ID="lDateUploaded" runat="server"></asp:Literal>
                        </td>    
                        
                         
                      
                        </tr>
                   <tr>
                      <td class="labelFreeForms"  valign="top">Classification</td>
                        <td class="dataFreeForms" valign="top">
                            <asp:DropDownList ID="dlClassification" runat="server" cssclass="entryfldw2" Width="80px" visible="false" style="vertical-align:middle">                                 
                                <asp:ListItem Value="0">External</asp:ListItem>
                                <asp:ListItem Value="1">Internal</asp:ListItem>                                
                            </asp:DropDownList>
                            <asp:Literal ID="lToClassification" runat="server" Visible="false" Text=" Current: "></asp:Literal><asp:Literal ID="lClassification" runat="server" Visible="True" Text=""></asp:Literal>
                            <asp:Literal ID="lClassificationCode" runat="server" Visible="False" Text=""></asp:Literal>         </td>
                        <td class="labelFreeForms" valign="top" >Uploaded By</td>
                        <td class="dataFreeForms" valign="top"><asp:Literal ID="lAuthor" runat="server"></asp:Literal><asp:Literal ID="lAuthorID" runat="server" Visible="false"></asp:Literal></td>
                                                         
                    </tr>
                    <tr><td class="labelFreeForms" valign="top" >Sender</td>
                        <td class="dataFreeForms" valign="top"><asp:Literal ID="lsendername" runat="server"></asp:Literal><asp:Literal ID="lsender" runat="server" Visible="false"></asp:Literal>
                        <asp:TextBox ID="tbSenderName" runat="server" class="entryfldw" MaxLength="100" Width="300px" Visible="false"></asp:TextBox>
                            <%--<asp:DropDownList ID="dlUser" runat="server" cssclass="entryfld2w" Width="210px" visible="false" style="vertical-align:middle">                                 
                            </asp:DropDownList>--%>
                        </td>
                        <td class="labelFreeForms" valign="top">
                            Check-Out
                        </td>
                        <td class="dataFreeForms" valign="top">
                           <asp:Literal ID="lIsCheckOut" runat="server"></asp:Literal> 
                        </td>      
                                                         
                    </tr>
                    <tr>
                        <td class="labelFreeForms" valign="top">
                                            Request Type
                    </td>
                    <td class="dataFreeForms" valign="top">
                            <asp:DropDownList ID="dlRequestType" runat="server" cssclass="entryfldw2" Width="210px" visible="false" style="vertical-align:middle">                                 
                            </asp:DropDownList>
                            <asp:Literal ID="lToNewRequestType" runat="server" Visible="false" Text=" Current: "></asp:Literal><asp:Literal ID="lRequestDescription" runat="server" Visible="true"></asp:Literal>
                            <asp:Literal ID="lRequestType" runat="server" Visible="false"></asp:Literal>
                    </td>
                      
                        <td class="labelFreeForms" valign="top">
                            Check-Out By
                        </td>
                        <td class="dataFreeForms" valign="top">
                            <asp:Literal ID="lCheckoutBy" runat="server"></asp:Literal>
                        </td>
                                                            
                    </tr>
                    <tr>
                     
                        <td class="labelFreeForms" valign="top">                            
                        Office
                        </td>
                        <td class="dataFreeForms" valign="top">
                            <asp:DropDownList ID="dlOfficeCode" runat="server" cssclass="entryfldw2" Width="210px" AutoPostBack="false" Visible="false" style="vertical-align:middle">
                            </asp:DropDownList>                    
                            <asp:Literal ID="lToNewOffice" runat="server" Visible="false" Text=" Current: "></asp:Literal><asp:Literal ID="lsOfficeName" runat="server" Visible="true"></asp:Literal>
                            <asp:Literal ID="lsOfficeCode" runat="server" Visible="false"></asp:Literal>
                            
                        </td>
                        <td class="labelFreeForms" valign="top" nowrap>
                            Receipt Printed
                        </td>
                        <td class="dataFreeForms" valign="top">
                            <asp:Literal ID="lPrinted" runat="server" Visible="true"></asp:Literal>
                        </td>                                    
                         
                    </tr>
                    <tr>
                    <td class="labelFreeForms" valign="top">
                        Last Action
                    </td>
                    <td class="dataFreeForms" valign="top"><asp:Literal ID="lstatusid" runat="server" Visible="false"></asp:Literal>
                            <asp:DropDownList ID="dlStatus" runat="server" cssclass="entryfldw2" Width="210px" visible="false" style="vertical-align:middle">                                 
                            </asp:DropDownList>
                            <asp:Literal ID="lToNewStatus" runat="server" Visible="false" Text=" Current: "></asp:Literal><asp:Label ID="lStatus" runat="server" Visible="true"></asp:Label>
                             <asp:ImageButton ID="imgEmailPointPerson" runat="server" imageurl="images/emailpoint.png" visible="true" ToolTip="Email Point Person" OnClientClick="showWindow('emailArchive')" style="vertical-align:top;margin:0px;padding:0px;width:18px;height:18px;"/> 
                            
                    </td>
                    <td class="labelFreeForms"  valign="top">
                            Due Date
                        </td>
                        <td class="dataFreeForms" valign="top">                        
                                                       
                            <asp:TextBox ID="tbDueDate" runat="server" cssclass="entryfldw"  Width="67px" Visible="false" style="vertical-align:middle"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="tbDueDate"  />
                            <asp:Literal ID="lOrigDueLabel" runat="server" Visible="false" Text=" Current: "></asp:Literal>
                            <asp:Literal ID="lOrigDue" runat="server" Visible="true"></asp:Literal>
                                  
                        </td>
                    
                    
                    </tr>
                    <tr>
                    <td class="labelFreeForms"  valign="top">
                            Status
                        </td>
                        <td class="dataFreeForms" valign="top">                        
                        <asp:UpdatePanel ID="pStatusUpd" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DropDownList ID="dlFinalStatus" runat="server" cssclass="entryfldw2" Width="210px" visible="false" style="vertical-align:middle">                                 
                                <asp:ListItem Value="Open">Open</asp:ListItem>
                                <asp:ListItem Value="Closed">Closed</asp:ListItem>
                                <asp:ListItem Value="Completed">Archived</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="lDocStat" runat="server" Visible="True" Text="Open"></asp:Label>       
                            <asp:Literal ID="lCdate" runat="server" Visible="false" Text=""></asp:Literal>    
                        </ContentTemplate>
                        </asp:UpdatePanel>    
                        </td>
                        <td class="labelFreeForms"  valign="top" nowrap>
                            Manner of Receipt
                        </td>
                        <td class="dataFreeForms" valign="top">                        
                            <asp:DropDownList ID="dlManner" runat="server" autopostback="true" cssclass="entryfldw2" Width="120px" visible="false" style="vertical-align:middle">                                 
                            </asp:DropDownList>
                            <asp:Literal ID="lTOManner" runat="server" Visible="false" Text=" Current: "></asp:Literal><asp:Literal ID="lManner" runat="server" Visible="true"></asp:Literal>                
                            <asp:Literal ID="lMannerId" runat="server" Visible="false"></asp:Literal>
                        </td>
                    </tr>
                    <tr id="trCopies" runat="server" visible="true">
                    <td class="labelFreeForms"  valign="top">
                            No of Copies
                        </td>
                        <td class="dataFreeForms" valign="top">                        
                            <asp:TextBox ID="tbNoCopies" runat="server" Visible="False" cssclass="entryfldw" Width="105px"></asp:TextBox>         
                            <asp:Literal ID="lNoCopies" runat="server" Visible="True" Text=""></asp:Literal>                
                        </td>
                        <td class="labelFreeForms"  valign="top">
                            
                        </td>
                        <td class="dataFreeForms" valign="top">                        
                        <asp:UpdatePanel ID="pReturn" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Panel ID="pnlRet" runat="server" Visible="false">
                            <asp:Literal ID="lLabelReturnCard" runat="server" Visible="true" Text="Return Card:"></asp:Literal>
                            <asp:TextBox ID="tbReturnCard" runat="server" Visible="false" cssclass="entryfldw"  Width="105px" MaxLength="100"></asp:TextBox>
                            <asp:Literal ID="lReturnCard" runat="server" Visible="true" Text=""></asp:Literal>    
                            </asp:Panel>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger  ControlID="dlManner" EventName="SelectedIndexChanged"/>
                            </Triggers>
                        </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                    <td class="labelFreeForms"  valign="top">No of Pages </td>
                        <td class="dataFreeForms" valign="top">
                            <asp:TextBox ID="tbNoPages" runat="server" Visible="False" cssclass="entryfldw"  Width="105px"></asp:TextBox>
                            <asp:Literal ID="lNoPages" runat="server" Visible="True" Text=""></asp:Literal>    
                        </td>
                    
                        <td class="labelFreeForms"  valign="top">Received By</td>
                        <td class="dataFreeForms" valign="top"><asp:TextBox ID="tbReceivedBy" runat="server" Visible="False" cssclass="entryfldw"  Width="105px" MaxLength="100"></asp:TextBox>
                            <asp:Literal ID="lReceivedBy" runat="server" Visible="True" Text=""></asp:Literal>    
                       
                        </td>
                    </tr>
                    <tr>
                        <td><asp:Literal ID="lconftext" runat="server" Visible="True" Text="Confidential"></asp:Literal>
                        </td>
                        <td>
                        <asp:Literal ID="lConf" runat="server" Visible="true"></asp:Literal>
                        <asp:UpdatePanel ID="upConf" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:CheckBox ID="cbConf" visible="false" runat="server" autopostback="true"/>

                            </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="labelFreeForms"  valign="top" nowrap>Received Date/Time</td>
                        <td class="dataFreeForms" valign="top"><asp:TextBox ID="tbReceivedDate" runat="server" Visible="False" cssclass="entryfldw"  Width="67px" MaxLength="10"></asp:TextBox><cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="tbReceivedDate"  />
                            <asp:Literal ID="lReceivedDate" runat="server" Visible="True" Text=""></asp:Literal>&nbsp;&nbsp;
                            <asp:TextBox ID="tbReceivedTime" runat="server" Visible="False" cssclass="entryfldw"  Width="55px" MaxLength="10"></asp:TextBox>
                            <cc1:MaskedEditExtender id="MeExt" runat="server" TargetControlID="tbReceivedTime"  AcceptAMPM="true" MaskType="Time" Mask="99:99" InputDirection="LeftToRight" AcceptNegative="None"/>
                            <asp:Literal ID="lReceivedTime" runat="server" Visible="True" Text=""></asp:Literal>        
                       
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                        <asp:Label ID="lcheckmsg" cssclass="msg_green" runat="server" Text=""></asp:Label>
                        </td>
                        
                    </tr>                                
                </table>                   
        </asp:Panel>
        </ContentTemplate>
        <Triggers>
        <asp:AsyncPostBackTrigger ControlID="cbConf"  EventName="CheckedChanged" /></Triggers>
                        </asp:UpdatePanel>         
                </td>
            </tr>
       </table> 
</asp:Content>


<asp:Content ID="Content5" ContentPlaceHolderID="HeaderMenuContent" runat="server">
    <table width="100%" class="tableheaderLG">
        <tr><td>
    <asp:UpdatePanel ID="upMenu" runat="server" UpdateMode="Conditional">
   <ContentTemplate>
   <asp:Panel ID="pMUpdate" runat="server" Visible="false">
   <asp:Button ID="btSaveUpdate" runat="server" CssClass="btnsmall2" Text="Save" />&nbsp;&nbsp;<asp:Button ID="btCancelUpdate" runat="server" CssClass="btnsmall2" Text="Cancel" />

        
   </asp:Panel>
   
   <asp:Panel ID="pMAddTag" runat="server" Visible="fALSE">
        <table border="0">
            <tr>
                <td >
                    <asp:ImageButton ID="imgUpdate" runat="server" imageurl="images/add.png" Height="12px" Width="12px" style="vertical-align:bottom"/></td>
                    <td >
                        <asp:LinkButton ID="lbAddTag" runat="server" class="menu2">Add Tag</asp:LinkButton></td>
            </tr>
        </table>
</asp:Panel>
</ContentTemplate>
   </asp:UpdatePanel>
   </td></tr>
       </table>
</asp:content>

<asp:Content ID="Content4" ContentPlaceHolderID="AddContent" runat="server">
    <table width="100%" class="tableheaderDG">
        <tr><td>
    <%--<asp:UpdatePanel ID="upAddTag" runat="server" UpdateMode="Conditional">
   <ContentTemplate>
   
        <uc:UserControlAddTag ID="UserControlAddTag1" runat="server" visible="false"/>
   </ContentTemplate>
   </asp:UpdatePanel>--%>
   </td></tr>
       </table>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  
    <div class="mainDiv3223" align="left">
<%--<asp:Panel ID="pnlTab" runat="server">--%>
        <asp:UpdatePanel ID="pnlTab" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
       
        <table cellspacing="0" cellpadding="0" border="0"  style="font-family:Verdana;font-size:12px;font-weight:normal;margin-left:15px;margin-top:15px;">
        <tr align="center">    
        <td id='TdHistory1' runat="server" class="selLRTab"></td>
        <td id='TdHistory2' runat="server" class="selMidTab">
            <asp:LinkButton ID="lbHistory" runat="server" class="menuNEW" visible="false">History</asp:LinkButton>
            <asp:Label ID="lHistory" runat="server" Text="History" Visible="True"></asp:Label>
        </td>
        <td id='TdHistory3' runat="server" class="selLRTab">&nbsp;</td>    
        <td id='TdView1' runat="server" class="unselLTab">&nbsp;</td>
        <td id='TdView2' runat="server" class="unselMidTab">
            <asp:LinkButton ID="lbView" runat="server" Visible="true" class="menuNEW">Version</asp:LinkButton>
            <asp:Label ID="lView" runat="server" Text="Version" Visible="false"></asp:Label>
        </td>
        <td id='TdView3' runat="server" class="unselRTab">&nbsp;</td>
        <td id='TdRouting1' runat="server" class="unselLRTab"></td>
        <td id='TdRouting2' runat="server" class="unselMidTab">
            <asp:LinkButton ID="lbRouting" runat="server" Visible="true" class="menuNEW">Routing</asp:LinkButton>
            <asp:Label ID="lRouting" runat="server" Text="Routing" Visible="false"></asp:Label>
        </td>
        <td id='TdRouting3' runat="server" class="unselLRTab">&nbsp;</td>
        
        <td id='TdIndex1' runat="server" class="unselLRTab"></td>
        <td id='TdIndex2' runat="server" class="unselMidTab">
            <asp:LinkButton ID="lbIndex" runat="server"  class="menuNEW">Index</asp:LinkButton>
            <asp:Label ID="lIndex" runat="server" Text="Index" Visible="false"></asp:Label>
        </td>
        <td id='TdIndex3' runat="server" class="unselLRTab">&nbsp;</td>
        <td id='TdLinks1' runat="server" class="unselLRTab"></td>
        <td id='TdLinks2' runat="server" class="unselMidTab">
            <asp:UpdatePanel runat="server" ID="pnlLinkCount" UpdateMode="Conditional">
            <ContentTemplate>  
            <asp:LinkButton ID="lbLinks" runat="server" class="menuNEW">Links</asp:LinkButton>
            <asp:Label ID="lLinks" runat="server" Text="Links" Visible="false" style="clear:left; "></asp:Label>
            <asp:Label ID="lLinksCount" cssclass="supertext2"  runat="server" Text=""></asp:Label>
            </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td id='TdLinks3' runat="server" class="unselLRTab">&nbsp;</td>
        <td id='TdNotes1' runat="server" class="unselLRTab"></td>
        <td id='TdNotes2' runat="server" class="unselMidTab"  style="width:auto;padding:2px;">
            
            <asp:UpdatePanel runat="server" ID="pnlNoteCount" UpdateMode="Conditional">
            <ContentTemplate>            
            <asp:LinkButton ID="lbNotes" runat="server" class="menuNEW" style="clear:left">Notes</asp:LinkButton>
            <asp:Label ID="lNotes" runat="server" Text="Notes"  Visible="false"  style="clear:left; "></asp:Label>
            <asp:Label ID="lNoteCount" cssclass="supertext2"  runat="server" Text=""></asp:Label>
            </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td id='TdNotes3' runat="server" class="unselLRTab">&nbsp;</td>
        <td id='TdTags1' runat="server" class="unselLRTab"></td>
        <td id='TdTags2' runat="server" class="unselMidTab">
            <asp:LinkButton ID="lbTags" runat="server" class="menuNEW" >Tags</asp:LinkButton>
            <asp:Label ID="lTags" runat="server" Text="Tags" Visible="false"></asp:Label>
        </td>
        <td id='TdTags3' runat="server" class="unselLRTab">&nbsp;</td>
        <td id='TdAttach1' runat="server" class="unselLRTab">&nbsp;</td>
        <td id='TdAttach2' runat="server" class="unselMidTab" style="width:auto;padding:2px;">
        <asp:UpdatePanel runat="server" ID="pnlAttachCount" UpdateMode="Conditional">
            <ContentTemplate>  
            <asp:LinkButton ID="lbAttach" runat="server" class="menuNEW">Attachment</asp:LinkButton>
            <asp:Label ID="lAttach" runat="server" Text="Attachment" Visible="false" style="clear:left; "></asp:Label>
            <asp:Label ID="lAttachCount" cssclass="supertext2"  runat="server" Text=""></asp:Label>
            </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td id='TdAttach3' runat="server" class="unselLRTab">&nbsp;</td>
        <td id='TdRoutingHistory1' runat="server" class="unselLRTab">&nbsp;</td>
        <td id='TdRoutingHistory2' runat="server" class="unselMidTab">
            <asp:LinkButton ID="lbRouteHistory" runat="server" class="menuNEW">Track</asp:LinkButton>
            <asp:Label ID="lRouteHistory" runat="server" Text="Track" Visible="false"></asp:Label>
        </td>
        <td id='TdRoutingHistory3' runat="server" class="unselLRTab">&nbsp;</td>
        <td id='TdSubTask1' runat="server" class="unselLRTab">&nbsp;</td>
        <td id='TdSubTask2' runat="server" class="unselMidTab" style="width:auto;padding:2px;">
        <asp:UpdatePanel runat="server" ID="pnlSubTaskCount" UpdateMode="Conditional">
            <ContentTemplate> 
            <asp:LinkButton ID="lbSubTask" runat="server" class="menuNEW">Sub-Task</asp:LinkButton>
            <asp:Label ID="lSubTask" runat="server" Text="Sub-Task" Visible="false"></asp:Label>
            <asp:Label ID="lSubTaskCount" cssclass="supertext2"  runat="server" Text=""></asp:Label>
            </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td id='TdSubTask3' runat="server" class="unselLRTab">&nbsp;</td>
        <%--<td id='TdRet1' runat="server" class="unselLRTab">&nbsp;</td>
        <td id='TdRet2' runat="server" class="unselMidTab">
            <asp:LinkButton ID="lbRet" runat="server" class="menuNEW">Retention</asp:LinkButton>
            <asp:Label ID="lRet" runat="server" Text="Retention" Visible="false"></asp:Label>
        </td>
        <td id='TdRet3' runat="server" class="unselLRTab">&nbsp;</td>--%>        
        <td id='Td22' runat="server" class="fillerTab" align="right"><asp:ImageButton ID="imgHideShow" runat="server" imageurl="images/show.png" visible="false"/></td>
        
        </tr>
       </table>
       
       </ContentTemplate>
       </asp:UpdatePanel>
        
        
        
        <table border="0" cellspacing="0" cellpadding="0" width="100%" >
        <tr>
            <td class="workarea">
            <!--start document view !-->                                 
            <asp:UpdatePanel ID="pDocView" runat="server" UpdateMode="Conditional">
            <ContentTemplate>            
            <asp:panel ID="pnlVersion" runat="server" Visible="false">              
                <uc:UserControlVersion ID="ucVersion" runat="server" />
            </asp:panel>
            </ContentTemplate>
            </asp:UpdatePanel>  

            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>            
            <asp:panel ID="Panel1" runat="server" Visible="false">
            <table border="0" cellpadding="0" cellspacing="0" width="100%" style="border-collapse:collapse">
            <tr>
            <td rowspan="2" valign="top" style="padding:2px 2px 2px 2px;width:130px">
                
            </td>
            <td style="padding:2px 2px 2px 2px" valign="top">
            
                
               
            </td>
            </tr>
            <tr>
            <td style="padding:2px 2px 2px 2px" align="center" >                        
                <iframe id="docvw" src="" border="0"  visible="true" style="border:solid 1px gray" runat="server" width="870px" height="600px"></iframe>                                                 
            </td>
            </tr>

            </table>
                    </asp:panel>
            </ContentTemplate>
            </asp:UpdatePanel>  
            <!--end document view !-->
            <!--start document routing !-->
             <asp:UpdatePanel ID="pDocRouting" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
            <asp:panel ID="pnlDocRouting" runat="server" Visible="false">
              
                <uc:UserControlDocRouting ID="ucDocRouting" runat="server" />
            </asp:panel>
             </ContentTemplate>
            </asp:UpdatePanel>                  
            <!--end document routing !-->

             <!--start document routing !-->
             <asp:UpdatePanel ID="pDocHistory" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
            <asp:panel ID="pnlDocHistory" runat="server" Visible="true">              
            <table width="100%" style="border:solid 1px #D4D4D4" cellpadding="0" cellspacing="0">
            <tr>                
                <td class="newtblheader"><img src="images/history_icon.png" />&nbsp;Document History</td>
                <td align="right" class="newtblheader">
                    <%--pager: step 2--%>
                    <asp:HiddenField ID="hfCurrent" runat="server" Value="1"/>
                    <asp:HiddenField ID="hfTotalRows" runat="server" Value="0"/>
                    <uc2:UserControlPager ID="ucPager" runat="server" />
                </td>
                </tr>
                <tr>
                <td colspan="2">
                <uc:UserControlDashBoard id="ucDocHistory" runat="server"></uc:UserControlDashBoard>                
                </td>
                
            </tr>
            </table>
            </asp:panel>
             </ContentTemplate>
            </asp:UpdatePanel> 
            <!--end document routing !-->
            

            <asp:UpdatePanel ID="pDocIndex" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
            <asp:Panel ID="pnlDocIndex" runat="server" Visible="false">
            
                <asp:UpdatePanel ID="pnlIndex" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                
                <table  border="0" width="100%" cellpadding="0" cellspacing="0" style="border:solid 0px gray;background-color:white;">
                                   <tr>
                                   <td colspan="2" align="center"><uc:UserControlDocumentIndex id="ucDocIndex" runat="server"></uc:UserControlDocumentIndex></td>
                                   </tr>                       
                    <tr>
                                <td></td>
                                <td align="right">
                                    <asp:Button ID="btSaveIndex" runat="server" CssClass="btnsmall" Text="Save" style="margin: 3px 3px 3px 3px" />
                                    </td>
                            </tr>
                            </table>
                    
                    </ContentTemplate>
                    </asp:UpdatePanel>
            </asp:Panel>
            </ContentTemplate>
            </asp:UpdatePanel>

            <asp:UpdatePanel ID="pDocLinks" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
            <asp:Panel ID="pnlDocLinks" Visible="false" runat="server">
                <uc:UserControlDocLink ID="UserDocLinks" runat="server" />
            </asp:Panel>
            
            </ContentTemplate>
            </asp:UpdatePanel>  

            <asp:UpdatePanel ID="pDocNotes" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
            <asp:panel ID="pnlDocNotes" runat="server" Visible="false">                
                <uc:UserControlDocNotes ID="UserDocNotes" runat="server" />               
            </asp:panel>
            </ContentTemplate>
            </asp:UpdatePanel> 

            <!--start document tag !-->
            <asp:UpdatePanel ID="pDocTags" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
            <asp:panel ID="pnlDocTags" visible="false" runat="server">
            
                <uc:UserControltag ID="UControlTAG" runat="server" />                        
            
            
            </asp:panel>    
            </ContentTemplate>
            </asp:UpdatePanel>
            <!--end document tag !-->
            <!--start document attachment !-->
            
                
            <asp:UpdatePanel ID="pAttachment" runat="server" UpdateMode="Conditional">
            <ContentTemplate>            
            
            <asp:panel ID="pnlAttachment" runat="server" Visible="false">        
                
                <uc:UserControlAttach ID="uControlAttach" runat="server" />
                <asp:Panel ID="pnAddAttachment" runat="server" align="right" Visible="false">
                <table cellpadding="4" cellspacing="0" border="0">
                    <tr>
                    <td>
                        <asp:ImageButton ID="imgAttachment" style="vertical-align:bottom" imageUrl="images/clip.png" onmouseover="this.src='images/clip.png'"  onmouseout="this.src='images/clip.png';" Tooltip="Attached supporting documents" runat="server" Visible="True" />
                    </td>
                    <td><asp:LinkButton ID="lbAttachment" runat="server" class="menu">Add Attachment</asp:LinkButton></td>
                    </tr>
                    </table>    
                    </asp:Panel>
            </asp:panel>
            
            
    
    
            </ContentTemplate>
            <Triggers>
            <asp:PostBackTrigger ControlID="imgAttachment" />
            <asp:PostBackTrigger ControlID="lbAttachment" />
            </Triggers>
            </asp:UpdatePanel>
            
            <!--routing history !-->
            <asp:UpdatePanel ID="pRoutingHistory" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
            <asp:panel ID="pnlRoutingHistory" runat="server" Visible="true">              
            <table width="100%" style="border:solid 1px #D4D4D4" cellpadding="0" cellspacing="0">
            <tr>
                <%--<td style="height:35px;font-size:12pt" align="left">RECENT ACTIVITIES</td>--%>
                <td class="newtblheader"><img src="images/history_icon.png" />&nbsp;Routing History</td>
                <td align="right" class="newtblheader">
                    <%--pager: step 2--%>
                    <asp:HiddenField ID="hfRoutingHistoryCurrentRows" runat="server" Value="1"/>
                    <asp:HiddenField ID="hfRoutingHistoryTotalRows" runat="server" Value="0"/>
                    <uc2:UserControlPager ID="ucPagerRoutingHistory" runat="server" />
                </td>
                </tr>
                <tr>
                <td colspan="2" align="center">
                <uc:ucTrackStatus id="uTrackStatus" runat="server"></uc:ucTrackStatus>
                <%--<uc:UserControlDocHistory ID="ucDocHistory1" runat="server" />--%>
                </td>
                
            </tr>
            </table>
            </asp:panel>
             </ContentTemplate>
            </asp:UpdatePanel>
            <!--sub task !-->
            <asp:UpdatePanel ID="pSubTask" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
            <asp:panel ID="pnlSubTask" runat="server" Visible="true">              
            <table width="100%" style="border:solid 1px #D4D4D4" cellpadding="0" cellspacing="0">
            
                            <tr>
                               <td class="newtblheader" width="100%"><img src="images/link_icon.png" />&nbsp;Sub-Task</td> <td class="newtblheader" align="right" width="30px"><asp:ImageButton ID="imgSelect"  visible="false" tooltip="Delete selected links." runat="server"  ImageUrl="images/del.png" /></td>
                            </tr>
            
                <tr>
                <td align="center" colspan="2">
                    <uc:ucSTask id="uSTask"  runat="server"></uc:ucSTask>                
                </td>
                
            </tr>
            <tr>
                <td  colspan="2">
                <asp:Panel ID="pnlAddSubTask" runat="server" align="right" Visible="true">
                <table cellpadding="4" cellspacing="0" border="0">
                    <tr>
                    <td>
                        <asp:ImageButton ID="imgAddSubTask" style="vertical-align:bottom" imageUrl="images/task.png" onmouseover="this.src='images/task.png'"  onmouseout="this.src='images/task.png';" Tooltip="Add Sub-Task" runat="server" Visible="True" width="14px" Height="16px"/>
                    </td>
                    <td><asp:LinkButton ID="lbAddSubTask" runat="server" class="menu">Add SubTask</asp:LinkButton></td>
                    </tr>
                    </table>    
                    </asp:Panel>
                </td>
            </tr>
            </table>
            </asp:panel>
             </ContentTemplate>
             <Triggers><asp:PostBackTrigger ControlID="imgAddSubTask" /><asp:PostBackTrigger ControlID="lbAddSubTask" /></Triggers>
            </asp:UpdatePanel>            
            
            </td>
        </tr>      
        </table>        
       

</div>
</asp:Content>

<asp:Content ID="cntUpload" runat="server" ContentPlaceHolderID="cntntUpload">
    <asp:UpdatePanel ID="upEmail" runat="server" UpdateMode="Conditional">       
<ContentTemplate>          
<div id="emailArchive" style="background-repeat:repeat;background:black;filter:alpha(opacity=70);opacity:0.7;position:fixed;width:100%;height:100%;top:0px;left:0px;z-index:20000;visibility:hidden">
</div>
<div id="emailArchive2" style="position:fixed;left:0px;top:0px;width:100%;height:100%;z-index:20001;visibility:hidden;">
      
    <uc:ucPEmail runat="server" id="uEmail" visible="true" pId="emailArchive" ClientIDMode="Static" pTitle="Email Archived Point Person" pMessage="Recipient(s):" pOKLabel="Send" pCloseLabel="Cancel"></uc:ucPEmail>
</div>
    </ContentTemplate>
</asp:UpdatePanel>

<asp:UpdatePanel ID="pnlCancelCC" runat="server" UpdateMode="Conditional">       
<ContentTemplate>                
<div id="filtercc" style="background-repeat:repeat;background:black;filter:alpha(opacity=70);opacity:0.7;position:fixed;width:100%;height:100%;top:0px;left:0px;z-index:20000;visibility:hidden">
</div>
<div id="filtercc2" style="position:fixed;left:0px;top:0px;width:100%;height:100%;z-index:20001;visibility:hidden;">
    <uc:ucPrompt runat="server" id="uPromptCancelCC" pId="filtercc" ClientIDMode="Static" pTitle="Copy Furnish" pMessage="Are you sure you want to cancel this routing?" pOKLabel="Yes" pCloseLabel="No"></uc:ucPrompt>
</div>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdatePanel ID="pnlCancelRoute" runat="server" UpdateMode="Conditional">       
<ContentTemplate>                
<div id="dCancelRoute" style="background-repeat:repeat;background:black;filter:alpha(opacity=70);opacity:0.7;position:fixed;width:100%;height:100%;top:0px;left:0px;z-index:20000;visibility:hidden">
</div>
<div id="dCancelRoute2" style="position:fixed;left:0px;top:0px;width:100%;height:100%;z-index:20001;visibility:hidden;">
    <uc:ucPrompt runat="server" id="uPromptCancelRoute" pId="dCancelRoute" ClientIDMode="Static" pTitle="Document Routing" pMessage="Are you sure you want to cancel this routing?" pOKLabel="Yes" pCloseLabel="No"></uc:ucPrompt>
</div>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdatePanel ID="pnlMergeRoute" runat="server" UpdateMode="Conditional">       
<ContentTemplate>                
<div id="dMergeRoute" style="background-repeat:repeat;background:black;filter:alpha(opacity=70);opacity:0.7;position:fixed;width:100%;height:100%;top:0px;left:0px;z-index:20000;visibility:hidden">
</div>
<div id="dMergeRoute2" style="position:fixed;left:0px;top:0px;width:100%;height:100%;z-index:20001;visibility:hidden;">
    <uc:ucPrompt runat="server" id="uPromptMergeRoute" pId="dCancelRoute" ClientIDMode="Static" pTitle="Document Merge" pMessage="Are you sure you want to merge and route this document?" pOKLabel="Yes" pCloseLabel="No"></uc:ucPrompt>
</div>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdatePanel ID="pnlDelSubTask" runat="server" UpdateMode="Conditional">       
<ContentTemplate>                
<div id="dDelSubTask" style="background-repeat:repeat;background:black;filter:alpha(opacity=70);opacity:0.7;position:fixed;width:100%;height:100%;top:0px;left:0px;z-index:20000;visibility:hidden">
</div>
<div id="dDelSubTask2" style="position:fixed;left:0px;top:0px;width:100%;height:100%;z-index:20001;visibility:hidden;">
    <uc:ucPrompt runat="server" id="uPromptDelSubTask" pId="dDelSubTask" ClientIDMode="Static" pTitle="Delete Subtask" pMessage="Are you sure you want to delete this subtask?" pOKLabel="Yes" pCloseLabel="No"></uc:ucPrompt>
</div>
</ContentTemplate>
</asp:UpdatePanel>    
    <uc:usercontrolshare runat="server" id="ucShare" visible="False"/>
    <uc:UserControlDocumentUpload runat="server" id="ucUpload" visible="False"/>
    <uc:uAttach runat="server" id="ucAtt" visible="False"/>
    <uc:uDocUpload runat="server" id="ucUp" visible="False"/>
    <asp:Panel ID="pUpload1" cssclass="Div1" runat="server" Visible="false"></asp:Panel>
<asp:Panel ID="pUpload2" cssclass="Div2" runat="server" Visible="false">
            
<table cellspacing="0" cellpadding="0" style="width:100%;height:100%;">
<tr>
    <td valign="middle" align="center">                
            <asp:Panel id="pDoc" runat="server" Visible="true">
            <!-- start - search criteria //-->
          
            <table border="1" class="brdr2" cellspacing="0" cellpadding="2" style="border-collapse:collapse;background-color:White;width:50%;">

            <tr>
            <td>
                <table cellspacing="0" cellpadding="0" border="0" style="width:100%">
                    <tr>
                       <td align="left"  class="brdrhdr2">&nbsp;&nbsp;DOCUMENT - 
                        <asp:Literal ID="lAction" runat="server" Text="Checkout"></asp:Literal></td><td class="brdrhdr2" align="right" valign="bottom">
                        <asp:ImageButton ID="imgClose" runat="server" Height="20px" Width="20px" imageurl="images/close.png" onmouseover="this.src='images/close_h.png'"  onmouseout="this.src='images/close_h.png'" /></td>
                    </tr>
                </table>
            </td>
            </tr>
            <tr>
                <td align="left">
                <!--start 1//-->
                <asp:Panel ID="pInstuction" runat="server" Visible="true">
                <table>
                <tr>
                    <td>Do you want to proceed with the Check-Out?</td><td><asp:Button ID="btCheckOut" runat="server" CssClass="btnsmall" Text="OK" />&nbsp;<asp:Button ID="btCheckoutCancel" runat="server" CssClass="btnsmall" Text="Cancel" /></td>
                </tr>
                <tr>
                    <td colspan="2">
                    
                    <p class="helpnotes" ><b>Check-Out Instruction:</b><br />
                                                                    1. Click on OK button to complete the Check-Out.<br />                                                                    
                                                                    2. A document that has been checked-out will become inaccessible until it was check-in again. <br />
                                                                    3. To get a copy of the document, click on Download button which will appear after confirming the Check-Out. <br />                                                                    
                                                                </p>
                                                                
                    </td>


                </tr>                
                
                </table>
                </asp:Panel>
                <!--end 1//-->
                <!--start 2//-->
                <asp:Panel ID="pnlCheckin" runat="server" Visible="false"> 
                <table>
                <tr>
                    <td colspan="2">
                    <p class="helpnotes" ><b>Cancel Check-Out Instruction:</b><br />                                                                    
                                                                    1. Click Cancel Check-Out button. <br />
                                                                    2. Click on Reload button to refresh the screen. <br />
                                                                                                                                   
                                                                </p>
                    <p class="helpnotes" ><b>Check-In Instruction:</b><br />                                                                    
                                                                    1. Select the updated file to be Uploaded by clicking on Browse button. <br />
                                                                    2. Enter some comments. <br />
                                                                    3. Click Upload to finalize the check-in. <br />                                                                    
                                                                </p>
                                                                
                    </td>
                </tr>                
                <tr>
                <td colspan="2">
                    <table>
                        <tr>
                            <td class="labelFreeForm">Select File to Upload:</td><td><asp:FileUpload ID="fileCheckIn" runat="server" class="entryfld2"></asp:FileUpload></td>
                        </tr>
                        <tr>
                            <td title="Maximum of 200 characters" valign="top"  class="labelFreeForm">Enter Comments:</td><td><asp:TextBox ID="tbComments" cssclass="entryfld" TextMode="MultiLine" height="100px" runat="server" Width="300px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td></td><td><asp:Button ID="btUpload" runat="server" CssClass="btnsmall" width="65px" Text="Upload" visible="true"/><asp:Button ID="btCancelCheckout" runat="server" CssClass="btnsmall" width="120px" style="margin-left:50px" Text="Cancel Check-Out" visible="true"/><asp:Button ID="btClose" runat="server" CssClass="btnsmall" width="65px" Text="Reload" visible="false"/></td>
                        </tr>
                    </table>
                </td>
                </tr>
                <tr><td colspan="2">
                    <asp:Label ID="lcinmessage" runat="server" Text="" cssclass="msg"></asp:Label></td></tr>
                </table>               
                </asp:Panel>
                <!--end 2//-->
                </td>
            </tr>
            </table>
                
           
            </asp:Panel>
    </td>
</tr>                
</table>
            
</asp:Panel>



</asp:Content>

<asp:Content ID="cntPopup" runat="server" ContentPlaceHolderID="PopupMenu">
    <asp:UpdatePanel ID="pnlConfirm" runat="server" UpdateMode="Conditional">
    <ContentTemplate>  
    <asp:Panel id="pConfirm" runat="server" Visible="false" Width="500px">
    <!-- start - search criteria //-->
    <center>
    
    
        <table border="0" class="popuphdrbox" cellspacing="0" cellpadding="0" style="border: solid 1px #3A5671;border-collapse:collapse;width:100%">

            <tr>
               <td align="center">
                  <table cellspacing="0" class="popuphdr" cellpadding="0" border="0" style="width:100%">
                        <tr height="30px">
                        <td align="left" valign="middle" colspan="2">&nbsp;<img height="25px" width="20px" src="images/question4.png" />&nbsp;Notification</td>
                                            
                        <td  align="right" valign="top">
                            <asp:ImageButton ID="imgSaveCancel" runat="server" imageurl="images/close_window.gif" onmouseover="this.src='images/close_window.gif'"  onmouseout="this.src='images/close_window.gif'" width="18px" Height="18px"/>
                        </td>
                    </tr>
                  </table>
               </td>
            </tr>
            <tr>
            <td style="padding-left:15px">
        
                <table align="left">
                <tr>
                    <td align="left" style="font-family:Tahoma;font-size:11pt;color:#003399; padding:12px;">
                        Are you sure you want to update document properties? Please click OK to proceed.
                    </td>
                </tr>
                <tr>
                      
                        
                        <td align="right">
                            <asp:Button ID="btContinue" runat="server" CssClass="btnsmall2" Text="OK" Width="40px" />&nbsp;<asp:Button ID="btSaveCancel" runat="server" CssClass="btnsmall2" Text="Cancel" />
                        </td>                        
                </tr>
                
                
                </table>
                
                </td>
                </tr>
                </table>
                
                </center>
    </asp:Panel>
    <cc1:DropShadowExtender ID="dse2" runat="server" TargetControlID="pConfirm" Opacity=".5" Rounded="false" TrackPosition="False"  />
    </ContentTemplate>
    </asp:UpdatePanel>  

   
</asp:Content>


    

