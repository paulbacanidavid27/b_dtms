<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Report.Master" CodeBehind="DocReceivingReport.aspx.vb" Inherits="dms.DocReceivingReport" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
    <%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Document Receiving Report</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<table><tr>
<td>Date Range:</td>
<td><asp:DropDownList ID="dlMonth" runat="server" Visible="true" Width="102px">
                <asp:ListItem Text="January" Value="1" />
                <asp:ListItem Text="February" Value="2" />
                <asp:ListItem Text="March" Value="3" />
                <asp:ListItem Text="April" Value="4" />
                <asp:ListItem Text="May" Value="5" />
                <asp:ListItem Text="June" Value="6" />
                <asp:ListItem Text="July" Value="7" />
                <asp:ListItem Text="August" Value="8" />
                <asp:ListItem Text="September" Value="9" />
                <asp:ListItem Text="October" Value="10" />
                <asp:ListItem Text="November" Value="11" />
                <asp:ListItem Text="December" Value="12" />
              </asp:DropDownList>

                                            -
                                            <asp:TextBox ID="tbYear" runat="server" cssclass="entryfld"  Width="67px"></asp:TextBox>
                                            </td>
                                            <td>Delivered By:</td>
                                            <td><asp:TextBox ID="tbDeliveredBy" runat="server" cssclass="entryfld" Text="Psalm Joseph P. Blanco"></asp:TextBox></td>
                                            <td rowspan="4"><asp:Button ID="btPreview" CssClass="btnsmall" runat="server" Text="Preview" /></td></tr>
    <tr>
    <td>Prepared By:</td><td><asp:TextBox ID="tbPreparedBy" runat="server" cssclass="entryfld" Text="Psalm Joseph P. Blanco"></asp:TextBox></td>
    <td>Designation:</td><td><asp:TextBox ID="tbPreparedByDesignation" runat="server" cssclass="entryfld" Text="Job Order"></asp:TextBox></td>
    </tr>
    <tr>
    <td>Reviewed By:</td><td><asp:TextBox ID="tbReviewedBy" runat="server" cssclass="entryfld" Text="Sheen Rose B. Lauron"></asp:TextBox></td>
    <td>Designation:</td><td><asp:TextBox ID="tbReviewedByDesignation" runat="server" cssclass="entryfld" Text="Administrative Asst. III"></asp:TextBox></td>
    </tr><tr>
    <td>Submitted By:</td><td><asp:TextBox ID="tbSubmittedBy" runat="server" cssclass="entryfld" Text="Marissa A. Santos"></asp:TextBox></td>
    <td>Designation:</td><td><asp:TextBox ID="tbSubmittedByDesignation" runat="server" cssclass="entryfld" Text="Chief Administrative Officer"></asp:TextBox></td>
    </tr>
    <tr>
    <td colspan="5" align="center" style="color:Red;font-weight:bold"><b><asp:Literal ID="lmsg" runat="server"></asp:Literal></b></td></tr></table>   
      
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
            Font-Size="8pt" InteractiveDeviceInfos="(Collection)"  style="width:auto;" height="650px" 
            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
            <LocalReport ReportPath="DocReceivingReport.rdlc">          
            
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ds_reportdata" 
                        Name="dsDocReceiving" />
                </DataSources>
            <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ds_reportdata2" 
                        Name="dsDocReceiving2" />
                </DataSources>
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ds_reportdatasummary" 
                        Name="dsDocReceivingSummary" />
                </DataSources>
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ds_reportdatagraphsummary" 
                        Name="dsDocReceivingGraphSummary" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
                      
        
        
                      
        
        <asp:SqlDataSource ID="ds_reportdata" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:RptConnString %>" 
                    SelectCommand="DMSP_REPORT_UPLOADCOUNT" SelectCommandType="Text">
                    
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="ds_reportdata2" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:RptConnString %>" 
                    SelectCommand="DMSP_REPORT_UPLOADCOUNT" SelectCommandType="Text">
                    
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="ds_reportdatasummary" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:RptConnString %>" 
                    SelectCommand="DMSP_REPORT_UPLOADCOUNT" SelectCommandType="Text" >
                    
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="ds_reportdatagraphsummary" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:RptConnString %>" 
                    SelectCommand="DMSP_REPORT_UPLOADCOUNT" SelectCommandType="Text" >
                    
                </asp:SqlDataSource>
               

</asp:Content>

