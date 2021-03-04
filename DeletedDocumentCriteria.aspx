<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="DeletedDocumentCriteria.aspx.vb" Inherits="dms.DeletedDocumentCriteria" EnableEventValidation="true" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ MasterType VirtualPath="~/Site.Master"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">    
        <script src="Scripts/print_report.js" type="text/javascript"></script>
        <title>Deleted Document - Report Criteria</title>
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
                            <td class="hdrtitle_1"><asp:Image ID="Image8" runat="server" width="20px" Height="20px" imageurl="images/file_history.png" />&nbsp;Deleted Document Report </td>
                         </tr>
    </table>    
         
</asp:Content>
<%--main headr content end--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mainDiv_" align="left">
       
        
      <asp:UpdatePanel ID="pnlRepeater" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
     </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel id="pRepeater" runat="server" DefaultButton="btSearch">  
    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="tblHdr2">
    <tr>
        <td valign="top">
            <div style="width:100%;">
                
                <asp:Panel ID="idAdvSrch" runat="server" Visible="false" style="height:auto;width:100%">
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
                                                Reference No:
                                        </td>
                                        <td align="left" >
                                            
                                            <asp:TextBox ID="tbRefNo" runat="server" cssclass="entryfld"  Width="250px"></asp:TextBox>
                                            
                                        </td>                                       
                                        <td align="RIGHT" class="labelFreeForm"  style="padding-left:10">
                                        Title:
                                            
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="tbTitle" runat="server" cssclass="entryfld" Width="250px"></asp:TextBox>
                                            <%--<cc1:autocompleteextender runat="server" ID="acomplete" TargetControlID="tbTitle"
                                                 ServiceMethod="getTitle" ServicePath="getUser.asmx" CompletionInterval="1000" EnableCaching="true"
                                                  MinimumPrefixLength="1"
                                                 completionsetcount="25" />--%>
                                        </td>  
                                        </tr>
                                        <tr>    
                                        <td align="right" class="labelFreeForm"  style="padding-left:10">
                                                Remarks:
                                        </td>
                                        <td align="left" >
                                                    <%--<asp:DropDownList ID="dlOfficeCode" runat="server" cssclass="entryfld2" Width="210px" AutoPostBack="false">
                                            </asp:DropDownList>--%>
                                            <asp:TextBox ID="tbRemarks" runat="server" cssclass="entryfld"  Width="250px"></asp:TextBox>
                                            <asp:Label ID="lOfficeName" runat="server" Text="" Visible="false"></asp:Label>
                                        </td>
                                         <td align="right"   class="labelFreeForm"  style="padding-left:10">
                                            Deleted Date:
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
                                            <asp:DropDownList ID="lbDocType" runat="server" cssclass="entryfld2" AutoPostBack="true" Width="250px">
                                            </asp:DropDownList>
                                          
                                            
                                        </td>
                                        
                                         <td align="right"   class="labelFreeForm"  style="padding-left:10">
                                            
                                        </td>
                                        <td></td>
                                        </tr>
                                        <tr>                                 
                                        <td colspan="4" align="right">
                                        <asp:Button ID="btSearch" runat="server" CssClass="btn" Text="Preview" style="margin:3px 3px 3px 3px"/>
                                        </td>
                                    </tr>

                                </table>
                            </td>
                         </tr>                         
                        <tr>
                            <td style="text-align:CENTER;">
                                <asp:Label ID="msg" runat="server" Text="" CssClass="msg_red"></asp:Label></td>
                         </tr>
                    </table>        
                </asp:Panel>
                              
              <%-- <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
            Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="993px"  >
            <LocalReport ReportPath="report_dochistory.rdlc">          
            
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ds_reportdata" Name="ds_reportdata" />
                </DataSources>
            
            </LocalReport>
        </rsweb:ReportViewer>
                
              
                
                <asp:SqlDataSource ID="ds_reportdata" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:RptConnString %>" 
                    SelectCommand="xMSP_REPORT_DOCHISTORY" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="tbDCFrom" Name="asDateFrom" DefaultValue=" "
                            PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="tbDCTo" Name="asDateTo" PropertyName="Text" DefaultValue=" "
                            Type="String" />
                        <asp:ControlParameter ControlID="tbTitle" Name="asTitle" PropertyName="Text" DefaultValue=" "
                            Type="String" />
                        <asp:SessionParameter Name="groupid" SessionField="s_userGroup" Type="String" />
                        <asp:ControlParameter ControlID="lbDocType" Name="asDocType" DefaultValue=" "
                            PropertyName="SelectedValue" Type="String" />
                        
                    </SelectParameters>
                </asp:SqlDataSource>--%>
                      
        
                
                      
        
                
              
                
            </div>
        </td>        
    </tr>
    </table>
    
   
        </asp:Panel>
   
    </div>
</asp:Content>
