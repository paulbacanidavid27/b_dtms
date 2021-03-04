<%@ Page Title="Home" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="dashboard.aspx.vb" Inherits="dms.dashboard"  EnableEventValidation="true" %>
<%@ MasterType VirtualPath="~/Site.Master"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register src="ucHr.ascx" tagname="ucHr" tagprefix="uc1" %>
<%@ Register src="ucDashBoard.ascx" tagname="UserControlDashBoard" tagprefix="uc" %>
<%@ Register src="ucDashBoardCount.ascx" tagname="UserControlDashBoardCount" tagprefix="uc" %>
<%--pager: step 1--%>
<%@ Register src="UserControlPager.ascx" tagname="UserControlPager" tagprefix="uc2" %>
<%@ Register src="UserControlUpload.ascx" tagname="UserControlDocumentUpload" tagprefix="uc" %>    
<%@ Register src="ucButton.ascx" tagname="ucButton" tagprefix="uc" %>    

<asp:Content ID="cntUpload" runat="server" ContentPlaceHolderID="cntntUpload">   
    <uc:UserControlDocumentUpload runat="server" id="ucUpload" visible="False"/>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Dashboard</title>
</asp:Content>

<asp:Content ID="Content_1" ContentPlaceHolderID="MenuContent" runat="server">
    <uc:ucButton id="ucAddDoc" runat="server" pText="Upload Document" pImage="images/upload2.png"></uc:ucButton>    
    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
    <ContentTemplate>
        <uc:UserControlDashBoardCount id="ucDBCount" runat="server" ></uc:UserControlDashBoardCount>
    </ContentTemplate>
    </asp:UpdatePanel>
    
    <div style="display:none;border-radius:5px;border-style: solid; border-width: 1px; border-color: #F1F4F8 #CFDBE7 #81A0C0 #CEDAE8; background-color: #FFFFFF; width: 98%; margin-top: 8px; margin-left: 1px">
                            <asp:UpdatePanel ID="pnlFilter" runat="server" UpdateMode="Conditional" Visible="false">
                                   <ContentTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="newtblheader2" >
                                        <tr height="25px">
                                            <td align="left"  class="tableHead27" style="padding-left:3px">
                                                <img alt="" width="24px" Height="20px" src="images/find.png" />&nbsp;&nbsp;<asp:Label ID="lbBookMrk" runat="server" style="color:#EEEEEE;font-family:Arial;font-size:10pt;font-weight:bold;font-style:normal;color:#CCCCCC">Filter Activities</asp:Label></td>
                                                <td width="50px" align="right" valign="top" class="tableHead27" >                                                    
                                                    <asp:ImageButton ID="imgBk" runat="server" imageurl="images/showpanel.png"/>
                                                    </td>
                                        </tr>
                                    </table>
                    
                                <asp:Panel runat="server" ID="pFilter" Visible="false" DefaultButton="btSearch"> 
                                <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                        <tr>
                                            <td align="left" class="labelFreeForm" style="padding-left:5px">Activity:</td>
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
                                                    <asp:ListItem Value="Notes">Notes</asp:ListItem>
                                                    <asp:ListItem Value="Routing">Routing</asp:ListItem>
                                                    <asp:ListItem Value="Tag">Tag</asp:ListItem>
                                                    <asp:ListItem Value="Upload">Upload</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>            
                                            <td align="left">&nbsp;</td>            
                                        </tr>                                          
                                        <tr>
                                            <td align="left"  class="labelFreeForm"  style="padding-left:5px">
                                                Activity Date:</td>
                                            <td align="left">
                                                <asp:TextBox ID="txDateActivity" runat="server"  MaxLength="100" Width="55px"  cssclass="entryfldw"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txDateActivity" />
                                            </td>            
                                            <td align="left">&nbsp;</td>            
                                        </tr>  
                                        <tr>
                                            <td align="left">&nbsp;</td>
                                            <td align="right">
                                                <asp:Button ID="btSearch" runat="server" CssClass="btnsmall" Text="Filter" />
                                            </td>            
                                            <td align="left">&nbsp;</td>            
                                        </tr>                            
                                    </table>
                                </asp:Panel>
                                </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <br />
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="cpPeriod">
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
<tr>
<td>
<asp:UpdatePanel runat="server" ID="pnlGroup" UpdateMode="Conditional">
<ContentTemplate>
<table>
<tr>
<td>
    <asp:HiddenField ID="hfCount" runat="server" value="5" />
    <asp:HiddenField ID="hfIndex" runat="server" value="1" />
    <asp:HiddenField ID="hfOfcCode" runat="server" value="" />    
    <asp:ImageButton ID="imgLeft" runat="server" visible="false" ImageUrl="images/larrow_h.png" />
</td>
<td>
    
    <div id="divGrpAll" runat="server" style="padding:2px;border-radius:4px;text-align:center; padding-top:5px; vertical-align:middle; height:20px; width:60px; white-space:nowrap; overflow:hidden; text-overflow:ellipsis; border:solid 1px #C0C0C0; background-color: #26269B; margin-right:5px;color:#FFFFFF;">
    
    <asp:LinkButton ID="lbGroupAll" runat="server" Text='All' style="color: inherit"></asp:LinkButton>       
    
</div>
</td>

<asp:Repeater ID="rptGroups" runat="server">
<HeaderTemplate>
</HeaderTemplate>
<ItemTemplate>
<td>
<div runat="server" id="divGrp" style="padding:2px;border-radius:4px;text-align:center; padding-top:5px; vertical-align:middle; height:20px; width:60px; white-space:nowrap; overflow:hidden; text-overflow:ellipsis; border:solid 1px #C0C0C0; background-color: transparent; margin-right:5px;color:#222222;">

    <asp:LinkButton ID="lbGroup" runat="server" onClick="fSelect" style="color: inherit" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Description"))%>' ToolTip='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Description"))%>'></asp:LinkButton>       
    <asp:Literal ID="lOfcCode" runat="server" Visible="false" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "OfficeCode"))%>'></asp:Literal>
    
</div>
</td>
</ItemTemplate>
<FooterTemplate>
</FooterTemplate>
</asp:Repeater>

<td>
<asp:ImageButton ID="imgRight" runat="server"  visible="false" ImageUrl="images/rarrow_h.png"/>
</td>
</tr>
</table>
</ContentTemplate>
    </asp:UpdatePanel>
</td>
            <td align="Right" style="font-weight:bold">
            <asp:UpdatePanel ID="pnlSearch" runat="server" UpdateMode="Conditional" Visible="true">
                <ContentTemplate>
                <asp:Panel ID="pSrch" runat="server" DefaultButton="imgSearch">
                Period Covered: 
                <asp:TextBox ID="tbDCFrom" runat="server" cssclass="entryfld"  Width="67px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="tbDCFrom" />

                                            -
                                            <asp:TextBox ID="tbDCTo" runat="server" cssclass="entryfld"  Width="67px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="tbDCTo"/>
                <asp:ImageButton ID="imgSearch" runat="server" ImageUrl="images/find.png" />
                </asp:Panel>
                <asp:ImageButton ID="imgPrint" runat="server" width="25px" Height="25px" imageurl="images/print.png"/>
                <asp:ImageButton ID="imgASearch" runat="server" width="22px" Height="22px" ImageUrl="images/asearch.png" />
                <cc1:HoverMenuExtender ID="hme2" runat="Server"
    TargetControlID="imgASearch"
    PopupControlID="pAdvancedSearch"
    HoverCssClass="popupHover"
    PopupPosition="Bottom"   
    OffsetX="0"
    OffsetY="0"
    PopDelay="50" />
    <%--<cc1:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtDueDate" />--%>
                </ContentTemplate>
                <Triggers>
                <asp:PostBackTrigger ControlID="imgPrint" /></Triggers>
                </asp:UpdatePanel>
            </td>
            </tr>
            </table>
            <asp:Panel style="padding:5px;border-radius:3px;border:solid 1px #CCCCCC;background-color:White;text-align:left;box-shadow:2px 2px 2px;" ID="pAdvancedSearch" 
        runat="server">
            <table border="0" cellpadding="3" cellspacing="0" style="border-collapse:collapse">
                <tr><td colspan="2"><b>Additional Filter</b></td></tr>
                <tr><td>Last Action</td><td><asp:DropDownList ID="dlStatus" cssclass="entryfld2"  runat="server" Width="250px">
                                            </asp:DropDownList></td></tr>
                <tr><td>Due Date: </td>
                <td><asp:TextBox ID="txtDueDate" runat="server" cssclass="entryfld"  Width="120px"></asp:TextBox>
                </td>
                </tr>            
                <tr><td>Reference No: </td>
                <td><asp:TextBox ID="txtRefNo" runat="server" cssclass="entryfld"  Width="220px"></asp:TextBox>
                </td>
                </tr>
                
                <tr><td>Subject: </td>
                <td><asp:TextBox ID="txtSubject" runat="server" cssclass="entryfld"  Width="220px"></asp:TextBox></td>
                </tr>
                <tr id="trPersonnel" runat="server" visible="true"><td>Personnel In-Charge:&nbsp;</td>
                <td><asp:TextBox ID="txtPersonnelInCharge" runat="server" cssclass="entryfld"  Width="220px"></asp:TextBox></td>
                </tr> 
                <tr><td></td><td>
                    <asp:Button ID="btFilter" runat="server"  CssClass="btnsmall" Text="Filter" /></td></tr>
            </table>
        </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="MainHeaderContent">
<asp:UpdatePanel ID="upPgr" runat="server" UpdateMode="Conditional">
                                   <ContentTemplate>
    <table width="100%">
<tr>
            <td align="left">List of Request - 
                <asp:Literal ID="lHdrTitle" runat="server" Text="All"></asp:Literal></td>
            <td align="right">
                <%--pager: step 2--%>
                <asp:HiddenField ID="hfCurrent" runat="server" Value="1"/>
                <asp:HiddenField ID="hfAction" runat="server" Value=""/>
                <asp:HiddenField ID="hfSortCol" runat="server" Value="Received"/>
                <asp:HiddenField ID="hfSortOrder" runat="server" Value="Asc"/>
                <asp:HiddenField ID="hfTotalRows" runat="server" Value="0"/>
                
                <uc2:UserControlPager ID="ucPager" runat="server" />
                
            </td>
        </tr>
        </table>
        </ContentTemplate>
        </asp:UpdatePanel>
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
