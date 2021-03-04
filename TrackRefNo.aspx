<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="TrackRefNo.aspx.vb" Inherits="dms.TrackRefNo" EnableEventValidation="true" %>
<%@ MasterType VirtualPath="~/Site.Master"  %>
<%@ Register src="ucTrackStatus.ascx" tagname="ucTrackStatus" tagprefix="uc" %>    
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
                                        <td align="left">&nbsp;&nbsp;Track Reference Number</td>
                                        <td width="50px" align="right" valign="top" class="tableHead27" >
                                        <asp:ImageButton ID="imgBk" runat="server" imageurl="images/hidepanel.png"/></td>
                                    </tr>
                                </table>                            
                            </td>
                         </tr>       
                         <tr>
                            <td align="left" style="background-color:#FFFFFF;border-bottom:solid 1px #C2C2C2;padding-right:30px;">
                                
                                
                            </td>
                            </tr>
                         <tr>
                            <td align="center" style="background-color:#FFFFFF">
                                <asp:panel runat="server" id="pnlAS" Visible="true" DefaultButton="btSearch" style="text-shadow:0px 0px White">
                                
                                    <div width="100%" align="left" style="border-bottom:solid 1px #C2C2C2;color:Navy;font-size:9pt;">
                                    <img src="images\note_icon.png" /> <B>Notes:</B> <br />1. This screen can be used for locating documents not assigned or not yet routed to your office.
                                    <br />2. Just enter in the search box the reference number and click on Search button. You can use partial or exact search type.
                                    <br />3. To view the tracking of document, just click on document icon (<img src="images\doctype\pdf.png" width="14px" height="14px"/>) you can find in the search result.                                    
                                </div>
                                    
                                <table border="0">
                                    
                                    <tr>
                                        <td>Search Type</td>
                                        <td><asp:RadioButton ID="rbPartial" runat="server" GroupName="SearchType" Text="Partial" Checked="true"/><asp:RadioButton ID="rbExact" runat="server" GroupName="SearchType" Text="Exact"/></td>
                                        <td align="right" width="150px" class="labelFreeForm" style="padding-right:10px">
                                            Reference Number
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="tbRefNo" runat="server" cssclass="entryfldw"  Width="270px"></asp:TextBox>
                                        </td>
                                        <td>
                                        <div id="prc" style="display: none ; font-style: italic;font-size:10px;color:Blue;" CssClass="btn">
                                            <img src="images/processing.gif" /> Searching ...
                                        </div>
                                        <div id="pbtn" style=" display: inline">
                                            <asp:Button ID="btSearch" runat="server" CssClass="btn" Text="Search" style="margin:3px 3px 3px 3px" OnClientClick="document.getElementById('pbtn').style.display='none';document.getElementById('prc').style.display='';return true;"/>
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
    
                <asp:Panel ID="idSrchRslt" runat="server" Visible="true" style="height:auto;width:100%">
                
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
                            <tr runat="server" style="background-color:white" >
                                <td class="search0" align="center" valign="top">
                                                               
                            
                                <asp:ImageButton ID="imgUpd" runat="server" height="35px" width="25px" imageurl='' ToolTip='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "refno"))%>' OnClick="ViewTracking"/>
                                <div style="color:#C0C0C0;font-size:7pt;">
                                    <asp:Label ID="Label1" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "rn"))%>' ></asp:Label>
                                </div>
                                </td>
                                <td class="search0">
                                    <table width="100%">
                                        <tr>
                                               <td class="rowclass">Reference No: <span style="font-weight:bold;color:#222222"><asp:Literal ID="lrfno" runat="server" text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "refno"))%>'></asp:Literal></span>
                                               <asp:Literal ID="lFileName" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "FileName"))%>' Visible="false"></asp:Literal>
                                               
                                               <asp:Literal ID="lDocId" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DocId"))%>' visible="false"></asp:Literal>

                                               </td>
                                                
                                        </tr>
                                        <tr>                                       
                                            <td class="rowclass1"><span class="rsLabel">Title:</span><span class="rsLabelColNavy"><%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Title"))%></span></td>
                                        </tr>                                  
                                        <tr>                                       
                                            <td class="rowclass1"><span class="rsLabel">Uploaded By:</span><span class="rsLabelColNavy"><%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Originator"))%></span><span class="rsLabel">Created Date:</span><span class="rsLabelColNavy"><%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CreatedDate"))%></span><span class="rsLabel">Classification:</span><span class="rsLabelColNavy"><%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Classification"))%></span></td>
                                        </tr>
                                        <tr>                                       
                                            <td class="rowclass1"><span class="rsLabel">Location:</span>
                                            <span class="rsLabelColBlack"><%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "PersonnelInCharge"))%></span> 
                                            (Office: <span class="rsLabelColBlack" title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "OfficeDesc"))%>' style="margin-right:10px"><%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "OfficeDesc"))%></span> Landline:<span class="rsLabelColBlack" style="margin-right:10px"><%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "LandLineNo"))%></span>Local No:<span class="rsLabelColBlack"><%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "LocalNo"))%></span>) </td>
                                        </tr>
                                        <%--<tr>                                       
                                            <td class="rowclass1">
                                            <span class="rsLabel">Reference No:</span><span class="rsLabelColBlack"><asp:Literal ID="lrfno" runat="server" text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "refno"))%>'></asp:Literal></span>
                                                    
                                        </td>
                                        </tr>   --%>                                     
                                       
                                        
                                       
                                        
                                    </table>
                                   
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
            
        </td>        
    </tr>
    
    </table>


        </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>
 
</asp:Content>
<asp:Content ID="Content11" ContentPlaceHolderID="PopupMenu" runat="server">

    <asp:panel ID="pnlRoutingHistory" runat="server" Visible="false" Width="800px" style="border:solid 1px #C2C2C2;background-color:#FFFFFF"> 
    <table style="width:100%;border-collapse:collapse"><tr><td class="tableHead2">Document Tracking</td><td  class="tableHead2" align="right"><asp:ImageButton ID="imgClose" runat="server" imageurl="images/close_window.gif"/></td></tr></table>
        
        <uc:ucTrackStatus id="uTrackStatus" runat="server"></uc:ucTrackStatus>
    </asp:panel>
    
</asp:Content>
