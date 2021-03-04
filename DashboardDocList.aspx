<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Report.Master" CodeBehind="DashboardDocList.aspx.vb" Inherits="dms.DashboardDocList" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Dashboard Document List Report</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
            Font-Size="8pt" InteractiveDeviceInfos="(Collection)"  Width="100%" Height="100%"
            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"   >
            <LocalReport ReportPath="DocumentList.rdlc">          
            
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="dsDashBoardStatusList" 
                        Name="dsDashBoardDoclist" />                    
                </DataSources>                
                
            </LocalReport>
            
        </rsweb:ReportViewer>
                
            
                
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        SelectMethod="GetData" 
        TypeName="dms.dsDBLTableAdapters.sp_docreturnlistTableAdapter">
    </asp:ObjectDataSource>
                
            
                
                <asp:SqlDataSource ID="dsDashBoardStatusList" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:RptConnString %>" 
                    SelectCommand="sp_docreturnlist" SelectCommandType="Text" >                   
                </asp:SqlDataSource>
</asp:Content>
