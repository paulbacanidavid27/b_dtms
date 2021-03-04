<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="OfficeHoliday.aspx.vb" Inherits="dms.OfficeHoliday" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register src="UserControlOfficeHoliday.ascx" tagname="UserControlHoliday" tagprefix="uc" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register src="UserControlAddOfficeHoliday.ascx" tagname="UserControlAddHoliday" tagprefix="uc" %>    
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
    <%--<asp:ImageButton ID="imgAddHoliday" visible="false" runat="server" ImageUrl="images/button/addholiday.png" onmouseover="this.src='images/button/addholiday.png'"  onmouseout="this.src='images/button/addholiday.png'"  tooltip="Add Holiday"/>--%>
   <%-- <br /><asp:Button ID="btAddNewHoliday" runat="server" Text="Add Holiday" cssclass="btn2" Visible="true"/>   <br /><br />
    <asp:Button ID="btImportHoliday" runat="server" Text="Import Holiday" cssclass="btn2" Visible="true"/> <br /><br />
    <asp:Button ID="btCopyHoliday" runat="server" Text="Copy Holiday" cssclass="btn2" Visible="false"/>--%>
    <%--<uc:ucButton id="ucNewHoliday" runat="server" pText="Add New Holiday" pImage="images/holiday_icon.png"></uc:ucButton>    
    <uc:ucButton id="ucCopyHoliday" runat="server" pText="Copy Office Holiday" pImage="images/copydate.png"></uc:ucButton>--%>
    <asp:updatepanel id="pnlButtons" runat="server" updatemode="conditional">
        <ContentTemplate>

        
        <table>
            <tr>
    <td>
        <asp:ImageButton ID="imgAddHoliday" ClientIDMode="Static" style="vertical-align:bottom;height:20px;width:20px;" imageUrl="images/add.png" onmouseover="this.src='images/add.png';"  onmouseout="this.src='images/add.png';" Tooltip="Add Holiday" runat="server" Visible="True" />                                
    </td>
    <td ><asp:LinkButton ID="lbAddHoliday" runat="server" class="menu" OnClientClick="ShowProgress(this,'#imgAddHoliday')">Add Holiday</asp:LinkButton></td>
    </tr>
            <tr>
    <td>
        <asp:ImageButton ID="imgImportHoliday" ClientIDMode="Static" style="vertical-align:bottom;height:20px;width:20px;" imageUrl="images/import.png" onmouseover="this.src='images/import.png';"  onmouseout="this.src='images/import.png';" Tooltip="Add Holiday" runat="server" Visible="True" />                                
    </td>
    <td ><asp:LinkButton ID="lbImportHoliday" runat="server" class="menu" OnClientClick="ShowProgress(this,'#imgImportHoliday')">Copy Holiday</asp:LinkButton></td>
    </tr>
    <tr>
    <td>
        <asp:ImageButton ID="imgCpyHoliday" ClientIDMode="Static" style="vertical-align:bottom;height:20px;width:20px;" imageUrl="images/copydate.png" onmouseover="this.src='images/copydate.png';"  onmouseout="this.src='images/copydate.png';" Tooltip="Copy Holiday" runat="server" Visible="True" />                                
    </td>
    <td ><asp:LinkButton ID="lbCopy" runat="server" class="menu" OnClientClick="ShowProgress(this,'#imgCpyHoliday')">Import Holiday</asp:LinkButton></td>
    </tr>
    </table>
            </ContentTemplate>
        
    </asp:updatepanel>
</asp:Content>
<%--menu content end--%>


<%--head content start--%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>List of Holidays</title>
</asp:Content>
<%--head content end--%>
<%--main headr content start--%>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="MainHeaderContent">
    <table cellpadding="0" cellspacing="3" border="0" width="100%" class="tableheaderGreen">
                   <tr >
                        <td valign="middle"> <img  alt="" width="20px" height="20px" src="images/holiday_icon.png" style="vertical-align:middle" />&nbsp;List of Holidays</td><td align="right">
                        <asp:ImageButton ID="imgCopyHoliday" runat="server" width="22px" height="22px" visible="false" ImageUrl="images/copydate.png" ToolTip="Copy Holiday to Another Office"/></td>

            </tr>
     </table>
</asp:Content>
<%--main headr content end--%>



<%--main content start--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="pnlHoliday" runat="server" UpdateMode="Conditional">
<ContentTemplate>
    <uc:UserControlAddHoliday ID="ucAddHoliday" runat="server" visible="false"/>
    </ContentTemplate>
        
</asp:UpdatePanel>
    <uc:UserControlHoliday ID="ucHoliday" runat="server" />
</asp:Content>
<%--main content end--%>
<%--popupmenu content start--%>
<asp:Content ID="Content4" ContentPlaceHolderID="PopupMenu" runat="server">
    <asp:UpdatePanel ID="pnlCopy" runat="server" UpdateMode="Conditional">
    <ContentTemplate>  
    <asp:Panel id="pCopyOffice" runat="server" Visible="False" Width="500px" DefaultButton="btSave">
    <!-- start - search criteria //-->
    <center>  
    
        <table border="0" class="popuphdrbox" cellspacing="0" cellpadding="0" style="border: solid 1px #3A5671;border-collapse:collapse;">

            <tr>
               <td align="center">
                  <table cellspacing="0" class="popuphdr" cellpadding="0" border="0" style="width:100%">
                        <tr height="30px">
                        <td align="left" valign="middle" colspan="2">&nbsp;<img height="25px" width="20px" src="images/copydate.png" />&nbsp;Import Holiday</td>
                                            
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
                    1. Select a year where to copy the holidays. <br />
                    2. Select different source and destination office in order to copy holidays. <br />                                                                    
                    3. Existing holidays will be deleted from the destination office before copying from the source office.<br />                                                                    
                                                                    
                                                                    
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
                <td align="right"  class="labelFreeForm">Source Office</td>
                <td align="left" colspan="4">
                    <asp:DropDownList ID="dlOffice1" runat="server" cssclass="entryfld2" Visible="True" Width="350px"></asp:DropDownList>
                </td>
             </tr>
             <tr>        
                <td align="right"  class="labelFreeForm">Destination Office</td>
                <td align="left" colspan="4">
                    <asp:DropDownList ID="dlOffice2" runat="server" cssclass="entryfld2" Visible="True" Width="350px"></asp:DropDownList>
                </td>
            </tr>
            
                <tr>
                        
                        <td colspan="5" align="left" style="padding-top:20px">
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
    
    </ContentTemplate>
    </asp:UpdatePanel>  
    <asp:UpdatePanel ID="pnlImport" runat="server" UpdateMode="Conditional">
    <ContentTemplate>  
    <asp:Panel id="pImport" runat="server" Visible="False" Width="500px" DefaultButton="btSave">
    <!-- start - search criteria //-->
    <center>
    
    
        <table border="0" class="popuphdrbox" cellspacing="0" cellpadding="0" style="border: solid 1px #3A5671;border-collapse:collapse;">

            <tr>
               <td align="center">
                  <table cellspacing="0" class="popuphdr" cellpadding="0" border="0" style="width:100%">
                        <tr height="30px">
                        <td align="left" valign="middle" colspan="2">&nbsp;<img height="25px" width="20px" src="images/copydate.png" />&nbsp;Copy Holidays From Previous Year</td>
                                            
                        <td  align="right" valign="top">
                            <asp:ImageButton ID="imgCloseImport" runat="server" imageurl="images/close_window.gif" onmouseover="this.src='images/close_window.gif'"  onmouseout="this.src='images/close_window.gif'" width="18px" Height="18px"/>
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
                    1. Select different Copy From and Copy To year in order to copy holidays. <br />
                    2. Existing holidays will be deleted from the destination year before copying from the source year.<br />                                                                    
                                                                                                                                        
                                                                    
                                                                </p>                      
                    
                </td>
            </tr>
            <tr>
                <td colspan="5" align="left">
                    <hr />  
                </td></tr>
            <tr>
                <td align="left" class="labelFreeForm" width="125px">Copy From Year</td><td colspan="4" align="left">
                   <asp:TextBox ID="tbCopyFromYear" Readonly="True" runat="server" Width="40px" cssclass="entryfld2d" MaxLength="4" style="margin-top:0px;text-align:center" tooltip="Enter from what year you want to copy the holidays" ></asp:TextBox>
                   </td>
            </tr>
                <tr>
                <td align="left" class="labelFreeForm">Copy To Year</td><td colspan="4" align="left">
                   <asp:TextBox ID="tbCopyToYear" runat="server" Width="40px" cssclass="entryfld2" AutoPostBack="true" MaxLength="4" style="margin-top:0px;text-align:center" tooltip="Enter the year you want to copy the holidays" ></asp:TextBox>
                   </td>
            </tr>
            
            <tr><td colspan="5"><hr /></td></tr>
                <tr>
                        
                        <td colspan="5" align="left">

                            &nbsp;<asp:Button ID="btImportSave" runat="server" CssClass="btn" Text="Copy" />&nbsp;<asp:Button ID="btCloseImport" runat="server" CssClass="btn" Text="Close" />
                        </td>                        
                </tr>
                
                
                </table>
                </td>
                </tr>
                </table>
                
                </center>
    </asp:Panel>
    
    </ContentTemplate>
    </asp:UpdatePanel> 
</asp:Content>
<%--popupmenu content start--%>
