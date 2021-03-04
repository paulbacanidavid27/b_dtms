<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Report.Master" CodeBehind="Agency.aspx.vb" Inherits="dms.Agency" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Document Status - Report Criteria</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
            Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%" Height="100%"   >
            <LocalReport ReportPath="rptDocumentStatus.rdlc">          
            
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="dsDocStatus" Name="dsStatusReport" />                    
                </DataSources>                
                
            </LocalReport>
            
        </rsweb:ReportViewer>
                
                <asp:SqlDataSource ID="dsDocStatus" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:RptConnString %>" 
                    SelectCommand="ds_report_docstatus" SelectCommandType="Text" >                   
                </asp:SqlDataSource>
    <asp:GridView ID="GridView1" runat="server" DataSourceID="EBudget"></asp:GridView>
    <asp:SqlDataSource ID="EBudget" runat="server"></asp:SqlDataSource>
</asp:Content>
