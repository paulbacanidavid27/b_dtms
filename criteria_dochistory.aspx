<%@ Page Title="Document Changes - Report Criteria" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="criteria_dochistory.aspx.vb" Inherits="dms.criteria_dochistory" EnableEventValidation="true" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ MasterType VirtualPath="~/Site.Master"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">    
        <script src="Scripts/print_report.js" type="text/javascript"></script>
        <title>Document Changes - Report Criteria</title>
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
                            <td class="hdrtitle_1"><asp:Image ID="Image8" runat="server" width="20px" Height="20px" imageurl="images/file_history.png" />&nbsp;Document Changes Report </td>
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
                                                Office:
                                        </td>
                                        <td align="left" >
                                            <asp:DropDownList ID="dlOfficeCode" runat="server" cssclass="entryfld2" Width="350px" AutoPostBack="false">
                                            </asp:DropDownList>
                                            <asp:Label ID="lOfficeName" runat="server" Text="" Visible="false"></asp:Label>
                                        </td>
                                        <td align="right" class="labelFreeForm"  style="padding-left:10">Uploaded By:
                                    </td>
                                    <td>
                                    <asp:TextBox ID="tbAuthor" runat="server" cssclass="entryfld" AutoComplete="off"  Width="350px"> </asp:TextBox>
                                            <cc1:autocompleteextender runat="server" ID="acomplete" TargetControlID="tbAuthor"
                                                 ServiceMethod="getUsers" ServicePath="getUser.asmx" CompletionInterval="800" EnableCaching="true"
                                                  MinimumPrefixLength="1" FirstRowSelected="false"
                                                 completionsetcount="25" />
                                    </td>
                                                                               
                                          
                                        </tr>
                                        <tr>    
                                        <td align="right"class="labelFreeForm"  style="padding-left:10">
                                        Document Type:
                                            
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="lbDocType" runat="server" cssclass="entryfld2" Width="350px"  AutoPostBack="true">
                                            </asp:DropDownList>
                                          
                                            
                                        </td>
                                         <td align="right"   class="labelFreeForm"  style="padding-left:10">
                                            Changes Date:
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="uploadsdate" runat="server" cssclass="entryfld"  Width="67px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="uploadsdate"/>

                                            -
                                            <asp:TextBox ID="uploadedate" runat="server" cssclass="entryfld"  Width="67px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="uploadedate"/>
                                        </td>
                                        </tr>
                                        <tr>
                                        <td align="right" class="labelFreeForm"  style="padding-left:10">
                                        Subject/Title:
                                            
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="tbTitle" runat="server" Width="350px" cssclass="entryfld"></asp:TextBox>
                                            <%--<cc1:autocompleteextender runat="server" ID="acomplete" TargetControlID="tbTitle"
                                                 ServiceMethod="getTitle" ServicePath="getUser.asmx" CompletionInterval="1000" EnableCaching="true"
                                                  MinimumPrefixLength="1"
                                                 completionsetcount="25" />--%>
                                        </td>
                                        <td align="right" class="labelFreeForm"  style="padding-left:10">
                                        Date Uploaded:
                                            
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="tbCDFrom" runat="server" cssclass="entryfld"  Width="67px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="tbCDFrom"/>

                                            -
                                            <asp:TextBox ID="tbCDTo" runat="server" cssclass="entryfld"  Width="67px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="tbCDTo"/>
                                        </td>
                                        </tr>
                                        <tr>
                                        <td title="For multiple document no, separate it with comma" align="right"   class="labelFreeForm"  style="padding-left:10">Document No:
                                    </td>
                                    <td>
                                    <asp:TextBox ID="tbDocNo" runat="server" cssclass="entryfld" Width="350px"> </asp:TextBox>
                                    </td>
                                    <td align="right" class="labelFreeForm">
                                                Show:</td>
                                            <td align="left">                                    
                                                <asp:DropDownList ID="dlShow" runat="server" cssclass="entryfld2" Width="250px">
                                                    <asp:ListItem Value="" Text="All"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Confidential"></asp:ListItem>
                                                    <asp:ListItem Value="0" Text="Non-Confidential"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
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
                                                <asp:ListItem Text="Document No" Value="dl.refno" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Subject/Title" Value="dl.Title" Selected="False"></asp:ListItem>                                                
                                                <asp:ListItem Text="Document Type" Value="dl.DocType" Selected="False"></asp:ListItem>                                                                                                
                                                <asp:ListItem Text="Changes Date" Value="adate" Selected="False"></asp:ListItem>
                                                <asp:ListItem Text="Uploaded Date" Value="cdate" Selected="False"></asp:ListItem>
                                                <asp:ListItem Text="Office" Value="dl.OfficeCode" Selected="False"></asp:ListItem>                                                
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
