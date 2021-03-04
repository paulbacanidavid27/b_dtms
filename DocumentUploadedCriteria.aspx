<%@ Page Title="Document Uploaded - Report Criteria" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="DocumentUploadedCriteria.aspx.vb" Inherits="dms.DocumentUploadedCriteria" EnableEventValidation="true" %>

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
                            <td><img width="20px" Height="20px" src="images/upload.png" />Documents Uploaded Report</td>
                         </tr>
    </table>    
         
</asp:Content>
<%--main headr content end--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
       
        
      <asp:UpdatePanel ID="pnlRepeater" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    
    <asp:Panel id="pRepeater" runat="server" DefaultButton="btSearch">  
    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="tblHdr2_">
    <tr>
        <td valign="top">
            <div style="width:100%;">
                
                
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
                                        
                                                    <asp:DropDownList ID="dlOfficeCode" runat="server" cssclass="entryfld2" Width="350px" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <asp:Label ID="lOfficeName" runat="server" Text="" Visible="false"></asp:Label>
                                            
                                        </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    </tr>        
                                    <tr>
                                        <td align="right" class="labelFreeForm"  style="padding-left:10">
                                                Group:
                                        </td>
                                        <td align="left">
                                            <asp:UpdatePanel ID="pnlGroup" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                    <asp:DropDownList ID="dlGroup" runat="server" cssclass="entryfld2" Width="350px" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <asp:Label ID="lGroupName" runat="server" Text="" Visible="false"></asp:Label>
                                            </ContentTemplate>
                                            <Triggers><asp:AsyncPostBackTrigger ControlID="dlOfficeCode"  EventName="SelectedIndexChanged" /></Triggers>
                                        </asp:UpdatePanel>
                                        </td>
                                        <td align="right"   class="labelFreeForm"  style="padding-left:10">
                                            Date Uploaded:
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="tbDCFrom" runat="server" cssclass="entryfld"  Width="67px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="tbDCFrom"/>

                                            -
                                            <asp:TextBox ID="tbDCTo" runat="server" cssclass="entryfld"  Width="67px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="tbDCTo"/>
                                        </td>
                                    </tr>                         
                                     <tr>
                                        <td align="RIGHT"class="labelFreeForm"  style="padding-left:10">
                                        Document Type:
                                            
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="lbDocType" runat="server" cssclass="entryfld2" Width="350px" AutoPostBack="false">
                                            </asp:DropDownList>
                                          
                                            
                                        </td>                                       
                                        
                                        <td></td>
                                        <td></td>
                                        </tr>
                                        <tr>
                                        
                                        <td align="RIGHT" width="150px"  class="labelFreeForm"  style="padding-left:10">
                                        Uploaded By:
                                            
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="tbAuthor" cssclass="entryfld" runat="server" Visible="false" ></asp:TextBox>
                                            <cc1:autocompleteextender runat="server" ID="acomplete" TargetControlID="tbAuthor"
                                                 ServiceMethod="getUsers" ServicePath="getUser.asmx" CompletionInterval="800" EnableCaching="true"
                                                  MinimumPrefixLength="1" FirstRowSelected="false"
                                                 completionsetcount="25" />
                                                 <asp:UpdatePanel ID="pUsers" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                 <asp:DropDownList ID="dluser" runat="server" cssclass="entryfld2" Width="350px" Visible="true" AutoPostBack="false">
                                            </asp:DropDownList>
                                            </ContentTemplate>
                                            <Triggers><asp:AsyncPostBackTrigger ControlID="dlGroup"  EventName="SelectedIndexChanged" /></Triggers>
                                        </asp:UpdatePanel>
                                        </td>                                       
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        
                                        <td align="left" style="padding-left:10;font-style:italic;font-size:9pt;background-color:#CCCCCC" colspan="4" >
                                            Sort Option
                                        </td>                                        
                                    </tr>     
                                    
                                     <tr>
                                        <td class="labelFreeForm"  align="right">
                                            Column Sort:
                                            
                                        </td>
                                        <td colspan="3">

                                            <asp:DropDownList ID="dlColumns" runat="server" Visible="true" AutoPostBack="false" 
                                                cssclass="entryfld2" Width="210px">                                                
                                                <asp:ListItem Text="Uploaded By" Value="author" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Document Type" Value="docname" Selected="False"></asp:ListItem>                                                
                                                <asp:ListItem Text="Uploaded Date" Value="cdate" Selected="False"></asp:ListItem>                                                                                                
                                                <asp:ListItem Text="Total Count" Value="totalcount" Selected="False"></asp:ListItem>
                                                
                                            </asp:DropDownList>
                                        </td>
                                        
                                    </tr>
                                    <tr>
                                        <td class="labelFreeForm"  align="right">
                                            Sort Order:
                                            
                                        </td>
                                        <td  colspan="3">

                                            <asp:DropDownList ID="dlSortOption" runat="server" Visible="true" AutoPostBack="false" 
                                                cssclass="entryfld2" Width="210px">
                                                <asp:ListItem Text="Ascending" Value="asc" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Descending" Value="desc" Selected="False"></asp:ListItem>
                                                
                                            </asp:DropDownList>
                                        </td>
                                        
                                    </tr>
                                    <tr>
                                    <td colspan="4" align="right"><asp:Button ID="btSearch" runat="server" CssClass="btn" Text="Preview" style="margin:3px 3px 3px 3px"/><asp:Button ID="btDetails" runat="server" CssClass="btn" Text="Details" style="margin:3px 3px 3px 3px"/></td>
                                    </tr>
                                </table>
                            </td>
                         </tr>
                         
                         <tr>
                            <td style="text-align:CENTER;">
                                <asp:Label ID="msg" runat="server" Text="" CssClass="msg_red"></asp:Label></td>
                         </tr>
                    </table>        
                
                              
               <%--<rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
            Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="993px"  >
            <LocalReport ReportPath="report_uploadcount.rdlc">          
            
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ds_reportdata" Name="ds_reportdata" />
                </DataSources>
            
            </LocalReport>
        </rsweb:ReportViewer>
                      
        
                <asp:SqlDataSource ID="ds_reportdata" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:RptConnString %>" 
                    SelectCommand="xMSP_REPORT_UPLOADCOUNT" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                    <asp:SessionParameter DefaultValue=" " Name="asDocType" 
                    SessionField="r_DocType" Size="50" Type="String" />
                <asp:SessionParameter DefaultValue=" " Name="asDateFrom" 
                    SessionField="r_StartDate" Size="10" Type="String" />
                <asp:SessionParameter DefaultValue=" " Name="asDateTo" 
                    SessionField="r_EndDate" Type="String" />
                <asp:SessionParameter DefaultValue=" " Name="asAuthor" 
                    SessionField="r_ColValue" Type="String" />        
                
                        <asp:SessionParameter Name="GroupId" SessionField="s_userGroup" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>--%>
              
                
            </div>
        </td>        
    </tr>
    </table>
    
   
        </asp:Panel>
    
    </ContentTemplate>
    <Triggers>
<asp:PostBackTrigger ControlID="btSearch" />
<asp:PostBackTrigger ControlID="btDetails" /></Triggers>
    </asp:UpdatePanel>
</asp:Content>
