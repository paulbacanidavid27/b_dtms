<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ChangeLog.aspx.vb" Inherits="dms.ChangeLog" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register src="ucHr.ascx" tagname="ucHr" tagprefix="uc1" %>
<%@ Register src="UserControlAdminMenuH.ascx" tagname="UserControlAdminMenuH" tagprefix="uc" %>
<%@ Register src="UserControlPager.ascx" tagname="UserControlPager" tagprefix="uc" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Tack Admin Changes</title>
</asp:Content>
<%--main headr content start--%>
<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="MainHeaderContent">
 
</asp:Content>
<%--main headr content end--%>

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
<%--main content end--%>

<%--main headr content start--%>

<asp:Content ID="Content8" runat="server" ContentPlaceHolderID="MainFooterContent">
    <table cellpadding="0" cellspacing="3" border="0" width="100%" class="tableheaderGreen">
                   <tr >
                        <td valign="middle"><div class="notes2">&nbsp;<asp:Literal ID="lRecordCount" runat="server"></asp:Literal></div>
                        </td>
                        </tr>
                        </table>
</asp:Content>
<%--main headr content end--%>
<asp:Content ID="Content6" ContentPlaceHolderID="AddContent" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0" class="tableheaderGreen">
        <tr><td></td></tr></table>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mainDiv_" align="left">
 <table border="0" width="100%" class="tableheaderGreen">
        <tr>
            <td class="tableheader_1"> <img  alt="" Width="20px" height="20px"  src="images/changelog.png" style="vertical-align:middle" />&nbsp;Track Changes</td>
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
             <table width="100%" cellpadding="0" cellspacing="0" style="margin-top:5px;margin-bottom:5px;">
        <tr>
            <td width="100px" class="labelFreeForm" align="right" style="padding-right:4px">Table Name
            </td>
            <td align="left">
                <asp:DropDownList ID="dlTables" runat="server" CssClass="entryfld2">
                    <asp:ListItem Value="Users">Users</asp:ListItem>
                    <asp:ListItem Value="Group">User Groups</asp:ListItem>
                    <asp:ListItem Value="DocType">Document Type</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="labelFreeForm"  align="right" style="padding-right:4px">Modified By</td>
            <td><asp:TextBox ID="tbUserName" width="250px"  cssclass="entryfld" runat="server"  maxlength="100" Text="" Visible="true"></asp:TextBox>
            <cc1:autocompleteextender runat="server" ID="acomplete" TargetControlID="tbUserName"
                                                 ServiceMethod="getUsers" ServicePath="getUser.asmx" CompletionInterval="1000" EnableCaching="true"
                                                  MinimumPrefixLength="1"
                                                 completionsetcount="25" />
            </td>
            
            
        </tr>
        
        <tr>
            <td  class="labelFreeForm" align="right" style="padding-right:4px">Modified Date</td>
            <td>
            <asp:TextBox ID="tbProcessDate" width="67px"  cssclass="entryfld" runat="server"  maxlength="10" Text="" Visible="true"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="tbProcessDate" />
            </td>
            <td align="right" colspan="2"><asp:Button ID="btRetrieve" runat="server" CssClass="btn" Text="Retrieve" visible="true"/>&nbsp;<asp:Button ID="btProcess" runat="server" CssClass="btn" Text="Process" visible="false"/></td>
        </tr>
        </table>
        
             <asp:UpdatePanel ID="pnlRepeater2" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    <asp:Panel id="pRepeater" runat="server" >  
    
        
    <asp:UpdatePanel id="pnlRepeater" runat="server" UpdateMode="Conditional">
    <ContentTemplate>

    
    <table border="0" class="codetbl" cellspacing="0" cellpadding="0" style="border-collapse:collapse;width:100%;z-index:900;border:solid 1px #D4D4D4">
                    <tr >
                        <%--<td class="newtblheader" style="width:20px"><asp:ImageButton ID="imgDelete" runat="server" imageurl="~/img_delete.jpg"/></td>--%>
                        
                        <td class="newtblheader">
                        <asp:LinkButton ID="lbSort2" runat="server" class="sortcol" tooltip="Sort by ID" OnClick="sortColumnHeader">ID</asp:LinkButton><asp:Image ID="imgSort2" imageurl="images/asc.png" runat="server" visible="false"/></td>
                        <td class="newtblheader">
                        <asp:LinkButton ID="lbSort3" runat="server" class="sortcol" tooltip="Sort by Column Name" OnClick="sortColumnHeader">Column Name</asp:LinkButton><asp:Image ID="imgSort3" imageurl="images/asc.png" runat="server" visible="false"/></td>                       
                        <td class="newtblheader">
                        <asp:LinkButton ID="lbSort4" runat="server" class="sortcol" tooltip="Sort by Old Value" OnClick="sortColumnHeader">Old Value</asp:LinkButton><asp:Image ID="imgSort4" imageurl="images/asc.png" runat="server" visible="false"/></td>                       
                        <td class="newtblheader">
                        <asp:LinkButton ID="LinkButton1" runat="server" class="sortcol" tooltip="Sort by New Value" OnClick="sortColumnHeader">New Value</asp:LinkButton><asp:Image ID="Image1" imageurl="images/asc.png" runat="server" visible="false"/></td>                       
                        <td class="newtblheader">
                        <asp:LinkButton ID="LinkButton2" runat="server" class="sortcol" tooltip="Sort by Modified Date" OnClick="sortColumnHeader">Modified Date</asp:LinkButton><asp:Image ID="Image2" imageurl="images/asc.png" runat="server" visible="false"/></td>                       
                        <td class="newtblheader">
                        <asp:LinkButton ID="LinkButton3" runat="server" class="sortcol" tooltip="Sort by Modified By" OnClick="sortColumnHeader">Modified BY</asp:LinkButton><asp:Image ID="Image3" imageurl="images/asc.png" runat="server" visible="false"/></td>                                               
                        <td class="newtblheader">
                        <asp:LinkButton ID="LinkButton4" runat="server" class="sortcol" tooltip="Sort by IP Address" OnClick="sortColumnHeader">IP Address</asp:LinkButton><asp:Image ID="Image4" imageurl="images/asc.png" runat="server" visible="false"/></td>                                               
                        
                    </tr>            
                    
    <asp:Repeater ID="Repeater1" visible="true" runat="server" >

            <HeaderTemplate>
                
            </HeaderTemplate>
            <ItemTemplate>                
                   <tr>
                        <%--<td><asp:CheckBox ID="cbxDelete" runat="server" /></td>    --%>
                        
                        <td style="padding-left:2px"><asp:Label ID="lRecordId" runat="server" TExt='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "RecordId"))%>' ></asp:Label></td>
                        <td style="padding-left:2px"><asp:Literal ID="lColumnName" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ColumnName"))%>'></asp:Literal></td>
                        <td style="padding-left:2px"><asp:Literal ID="lOldValue" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "OldValue"))%>'></asp:Literal></td>
                        <td style="padding-left:2px"><asp:Literal ID="lNewValue" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "NewValue"))%>'></asp:Literal></td>
                        <td style="padding-left:2px"><asp:Literal ID="lModifiedDate" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ModifiedDate"))%>'></asp:Literal></td>
                        <td style="padding-left:2px"><asp:Literal ID="lModBy" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "username"))%>'></asp:Literal></td>
                        <td style="padding-left:2px"><asp:Literal ID="lGroupAccessId" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "IPAddress"))%>'></asp:Literal>                        
                        </td>
                    </tr>                                    
                    
                    <tr>
                        
                        <td colspan="7" class="tbldashed"></td>
                    </tr>
            </ItemTemplate>
            <FooterTemplate>
                                           
            </FooterTemplate>
        </asp:Repeater>
                    <tr>
                            <td colspan="7" class="dashremover"></td>
                    </tr>
        </table>      
        <%--<table width="100%">
        <tr>
                        
                        <td align="center">
                            <asp:Label ID="lmsg" runat="server" Text="" CssClass="msg_red" Visible="false"></asp:Label></td>
                    </tr>
                    </table>--%>
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
</asp:Content>