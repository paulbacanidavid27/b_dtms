<%@ Page Title="Home" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false"
    CodeBehind="DashboardList.aspx.vb" Inherits="dms.DashboardList"  EnableEventValidation="true" %>
<%@ MasterType VirtualPath="~/Site.Master"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register src="ucHr.ascx" tagname="ucHr" tagprefix="uc1" %>
<%@ Register src="UserControlDashBoard.ascx" tagname="UserControlDashBoard" tagprefix="uc" %>
<%--pager: step 1--%>
<%@ Register src="UserControlPager.ascx" tagname="UserControlPager" tagprefix="uc2" %>
<%@ Register src="UserControlUpload.ascx" tagname="UserControlDocumentUpload" tagprefix="uc" %>    
<%@ Register src="ucButton.ascx" tagname="ucButton" tagprefix="uc" %>    

<asp:Content ID="cntUpload" runat="server" ContentPlaceHolderID="cntntUpload">   
    <uc:UserControlDocumentUpload runat="server" id="ucUpload" visible="False"/>
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Home</title>
</asp:Content>
<asp:Content ID="Content_1" ContentPlaceHolderID="MenuContent" runat="server">

    <uc:ucButton id="ucAddDoc" runat="server" pText="Upload Document" pImage="images/upload2.png"></uc:ucButton>    
    <div style="border-radius:5px;border-style: solid; border-width: 1px; border-color: #F1F4F8 #CFDBE7 #81A0C0 #CEDAE8; background-color: #FFFFFF; width: 98%; margin-top: 8px; margin-left: 1px">
                                <asp:UpdatePanel ID="pnlFilter" runat="server" UpdateMode="Conditional">
                                   <ContentTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="newtblheader2" >
                                        <tr height="25px">
                                            <td align="left"  class="tableHead27" style="padding-left:3px">
                                                <img alt="" width="24px" Height="20px" src="images/find.png" />&nbsp;&nbsp;<asp:Label ID="lbBookMrk" runat="server" style="color:#EEEEEE;font-family:Arial;font-size:10pt;font-weight:bold;font-style:normal;color:#CCCCCC">Filter Activities</asp:Label></td>
                                                <td width="50px" align="right" valign="top" class="tableHead27" >
                                                    <asp:ImageButton ID="imgBk" runat="server" imageurl="images/showpanel.png"/></td>
                                        </tr>
                                    </table>
                    
                                <asp:Panel runat="server" ID="pFilter" Visible="false" DefaultButton="btSearch"> 
                                <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                        <tr>
                                            <td align="left" class="labelFreeForm" style="padding-left:5px">
                                                Activity:</td>
                                            <td align="left">                                    
                                                <asp:DropDownList ID="dlActivities" runat="server" cssclass="entryfld2w" Width="250px">
                                                    <asp:ListItem Value="">All</asp:ListItem>
                                                    <asp:ListItem Value="Checkin">Checkin</asp:ListItem>
                                                    <asp:ListItem Value="Checkout">Checkout</asp:ListItem>
                                                    <asp:ListItem Value="Download">Download</asp:ListItem>
                                                    <asp:ListItem Value="Edit">Edit</asp:ListItem>
                                                    <asp:ListItem Value="Email">Email</asp:ListItem>
                                                    <asp:ListItem Value="Link">Link</asp:ListItem>
                                                    <asp:ListItem Value="System">Login</asp:ListItem>
                                                    <asp:ListItem Value="Logout">Logout</asp:ListItem>
                                                    <asp:ListItem Value="Notes">Notes</asp:ListItem>
                                                    <asp:ListItem Value="Routing">Routing</asp:ListItem>
                                                    <asp:ListItem Value="Tag">Tag</asp:ListItem>
                                                    <asp:ListItem Value="Upload">Upload</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
            
                                            <td align="left">
                                                &nbsp;</td>
            
                                        </tr>                                          
                                        <tr>
                                            <td align="left"  class="labelFreeForm"  style="padding-left:5px">
                                                Activity Date:</td>
                                            <td align="left">
                                                <asp:TextBox ID="txDateActivity" runat="server"  MaxLength="100" Width="65px"  cssclass="entryfldw"></asp:TextBox>-
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txDateActivity" />
                                                <asp:TextBox ID="txDateActivityTo" runat="server"  MaxLength="100" Width="65px"  cssclass="entryfldw"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txDateActivityTo" />
                                            </td>
            
                                            <td align="left">
                                                &nbsp;</td>
            
                                        </tr>  
                                        <tr>
                                            <td align="left"  class="labelFreeForm"  style="padding-left:5px">
                                            <asp:Literal ID="lAll" runat="server" visible="false" Text="Select User:"></asp:Literal>
                                                </td>
                                            <td align="left">
                                                <asp:CheckBox ID="cbAll" runat="server" visible="false"  />
                                                <asp:DropDownList ID="dlUser" runat="server" Visible="false" width="300px">
                                                </asp:DropDownList>
                                            </td>
            
                                            <td align="left">
                                                </td>
            
                                        </tr>  
                                        <tr>
                                            <td align="left"  class="labelFreeForm"  style="padding-left:5px">
                                            <asp:Literal ID="lIP" runat="server" visible="false" Text="IP Address:"></asp:Literal>
                                                </td>
                                            <td align="left">
                                                <asp:TextBox ID="tbIPAdd" runat="server"  MaxLength="100" Width="300px" visible="false" cssclass="entryfldw"></asp:TextBox>
                                            </td>
            
                                            <td align="left">
                                                </td>
            
                                        </tr>
                                        <tr>
                                        
                                        <td align="left" style="padding-left:10;font-style:italic;font-size:9pt;background-color:#CCCCCC" colspan="3" >
                                            Sort Option
                                        </td>                                        
                                    </tr>     
                                     <tr>
                                        <td class="labelFreeForm"  align="right">
                                            Column Sort:
                                            
                                        </td>
                                        <td>

                                            <asp:DropDownList ID="dlColumns" runat="server" Visible="true" AutoPostBack="false" 
                                                cssclass="entryfld2" Width="210px">
                                                <asp:ListItem Text="Activity Date" Value="t.actiondate" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Activity" Value="t.task" Selected="false"></asp:ListItem>
                                                <asp:ListItem Text="IP Address" Value="t.ipaddres" Selected="False"></asp:ListItem>
                                                <asp:ListItem Text="User" Value="uname" Selected="False"></asp:ListItem>                                                
                                            </asp:DropDownList>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="labelFreeForm"  align="right">
                                            Sort Order:
                                            
                                        </td>
                                        <td>

                                            <asp:DropDownList ID="dlSortOption" runat="server" Visible="true" AutoPostBack="false" 
                                                cssclass="entryfld2" Width="210px">
                                                <asp:ListItem Text="Ascending" Value="asc" Selected="False"></asp:ListItem>
                                                <asp:ListItem Text="Descending" Value="desc" Selected="True"></asp:ListItem>
                                                
                                            </asp:DropDownList>
                                        </td>
                                        <td></td>
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
                            <br />
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="MainHeaderContent">
    <table width="100%">
<tr>
            <td align="left">Recent Activities</td>
            <td align="right">
                <%--pager: step 2--%><asp:UpdatePanel ID="upPgr" runat="server" UpdateMode="Conditional">
                                   <ContentTemplate>
                <asp:HiddenField ID="hfCurrent" runat="server" Value="1"/>
                <asp:HiddenField ID="hfTotalRows" runat="server" Value="0"/>
                
                <uc2:UserControlPager ID="ucPager" runat="server" />
                </ContentTemplate>
        </asp:UpdatePanel>
            </td>
        </tr>
        </table>
        </asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
       
    <div id="mainDiv_" align="left">
    <asp:UpdatePanel ID="upDB" runat="server" UpdateMode="Conditional">
                                   <ContentTemplate>
        <uc:UserControlDashBoard id="ucDB" runat="server"></uc:UserControlDashBoard>       
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
        
    
</asp:Content>
