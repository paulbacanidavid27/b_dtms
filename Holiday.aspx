<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Holiday.aspx.vb" Inherits="dms.Holiday" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register src="UserControlHoliday.ascx" tagname="UserControlHoliday" tagprefix="uc" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register src="UserControlAddHoliday.ascx" tagname="UserControlAddHoliday" tagprefix="uc" %>    
<%@ Register src="UserControlAdminMenuH.ascx" tagname="UserControlAdminMenuH" tagprefix="uc" %>
<%@ Register src="ucButton.ascx" tagname="ucButton" tagprefix="uc" %>    
<%--menu content start--%>
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
<%--main content end--%>
<%--menu content start--%>
<asp:Content ID="mContent" runat="server" ContentPlaceHolderID="menuContent">
    <asp:ImageButton ID="imgAddHoliday" visible="false" runat="server" ImageUrl="images/button/addholiday.png" onmouseover="this.src='images/button/addholiday.png'"  onmouseout="this.src='images/button/addholiday.png'"  tooltip="Add Holiday"/>
    
    <uc:ucButton id="ucNewHoliday" runat="server" pText="Add New Holiday" pImage="images/holiday_icon.png"></uc:ucButton>    
    <uc:ucButton id="ucCopyHoliday" runat="server" pText="Copy Group Holiday" pImage="images/copydate.png"></uc:ucButton>
        
</asp:Content>
<%--menu content end--%>


<%--head content start--%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Holiday Maintenance</title>
</asp:Content>
<%--head content end--%>
<%--main headr content start--%>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="MainHeaderContent">
    <table cellpadding="0" cellspacing="3" border="0" width="100%" class="tableheaderGreen">
                   <tr >
                        <td valign="middle"> <img  alt="" width="20px" height="20px" src="images/holiday_icon.png" style="vertical-align:middle" />&nbsp;Holiday Maintenance</td><td align="right">
                        <asp:ImageButton ID="imgCopyHoliday" runat="server" width="22px" height="22px" visible="false" ImageUrl="images/copydate.png" ToolTip="Copy Holiday to Another Group"/></td>

            </tr>
     </table>
</asp:Content>
<%--main headr content end--%>



<%--main content start--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<uc:UserControlAddHoliday ID="ucAddHoliday" runat="server" visible="false"/>
    <uc:UserControlHoliday ID="ucHoliday" runat="server" />
</asp:Content>
<%--main content end--%>
<%--popupmenu content start--%>
<asp:Content ID="Content4" ContentPlaceHolderID="PopupMenu" runat="server">
    <asp:UpdatePanel ID="pnlCopy" runat="server" UpdateMode="Conditional">
    <ContentTemplate>  
    <asp:Panel id="pCopyGroup" runat="server" Visible="False" Width="500px" DefaultButton="btSave">
    <!-- start - search criteria //-->
    <center>
    
    
        <table border="0" class="popuphdrbox" cellspacing="0" cellpadding="0" style="border: solid 1px #3A5671;border-collapse:collapse;">

            <tr>
               <td align="center">
                  <table cellspacing="0" class="popuphdr" cellpadding="0" border="0" style="width:100%">
                        <tr height="30px">
                        <td align="left" valign="middle" colspan="2">&nbsp;<img height="25px" width="20px" src="images/copydate.png" />&nbsp;Holiday Group - Copy</td>
                                            
                        <td  align="right" valign="top">
                            <asp:ImageButton ID="imgClose" runat="server" imageurl="images/close_window.gif" onmouseover="this.src='images/close_window.gif'"  onmouseout="this.src='images/close_window.gif'" width="18px" Height="18px"/>
                        </td>
                    </tr>
                  </table>
               </td>
            </tr>
            <tr>
            <td style="padding-left:15px">
            <table>
            <tr>
                <td colspan="5" align="left">
                <p class="helpnotes" ><b>Notes:</b><br />
                                                                    1. Existing holidays will be deleted from the destination group before copying from the soure group.<br />                                                                    
                                                                    2. Different groups should be selected in order to copy holidays. <br />                                                                    
                                                                    
                                                                </p>                      
                    
                </td>
            </tr>
            <tr>
                <td colspan="5" align="left">
                    <hr />
                </td></tr>
            <tr>
                <td align="right" class="labelFreeForm">Enter Year</td><td colspan="4" align="left">
                   <asp:TextBox ID="txYear" runat="server" Width="30px" cssclass="entryfld2" MaxLength="4" style="margin-top:0px" tooltip="Enter the year you want to copy the holidays" ></asp:TextBox>
                   </td>
            </tr>
            <tr>
                <td align="right"  class="labelFreeForm">Copy From Group</td>
                <td align="left">
                    <asp:DropDownList ID="dlGroup1" runat="server" cssclass="entryfld2" Visible="True"></asp:DropDownList>
                </td>
                <td >&nbsp;</td>            
                <td align="right"  class="labelFreeForm">Copy To Group</td>
                <td align="left">
                    <asp:DropDownList ID="dlGroup2" runat="server" cssclass="entryfld2" Visible="True"></asp:DropDownList>
                </td>
            </tr>
            
                <tr>
                        
                        <td colspan="5" align="left">
                            &nbsp;<asp:Button ID="btSave" runat="server" CssClass="btn" Text="Copy" />&nbsp;<asp:Button ID="btClose" runat="server" CssClass="btn" Text="Close" />
                        </td>                        
                </tr>
                
                <tr>
                    <td align="left" colspan="5">
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
    <cc1:DropShadowExtender ID="dse2" runat="server" TargetControlID="pCopyGroup" Opacity=".5" Rounded="false" TrackPosition="True"  />
    </ContentTemplate>
    </asp:UpdatePanel>  
</asp:Content>
<%--popupmenu content start--%>
