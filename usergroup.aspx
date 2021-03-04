<%@ Page Title="Group Maintenance" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false"
    CodeBehind="usergroup.aspx.vb" Inherits="dms.usergroup" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register src="ucHr.ascx" tagname="ucHr" tagprefix="uc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register src="UserControlCheckBox.ascx" tagname="UserControlCheckBox" tagprefix="uc2" %>
<%@ Register src="UserControlPager.ascx" tagname="UserControlPager" tagprefix="uc" %>
<%@ Register src="UserControlWorkSched.ascx" tagname="UserControlWorkSched" tagprefix="uc3" %>
<%@ Register src="UserControlGroupDocTypeAccess.ascx" tagname="UserControlDocAccess" tagprefix="uc3" %>
<%@ Register src="UserControlAdminMenuH.ascx" tagname="UserControlAdminMenuH" tagprefix="uc" %>    
<%@ Register src="ucButton.ascx" tagname="ucButton" tagprefix="uc" %>    
<%--menu content start--%>

<%--main content end--%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>User Group Maintenance</title>
</asp:Content>

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
                        <td valign="middle"><div class="notes2">&nbsp;<asp:Literal ID="lRecordCount"  Visible="false" runat="server"></asp:Literal></div>
                        </td>
                        </tr>
                        </table>
</asp:Content>

<asp:Content ID="Content12" runat="server" ContentPlaceHolderID="AdminMenu">
    <uc:UserControlAdminMenuH id="UserControlAdminMenuH1" runat="server"></uc:UserControlAdminMenuH>                                       
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="MainHeaderContent">

                <table border="0" width="100%" class="tableheaderGreen">
        <tr >
            <td class="hdrtitle_1" > <img  alt="" src="images/group.png" style="vertical-align:middle" Width="20px" height="20px"/>&nbsp;User Groups</td>
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
                            <%--<asp:ImageButton ID="imgAddGroup" runat="server" ImageUrl="images/button/addgroup.png" onmouseover="this.src='images/button/addgroup.png'"  onmouseout="this.src='images/button/addgroup.png'"  tooltip="Add User Group" visible="false"/>--%>
                            
                            <uc:ucButton id="ucAdd" runat="server" pText="Add New Group" pImage="images/group2.png"></uc:ucButton>
                            <asp:UpdatePanel ID="pnlFilter" runat="server" UpdateMode="Conditional">
                                   <ContentTemplate>
                                   <div style="border-radius:5px;border-style: solid; border-width: 1px; border-color: #F1F4F8 #CFDBE7 #81A0C0 #CEDAE8; background-color: #FFFFFF; width: 98%; margin-top: 8px; margin-left: 1px">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="tableheaderGreen" >
                                        <tr height="25px">
                                            <td align="left"  class="tableHead27" style="padding-left:3px">
                                                <img alt="" width="24px" Height="20px" src="images/find.png" />&nbsp;&nbsp;<asp:Label ID="lbUser" runat="server" style="color:#EEEEEE;font-family:Arial;font-size:10pt;font-weight:bold;font-style:normal;color:#CCCCCC">Filter User Groups</asp:Label></td>
                                                <td width="50px" align="right" valign="top" class="tableHead27" >
                                                    <asp:ImageButton ID="imgUser" runat="server" imageurl="images/showpanel.png"/></td>
                                        </tr>
                                    </table>
                    
                                <asp:Panel runat="server" ID="pFilter" Visible="false" DefaultButton="btSearch"> 
                                <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                        <tr>
                                <td align="left" class="labelFreeForm">
                                    Group Code:</td>
                                <td align="left">
                                    <asp:TextBox ID="GroupCode"  MaxLength="5" CssClass="entryfld" runat="server"></asp:TextBox>
                                </td>
            
                                <td align="left">
                                    &nbsp;</td>
            
                            </tr>
                            <tr>
                                <td align="left" class="labelFreeForm">
                                    Group Name:</td>
                                <td align="left">
                                    <asp:TextBox ID="tbDesc" runat="server" CssClass="entryfld"></asp:TextBox>
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
                                </div>
                                </ContentTemplate>
                                </asp:UpdatePanel>
                                <br />
    
         
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    
    <div class="mainDiv_" align="left">
    
        
    <!-- end - search criteria //-->
    <!-- start - resultset //-->                                                                
    <asp:UpdatePanel ID="pnlRepeater" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    <asp:Panel id="pRepeater" runat="server" >  
    <table border="0" class="codetbl" cellspacing="0" cellpadding="0" style="border-collapse:collapse;width:100%;z-index:900;border:solid 1px #D4D4D4">
                    <tr >
                        <td class="newtblheader" ></td>                         
                        <td class="newtblheader">
                        <asp:LinkButton ID="lbSort1" runat="server" cssclass="sortcol" tooltip="Sort by Group Code" OnClick="sortColumnHeader">Group Code</asp:LinkButton><asp:Image ID="imgSort1" imageurl="images/asc.png" runat="server" visible="true"/></td>
                        <td class="newtblheader">
                        <asp:LinkButton ID="lbSort2" runat="server" cssclass="sortcol" tooltip="Sort by Group Name" OnClick="sortColumnHeader">Group Name</asp:LinkButton><asp:Image ID="imgSort2" imageurl="images/asc.png" runat="server" visible="false"/></td>
                        
                        <td class="newtblheader">
                        <asp:LinkButton ID="lbSort3" runat="server" cssclass="sortcol" tooltip="Sort by Report Access" OnClick="sortColumnHeader">Report Access</asp:LinkButton><asp:Image ID="imgSort3" imageurl="images/asc.png" runat="server" visible="false"/></td>                       
                        <td class="newtblheader">
                        <asp:LinkButton ID="lbSort5" runat="server" cssclass="sortcol" tooltip="Sort by Archive Doc" OnClick="sortColumnHeader">Edit Index</asp:LinkButton><asp:Image ID="imgSort5" imageurl="images/asc.png" runat="server" visible="false"/></td>
                        <%--<td class="newtblheader">
                        <asp:LinkButton ID="lbSort7" runat="server" cssclass="sortcol" tooltip="Sort by Import Doc" OnClick="sortColumnHeader">Import Doc</asp:LinkButton><asp:Image ID="imgSort7" imageurl="images/asc.png" runat="server" visible="false"/></td>                                              --%>
                        <td class="newtblheader">
                        <asp:LinkButton ID="lbSort8" runat="server" cssclass="sortcol" tooltip="Sort by Report Access" OnClick="sortColumnHeader">Print Access</asp:LinkButton><asp:Image ID="Image1" imageurl="images/asc.png" runat="server" visible="false"/></td>                       
                        <td class="newtblheader">
                        <asp:LinkButton ID="lbSort9" runat="server" cssclass="sortcol" tooltip="Sort by Archive Doc" OnClick="sortColumnHeader">Download Access</asp:LinkButton><asp:Image ID="Image2" imageurl="images/asc.png" runat="server" visible="false"/></td>
                        <td class="newtblheader">
                        <asp:LinkButton ID="lbSort10" runat="server" cssclass="sortcol" tooltip="Sort by Version Control" OnClick="sortColumnHeader">Version Control</asp:LinkButton><asp:Image ID="Image3" imageurl="images/asc.png" runat="server" visible="false"/></td>                                              
                        <td class="newtblheader">
                        <asp:LinkButton ID="lbSort4" runat="server" cssclass="sortcol" tooltip="Sort by Work Schedules" OnClick="sortColumnHeader">Work Schedules</asp:LinkButton><asp:Image ID="imgSort4" imageurl="images/asc.png" runat="server" visible="false"/></td>                                               
                        <td class="newtblheader"><asp:LinkButton ID="lbSort6" runat="server" class="sortcol" tooltip="Sort by Office" OnClick="sortColumnHeader">Office</asp:LinkButton><asp:Image ID="imgSort6" imageurl="images/asc.png" runat="server" visible="false"/></td>                                               
                        <td class="newtblheader" style="width:20px" align="center"><asp:ImageButton ID="imgDelete" runat="server" imageurl="images/del.png"/></td>

                    </tr>            
    <asp:Repeater ID="Repeater1" visible="true" runat="server" >

            <HeaderTemplate>
                
            </HeaderTemplate>
            <ItemTemplate>                
                    <tr>
                    <td align="center" style="width:40px">
                    
                    <asp:ImageButton ID="imgUpdate" runat="server" imageurl="images/update.png" Width="15px" Height="15px"/></td>
                    
                        <td><asp:ImageButton ID="imgPlus" runat="server" ImageUrl="images/plus.jpg" OnClick="imgShowWorkSched"/>
                            <asp:ImageButton ID="imgMinus" runat="server" ImageUrl="images/minus.jpg" Visible="false" OnClick="imgShowWorkSched"/>
                            <asp:Literal ID="GroupCode" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "GroupId"))%>'></asp:Literal>
                            <asp:Literal ID="lColor" Visible="false" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TextColor"))%>'></asp:Literal>
                            <asp:Literal ID="lTColor" Visible="false" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TrackingColor"))%>'></asp:Literal>
                            <asp:Literal ID="lGroupLogo" Visible="false" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "GroupLogo"))%>'></asp:Literal>
                            <asp:Literal ID="lReceiptReplyTitle" Visible="false" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ReceiptReplyName"))%>'></asp:Literal>
                            </td>
                        <td><asp:Literal ID="GroupName" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "GroupName"))%>'></asp:Literal>
                        
                            <asp:TextBox ID="tbGroupName" runat="server" CssClass="txt"  Text='<%#DataBinder.Eval(Container.DataItem, "GroupName")%>' Visible="false" MaxLength="100"></asp:TextBox>
                        </td>                        
                        <td><asp:Label ID="lReportAccess" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CanReport"))%>'></asp:Label>
                        
                        </td>
                        <td>
                           <asp:Label ID="lEditIndexDoc" runat="server" visible="True" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CanEditIndex"))%>'></asp:Label>
                        </td>
                        <%--<td>
                           <asp:Label ID="lImportDoc" runat="server" visible="True" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CanImport"))%>'></asp:Label>
                        </td>--%>
                        <td><asp:Label ID="lCanPrint" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Canprint"))%>'></asp:Label>
                        
                        </td>
                        <td>
                           <asp:Label ID="lCanDownload" runat="server" visible="True" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CanDownload"))%>'></asp:Label>
                        </td>
                        <td>
                           <asp:Label ID="lVersionControl" runat="server" visible="True" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CanVersionControl"))%>'></asp:Label>
                        </td>
                        <td><asp:Literal ID="lWorkSched" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "AlwaysAllowedDesc"))%>'></asp:Literal>
                        </td>
                        <td><asp:Literal ID="OfficeCode" Visible="true" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "OfficeCode"))%>'></asp:Literal>
                        <asp:Literal ID="MainGroupId" Visible="false" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "MainGroupId"))%>'></asp:Literal>
                        </td>                        
                        <td  align="center"><asp:CheckBox ID="cbxDelete" runat="server" />
                            <asp:Literal ID="lAllowed" visible="false" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "AlwaysAllowed"))%>'></asp:Literal>
                    </td>    
                    </tr>                
                    <tr>
                        <td colspan="9" align="center">
                            <table>
                            <tr>
                            <td valign="top">
                            <asp:Panel ID="pDocAccess" runat="server" Visible="false">
                                <uc3:UserControlDocAccess ID="ucDocAccess" runat="server" visible="true"/>
                            </asp:Panel>    
                            </td>
                            <td valign="top">
                            <asp:Panel ID="pLogo" runat="server" Visible="false" style="margin-bottom:3px">
                            <table style="border:solid 1px #CCCCCC;border-collapse:collapse;" width="100%">
                            <tr>
                            <td  style="font-size:10px;font-weight:bold;padding:3px;background-color:#CCCCCC; ">Group Logo</td>
                            </tr>
                            <tr>
                            <td align="center">
                                <asp:Image ID="imgLogo" runat="server" imageurl='<%#  "images/logo/" & Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "grouplogo"))%>'/>
                            </td>
                            </tr>
                            </table>
                            </asp:Panel>
                            <asp:Panel ID="WorkDetails" runat="server" Visible="false">
                                <uc3:UserControlWorkSched ID="ucWorkSched" runat="server" visible="true"/>
                            </asp:Panel>

                            </td>
                            </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="9" class="tbldashed"></td>
                    </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>             
                    <tr style="background-color:#D2E9FF">
                    <td align="center" style="width:40px;">
                    
                    <asp:ImageButton ID="imgUpdate" runat="server" imageurl="images/update.png" Width="15px" Height="15px"/></td>
                    
                        <td><asp:ImageButton ID="imgPlus" runat="server" ImageUrl="images/plus.jpg" OnClick="imgShowWorkSched"/>
                            <asp:ImageButton ID="imgMinus" runat="server" ImageUrl="images/minus.jpg" Visible="false" OnClick="imgShowWorkSched"/>
                            <asp:Literal ID="GroupCode" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "GroupId"))%>'></asp:Literal>
                            <asp:Literal ID="lColor" Visible="false" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TextColor"))%>'></asp:Literal>
                            <asp:Literal ID="lTColor" Visible="false" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TrackingColor"))%>'></asp:Literal>
                            <asp:Literal ID="lGroupLogo" Visible="false" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "GroupLogo"))%>'></asp:Literal>
                            <asp:Literal ID="lReceiptReplyTitle" Visible="false" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ReceiptReplyName"))%>'></asp:Literal>
                            </td>
                        <td><asp:Literal ID="GroupName" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "GroupName"))%>'></asp:Literal>
                        
                            <asp:TextBox ID="tbGroupName" runat="server" CssClass="txt"  Text='<%#DataBinder.Eval(Container.DataItem, "GroupName")%>' Visible="false" MaxLength="100"></asp:TextBox>
                        </td>                        
                        <td><asp:Label ID="lReportAccess" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CanReport"))%>'></asp:Label>
                        
                        </td>
                        <td>
                           <asp:Label ID="lEditIndexDoc" runat="server" visible="True" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CanEditIndex"))%>'></asp:Label>
                        </td>
                        <td>
                           <asp:Label ID="lImportDoc" runat="server" visible="True" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CanImport"))%>'></asp:Label>
                        </td>
                        <td><asp:Label ID="lCanPrint" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Canprint"))%>'></asp:Label>
                        
                        </td>
                        <td>
                           <asp:Label ID="lCanDownload" runat="server" visible="True" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CanDownload"))%>'></asp:Label>
                        </td>
                        <td>
                           <asp:Label ID="lVersionControl" runat="server" visible="True" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CanVersionControl"))%>'></asp:Label>
                        </td>
                        <td><asp:Literal ID="lWorkSched" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "AlwaysAllowedDesc"))%>'></asp:Literal>
                        </td>
                        <td><asp:Literal ID="OfficeCode" Visible="true" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "OfficeCode"))%>'></asp:Literal>
                        <asp:Literal ID="MainGroupId" Visible="false" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "MainGroupId"))%>'></asp:Literal>
                        </td>                        
                        <td  align="center"><asp:CheckBox ID="cbxDelete" runat="server" />
                            <asp:Literal ID="lAllowed" visible="false" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "AlwaysAllowed"))%>'></asp:Literal>
                    </td>    
                    </tr>                
                    <tr  style="background-color:#D2E9FF">
                        <td colspan="9" align="center">
                            <table>
                            <tr>
                            <td valign="top">
                            <asp:Panel ID="pDocAccess" runat="server" Visible="false">
                                <uc3:UserControlDocAccess ID="ucDocAccess" runat="server" visible="true"/>
                            </asp:Panel>    
                            </td>
                            <td valign="top">
                            <asp:Panel ID="pLogo" runat="server" Visible="false" style="margin-bottom:3px">
                            <table style="border:solid 1px #CCCCCC;border-collapse:collapse;" width="100%">
                            <tr>
                            <td  style="font-size:10px;font-weight:bold;padding:3px;background-color:#CCCCCC; ">Group Logo</td>
                            </tr>
                            <tr>
                            <td align="center">
                                <asp:Image ID="imgLogo" runat="server" imageurl='<%#  "images/logo/" & Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "grouplogo"))%>'/>
                            </td>
                            </tr>
                            </table>
                            </asp:Panel>
                            <asp:Panel ID="WorkDetails" runat="server" Visible="false">
                                <uc3:UserControlWorkSched ID="ucWorkSched" runat="server" visible="true"/>
                            </asp:Panel>
                            </td>
                            </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="9" class="tbldashed"></td>
                    </tr>
            
            </AlternatingItemTemplate>
            <FooterTemplate>
                    <tr>
                        <td style="border-top:solid 1px #ffffff" colspan="7"></td>
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

        <asp:UpdatePanel ID="pnlAddGroup" runat="server" UpdateMode="Conditional">
    <ContentTemplate>  
    <asp:Panel id="pAddGroup" runat="server" Visible="false" Width="850px"  DefaultButton="btSave">
    <!-- start - search criteria //-->
    <center>
    
    
        
         <table border="0" class="popuphdrbox" cellspacing="0" cellpadding="0" style="border: solid 1px #3A5671;border-collapse:collapse;width:100%">

            <tr>
               <td align="center">
                  <table cellspacing="0" class="popuphdr" cellpadding="0" border="0" style="width:100%">
                        <tr height="30px">
                        <td align="left" valign="middle" colspan="2">&nbsp;<img height="25px" width="20px" src="images/group.png" />&nbsp;User Groups - <asp:Literal ID="lAction" runat="server"></asp:Literal></td>
                                            
                        <td  align="right" valign="top">
                            <asp:ImageButton ID="imgClose" runat="server" imageurl="images/close_window.gif" onmouseover="this.src='images/close_window.gif'"  onmouseout="this.src='images/close_window.gif'" width="18px" Height="18px"/>
                        </td>
                    </tr>
                  </table>
               </td>
            </tr>
            <tr>
            <td  valign="top">
            
            <table cellpadding="0" cellspacing = "0" width="100%" border="0" style="border-collapse:collapse">
            <tr>
            <td valign="top" width="50%" style="padding:2px">
            <table width="100%" border="0" style="border:solid 1px #CCCCCC;border-collapse:collapse;" cellpadding="0" cellspacing="0">
            <tr><td colspan="2" class="tableHead2">Group Information</td></tr>
            <tr>
                <td align="left" style="padding-top:5px">* Group Code:</td>
                <td align="left" style="padding-top:5px"><asp:TextBox ID="tbGroupCode" class="entryfld" MaxLength="5" runat="server" TabIndex="10"></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td align="left">* Group Name:</td>
                <td align="left"><asp:TextBox ID="tbGroupName"  CssClass="entryfld" width="250px" runat="server" MaxLength="100" TabIndex="11"></asp:TextBox>
                <asp:Literal ID="lGroupNameOrig" runat="server" Text='' Visible="false"></asp:Literal>
                </td>
                                        
            </tr>
            <tr>
                <td align="left">* Office:</td>
                <td align="left"><asp:DropDownList ID="dlOfficeCode" runat="server" cssclass="entryfld2" Width="252px" AutoPostBack="True"  TabIndex="12">
                                            </asp:DropDownList>
                </td>
                
            </tr>
            <tr>
                <td align="left">* Main Group:</td>
                <td align="left"><asp:DropDownList ID="ddlMainGroup" runat="server" cssclass="entryfld2" Width="252px" AutoPostBack="True"  TabIndex="13">
                                            </asp:DropDownList>
                </td>
                
            </tr>
            <tr>
                <td align="left"> Receipt / Reply Title:</td>
                <td align="left">
                    <asp:TextBox ID="tbRRTitle" class="entryfld" MaxLength="200" runat="server" Width="250px" TabIndex="26"></asp:TextBox>
                </td>
                
            </tr>
            <tr>
            <td  style="padding-bottom:3px">Group Logo:</td>
            <td style="padding-bottom:3px"><asp:TextBox ID="tbGroupLogo" class="entryfld" MaxLength="50" runat="server" Width="250px" Enabled="true"></asp:TextBox></td>
            </tr>                        
            </table>
            <table style="margin-top:4px;border-collapse:collapse;border:solid 1px #CCCCCC" width="100%">
                        <tr>
                            <td colspan="4" class="tableHead2">Tracking Color</td>
                            
                        </tr>                        
                        <tr>
                            <td  align="left">Background Color:</td><td>
                                <asp:TextBox ID="tbgColor" runat="server" CssClass="entryfld"  Width="60px"  TabIndex="23"></asp:TextBox>
                                <cc1:ColorPickerExtender runat="server" 
    ID="ColorPicker1"
    TargetControlID="tbgColor"
    OnClientColorSelectionChanged="colorChanged" /></td>
                                                  <td  align="left">Text Color:</td><td>
                                <asp:TextBox ID="tColor" runat="server"  CssClass="entryfld"  Width="60px" TabIndex="24"></asp:TextBox>
                                <cc1:ColorPickerExtender runat="server" 
    ID="ColorPicker2"
    TargetControlID="tColor"
    OnClientColorSelectionChanged="colorChanged" />
    </td>
                        </tr>
                        </table>
            </td>
            <td valign="top" width="50%" style="paddinG:2px">
            
            <table width="100%" border="0" style="margin-top:4px;border:solid 1px #CCCCCC;border-collapse:collapse;" cellpadding="0" cellspacing="0">
                <tr><td colspan="2" class="tableHead2">Group Access</td></tr>
                <tr>                
                <td align="left"><asp:CheckBox ID="cbReportAccess" runat="server"  Text="Access Reports" TabIndex="27"></asp:CheckBox>
                <asp:Literal ID="lRepAccess" runat="server" Text='' Visible="false"></asp:Literal>
                
                <td align="left"><asp:CheckBox ID="cbEditIndex" runat="server" Text="Edit Document Index" TabIndex="28"></asp:CheckBox>                
                <asp:Literal ID="lEditIndexAccess" runat="server" Text='' Visible="false"></asp:Literal>
                </td>
                </tr>
                <tr>
                <td align="left"><asp:CheckBox ID="cbDownload" runat="server" text="Download Document" TabIndex="29"></asp:CheckBox>
                <asp:Literal ID="lDownload" runat="server" Text='' Visible="false"></asp:Literal>
                </td>
                                
                <td align="left"><asp:CheckBox ID="cbPrint" runat="server" Text="Print Document" TabIndex="27"></asp:CheckBox>
                <asp:Literal ID="lPrint" runat="server" Text='' Visible="false"></asp:Literal>
                </td>
                </tr>
                <tr>
                <td align="left"><asp:CheckBox ID="cbVersionControl" text="Checkout Document" runat="server"  TabIndex="28"></asp:CheckBox>                
                <asp:Literal ID="lVersionControl" runat="server" Text='' Visible="false"></asp:Literal>
                </td>
                
                <td align="left"><%--<asp:CheckBox ID="cbImport" Text="Import Document" runat="server"  TabIndex="29"></asp:CheckBox>
                <asp:Literal ID="lImportAccess" runat="server" Text='' Visible="false"></asp:Literal>--%>
                </td>
                
                
                </tr>           
            </table>
            <table width="100%" border="0" style="margin-top:4px;border:solid 1px #CCCCCC;border-collapse:collapse;" cellpadding="0" cellspacing="0">
                <tr><td colspan="2" class="tableHead2">Copy Group Document Access</td></tr>
                <tr>
                    <td align="left"> Group Name:</td>
                    <td align="left">
                    <asp:DropDownList ID="ddlGroup" runat="server" cssclass="entryfld2" Width="250px" AutoPostBack="false"  TabIndex="25">
                                            </asp:DropDownList>
                    </td>
                
                </tr>
                </table>
                <table style="width:100%;">
                                <tr>
                                    <td style="border:solid 1px #CCCCCC" align="center">
                                        <asp:UpdatePanel ID="pnlpic" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:Image ID="imgPic" runat="server" Height="103px" Width="103px" ImageAlign="Middle" 
                                                    />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                
                                <td valign="top">
                                <table style="width:100%;">
                                <tr>
                                    <td align="left" class="labelFreeForm">
                                        Upload Logo:</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:FileUpload ID="uploadPic" runat="server" CssClass="entryfld2" />
                                    </td>
                                </tr>
                                </table>
                                </td>
                                </tr>
                            </table>
            </td>
            </tr>
            <tr>
            <td colspan="2"  valign="top">
            <table width="100%">
            <tr>
            <td align="left" colspan="3">
            <div id="docaccess" style="overflow: auto;max-height:280px;width:100%">
             <asp:UpdatePanel ID="pnlGAccess" runat="server" UpdateMode="conditional"  Visible="True">          
                    <ContentTemplate>
            <table  border="0" class="codetbl" width="100%" cellpadding="0" cellspacing="0" style="border:solid 1px #d4d4d4;background-color:white;border-bottom:4px;border-collapse:collapse">
                    <tr>
                    <td class="tableHead2" colspan="3">Document Access</td>
                    
                
                    </tr>
                    <asp:Repeater ID="Repeater3" visible="true" runat="server" >
            <HeaderTemplate>
            
                    <tr height="25px" >
                        <td class="newtblheader" style="width:5%" align="center"><asp:Image visible="false" ID="imgSelect" runat="server" imageurl="images/select.png" ToolTip="Select the document type"/></td>
                        <td class="newtblheader" style="width:30%">
                            <asp:LinkButton ID="lbDocType" runat="server" OnClick="fSelectDocType" ToolTip="Select All Document Types">Doc Type</asp:LinkButton></td>
                        <td class="newtblheader" style="width:47%">Description</td>                       
                        
                    </tr>            
            </HeaderTemplate>
            <ItemTemplate>       
                  
                    <tr id="tr1" runat="server" ><td valign="top"   class="tbldashed" align="center">
                        <asp:CheckBox ID="cbSelect" runat="server" />
                        <%--<asp:ImageButton ID="imgSelect" runat="server" ImageUrl="images/box.png" />
                        <asp:ImageButton ID="ImgSelected" runat="server" ImageUrl="images/checkbox.png" visible="false" /> --%>                           
                      
                        </td>
                        <td valign="top" class="tbldashed" ><asp:Literal ID="DocType" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DocType"))%>'></asp:Literal>
                        <asp:Literal ID="GDocType" visible="false" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "GDocType"))%>'></asp:Literal>
                        
                        
                        </td>
                        <td valign="top" class="tbldashed" ><asp:Literal ID="DocName" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DocName"))%>'></asp:Literal>                           
                        </td>                     
                        
                    </tr>      
              
                    
            </ItemTemplate>
            <FooterTemplate>
            <tr>
               <td style="border-top:solid 1px #ffffff" colspan="6"></td>
            </tr>
                 </table>                                
            </FooterTemplate>
        </asp:Repeater>
      </ContentTemplate>
                    </asp:UpdatePanel>
            </div>
                </td>
            </tr>
            
                </table>
            </td>
            </tr>
            </table>
            
            
            
            
                </td>
                </tr>
                <tr>
                <td align="left">
                <table>
                <tr>
            <td>
                            &nbsp;<asp:Button ID="btSave" runat="server" CssClass="btn" Text="Save" />&nbsp;
                            <asp:Button ID="btClose" runat="server" CssClass="btn" Text="Close" />
                        </td>                        
                    <td align="left" colspan="3">
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
    <cc1:DropShadowExtender ID="dse3" runat="server" TargetControlID="pAddGroup" Opacity=".5" Rounded="false" TrackPosition="False"  />
    </ContentTemplate>
    <Triggers><asp:PostBackTrigger ControlID="btSave" /></Triggers>
    </asp:UpdatePanel>  
    
    <!-- start - resultset delete//-->                                                                
    <asp:UpdatePanel ID="pnlDeleteGroup" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    <asp:Panel id="pDeleteGroup" runat="server" visible="false" width="800px">  
    <table border="0" class="popuphdrbox" cellspacing="0" cellpadding="0" style="border: solid 1px #3A5671;border-collapse:collapse;width:100%">

            <tr>
               <td align="center">
                  <table cellspacing="0" class="popuphdr" cellpadding="0" border="0" style="width:100%">
                        <tr height="30px">
                        <td align="left" valign="middle" colspan="2">&nbsp;<img height="25px" width="20px" src="images/group.png" />&nbsp;User Groups - Delete</td>
                                            
                        <td  align="right" valign="top">
                            <asp:ImageButton ID="imgClose2" runat="server" imageurl="images/close_window.gif" onmouseover="this.src='images/close_window.gif'" onmouseout="this.src='images/close_window.gif'" width="18px" Height="18px"/>
                        </td>
                    </tr>
                  </table>
               </td>
            </tr>
            <tr>
            <td align="left" style="padding:2px">
                <table border="0" cellspacing="0" cellpadding="0" style="border-collapse:collapse;background-color:White;width:100%;border:solid 1px #D4D4D4">
    <asp:Repeater ID="Repeater2" visible="true" runat="server" >
            <HeaderTemplate>
                
                    <tr><td colspan="5" style="background-color:Gray;color:White;padding-left:2px;">Notes: <br />1. Only the records with green highlight will be deleted. Refer to the comments column for more info. <br />2. Click on Delete button to confirm deleting of selected records.</td></tr>
                    <tr>                        
                        <td class="newtblheader">Group Code</td>
                        <td class="newtblheader">Group Name</
                        td>                        
                        <td class="newtblheader">Report Access</td>
                        <td class="newtblheader">Work Schedules</td>
                        <td class="newtblheader">Comment</td>
                    </tr>            
            </HeaderTemplate>
            <ItemTemplate>                
                    <tr  id="rw" runat="server" height="20px">
                        <td class="tbldashed"><asp:Literal ID="GroupCode" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "GroupCode"))%>'></asp:Literal></td>
                        <td class="tbldashed"><asp:Literal ID="GroupName" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "GroupName"))%>'></asp:Literal>                       
                        </td>                        
                        <td class="tbldashed"><asp:Literal ID="lReportAccess" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ReportAccess"))%>' Visible="true"></asp:Literal></td>
                        <td class="tbldashed"><asp:Literal ID="lWorkSched" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "WorkSchedules"))%>' Visible="false"></asp:Literal>
                        </td>                        
                        <td class="tbldashed"><asp:Literal ID="lComment" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Comment"))%>' Visible="true"></asp:Literal> 
                        </td>                        
                    </tr>                      
            </ItemTemplate>
            <FooterTemplate>
                 <tr>
                    <td style="border-top:solid 1px #ffffff" colspan="5"></td>
                    </tr>
            </FooterTemplate>
                 
        </asp:Repeater>
        </table>
        <table>
        <tr><td colspan="5">
                     <asp:Button ID="btDelete" runat="server" Text="Delete" cssclass="btn" />
                     
                     </td></tr>
                     </table>
    </td>
            </tr>
            </table>
        </asp:Panel>
        <cc1:DropShadowExtender ID="DropShadowExtender1" runat="server" TargetControlID="pDeleteGroup" Opacity=".5" Rounded="false" TrackPosition="False"  />
    </ContentTemplate>
    </asp:UpdatePanel>
    <!-- end - resultset delete//-->  
        
    </asp:Content>
    


