<%@ Page Title="Home" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false"
    CodeBehind="home.aspx.vb" Inherits="dms._home" ValidateRequest="false" EnableEventValidation="true" %>
<%@ MasterType VirtualPath="~/Site.Master"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<%@ Register src="userControlApproval.ascx" tagname="ucApproval" tagprefix="uc" %>
<%@ Register src="userControlCarbonCopy.ascx" tagname="ucCarbonCopy" tagprefix="uc" %>
<%@ Register src="userControlConfidential.ascx" tagname="ucConfidential" tagprefix="uc" %>
<%@ Register src="UserControlBookMark.ascx" tagname="UserControlBk" tagprefix="uc" %>    
<%@ Register src="UserControlUpload.ascx" tagname="UserControlDocumentUpload" tagprefix="uc" %>    
<%@ Register src="UserControlPendingList.ascx" tagname="UserControlPendingList" tagprefix="uc" %>
<%@ Register src="UserControlDocCount.ascx" tagname="UserControlDocCount" tagprefix="uc" %>    
<%@ Register src="ucButton.ascx" tagname="ucButton" tagprefix="uc" %>    
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Home</title>
</asp:Content>
<asp:Content ID="mContent" runat="server" ContentPlaceHolderID="menuContent">   
    <uc:ucButton id="ucAddDoc" runat="server" pText="Upload Document" pImage="images/upload2.png"></uc:ucButton>    
    <uc:UserControlBk id="ucb" runat="server"></uc:UserControlBk>
    
    <br />
</asp:content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="MainHeaderContent">
    <table width="100%">
<tr>
<td><img alt="" src="images/docstat.png" height="32px" width="32px" style="vertical-align:middle;display:none" />&nbsp;Welcome to DMS!
</td></tr>
</table>
                            
                            </asp:Content>
                            
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
       
    <%--<div id="mainDiv3">--%>
      <%--<asp:UpdatePanel ID="pnlRepeater" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    <asp:Panel id="pRepeater" runat="server">  
    <table  cellspacing="0" cellpadding="0" border="0" width="100%">
    <tr>
        <td valign="top">
            <div style="width:100%;">                
                <div id="tblgrid" style="margin-top:2px;margin-bottom:2px;">                             --%>
                <div align="left" style="padding-left:15px;padding-right:15px">
                    <uc:ucConfidential runat="server" id="uConfidential"></uc:ucConfidential>              
                    </div>
                <div align="left" style="padding-left:15px;padding-right:15px">
                    <uc:ucApproval runat="server" id="uapproval"></uc:ucApproval>              
                    </div>
                    <div align="left" style="padding-left:15px;padding-right:15px">
                    <uc:ucCarbonCopy runat="server" id="uCarbonCopy"></uc:ucCarbonCopy>              
                    </div>
                    <div align="left" style="padding-left:15px;padding-right:15px">
                    <uc:UserControlPendingList id="ucPending" runat="server"></uc:UserControlPendingList>
                    </div>
                    <div align="left" style="padding-left:15px;padding-right:15px;margin-top:20px;">
                    <uc:UserControlDocCount runat="server" id="ucDocCount"></uc:UserControlDocCount>              
                    </div>
                    <%--<div align="left" style="margin-top:5px;padding-left:19px;padding-right:15px">
                    <asp:LinkButton ID="lbUpload" runat="server" class="menu">Upload Document</asp:LinkButton>
                    </div>--%>
                    <div>&nbsp;
                    </div>
                    
                <%--</div>    
            </div>
        </td>     
    </tr>
    </table>


        </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>--%>
    <%--</div>--%>
        
    
</asp:Content>
<asp:Content ID="cntmid" runat="server" ContentPlaceHolderID="MainFooterContent">
</asp:Content>
<asp:Content ID="cntUpload" runat="server" ContentPlaceHolderID="cntntUpload">   
    <uc:UserControlDocumentUpload runat="server" id="ucUpload" visible="False"/>
</asp:Content>