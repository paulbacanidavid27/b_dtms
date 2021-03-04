<%@ Page Title="Documents Uploaded Hourly Report" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="DocumentUploadedHourlyCriteria.aspx.vb" Inherits="dms.DocumentUploadedHourlyCriteria" EnableEventValidation="true" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ MasterType VirtualPath="~/Site.Master"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register src="UserControlDocumentIndex.ascx" tagname="UserControlDocumentIndex" tagprefix="uc" %>    
<%@ Register src="UserControlReport.ascx" tagname="UserControlReport" tagprefix="uc" %>    
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">    
        <script src="Scripts/print_report.js" type="text/javascript"></script>
        <title>Document Uploaded - Report Criteria</title>
</asp:Content>

<%--menu content start--%>
<asp:Content ID="Content6" runat="server" ContentPlaceHolderID="menuContent">    
    <uc:UserControlReport id="ucReport" runat="server"></uc:UserControlReport>       
</asp:Content>
<%--main headr content start--%>
<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="MainHeaderContent">
   
            <table border="0" width="100%" class="tblHdr_1">
      <tr>
                            <td>
                                <asp:ImageButton ID="imgIcon" width="20px" Height="20px" runat="server" imageurl="images/report_icon.png" />Documents Uploaded Hourly Report</td>
                            <td align="right">
                                <asp:ImageButton ID="imgDownload"  imageurl="images/download_doc.png" runat="server" />&nbsp;<asp:ImageButton ID="imgUpload" imageurl="images/upload2.png" runat="server" />
                                <asp:UpdatePanel ID="upnlUpload" runat="server" UpdateMode="Conditional"><ContentTemplate>
                                <asp:Panel ID="pnlUpload" runat="server" Visible="false">
                                    <asp:FileUpload ID="fUpload" runat="server"/><asp:Button ID="btUpload" runat="server" Text="Upload" cssclass="btnsmall" />
                                </asp:Panel>
                                </ContentTemplate>
                                <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="imgUpload" EventName="click"/>
                                <asp:PostBackTrigger ControlID="btUpload" />
                                </Triggers>
                                </asp:UpdatePanel>
                            </td>
                         </tr>
    </table>    
         
</asp:Content>
<%--main headr content end--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
       
        
      <asp:UpdatePanel ID="pnlRepeater" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    
    <asp:Panel id="pRepeater" runat="server" DefaultButton="btSearch">  
    
                
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
                                                Office:
                                        </td>
                                        <td align="left" >
                                                    <asp:DropDownList ID="dlOfficeCode" runat="server" cssclass="entryfld2" Width="350px" AutoPostBack="false">
                                            </asp:DropDownList>
                                            <asp:Label ID="lOfficeName" runat="server" Text="" Visible="false"></asp:Label>
                                        </td>
                                        <td colspan="2"></td>                                    
                                     <tr>
                                        <td align="RIGHT"class="labelFreeForm"  style="padding-left:10">
                                        Date Uploaded:
                                            
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="tbDate" runat="server" cssclass="entryfld"  Width="75px"></asp:TextBox> -
                                            <asp:TextBox ID="tbTDate" runat="server" cssclass="entryfld"  Width="75px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="calExt2" runat="server" TargetControlID="tbDate"/>                                            
                                            <cc1:CalendarExtender ID="calExt1" runat="server" TargetControlID="tbTDate"/>                                            
                                        </td>                                       
                                        
                                        
                                        
                                        <td align="RIGHT" width="150px"  class="labelFreeForm"  style="padding-left:10">
                                        Uploaded By:
                                            
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="tbAuthor" cssclass="entryfld" runat="server"></asp:TextBox>
                                            <cc1:autocompleteextender runat="server" ID="acomplete" TargetControlID="tbAuthor"
                                                 ServiceMethod="getUsers" ServicePath="getUser.asmx" CompletionInterval="800" EnableCaching="true"
                                                  MinimumPrefixLength="1" FirstRowSelected="false"
                                                 completionsetcount="25" />
                                        </td>                                       
                                        
                                    </tr>
                                    <tr>
                                     <td align="right"   class="labelFreeForm"  style="padding-left:10">
                                            Time Uploaded:
                                        </td>
                                        <td align="left">
                                            
                                            <asp:TextBox ID="txtStart" runat="server" width="60px" cssclass="entryfld" Text='07:00 AM' Enabled="true"></asp:TextBox>
                                            <cc1:MaskedEditExtender id="MeExt" runat="server" TargetControlID="txtStart"  AcceptAMPM="true" MaskType="Time" Mask="99:99" InputDirection="LeftToRight" AcceptNegative="None"/>
                                            -
                                            <asp:TextBox ID="txtEnd" runat="server" width="60px" cssclass="entryfld" Text='07:00 PM'   Enabled="true"></asp:TextBox>
                                                <cc1:MaskedEditExtender id="MaskedEditExtender1" runat="server" TargetControlID="txtEnd"  AcceptAMPM="true" MaskType="Time"  Mask="99:99" InputDirection="LeftToRight" AcceptNegative="None"/>
                                        </td>
                                    <td align="right" class="labelFreeForm"  style="padding-left:10">
                                                
                                        </td>
                                        <td align="left">
                                              <%--      <asp:DropDownList ID="dlOfficeCode" runat="server" cssclass="entryfld2" Width="210px" AutoPostBack="false">
                                            </asp:DropDownList>--%>
                                        </td>
                                        <td colspan="2"></td>
                                    </tr>
                                    <tr>
                                    <td colspan="4" align="right"><asp:Button ID="btSearch" runat="server" CssClass="btn" Text="Preview" style="margin:3px 3px 3px 3px"/><asp:Button ID="btGraph" runat="server" CssClass="btn" Text="Daily Graph" style="margin:3px 3px 3px 3px"/><asp:Button ID="btMonthGraph" runat="server" CssClass="btn2" Text="Monthly Graph" style="margin:3px 3px 3px 3px"/></td>
                                    </tr>
                                </table>
                            </td>
                         </tr>
                         <tr>
                            <td >
                            
                            <%--<asp:UpdatePanel runat="server" ID="plIndex" UpdateMode="Conditional">
                                 <ContentTemplate>
                               <uc:UserControlDocumentIndex id="ucDocIndex" runat="server"></uc:UserControlDocumentIndex>
                        </ContentTemplate>
                            </asp:UpdatePanel>--%>
                        
                            </td>
                         </tr>
                         <tr>
                            <td style="text-align:CENTER;">
                                <asp:Label ID="msg" runat="server" Text="" CssClass="msg_red"></asp:Label></td>
                         </tr>
                    </table>        
                
                      <asp:UpdatePanel ID="pnlView" runat="server" UpdateMode="Conditional"><ContentTemplate>
                    <asp:Panel ID="pView" runat="server" Visible="false" style="width:100%;max-height:400px;border:solid 1px #CCCCCC;overflow:auto">
                        <b>Import Results</b>
                <asp:GridView ID="GridView1" runat="server" ShowHeader="false">
                </asp:GridView>
                </asp:Panel>
                </ContentTemplate>
                
                </asp:UpdatePanel>
                
           
    
   
        </asp:Panel>
    
    </ContentTemplate>
    <Triggers><asp:PostBackTrigger ControlID="btSearch" /><asp:PostBackTrigger ControlID="btGraph" /><asp:PostBackTrigger ControlID="btMonthGraph" /></Triggers>
    </asp:UpdatePanel>
</asp:Content>
