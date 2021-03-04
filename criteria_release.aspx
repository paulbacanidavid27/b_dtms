<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="criteria_release.aspx.vb" Inherits="dms.criteria_release" EnableEventValidation="true" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ MasterType VirtualPath="~/Site.Master"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register src="UserControlDocumentIndex.ascx" tagname="UserControlDocumentIndex" tagprefix="uc" %>    
<%@ Register src="UserControlReport.ascx" tagname="UserControlReport" tagprefix="uc" %>    
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">    
        <script src="Scripts/print_report.js" type="text/javascript"></script>
        <title>Total Amount Released Per Bureau - Report Criteria</title>
</asp:Content>

<%--menu content start--%>
<asp:Content ID="Content6" runat="server" ContentPlaceHolderID="menuContent">    
    <uc:UserControlReport id="ucReport" runat="server"></uc:UserControlReport>       
</asp:Content>
<%--main headr content start--%>
<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="MainHeaderContent">
   
            <table border="0" width="100%" class="tblHdr_1">
      <tr>
                            <td><asp:Image ID="Image8" runat="server" width="20px" Height="20px" imageurl="images/upload.png" />&nbsp;Total Amount Released Per Bureau</td>
                         </tr>
    </table>    
         
</asp:Content>
<%--main headr content end--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mainDiv_" width="100%">
       
        
      <asp:UpdatePanel ID="pnlRepeater" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel id="pRepeater" runat="server" DefaultButton="btSearch">  
    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="tblHdr2_">
    <tr>
        <td valign="top">
            <div style="width:100%;">
                
                <asp:Panel ID="idAdvSrch" runat="server" Visible="false" style="height:auto;width:100%">
                    <table width="100%" cellspacing="0" cellpadding="0" border="0">
                         <tr>
                            <td align="left">
                                
                                <table border="0"  cellspacing="1" cellpadding="3" width="100%" style="border:solid 1px #D4D4D4;">
                                    <tr>
                                        
                                        <td align="left" style="padding-left:10;font-style:italic;font-size:9pt;background-color:#CCCCCC" colspan="4" >
                                            Report Criteria
                                        </td>                                        
                                    </tr>                                    
                                     <tr>
                                     <td align="right" class="labelFreeForm"  style="padding-left:10">
                                                Bureau:
                                        </td>
                                        <td align="left">
                                                    <asp:DropDownList ID="dlOfficeCode" runat="server" cssclass="entryfld2" Width="210px" AutoPostBack="false">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="RIGHT"class="labelFreeForm"  style="padding-left:10">
                                        Document Type:
                                            
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="lbDocType" runat="server" cssclass="entryfld2" AutoPostBack="true">
                                            </asp:DropDownList>
                                                                                      
                                        </td>                                       
                                        
                                    </tr>
                                    <tr>
                                        <td align="right"   class="labelFreeForm"  style="padding-left:10">
                                            Issued Date Range:
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="tbDCFrom" runat="server" cssclass="entryfld"  Width="67px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="tbDCFrom" />

                                            -
                                            <asp:TextBox ID="tbDCTo" runat="server" cssclass="entryfld"  Width="67px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="tbDCTo"/>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                    <td colspan="4" align="right"><asp:Button ID="btSearch" runat="server" CssClass="btn" Text="Preview" style="margin:3px 3px 3px 3px"/></td>
                                    </tr>
                                    <tr>
                                    <td colspan="4" align="left">
                                        <asp:LinkButton ID="lbShow" style="color:#222222;padding-left:10px; text-decoration:underline;" runat="server">Show Documents with empty or invalid amount</asp:LinkButton></td>
                                    </tr>
                                </table>
                            </td>
                         </tr>
                         <tr>
                            <td >
                            
                                                    
                            </td>
                         </tr>
                         <tr>
                            <td style="text-align:CENTER;">
                                <asp:Label ID="msg" runat="server" Text="" CssClass="msg_red"></asp:Label></td>
                         </tr>
                    </table>        
                </asp:Panel>
                              
                             
                
            </div>
        </td>        
    </tr>
    </table>
    
   
        </asp:Panel>
    
    </div>
</asp:Content>
