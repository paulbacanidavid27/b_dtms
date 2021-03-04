<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="DocumentIndexCriteria.aspx.vb" Inherits="dms.DocumentIndexCriteria" EnableEventValidation="true" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ MasterType VirtualPath="~/Site.Master"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register src="UserControlDocumentIndex.ascx" tagname="UserControlDocumentIndex" tagprefix="uc" %>    
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">    
        <script src="Scripts/print_report.js" type="text/javascript"></script>
        <title>Document Index - Report Criteria</title>
</asp:Content>
<%@ Register src="UserControlReport.ascx" tagname="UserControlReport" tagprefix="uc" %>    
<%--menu content start--%>
<asp:Content ID="Content6" runat="server" ContentPlaceHolderID="menuContent">    
    <uc:UserControlReport id="ucReport" runat="server"></uc:UserControlReport>       
</asp:Content>
<%--main headr content start--%>
<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="MainHeaderContent">
   
            <table border="0" width="100%">
        <tr>
                            <td ><asp:Image ID="Image8" runat="server" width="20px" Height="20px" imageurl="images/index.png" />&nbsp;Document Index Report </td>
                         </tr>
    </table>    
         
</asp:Content>
<%--main headr content end--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mainDiv_">
       
        
      <asp:UpdatePanel ID="pnlRepeater" runat="server" UpdateMode="Conditional">
    <ContentTemplate></ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel id="pRepeater" runat="server" DefaultButton="btSearch">  
    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="tblHdr2" style="border-collapse:collapse">
    <tr>
        <td valign="top">
            <div style="width:100%;">
                
                <asp:Panel ID="idAdvSrch" runat="server" Visible="false" style="height:auto;width:100%">
                    <table width="100%" cellspacing="0" cellpadding="0" border="0">
                         
                         
                         <tr>
                            <td align="left">
                                
                                <table border="0"  cellspacing="1" cellpadding="3" width="100%" style="border:solid 1px #D4D4D4;">
                                    <tr>
                                        
                                        <td align="left" style="padding-left:10;font-style:italic;font-size:9pt;background-color:#CCCCCC" colspan="5" >
                                            Report Criteria
                                        </td>                                        
                                    </tr>                                    
                                     <tr>
                                        <td align="RIGHT" width="150px"  class="labelFreeForm"  style="padding-left:10">
                                        Document Type:
                                            
                                        </td>
                                        <td align="left">
                                        <asp:DropDownList ID="dlDocType" runat="server" cssclass="entryfld2" Width="175px"  AutoPostBack="true"></asp:DropDownList>
                                            
                                        </td>                                       
                                        
                                         <td align="RIGHT" width="150px"  class="labelFreeForm"  style="padding-left:10">
                                            Date Created:
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="tbDCFrom" runat="server" cssclass="entryfld"  Width="67px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="tbDCFrom" />

                                            -
                                            <asp:TextBox ID="tbDCTo" runat="server" cssclass="entryfld"  Width="67px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="tbDCTo"  />
                                        </td>
                                        <td align="right">
                                        <asp:Button ID="btSearch" runat="server" CssClass="btn" Text="Preview" style="margin:3px 3px 3px 3px"/>
                                        </td>
                                    </tr>

                                </table>
                            </td>
                         </tr>
                         <tr>
                            <td >
                                <asp:HiddenField ID="hfColValue" runat="server" Value=""/>
                            <asp:UpdatePanel runat="server" ID="plIndex" UpdateMode="Conditional">
                                 <ContentTemplate>
                               <uc:UserControlDocumentIndex id="ucDocIndex" runat="server"></uc:UserControlDocumentIndex>
                        </ContentTemplate>
                            </asp:UpdatePanel>
                        
                            </td>
                         </tr>
                         
                    </table>        
                </asp:Panel>
                              
               <%--<rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
            Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="993px">
            <LocalReport ReportPath="report_DocIndex.rdlc">          
            
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ds1" Name="ds1" />
                </DataSources>
            
            </LocalReport>
        </rsweb:ReportViewer>
        
        <asp:SqlDataSource ID="ds1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:RptConnString %>" 
            SelectCommand="xMSP_REPORT_DOCTYPE" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:ControlParameter ControlID="dlDocType" DefaultValue=" " Name="asDocType" 
                    PropertyName="SelectedValue" Size="50" Type="String" />
                <asp:ControlParameter ControlID="tbDCFrom" DefaultValue=" " Name="asDateFrom" 
                    PropertyName="Text" Size="10" Type="String" />
                <asp:ControlParameter ControlID="tbDCTo" DefaultValue=" " Name="asDateTo" 
                    PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="hfColValue" DefaultValue=" " Name="asColValue" 
                    PropertyName="Value" Type="String" />        
                <asp:SessionParameter DefaultValue=" " Name="GroupId" 
                    SessionField="s_userGroup" Type="String" />                    
            </SelectParameters>
        </asp:SqlDataSource>--%>
        
                
                        
                
               
                
            </div>
        </td>        
    </tr>
    </table>
    
   
        </asp:Panel>
    
    </div>
</asp:Content>
