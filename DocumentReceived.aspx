<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Report.Master" CodeBehind="DocumentReceived.aspx.vb" Inherits="dms.DocumentReceived" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Document List Per User - Report Criteria</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
            Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Height="650px" style="Width:auto"  >
            <LocalReport ReportPath="DocumentReceived.rdlc">          
            
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="dsUserDocReceived" Name="dsUserDocReceived" />                    
                </DataSources>                
                
            </LocalReport>
            
        </rsweb:ReportViewer>
                
                <asp:SqlDataSource ID="dsUserDocReceived" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:RptConnString %>" 
                    SelectCommand="ds_report_docstatus" SelectCommandType="Text" >                   
                </asp:SqlDataSource>
</asp:Content>
