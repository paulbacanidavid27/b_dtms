<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Report.Master" CodeBehind="rptDocUploaded.aspx.vb" Inherits="dms.rptDocUploaded" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<title>Document Uploaded Per Day Report</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
            Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%" Height="100%"  >
            <LocalReport ReportPath="report_uploadcount.rdlc">          
            
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ds_reportdata" Name="ds_reportdata" />
                </DataSources>
            
            </LocalReport>
        </rsweb:ReportViewer>
                      
        
                <asp:SqlDataSource ID="ds_reportdata" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:RptConnString %>" 
                    SelectCommand="DMSP_REPORT_UPLOADCOUNT" SelectCommandType="Text">
                    <%--<SelectParameters>
                    <asp:SessionParameter DefaultValue=" " Name="asDocType" 
                    SessionField="r_DocType" Size="50" Type="String" />
                <asp:SessionParameter DefaultValue=" " Name="asDateFrom" 
                    SessionField="r_StartDate" Size="10" Type="String" />
                <asp:SessionParameter DefaultValue=" " Name="asDateTo" 
                    SessionField="r_EndDate" Type="String" />
                <asp:SessionParameter DefaultValue=" " Name="asAuthor" 
                    SessionField="r_ColValue" Type="String" />        
                
                        <asp:SessionParameter Name="GroupId" SessionField="s_userGroup" Type="String" />
                    </SelectParameters>--%>
                </asp:SqlDataSource>
</asp:Content>
