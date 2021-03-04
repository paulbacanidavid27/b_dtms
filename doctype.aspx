<%@ Page Title="Document Type Maintenance" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false"  CodeBehind="doctype.aspx.vb" Inherits="dms.doctype"  EnableEventValidation="False" %>

    <%@ MasterType VirtualPath="~/Site.Master" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<%@ Register src="ucHr.ascx" tagname="ucHr" tagprefix="uc1" %>
<%@ Register src="UserControlPager.ascx" tagname="UserControlPager" tagprefix="uc" %>
<%@ Register src="UserControlAdminMenuH.ascx" tagname="UserControlAdminMenuH" tagprefix="uc" %>    
<%@ Register src="ucButton.ascx" tagname="ucButton" tagprefix="uc" %>    
<%--menu content start--%>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="HeaderMenuContent">
    <table cellpadding="0" cellspacing="0" border="0" width="100%" class="tableheaderGreen">
                   <tr >
                        <td valign="middle">
                        </td>
                        </tr>
                        </table>
</asp:Content>
<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="AddContent">
    <table cellpadding="0" cellspacing="0" border="0" width="100%" class="tableheaderGreen">
                   <tr >
                        <td valign="middle">
                        </td>
                        </tr>
                        </table>
</asp:Content>
<asp:Content ID="Content5" runat="server" ContentPlaceHolderID="MainFooterContent">
    <table cellpadding="0" cellspacing="3" border="0" width="100%" class="tableheaderGreen">
                   <tr >
                        <td valign="middle"><div class="notes2">&nbsp;<asp:Literal ID="lRecordCount" runat="server"></asp:Literal></div>
                        </td>
                        </tr>
                        </table>
</asp:Content>

<asp:Content ID="Content12" runat="server" ContentPlaceHolderID="AdminMenu">
    <uc:UserControlAdminMenuH id="UserControlAdminMenuH1" runat="server"></uc:UserControlAdminMenuH>                                       
</asp:Content>
<%--main content end--%>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <title>Document Types Maintenance</title>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="MainHeaderContent">
    <table border="0" width="100%" class="tableheaderGreen">
        <tr >
            <td class="hdrtitle_1" > <img  alt="" src="images/doctype.png" style="vertical-align:middle" Width="20px" height="20px"/>&nbsp;Document Types</td>
            <td align="right">
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
                                       
</asp:Content>
<asp:Content ID="mContent" runat="server" ContentPlaceHolderID="menuContent">
                            
                            
                                <uc:ucButton id="ucAdd" runat="server" pText="Add New Document Type" pImage="images/doctype.png"></uc:ucButton>
                                <div style="border-radius:5px;border-style: solid; border-width: 1px; border-color: #F1F4F8 #CFDBE7 #81A0C0 #CEDAE8; background-color: #FFFFFF; width: 98%; margin-top: 8px; margin-left: 1px">
                                <asp:UpdatePanel ID="pnlFilter" runat="server" UpdateMode="Conditional">
                                   <ContentTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="tableheaderGreen" >
                                        <tr height="25px">
                                            <td align="left"  class="tableHead27" style="padding-left:3px">
                                                <img alt="" width="24px" Height="20px" src="images/find.png" />&nbsp;&nbsp;<asp:Label ID="lbUser" runat="server" style="color:#EEEEEE;font-family:Arial;font-size:10pt;font-weight:bold;font-style:normal;color:#CCCCCC">Filter Document Types</asp:Label></td>
                                                <td width="50px" align="right" valign="top" class="tableHead27" >
                                                    <asp:ImageButton ID="imgUser" runat="server" imageurl="images/showpanel.png"/></td>
                                        </tr>
                                    </table>
                    
                                <asp:Panel runat="server" ID="pFilter" Visible="false" DefaultButton="btSearch"> 
                                <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                        <tr>
                                        <td align="left" class="labelFreeForm">
                                            Document Type:</td>
                                        <td align="left">
                                            <asp:TextBox ID="tDocType" runat="server" CssClass="entryfld"></asp:TextBox>
                                        </td>
            
                                        <td align="left">
                                            &nbsp;</td>
            
                                    </tr>
                                    <tr>
                                        <td align="left" class="labelFreeForm">
                                            Description:</td>
                                        <td align="left">
                                            <asp:TextBox ID="tbDesc" runat="server"  CssClass="entryfld"></asp:TextBox>
                                        </td>
            
                                        <td align="left">
                                            &nbsp;</td>
            
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
                                <table>
                  <tr>                    
                    <td width="373px">&nbsp;
                    </td>
                    <td>
                        <asp:ImageButton ID="imgSearch" runat="server" ImageUrl="images/img_search_h.jpg" Visible="false" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgUpdate" runat="server" ImageUrl="images/img_save_h.jpg" Visible="False"/>
                    </td>            
                    <td>
                        <asp:ImageButton ID="imgAddDoc" visible="false" runat="server" ImageUrl="images/img_add.jpg" onmouseover="this.src='images/img_add_h.jpg'"  onmouseout="this.src='images/img_add.jpg'" tooltip="Add Document Type"/>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2" align="center">
                         <asp:UpdatePanel ID="pnlSearchCriteria" runat="server"  UpdateMode="Conditional">
                                <ContentTemplate>      
                                <!-- start - search criteria //-->
                                <asp:Panel id="pSearchCriteria" runat="server" Visible="false">
    
                                <div style="width:400px" class="srch">
                                <!--div class="brdrhdr">
                                     &nbsp;&nbsp;SEARCH CRITERIA
                                </div//-->
                                <div class="brdr">
                                <table border="0" cellpadding="2">
                                    <tr>
                                        <td align="left" colspan="3" style="font-size:8pt;font-weight:bold;border-bottom: solid 1px">SEARCH CRITERIA</td>
        
                                
        
                                    </tr>        
                                    
                                    <tr>
                                        <td align="left">
                                            Update Mode:</td>
                                        <td align="left">
                                            <asp:CheckBox ID="cbUpdate" runat="server" />
                                        </td>
            
                                        <td align="left">
                                            &nbsp;</td>
            
                                    </tr>
                                </table>
                                </div>
                                </div>    
                                </asp:Panel>
                                </ContentTemplate>
                                </asp:UpdatePanel>
                    </td>
                    <td colspan="2"></td>
                    </tr>
                 </table>
                 <br />
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    
    <!-- start - search criteria //-->
    <div class="mainDiv_"  align="left">
    
    
    
    
    <!-- end - search criteria //-->
    <!-- start - resultset //-->                                                                
    <asp:UpdatePanel ID="pnlRepeater" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    <asp:Panel id="pRepeater" runat="server">  
    <table class="codetbl" border="0" cellspacing="0" cellpadding="0" style="border-collapse:collapse;width:100%;z-index:900;border:solid 1px #D4D4D4">
                    <tr >
                        <td class="newtblheader" style="width:30px"></td>
                        <td class="newtblheader" style="width:140px">
                        <asp:LinkButton ID="lbSort1" runat="server" class="sortcol" tooltip="Sort by Document Type" OnClick="sortColumnHeader">Document Type</asp:LinkButton><asp:Image ID="imgSort1" imageurl="images/asc.png" runat="server" visible="true"/>
                        </td>
                        <td class="newtblheader" >
                        <asp:LinkButton ID="lbSort2" runat="server" class="sortcol" tooltip="Sort by Description" OnClick="sortColumnHeader">Description</asp:LinkButton><asp:Image ID="imgSort2" imageurl="" runat="server" visible="false"/></td>
                        <td class="newtblheader" >
                        <asp:LinkButton ID="lbSort3" runat="server" class="sortcol" tooltip="Sort by Created By" OnClick="sortColumnHeader">Created By</asp:LinkButton><asp:Image ID="imgSort3" imageurl="" runat="server" visible="false"/>
                        </td>
                        <td class="newtblheader" ><asp:LinkButton ID="lbSort4" runat="server" class="sortcol" tooltip="Sort by Created Date" OnClick="sortColumnHeader">Created Date</asp:LinkButton><asp:Image ID="imgSort4" imageurl="" runat="server" visible="false"/>
                        </td>                        
                        <td class="newtblheader" ><asp:LinkButton ID="lbSort5" runat="server" class="sortcol" tooltip="Sort by File Required" OnClick="sortColumnHeader">File Required</asp:LinkButton><asp:Image ID="imgSort5" imageurl="" runat="server" visible="false"/>
                        </td>                        
                        <td class="newtblheader" ><asp:LinkButton ID="lbSort6" runat="server" class="sortcol" tooltip="Sort by Allow Printing" OnClick="sortColumnHeader">Allow Printing</asp:LinkButton><asp:Image ID="imgSort6" imageurl="" runat="server" visible="false"/>
                        </td>                        
                        <td class="newtblheader" style="width:20px"><asp:ImageButton ID="imgDelete" runat="server" tooltip="Click to delete selected Document Type"  OnClick="DeleteDocType" imageurl="images/del.png"/></td>

                    </tr>    
    <asp:Repeater ID="Repeater1" visible="true" runat="server" >
            <HeaderTemplate>
                        
            </HeaderTemplate>
            <ItemTemplate>                
                    <tr>
                        <td align="center">
                            <asp:ImageButton ID="imgUpd" runat="server" ToolTip="Click to Update Document Type" imageurl="images/update.png" Width="15px" Height="15px"/>                    
                        </td>
                        <td style="padding-left:2px">
                            <table>
                                <tr>
                                   <td>
                                    <asp:ImageButton ID="imgExpand" runat="server" imageurl="images/plus.jpg"/></td>
                                   <td>
                                    <asp:Literal ID="lDocType" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DocType"))%>'></asp:Literal>
                                   </td>                                   
                                </tr>
                            </table>
                        </td>   
                        <td style="padding-left:2px">
                            <asp:Literal ID="DocName" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DocName"))%>'></asp:Literal>
                            <asp:TextBox ID="tbDOcName" runat="server" CssClass="txt"  Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DocName"))%>' Visible="false"></asp:TextBox>
                        </td>
                        <td style="padding-left:2px">
                            <asp:Literal ID="lCreatedBy" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "cby"))%>'></asp:Literal>                            
                        </td>
                        <td style="padding-left:2px">
                            <asp:Literal ID="lCreatedDate" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CreatedDate"))%>'></asp:Literal>
                        </td>
                        <td style="padding-left:2px">
                            <asp:Literal ID="lFile" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DocumentRequired"))%>' Visible="false"></asp:Literal>
                            <asp:Literal ID="lRequired" runat="server" Text='Yes'></asp:Literal>
                        </td>
                        <td style="padding-left:2px">
                            <asp:Literal ID="lAllowPrinting" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "AllowPrinting"))%>' Visible="false"></asp:Literal>
                            <asp:Literal ID="lAllow" runat="server" Text='Yes'></asp:Literal>
                        </td>
 
                        <td align="center">
                            <asp:CheckBox ID="cbxDelete" runat="server" />
                        </td>                       
                    </tr>                
                    <tr>
                    <td></td>
                    <td colspan="6">
                    <table>
                    <tr>
                    <td>
                    <asp:Panel ID="hdr" runat="server" Visible="false">
                        <table width="100%" border="0" style="border-collapse:collapse;margin-bottom:8px;border:solid 1px #CCCCCC;background-color:#BFFFFF" >
                        <tr >
                            <td class="tableHead2">Retention Disposable Schedule</td>
                        </tr>
                        <tr>
                            <td style="padding:5px;font-style:italic;">
                                <asp:Literal ID="lTrigger" runat="server" Visible="false" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ucdt"))%>'></asp:Literal>
                                <asp:Literal ID="lTriggerStatus" runat="server" Visible="false" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TriggerStatus"))%>'></asp:Literal>
                                <asp:Literal ID="lEnableRetn" runat="server" Visible="false" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "EnableRetention"))%>'></asp:Literal>
                                <asp:Literal ID="lRetStat" runat="server" Visible="false" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "RetStatus"))%>'></asp:Literal>
                                <asp:Literal ID="ltrlAP" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "aPeriod"))%>' Visible="false"></asp:Literal>
                                <asp:Literal ID="ltrlSP" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "sPeriod"))%>' Visible="false"></asp:Literal>
                                <asp:Literal ID="lTriggerDesc" runat="server" Visible="true" Text=""></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="pRet" runat="server" Visible="false" style="width:100%">
                                    <table style="width:100%">
                                    
                                    <tr>
                                    <td>Active Period:</td>
                                    <td>
                                        <strong><asp:Literal ID="ltrlAD" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "aDays"))%>'></asp:Literal>
                                        <asp:Literal ID="lAPDesc" runat="server" Text=''></asp:Literal></strong>
                                        
                                    </td>
                                    </tr>
                                    <tr>
                                    <td>Storage Period:</td>
                                    <td>
                                        <strong><asp:Literal ID="ltrlSA" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "sDays"))%>'></asp:Literal>
                                        <asp:Literal ID="lSPDesc" runat="server" Text=''></asp:Literal></strong>
                                    </td>
                                    </tr>
                                    </table>

                                </asp:Panel>
                            </td>
                        </tr>
                        
                        </table>
                     <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:collapse;z-index:900;border:solid 1px #D4D4D4">
                                    <tr>
                                        <td colspan="5" class="tableHead2">
                                            Index Fields
                                        
                                            <asp:ImageButton ID="btEdit" runat="server" Visible="false"/>                                        
                                            <asp:Literal ID="lDiDocType" runat="server" Visible="false"></asp:Literal>
                                        </td>
                                    </tr>
                                    <tr>                                        
                                        <td class="newtblheader" style="padding-left:5px;padding-right:5px;">Column Name
                                            <asp:LinkButton ID="lbSort1" runat="server" class="sortcol" tooltip="Sort by Column Name" visible="false" OnClick="sortColumn">Column Name</asp:LinkButton><asp:Image ID="imgSort1" imageurl="images/asc.png" runat="server" visible="false"/></td>
                                        <td class="newtblheader" style="padding-left:5px;padding-right:5px;">Data Type
                                            <asp:LinkButton ID="lbSort2" runat="server" class="sortcol"  tooltip="Sort by Data Type"  visible="false" OnClick="sortColumn">Data Type</asp:LinkButton><asp:Image ID="imgSort2" runat="server" visible="false"/></td>
                                         <td class="newtblheaderint" style="padding-left:5px;padding-right:5px;" align="right">Length
                                            <asp:LinkButton ID="lbSort3" runat="server" class="sortcol" tooltip="Sort by length"  visible="false" OnClick="sortColumn">Length</asp:LinkButton><asp:Image ID="imgSort3" runat="server" visible="false"/></td>
                                         <td class="newtblheaderint" style="padding-left:5px;padding-right:5px;" align="right">Import Col Seq
                                            <asp:LinkButton ID="lbSort4" runat="server" class="sortcol" tooltip="Sort by Import Col Seq"  visible="false" OnClick="sortColumn">Import Col Seq</asp:LinkButton><asp:Image ID="imgSort4" runat="server" visible="false"/></td>
                                            <td class="newtblheader"  style="padding-left:5px;padding-right:5px;" align="center" >Display<asp:LinkButton ID="lbSort5" runat="server" class="sortcol" tooltip="Sort by Display" OnClick="sortColumn" Visible="false">Display</asp:LinkButton><asp:Image ID="Image5" imageurl="" runat="server" visible="false"/>
                        </td>
                                    </tr>
                                    </asp:Panel>
                        <asp:Repeater ID="Repeater5" runat="server" visible="false">
                            <HeaderTemplate>
                               
                                
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td  class="search" style="padding-left:2px;padding-right:2px;">
                                    <asp:Literal ID="lDocType" runat="server" 
                                            Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DocType"))%>' Visible="false"></asp:Literal>
                                            <asp:Literal ID="lColumnID" runat="server" 
                                            Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ColumnID"))%>' Visible="false"></asp:Literal>                                           
                                    <asp:Literal ID="lColumnName" runat="server" 
                                            Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ColumnName"))%>' Visible="true"></asp:Literal>
                                        
                                    </td>
                                    <td  class="search" style="padding-left:2px;padding-right:2px;">
                                        <asp:Literal ID="lDataTypeDesc" runat="server" 
                                            Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DataTypeDesc"))%>' Visible="true"></asp:Literal>
                                        <asp:Literal ID="lDataType" runat="server" 
                                            Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DataType"))%>' Visible="false"></asp:Literal>
                                    </td>
                                    <td align="right"  class="search" style="padding-left:2px;padding-right:2px;">
                                        <asp:Literal ID="lDataLength" runat="server" 
                                            Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DataLength"))%>' Visible="true"></asp:Literal>                                        
                                    </td>
                                    <td  class="search" align="right">                                        
                                        <asp:Literal ID="lDataDecimal" runat="server" 
                                            Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DataDecimal"))%>' Visible="true"></asp:Literal>                                        
                                    </td>
                                    <td class="search" align="center">
                                            <asp:Literal ID="lDisplay" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Display"))%>'></asp:Literal>
                                    </td>
                                </tr>
                                
                                        <asp:Repeater ID="RepeaterList" runat="server" visible="false">
                                                <HeaderTemplate>
                                                <tr >
                                    <td class="search"></td>
                                    <td class="search" colspan="3">
                                                    <table border="0" cellpadding="0" cellspacing="0" style="width:95%;border-collapse:collapse;border:solid 1px #D4D4D4; background-color: #DFFFDF;">
                                                        <tr >
                                                            <%--<td class="newtblheader" >
                                                                <asp:ImageButton ID="imgDelList" runat="server" visible ="true" imageurl="images/del.png" />
                                                            </td>                                                           --%>
                                                            <td style="font-weight:bold;padding:3px; background-color: #85D685; color: #00008A; font-size: 7pt;" align="left" colspan="2">
                                                                ITEM LIST</td>                                                                
                                                                
                                                        </tr>
                                
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        
                                                        <td align="left" style="padding:3px;border-bottom:solid 1px #D4D4D4">     
                                                        <%--<asp:Literal ID="lRow" runat="server" 
                                                                Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "rowno"))%>' Visible="true"></asp:Literal>--%>                                                            
                                                            <asp:Literal ID="lDocType" runat="server" 
                                                                Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DocType"))%>' Visible="false"></asp:Literal>
                                                                <asp:Literal ID="lColumnID" runat="server" visible="false"
                                                                Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ColumnID"))%>'></asp:Literal>
                                                                                            <asp:Literal ID="lCode" runat="server" 
                                                                Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Code"))%>' Visible="false"></asp:Literal>                                                       
                                                            <asp:Literal ID="lDesc" runat="server" 
                                                                Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CodeDesc"))%>' Visible="True"></asp:Literal>
                                                        </td>                                                       
                                                    </tr>                                                    
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </table>
                                                    </td>
                                </tr>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                    
                            </ItemTemplate>
                            <FooterTemplate>
                                
                            </FooterTemplate>
                        </asp:Repeater>
                        <asp:Panel ID="ftr" runat="server" Visible="false">
                        </table>
                        </asp:Panel>
                    </td>
                    </tr>
                    </table>
                    
                    </td>
                    </tr>
                    <tr>                        
                        <td colspan="7" class="tbldashed"></td>
                    </tr>

            </ItemTemplate>
            <FooterTemplate>
                    <tr>
                        <td style="border-top:solid 1px #ffffff" colspan="6"></td>
                    </tr>                     
            </FooterTemplate>
        </asp:Repeater>
        </table>        
            
        </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>
    <!-- end - resultset //-->     
    
</div>
</asp:Content>




<asp:Content ID="cntPopup" runat="server" ContentPlaceHolderID="PopupMenu">
    <!-- start - resultset delete//-->                                                                
    <asp:UpdatePanel ID="pnlDeleteDoc" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    <asp:Panel id="pDeleteDoc" runat="server" visible="false" Width="700px">  
    <table border="0" class="popuphdrbox" cellspacing="0" cellpadding="0" style="border: solid 1px #3A5671;border-collapse:collapse;width:100%">

            <tr>
               <td align="center">
                  <table cellspacing="0" class="popuphdr" cellpadding="0" border="0" style="width:100%">
                        <tr height="30px">
                        <td align="left" valign="middle" colspan="2">&nbsp;<img height="25px" width="20px" src="images/doctype.png" />&nbsp;Document Types - Delete</td>
                                            
                        <td  align="right" valign="top">
                            <asp:ImageButton ID="imgClose2" runat="server" imageurl="images/close_window.gif" onmouseover="this.src='images/close_window.gif'"  onmouseout="this.src='images/close_window.gif'" width="18px" Height="18px"/>
                        </td>
                    </tr>
                  </table>
               </td>
            </tr>
            <tr>
            <td align="left" style="padding:2px">
                <table  border="0" class="codetbl" width="100%" cellpadding="0" cellspacing="0" style="border:solid 1px #D4D4D4;background-color:white;border-collapse:collapse;">
        <%--<table border="0" class="codetbl" cellspacing="0" cellpadding="0" style="border-collapse:collapse;background-color:White;width:100%">--%>
        <asp:Repeater ID="Repeater2" visible="true" runat="server" >            
            <HeaderTemplate>                
                    <tr><td colspan="3" style="background-color:Gray;color:White;padding:2px;">Notes: <br />1. Only the records with green highlight will be deleted. Refer to the comments column for more info. <br />2. Click on Delete button to confirm deleting of selected records.</td></tr>
                    <tr >                        
                        <td class="newtblheader">Document Type</td>
                        <td class="newtblheader">Description</td>                                                
                        <td class="newtblheader">Comment</td>                                                
                    </tr>            
            </HeaderTemplate>
            <ItemTemplate>                
                    <tr id="rw" runat="server"  height="20px">
                        <td style="padding-left:2px;"><asp:Literal ID="tDocType" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DocType"))%>'></asp:Literal></td>
                        <td style="padding-left:2px;"><asp:Literal ID="tDesc" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DocName"))%>'></asp:Literal></td>                                                           
                        <td style="padding-left:2px;"><asp:Literal ID="lExists" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Exists"))%>'></asp:Literal></td>                                                           
                    </tr>                                    
                    <td class="tbldashed" colspan="3"></td>
                    </tr>             
            </ItemTemplate>
            <FooterTemplate>                 
            <tr>
               <td style="border-top:solid 1px #ffffff" colspan="3"></td>
            </tr>
            </FooterTemplate>                 
        </asp:Repeater>
            </table>
            <table style="margin-top:5px;">
            <tr>
                <td colspan="3" style="padding: 2 2 2 2 ">
                     <asp:Button ID="btDelete" runat="server" Text="Delete" cssclass="btn" />
                     <asp:Button ID="btCancel" runat="server" Text="Cancel" cssclass="btn" visible="false"/>
                </td>
            </tr>
        </table>      
        </td>
            </tr>
            </table>                                          
    </asp:Panel>
    <cc1:DropShadowExtender ID="dse3" runat="server" TargetControlID="pDeleteDoc" Opacity=".5" Rounded="false" TrackPosition="False"  />
    </ContentTemplate>
    </asp:UpdatePanel>
    <!-- end - resultset delete//-->  

    <asp:UpdatePanel ID="pnlAddDoc" runat="server" UpdateMode="Conditional">
    <ContentTemplate>  
    <asp:Panel id="pAddDoc" runat="server" Visible="false" CssClass="codetbl" Width="570px"  DefaultButton="btSave">
    <!-- start - search criteria //-->
    <center>
        
                        
         <table border="0" class="popuphdrbox" cellspacing="0" cellpadding="0" style="border: solid 1px #3A5671;border-collapse:collapse;width:100%">

            <tr>
               <td align="center">
                  <table cellspacing="0" class="popuphdr" cellpadding="0" border="0" style="width:100%">
                        <tr height="30px">
                        <td align="left" valign="middle" colspan="2">&nbsp;<asp:ImageButton ID="imgCopy" runat="server" imageurl="images/doctype.png" onmouseover="this.src='images/doctype.png'"  onmouseout="this.src='images/doctype.png'" width="25px" Height="20px"/>&nbsp;Document Types - <asp:Literal ID="lAction" runat="server" Text="Add"></asp:Literal></td>
                                            
                        <td  align="right" valign="top">
                            
                            <asp:ImageButton ID="imgClose" runat="server" imageurl="images/close_window.gif" onmouseover="this.src='images/close_window.gif'"  onmouseout="this.src='images/close_window.gif'" width="18px" Height="18px"/>
                        </td>
                    </tr>
                  </table>
               </td>
            </tr>
            <tr>
            <td style="padding-left:5px;padding-right:5px;">
            <table style="border-collapse:collapse;" border="0" cellpadding="3" cellspacing="0">
            <tr>
                <td valign="top">
            
            

                   <table  width="100%" height="100%" border="0" style="border-collapse:collapse;border:solid 1px #CCCCCC" >
            <tr>
                <td class="tableHead2" colspan="2">Details</td>
            </tr>
                
            <tr>
                <td align="left" class="labelFreeForm">Document Type:</td>
                <td align="left"><asp:TextBox ID="tbDocType" CssClass="entryfld" maxlength="50" runat="server"></asp:TextBox>
                </td>
                </tr><tr>
                <td align="left" class="labelFreeForm">Description:</td>            
                <td align="left">
                    <asp:TextBox ID="tbDocTypeDesc" runat="server" maxlength="100" CssClass="entryfld" Width="200px"></asp:TextBox>
                    <asp:Literal ID="lDocTypeDesc" runat="server" Visible="false"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td align="left" class="labelFreeForm" colspan="2">File Required (for uploading document):&nbsp;<asp:CheckBox ID="cbRequired" runat="server"></asp:CheckBox>
                </td>
                
                </tr>
                 <tr>
                <td align="left" class="labelFreeForm" colspan="2">Allow Printing (Archived Documents):&nbsp;<asp:CheckBox ID="cbAllowPrinting" runat="server"></asp:CheckBox>
                </td>
                
                </tr>
            
                </table>
               
                </td>
                <td valign="top">
                   <asp:UpdatePanel ID="pRetentionSched" runat="server" UpdateMode="Conditional" Visible="false">
                <ContentTemplate>
            <table  width="100%" height="100%" border="0" style="border-collapse:collapse;margin-bottom:3px;border:solid 1px #CCCCCC" >
            <tr>
                <td class="tableHead2" colspan="2">Retention Disposition Schedule</td>
            </tr>
            <tr>
                <td align="left" class="labelFreeForm" colspan="2">
                <asp:UpdatePanel ID="pEnableRetention" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    Enable Retention:&nbsp;<asp:CheckBox ID="cbRetention" runat="server" AutoPostBack="true"></asp:CheckBox>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                
                </tr>
            <%--<tr>
                <td align="left" class="labelFreeForm"><asp:RadioButton ID="rbStat"   GroupName="rbTrigger" runat="server"  Enabled="false" text="Trigger By Status" /></td>
                <td><asp:DropDownList ID="dlRetentionStatus" runat="server" Visible="true" CssClass="entryfld2" Enabled="false"></asp:DropDownList></td>
            </tr>
            <tr>
                <td align="left" class="labelFreeForm" colspan="2">
                    <asp:RadioButton ID="rbCreate" GroupName="rbTrigger" runat="server"  Enabled="false" text="Use Created Date" />
                </td>
            </tr>--%>
            <tr><td align="right" class="labelFreeForm">Active Period: </td>            
                <td align="left">
                    <asp:TextBox ID="tbAP" runat="server" maxlength="3" CssClass="entryfld" Width="30px"  Enabled="false"></asp:TextBox>
                    <asp:DropDownList ID="dlAP" runat="server" Visible="true"  Enabled="false" CssClass="entryfld2">
                        <asp:ListItem Value="D">Day(s)</asp:ListItem>
                        <asp:ListItem Value="M">Month(s)</asp:ListItem>
                        <asp:ListItem Value="Y">Year(s)</asp:ListItem>
                        </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" class="labelFreeForm">Storage Period: </td>            
                <td align="left">
                    <asp:TextBox ID="tbSP" runat="server" maxlength="3" CssClass="entryfld" Width="30px"  Enabled="false"></asp:TextBox>
                    <asp:DropDownList ID="dlSP"  runat="server" Visible="true"  Enabled="false" CssClass="entryfld2">
                        <asp:ListItem Value="D">Day(s)</asp:ListItem>
                        <asp:ListItem Value="M">Month(s)</asp:ListItem>
                        <asp:ListItem Value="Y">Year(s)</asp:ListItem>
                        </asp:DropDownList>
                </td>
            </tr>
            </table>
            </contenttemplate>
            <Triggers><asp:AsyncPostBackTrigger  ControlID="cbRetention" EventName="CheckedChanged"/></Triggers>
            </asp:UpdatePanel>
            </td>
            </tr>
            <tr>
            <td colspan="2">
                <asp:Panel ID="pCopy" runat="server" Visible="false">
                <table>
                <tr>
                <td align="left" class="labelFreeForm">Copy Document Index To:</td>
                <td align="left"><asp:DropDownList ID="dlDocType" runat="server" cssclass="entryfld2w" Width="175px" visible="true"></asp:DropDownList>
                </td>
                <td align="right" class="labelFreeForm"><asp:Button ID="btCopy" runat="server" CssClass="btn" Text="Copy" /></td>            
                
            </tr>
                </table>
                </asp:Panel>
            </td>
            </tr>           
                   
           
            <tr>
            <td colspan="2">
               
                <table width="100%" height="100%" style="border-collapse:collapse;border:solid 1px #CCCCCC">
                <tr >
                    <td class="tableHead2">Document Index
                    </td>
                </tr>
                <tr>
                    <td align="left" >
                    <div style="overflow:auto;height:300px">
                        <asp:Repeater ID="Repeater4" runat="server" visible="true">
                            <HeaderTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:collapse;border:solid 1px #D4D4D4">
                                    <tr >
                                        <td class="newtblheader" style="width:20px"><asp:ImageButton ID="imgDelete0" runat="server" visible ="false" imageurl="images/del.png" /></td>
                                        <td class="newtblheader" style="width:100px">Column Name</td>
                                        <td class="newtblheader" style="width:100px">DataType</td>
                                        <td class="newtblheaderint" style="width:60px">Length</td>
                                        <td class="newtblheaderint" style="width:110px">Import Col Seq</td>
                                        <td class="newtblheaderint" style="width:110px">Display</td>
                                    </tr>
                                
                            </HeaderTemplate>
                            <ItemTemplate>
                            
                                <tr>
                                    <td align="right" style="padding-right:3px">
                                    <asp:Literal ID="lRow" runat="server" 
                                            Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "rowno"))%>'></asp:Literal>
                                        <asp:CheckBox ID="cbxDelete" runat="server" visible="false"/>
                                        <asp:Literal ID="lDocType" runat="server" 
                                            Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DocType"))%>' Visible="false"></asp:Literal>
                                            <asp:Literal ID="lColumnID" runat="server" visible="false"
                                            Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ColumnID"))%>'></asp:Literal>
                                    </td>
                                    <td>
                                    
                                        <asp:TextBox ID="tColumnName" runat="server" CssClass="entryfldw" width="300px"
                                            Text='<%#Server.HtmlDecode(DataBinder.Eval(Container.DataItem, "ColumnName"))%>' 
                                            Visible="true"></asp:TextBox>
                                            <asp:Literal ID="lColumnname" runat="server" 
                                            Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ColumnName"))%>' Visible="false"></asp:Literal>
                                    </td>
                                    <td align="center">
                                    
                                        <asp:DropDownList ID="dlDataType" runat="server" cssclass="entryfld2" AutoPostBack="true">
                                        <asp:ListItem Value="1">Character</asp:ListItem>
                                        <asp:ListItem Value="2">Number</asp:ListItem>
                                        <asp:ListItem Value="3">Yes/No</asp:ListItem>
                                        <asp:ListItem Value="4">Date </asp:ListItem>
                                        <asp:ListItem Value="5">List</asp:ListItem>
                                        </asp:DropDownList>
                                      
                                        <asp:Literal ID="tDataType" runat="server" 
                                            Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DataType"))%>' Visible="false"></asp:Literal>
                                    </td>
                                    <td align="right" style="padding-right:3px">
                                        <asp:TextBox ID="tDataLength" width="50px" runat="server" CssClass="entryfldintw" 
                                            Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DataLength"))%>' 
                                            Visible="true"></asp:TextBox>
                                          
                                    </td>
                                   <td align="right" style="padding-right:3px">
                                          <asp:TextBox ID="tDataDecimal" width="100px" runat="server" CssClass="entryfldintw" 
                                            Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DataDecimal"))%>' 
                                            Visible="true" ></asp:TextBox>
                                       
                                    </td>
                                    <td align="center" style="padding-right:3px">
                                          <asp:CheckBox ID="cbDisplay" runat="server"   Checked='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DisplayInScreen"))%>' 
                                            Visible="true" ></asp:CheckBox>
                                       
                                    </td>
                                </tr>
                                <tr>
                                    
                                    <td colspan="6" align="center">
                                    <asp:UpdatePanel ID="pnlCodeList" runat="server" UpdateMode="Conditional">

                                            <ContentTemplate>                              
                                            <asp:Panel ID="pnlList" runat="server" visible="false">
                                            
                                            <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:collapse;border:solid 1px #D4D4D4; background-color: #DFFFDF;">
                                                        <tr >
                                                            <%--<td class="newtblheader" >
                                                                <asp:ImageButton ID="imgDelList" runat="server" visible ="true" imageurl="images/del.png" />
                                                            </td>     --%>                      
                                                            <td class="tableHead2" align="left"></td>                                
                                                            <td class="tableHead2" align="left">
                                                                Item List</td>                                                                
                                                                <td  align="right" class="tableHead2">
                                                                    <asp:ImageButton ID="imgAddRow" imageurl="images/add.png" width="14px" Height="14px" tooltip="Add new rows" runat="server" />
                                                                </td>
                                                        </tr>
                                            <asp:Repeater ID="RepeaterList" runat="server" Visible="false">
                                                <HeaderTemplate>
                                                    
                                
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td align="right" style="padding-right:3px;font-style:normal;color:Navy" >
                                                        <asp:Literal ID="lRow" runat="server" 
                                                                Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "rowno"))%>' Visible="true"></asp:Literal>
                                                            <asp:CheckBox ID="cbxDelete" runat="server" visible="false"/>
                                                            <asp:Literal ID="lDocType" runat="server" 
                                                                Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DocType"))%>' Visible="false"></asp:Literal>
                                                                <asp:Literal ID="lColumnID" runat="server" visible="false"
                                                                Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ColumnID"))%>'></asp:Literal>
                                                                                            <asp:Literal ID="lCode" runat="server" 
                                                                Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Code"))%>' Visible="false"></asp:Literal>
                                                        </td>                                                        
                                                        <td align="center" colspan="2">
                                                            <asp:TextBox ID="tDesc" runat="server" CssClass="entryfldw" width="250px"
                                                                Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CodeDesc"))%>' 
                                                                Visible="true"></asp:TextBox>
                                                            <asp:Literal ID="lDesc" runat="server" 
                                                                Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CodeDesc"))%>' Visible="false"></asp:Literal>
                                                        </td>                                                       
                                                    </tr>                                                    
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    
                                                </FooterTemplate>
                                            </asp:Repeater>
                                            </table>
                                            </asp:Panel>                                                        
                                            </contentTemplate>
                                            </asp:UpdatePanel>
                                    </td>
                                </tr>
                                
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                        </div>
                    </td>
                </tr>
            <tr>
                    
                    <td align="right" style="padding-right:20px">
                        
                        <asp:Button ID="btRows" runat="server" Text="Add More Rows"></asp:Button><asp:TextBox ID="tbRows" runat="server" MaxLength="3" Width="30px"></asp:TextBox>
                    </td>
                </tr>
                </table>
                
                <table style="margin-bottom:5px;margin-top:5px" align="left">
            <tr>
                        <td >
                            &nbsp;<asp:Button ID="btSave" runat="server" CssClass="btn" Text="Save" />&nbsp;<asp:Button ID="btClose" runat="server" CssClass="btn" Text="Close" /></td>

                        <td >
                        <asp:UpdatePanel ID="pnlMsg" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Label ID="msg" runat="server" cssclass="msg_red">&nbsp;</asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        </td>
                        
                        
                </tr>
                </table>
              </td>
            </tr>            
            </table>                
          </center>
    </asp:Panel>
    <cc1:DropShadowExtender ID="dse4" runat="server" TargetControlID="pAddDoc" Opacity=".5" Rounded="false" TrackPosition="False"  />
    </ContentTemplate>
    </asp:UpdatePanel>  
</asp:Content>
