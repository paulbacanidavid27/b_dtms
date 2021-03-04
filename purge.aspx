<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="purge.aspx.vb" Inherits="dms.purge" %>
    <%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register src="ucHr.ascx" tagname="ucHr" tagprefix="uc1" %>
<%@ Register src="UserControlAdminMenuH.ascx" tagname="UserControlAdminMenuH" tagprefix="uc" %>
<%@ Register src="UserControlPager.ascx" tagname="UserControlPager" tagprefix="uc" %>
<%@ Register src="UserControlCheckBox.ascx" tagname="UserControlCheckBox" tagprefix="uc" %>
<%@ Register src="UserControlImgViewer.ascx" tagname="UserControlImgViewer" tagprefix="uc" %>
<%@ Register src="UserControlPDFViewer.ascx" tagname="UserControlPDFViewer" tagprefix="uc" %>
<%@ Register src="UserControlDocViewer.ascx" tagname="UserControlDocViewer" tagprefix="uc" %>
<%@ Register src="UserControlDocumentIndex.ascx" tagname="UserControlDocumentIndex" tagprefix="uc" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register src="ucConfirm.ascx" tagname="ucConfirm" tagprefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Delete Documents</title>
</asp:Content>
<asp:Content ID="Content7" runat="server" ContentPlaceHolderID="HeaderMenuContent">
    <table cellpadding="0" cellspacing="0" border="0" width="100%" class="tableheaderGreen">
                   <tr >
                        <td valign="middle">
                        </td>
                        </tr>
                        </table>
</asp:Content>
<%--menu content start--%>
<asp:Content ID="Content12" runat="server" ContentPlaceHolderID="AdminMenu">
    <uc:UserControlAdminMenuH id="ucMenu" runat="server"></uc:UserControlAdminMenuH>                                       
    
</asp:Content>
<asp:Content ID="Content6" runat="server" ContentPlaceHolderID="MenuContent">
    <uc:UserControlPDFViewer runat="server" id="ucPDFViewer" visible="False"/>
    <uc:UserControlDocViewer runat="server" id="ucDocViewer" visible="False"/>
    <uc:UserControlImgViewer runat="server" id="ucViewer" visible="False"/>            
</asp:Content>
<%--main content end--%>

<%--main headr content start--%>
<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="MainHeaderContent">
    <table border="0" width="100%" class="tableheaderGreen">
        <tr>
            <td class="tableheader_1"><img  alt=""  Width="20px" height="20px" src="images/delete_doc.png" style="vertical-align:middle" />&nbsp;Delete Documents</td>
            <td align="right">
            
            </td>
        </tr>    
             </table>
</asp:Content>
<asp:Content ID="Content8" runat="server" ContentPlaceHolderID="MainFooterContent">
    <%--<asp:UpdatePanel ID="pagerBottom" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>--%>
    <table cellpadding="0" cellspacing="3" border="0" width="100%" class="tableheaderGreen">
                   <tr >
                        <td align="right">&nbsp;<%--<uc:UserControlPager ID="ucPagerBottom" runat="server" />--%></td>
                        </tr>
                        </table>
                        <%--</ContentTemplate>
                        </asp:UpdatePanel>--%>
</asp:Content>
<%--main headr content end--%>
<asp:Content ID="Content5" ContentPlaceHolderID="AddContent" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0" class="tableheaderGreen">
        <tr><td></td></tr></table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mainDiv_" align="left" width="100%" >

  <asp:UpdatePanel ID="pnlRepeater2" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    
    <asp:Panel ID="imprt" runat="server" DefaultButton="btRetrieve">
  <table width="100%" cellpadding="0" cellspacing="0" style="border-collapse:collapse;border:solid 1px #CCCCCC">
  <tr>
  <td align="left" style="padding-left:3px;font-style:italic;font-size:9pt;background-color:#CCCCCC" >
                                            Search Criteria <asp:Literal ID="lxls" runat="server" Text="|" Visible="false"></asp:Literal> 
      <asp:LinkButton ID="lbSearch" runat="server" Visible="false">Advanced Search</asp:LinkButton>
                                        </td><td align="right" style="padding-right:5px;font-style:italic;font-size:9pt;background-color:#CCCCCC"><asp:ImageButton ID="imgHelp" runat="server" ImageUrl="images/question.png" visible="false"/></td></tr>
                                        <tr>
                                        <td colspan="2">
                                        <asp:UpdatePanel runat="server" ID="pnlPurge" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                              <asp:Panel ID="ArchiveInstruction" runat="server" Visible="false">
                                                                <p class="helpnotes" ><b>Deleting Instruction:</b><br />
                                                                    1. Retrieve records based on the age of the documents. Age will be based from the document created date<br />                                                                    
                                                                    2. Age can be years or months. Minimum age should 1 month or 1 year. <br />
                                                                    3. Click on Retrieve button once you supply the age of the document.<br />
                                                                    4. From the retrieved records, select the document you want to delete by clicking on the checkbox.<br />
                                                                    5. You can select all the document in a page by clicking on the upper checkbox.<br />
                                                                    6. Once you are done selecting, click on Delete icon (<img alt="" src="images/delete_doc.png" height="20" width="20px" />)  to delete the document.<br />
                                                                    Note:Deleted document will not be available to the users. You will need to restore it to be viewed by the user again.
                                                                </p>
                                                                </asp:Panel>
                                                                <asp:Panel ID="RestoreInstruction" runat="server" Visible="false">
                                                                <p class="helpnotes" ><b>Restoring Instruction:</b><br />
                                                                    1. Retrieve deleted records based on the deleted date of the documents.<br />                                                                                                                                        
                                                                    2. Deleted date is optional, you can click on Retrieve button to retrieve all the deleted documents.<br />
                                                                    3. From the retrieved records, select the document you want to restore by clicking on the checkbox.<br />
                                                                    4. You can select all the document in a page by clicking on the upper checkbox.<br />
                                                                    5. Once you are done selecting, click on Restore icon (<img alt="" src="images/restore.gif" />)  to restore the document.<br />
                                                                </p>
                                                                </asp:Panel>
                                                                <asp:Panel ID="PurgeInstruction" runat="server" Visible="false">
                                                                <p class="helpnotes" ><b>Purging Instruction:</b><br />
                                                                    1. Retrieve deleted records based on the deleted date of the documents.<br />                                                                                                                                        
                                                                    2. Deleted date is optional, you can click on Retrieve button to retrieve all the deleted documents.<br />
                                                                    3. From the retrieved records, select the document you want to purge by clicking on the checkbox.<br />
                                                                    4. You can select all the document in a page by clicking on the upper checkbox.<br />
                                                                    5. Once you are done selecting, click on Purge icon (<img alt="" src="images/purge.png" height="20" width="20px" />)  to purge the document.<br />
                                                                    Note: Purging a document means to delete it physically in the table so please be careful with the records you are purging.
                                                                </p>
                                                                </asp:Panel>
                                                                </ContentTemplate>
                                                             </asp:UpdatePanel>
                                        </td>
    </tr>
  <tr>

  <td>
    <asp:Panel ID="pDeleteCriteria" runat="server" Visible="True">
        <table>
       
        <tr>
            <td class="labelFreeForm">1. Reference No:
            </td>
            <td align="left">
                <asp:TextBox ID="tbRefNo" runat="server" cssclass="entryfld" Width="520px" Text=""></asp:TextBox>
            </td>
            <td>
                <asp:RadioButtonList ID="rbAge" runat="server" RepeatDirection="Horizontal" visible="false" AutoPostBack="true" cssclass="entryfld2">
             <asp:ListItem Selected="True" Value="year">Year(s)</asp:ListItem>
             <asp:ListItem Value="month">Month(s)</asp:ListItem>             
             
             </asp:RadioButtonList></td>
            
        </tr>
        <tr>
            <td class="labelFreeForm">2. Title:
            </td>
            <td align="left">
                <asp:TextBox ID="tbTitle" runat="server" cssclass="entryfld" Width="520px" Text=""></asp:TextBox>
            </td>
            <td>
            </td>
        <tr>
            <td class="labelFreeForm">3. Document Type:
            </td>
            <td align="left">
                <asp:DropDownList ID="dlDDocType" runat="server" cssclass="entryfld2" Width="520px">
                </asp:DropDownList>
            </td>
            
            
        </tr>
        <tr>
            <td class="labelFreeForm">4. Office:
            </td>
            <td align="left">
                <asp:DropDownList ID="dlDOffice" runat="server" cssclass="entryfld2" Width="520px">
                </asp:DropDownList>
            </td>
            
            
        </tr>
        <tr>
            <td class="labelFreeForm">5. User:
            </td>
            <td align="left">
                <asp:DropDownList ID="dlUser" runat="server" cssclass="entryfld2" Width="520px">
                </asp:DropDownList>
            </td>
            
            
        </tr>
        </table>
        </asp:Panel>
        
  </td>
  
  <td valign="top">
  <asp:Button ID="btRetrieve" runat="server" CssClass="btnsmall" Text="Retrieve" visible="true"/>
  </td>
  </tr>
 
</table>
        <table width="100%">
        
        <tr >
            <td align="right" class="tableheaderGreen">
                    <asp:UpdatePanel ID="pPager" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                <asp:HiddenField ID="hfCurrent" runat="server" Value="1"/>
                                            <asp:HiddenField ID="hfTotalRows" runat="server" Value="0"/>
                                            <asp:HiddenField ID="hfSortCol" runat="server" Value=""/>
                                            <asp:HiddenField ID="hfSortOrder" runat="server" Value=""/>
                                            <uc:UserControlPager ID="ucPager" runat="server" />
                                </ContentTemplate>
                                </asp:UpdatePanel>
            </td>
            </tr>
    </table>
       </asp:Panel>    
        
    <asp:Panel id="pRepeater" runat="server" >  
    
    <asp:UpdatePanel id="pnlRepeater" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
            
    <uc:ucConfirm id="ucCon" runat="server" Visible="false" pText="Are you sure you want to delete the selected document(s)? Please click OK to proceed"></uc:ucConfirm>           
    
    <table border="0" class="codetbl" cellspacing="0" cellpadding="0" width="100%" style="border-collapse:collapse;z-index:900;border:#D4D4D4 solid 1px">
     
                    <tr >
                        <td class="newtblheader">
                            </td>
                        
                        <td class="newtblheader"><asp:LinkButton ID="lbSort2" runat="server" class="sortcol" tooltip="Sort by Title" OnClick="sortColumnHeader">Title</asp:LinkButton><asp:Image ID="imgSort2" imageurl="images/asc.png" runat="server" visible="false"/></td>
                        <td class="newtblheader"><asp:LinkButton ID="lbSort6" runat="server" class="sortcol" tooltip="Sort by Reference No" OnClick="sortColumnHeader" Visible="true">Reference No</asp:LinkButton><asp:Image ID="imgSort6" imageurl="images/asc.png" runat="server" visible="false"/></td>
                        <td class="newtblheader"><asp:LinkButton ID="lbSort1" runat="server" class="sortcol" tooltip="Sort by Document Type" OnClick="sortColumnHeader">Type</asp:LinkButton><asp:Image ID="imgSort1" imageurl="images/asc.png" runat="server" visible="true"/></td>
                        <td class="newtblheader"><asp:LinkButton ID="lbSort3" runat="server" class="sortcol" tooltip="Sort by Created Date" OnClick="sortColumnHeader" Visible="true">Created Date</asp:LinkButton><asp:Image ID="imgSort3" imageurl="images/asc.png" runat="server" visible="false"/>
                        
                        </td>
                        <td class="newtblheader"><asp:LinkButton ID="lbSort4" runat="server" class="sortcol" tooltip="Sort by Uploaded By" OnClick="sortColumnHeader">Uploaded By</asp:LinkButton><asp:Image ID="imgSort4" imageurl="images/asc.png" runat="server" visible="false"/></td>
                        <td class="newtblheader">Reason</td>
                        <td  class="newtblheader"><uc:usercontrolCheckBox ID="cbSelectAll" runat="server" /><asp:ImageButton ID="imgProcess" runat="server" ToolTip="Purge selected documents" width="16px" Height="16px" imageurl="images/purge.png"/></td>
    <asp:Repeater ID="Repeater1" visible="true" runat="server" >
            <HeaderTemplate>
            
               
                        
                    </tr>            
            </HeaderTemplate>
            <ItemTemplate>                
                    <tr><td align="center" class="tbldashed">
                    
                    <asp:ImageButton ID="imgUpd" runat="server" height="20px" width="15px"  imageurl='' ToolTip='<%# "DBM-" &Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DocId"))%>'/>
       
                    
                    </td>
                    <td class="tbldashed"><div  class="xxx"  style="width:200px">
                                          <asp:LinkButton ID="lbtnDoc" runat="server" onmouseover="this.style.textDecoration = 'underline',this.style.color='#00005E'" onmouseout="this.style.textDecoration = 'none',this.style.color='blue'" style="color:blue;font-family:arial;font-size:8pt;" onclick="openDoc">
                                          <asp:Literal ID="DocTitle" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Title"))%>'></asp:Literal>
                                                    </asp:LinkButton>
                                          </div>
                            
                        </td>
                        <td class="tbldashed">
                            <asp:LinkButton ID="lbView" runat="server" onmouseover="this.style.textDecoration = 'underline',this.style.color='#00005E'" onmouseout="this.style.textDecoration = 'none',this.style.color='blue'" style="color:blue;font-family:arial;font-size:8pt;" onclick="ViewDoc">
                            <asp:Literal ID="lRef" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "refno"))%>' Visible="true"></asp:Literal>
                            </asp:LinkButton>
                        </td>
                        <td class="tbldashed">
                            
                                        <asp:Literal ID="lDocId" runat="server" visible="false" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DocId"))%>'></asp:Literal>                                        
                                        <asp:Literal ID="lFileVersion" runat="server" visible="false" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "FileVersion"))%>'></asp:Literal>                                        
                                        <asp:Literal ID="lGroupAccessId" runat="server" visible="false" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "GroupAccessId"))%>'></asp:Literal>                                        
                                        <asp:Literal ID="lDocType" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DocType"))%>' Visible="false"></asp:Literal>                                        
                                        <asp:Literal ID="lCDate" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CreatedDate"))%>' Visible="false"></asp:Literal>                                        
                                        <asp:Literal ID="lDocName" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DocName"))%>'></asp:Literal>
                                        <asp:Literal ID="lFileName" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "FileName"))%>' Visible="false"></asp:Literal>
                                
                        </td>   
                        
                        <td class="tbldashed"><asp:Literal ID="ModifiedDate" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CreatedDate"))%>'></asp:Literal>
                            <asp:Literal ID="lArchivedDate" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ModifiedDate"))%>' Visible="false"></asp:Literal>
                        </td>
                        <td class="tbldashed"><asp:Literal ID="ModifiedBy" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "originator"))%>'></asp:Literal>
                        <%--<td class="tbldashed"><asp:Literal ID="Status" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "StatusDesc"))%>'></asp:Literal>                       --%>
                        </td>                                                               
                        <td>
                            <asp:TextBox ID="tbReason" runat="server" Width="100px" MaxLength="100"></asp:TextBox></td>
                        <td class="tbldashed"><uc:usercontrolCheckBox ID="cbPurge" runat="server" /></td>
                        
                    </tr>                
                                 
            </ItemTemplate>
            
            <FooterTemplate>
                                            
            </FooterTemplate>
        </asp:Repeater>
        <tr>
                                    <td style="border-top:solid 1px #ffffff" colspan="6"></td>
                                </tr>
                    <%--<tr>
                        
                        <td colspan="7" style="> <uc1:ucHr ID="ucHr2" runat="server" /></td>
                    </tr>   --%>
                 </table>
        
        </ContentTemplate>
    </asp:UpdatePanel>
        </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel> 
    <!-- end - resultset //-->     
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cntntUpload" runat="server">
</asp:Content>
<asp:Content ID="cntPopup" runat="server" ContentPlaceHolderID="PopupMenu">
    <!-- start - resultset delete//-->                                                                
    <asp:UpdatePanel ID="pnlDeleteDoc" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    <asp:Panel id="pDeleteDoc" runat="server" visible="false">  
    <table border="1" class="brdr2" cellspacing="0" cellpadding="2" style="border-collapse:collapse;background-color:White;width:50%">
            <tr>
               <td>
                   <table cellspacing="0" cellpadding="0" border="0" style="width:100%">
                    <tr>
                        <td align="left"  class="brdrhdr2">
                            <img  alt="" src="images/document_16.png" style="vertical-align:middle" height="24px" width="24px"/>&nbsp;&nbsp;<asp:Label
                                ID="lblTitle" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="brdrhdr2" align="right" valign="bottom">
                            <asp:ImageButton ID="imgClose2" runat="server" Height="20px" Width="20px" imageurl="images/close.png" onmouseover="this.src='images/close_h.png'"  onmouseout="this.src='images/close_h.png'" />
                        </td>
                    </tr>
                    </table>
               </td>
            </tr>
            <tr>
                <td align="left">
        <table border="0" class="codetbl" cellspacing="0" cellpadding="0" style="border-collapse:collapse;background-color:White;width:100%;border:solid 1px gray">   
        <tr>
                <td height="100px" align="center" valign="middle" style="font-size:12pt">Please click OK button to confirm processing the records.</td>
        </tr>
        <tr>
                <td align="center" style="padding-bottom:10px">
                     <asp:Button ID="btOK" runat="server" Text="OK" cssclass="btn" />
                     <asp:Button ID="btCancel" runat="server" Text="Cancel" cssclass="btn"/>
                </td>
            </tr>
        </table>      
        </td>
            </tr>
            </table>                                          
        </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>
    <!-- end - resultset delete//-->  
    
</asp:Content>
