﻿<%@ Page Title="Manner of Receipt" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false"
    CodeBehind="DocReceipt.aspx.vb" Inherits="dms.DocReceipt" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register src="ucHr.ascx" tagname="ucHr" tagprefix="uc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register src="UserControlCheckBox.ascx" tagname="UserControlCheckBox" tagprefix="uc2" %>
<%@ Register src="UserControlPager.ascx" tagname="UserControlPager" tagprefix="uc" %>
<%@ Register src="UserControlDocAccess.ascx" tagname="UserControlDocAccess" tagprefix="uc3" %>
<%@ Register src="UserControlAdminMenuH.ascx" tagname="UserControlAdminMenuH" tagprefix="uc" %>    
<%@ Register src="ucButton.ascx" tagname="ucButton" tagprefix="uc" %>    
<%--menu content start--%>

<%--main content end--%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Manner of Receipt Maintenance</title>
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
                        <td valign="middle"><div class="notes2">&nbsp;<asp:Literal ID="lRecordCount" visible="false" runat="server"></asp:Literal></div>
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
            <td class="hdrtitle_1" > <img  alt="" src="images/docmanner.png" style="vertical-align:middle" Width="20px" height="20px"/>&nbsp;Manner of Receipt</td>
            <td align="right">
            <asp:UpdatePanel ID="pPager" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                <asp:HiddenField ID="hfCurrent" runat="server" Value="1"/>
                                            <asp:HiddenField ID="hfTotalRows" runat="server" Value="0"/>
                                            <asp:HiddenField ID="hfSortCol" runat="server" Value=""/>
                                            <asp:HiddenField ID="hfSortOrder" runat="server" Value="Asc"/>
                                            <uc:UserControlPager ID="ucPager" runat="server" />
                                </ContentTemplate>
                                </asp:UpdatePanel>
            </td>
        </tr>    
             </table>                            
</asp:Content>
<asp:Content ID="mContent" runat="server" ContentPlaceHolderID="menuContent">
                            
                            
                            <uc:ucButton id="ucAdd" runat="server" pText="Add Manner of Receipt" pImage="images/group2.png"></uc:ucButton>
                            <asp:UpdatePanel ID="pnlFilter" runat="server" UpdateMode="Conditional">
                                   <ContentTemplate>
                                   <div style="border-radius:5px;border-style: solid; border-width: 1px; border-color: #F1F4F8 #CFDBE7 #81A0C0 #CEDAE8; background-color: #FFFFFF; width: 98%; margin-top: 8px; margin-left: 1px">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="tableheaderGreen" >
                                        <tr height="25px">
                                            <td align="left"  class="tableHead27" style="padding-left:3px">
                                                <img alt="" width="24px" Height="20px" src="images/find.png" />&nbsp;&nbsp;<asp:Label ID="lbUser" runat="server" style="color:#EEEEEE;font-family:Arial;font-size:10pt;font-weight:bold;font-style:normal;color:#CCCCCC">Filter Status Groups</asp:Label></td>
                                                <td width="50px" align="right" valign="top" class="tableHead27" >
                                                    <asp:ImageButton ID="imgUser" runat="server" imageurl="images/showpanel.png"/></td>
                                        </tr>
                                    </table>
                    
                                <asp:Panel runat="server" ID="pFilter" Visible="false" DefaultButton="btSearch"> 
                                <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                        <tr>
                                <td align="left" class="labelFreeForm">
                                    Description:</td>
                                <td align="left">
                                    <asp:TextBox ID="tbSearchDesc"  MaxLength="3" CssClass="entryfld" runat="server"></asp:TextBox>
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
    
         
</asp:content>
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
                            <asp:LinkButton ID="lbSort1" runat="server" class="sortcol" tooltip="Sort by Manner of Receipt" OnClick="sortColumnHeader">Manner of Receipt</asp:LinkButton>
                            <asp:Image ID="imgSort1" imageurl="images/asc.png" runat="server" visible="true"/>
                        </td>                        
                        <td class="newtblheader" style="width:20px" align="center">
                            <asp:ImageButton ID="imgDelete" runat="server" imageurl="images/del.png"/>
                        </td>

                    </tr>            
            <asp:Repeater ID="Repeater1" visible="true" runat="server" >
            <HeaderTemplate>                
            </HeaderTemplate>
            <ItemTemplate>                
                    <tr>
                            <td align="center" style="width:40px">                    
                                <asp:ImageButton ID="imgUpdate" runat="server" imageurl="images/update.png" Width="15px" Height="15px"/>
                            </td>                    
                            <td>
                                <asp:ImageButton ID="imgPlus" runat="server" ImageUrl="images/plus.jpg" Visible="false"/>
                                <asp:ImageButton ID="imgMinus" runat="server" ImageUrl="images/minus.jpg" Visible="false" />
                                <asp:Literal ID="lReceiptDesc" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ReceiptDesc"))%>'></asp:Literal>                            
                                <asp:Literal ID="lReceiptID" visible="false" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ReceiptId"))%>'></asp:Literal>
                            </td>                            
                            <td  align="center">
                                <asp:CheckBox ID="cbxDelete" runat="server" />                            
                            </td>    
                    </tr>                                                        
                    <tr>
                        <td colspan="3" class="tbldashed"></td>
                    </tr>
            </ItemTemplate>
            <FooterTemplate>
                    <tr>
                        <td style="border-top:solid 1px #ffffff" colspan="3"></td>
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

        <asp:UpdatePanel ID="pnlAddReceipt" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel id="pAddReceipt" runat="server" Visible="false" Width="850px"  DefaultButton="btSave">
                    <!-- start - search criteria //-->
                    <center>
                        <table border="0" class="popuphdrbox" cellspacing="0" cellpadding="0" style="border: solid 1px #3A5671;border-collapse:collapse;width:100%">
                            <tr>
                                <td align="center">
                                    <table cellspacing="0" class="popuphdr" cellpadding="0" border="0" style="width:100%">
                                        <tr height="30px">
                                            <td align="left" valign="middle" colspan="2">
                                                &nbsp;<img height="25px" width="20px" src="images/group.png" />&nbsp;Manner of Receipt - 
                                                <asp:Literal ID="lAction" runat="server"></asp:Literal>
                                            </td>
                                            <td  align="right" valign="top">
                                                <asp:ImageButton ID="imgClose" runat="server" imageurl="images/close_window.gif" onmouseover="this.src='images/close_window.gif'"  onmouseout="this.src='images/close_window.gif'" width="18px" Height="18px"/>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-left:15px" valign="top">
                                    <fieldset class="notes" style="margin-right:15px;margin-top:0px">
                                        <legend >* - Required Field</legend>
                                        <table width="100%" border="0">
                                            <tr>
                                                <td align="left">
                                                    * Manner of Receipt:</td>
                                                <td align="left">
                                                    <asp:Literal ID="ltReceiptId" runat="server" Visible="false"></asp:Literal>
                                                    <asp:TextBox ID="tbDesc" class="entryfld" MaxLength="100" runat="server"></asp:TextBox>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td align="left" valign="top">
                                                    </td>
                                                <td align="left" valign="top">
                                                    
                                                </td>
                                                <td align="left" valign="top">
                                                    
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <table>
                                        <tr>
                                            <td>
                            &nbsp;<asp:Button ID="btSave" runat="server" CssClass="btn" Text="Save" />
                                                &nbsp;
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
                <cc1:DropShadowExtender ID="dse3" runat="server" TargetControlID="pAddReceipt" Opacity=".5" Rounded="false" TrackPosition="False"  />
            </ContentTemplate>
        </asp:UpdatePanel>
    
    <!-- start - resultset delete//-->                                                                
        <asp:UpdatePanel ID="pnlDeleteReceipt" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel id="pDeleteReceipt" runat="server" visible="false" width="800px">
                    <table border="0" class="popuphdrbox" cellspacing="0" cellpadding="0" style="border: solid 1px #3A5671;border-collapse:collapse;width:100%">
                        <tr>
                            <td align="center">
                                <table cellspacing="0" class="popuphdr" cellpadding="0" border="0" style="width:100%">
                                    <tr height="30px">
                                        <td align="left" valign="middle" colspan="2">
                                            &nbsp;<img height="25px" width="20px" src="images/group.png" />&nbsp;Manner of Receipt - Delete</td>
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
                                            <tr>
                                                <td style="background-color:Gray;color:White;padding-left:2px;" colspan="2">
                                                    Notes: 
                                                    <br />
                                                    1. Only the records with green highlight will be deleted. Refer to the comments column for more info. 
                                                    <br />
                                                    2. Click on Delete button to confirm deleting of selected records.</td>
                                            </tr>
                                            <tr>
                                                <td class="newtblheader">
                                                    Manner of Receipt</td><td class="newtblheader">Comments</td>
                                                
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr  id="rw" runat="server" height="20px">
                                                <td class="tbldashed">
                                                    <asp:Literal ID="lDesc" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ReceiptDesc"))%>'></asp:Literal>
                                                    <asp:Literal ID="lID" runat="server" visible="false" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ReceiptId"))%>'></asp:Literal>
                                                </td>
                                                <td class="tbldashed">
                                                    <asp:Literal ID="lComment" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Comment"))%>'></asp:Literal>
                                                    
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <tr>
                                                <td style="border-top:solid 1px #ffffff" colspan="2">
                                                </td>
                                            </tr>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </table>
                                <table>
                                    <tr>
                                        <td colspan="5">
                                            <asp:Button ID="btDelete" runat="server" Text="Delete" cssclass="btn" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <cc1:DropShadowExtender ID="DropShadowExtender1" runat="server" TargetControlID="pDeleteReceipt" Opacity=".5" Rounded="false" TrackPosition="False"  />
            </ContentTemplate>
        </asp:UpdatePanel>
    <!-- end - resultset delete//-->  
        
    </asp:Content>
    


