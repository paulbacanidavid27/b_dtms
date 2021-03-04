<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="criteria_docsize.aspx.vb" Inherits="dms.criteria_docsize" EnableEventValidation="true" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ MasterType VirtualPath="~/Site.Master"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register src="ucDailyStorageConsumption.ascx" tagname="ucStorage" tagprefix="uc" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">    
        <script src="Scripts/print_report.js" type="text/javascript"></script>
        <title>Total Files and Bytes Uploaded Report</title>
</asp:Content>
<%@ Register src="UserControlReport.ascx" tagname="UserControlReport" tagprefix="uc" %>    
<%--menu content start--%>
<asp:Content ID="Content6" runat="server" ContentPlaceHolderID="menuContent">    
    <uc:UserControlReport id="ucReport" runat="server"></uc:UserControlReport>       
</asp:Content>
<%--main headr content start--%>
<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="MainHeaderContent">
   
            <table border="0" width="100%" class="tblHdr_1">
            
       <tr>
                            <td class="hdrtitle_1"><asp:Image ID="Image8" runat="server" width="20px" Height="20px" imageurl="images/file_history.png" />&nbsp;Total Files and Bytes Uploaded Report</td>
                         </tr>
    </table>    
         
</asp:Content>
<%--main headr content end--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table border="0"  cellspacing="1" cellpadding="3" width="100%" style="border:solid 1px #D4D4D4;">
                                    <tr>
                                        
                                        <td align="left" style="padding-left:10;font-style:italic;font-size:9pt;background-color:#CCCCCC" colspan="4" >
                                            Report Criteria
                                        </td>                                        
                                    </tr>                                    
                                     <tr>
                                        <td align="left" class="labelFreeForm"  style="padding-left:10">
       <uc:ucStorage runat="server" id="uStorage" visible="true"/>
  </td>
  </tr>
  </table>
    
</asp:Content>
