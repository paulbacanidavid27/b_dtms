<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Report.Master" CodeBehind="ReleasingStatusMonitoring.aspx.vb" Inherits="dms.ReleasingStatusMonitoring" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Document Pending Approval Report</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
            Font-Size="8pt" InteractiveDeviceInfos="(Collection)"  style="width:auto;" height="650px" 
            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
            <LocalReport ReportPath="ReleasingStatusMonitoringReport.rdlc">          
            
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ds_reportdata" 
                        Name="dsMonitoringReport" />
                </DataSources>
            
            </LocalReport>
        </rsweb:ReportViewer>
                      
        
        <asp:SqlDataSource ID="ds_reportdata" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:RptConnString %>" 
                    SelectCommand="DMSP_REPORT_UPLOADCOUNT" SelectCommandType="Text">
                    
                </asp:SqlDataSource>
               

</asp:Content>
