<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="DocumentReceivedCriteria.aspx.vb" Inherits="dms.DocumentReceivedCriteria" EnableEventValidation="true" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ MasterType VirtualPath="~/Site.Master"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register src="UserControlReport.ascx" tagname="UserControlReport" tagprefix="uc" %>    
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">    
        <script src="Scripts/print_report.js" type="text/javascript"></script>
        <title>Document Received - Report Criteria</title>
</asp:Content>

<%--menu content start--%>
<asp:Content ID="Content6" runat="server" ContentPlaceHolderID="menuContent">    
    <uc:UserControlReport id="ucReport" runat="server" Visible="true"></uc:UserControlReport>       
</asp:Content>
<%--main headr content start--%>
<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="MainHeaderContent">
   
            <table border="0" width="100%" class="tblHdr_1">
      <tr>
                            <td><asp:Image ID="Image8" runat="server" width="20px" Height="20px" imageurl="images/task.png" />&nbsp;Document Received Report</td>
                         </tr>
    </table>    
         
</asp:Content>
<%--main headr content end--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mainDiv_" width="100%">
       
        
      <asp:UpdatePanel ID="pnlRepeater" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel id="pRepeater" runat="server" DefaultButton="btSearch">  
    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="tblHdr2_">
    <tr>
        <td valign="top">
            <div style="width:100%;">
                
                
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
                                                                           
                                        
                                        <td align="right" valign="top" class="labelFreeForm"  style="padding-left:10" title="Upload Date">
                                            Received Date Range:
                                        </td>
                                        <td align="left"  valign="top">
                                            <asp:TextBox ID="uploadsdate" runat="server" cssclass="entryfld"  Width="67px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="uploadsdate" />

                                            -
                                            <asp:TextBox ID="uploadedate" runat="server" cssclass="entryfld"  Width="67px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="uploadedate"/>
                                        </td>
                                        <td align="right" width="150px"  class="labelFreeForm"  style="padding-left:10">
                                        Action Taken:
                                            
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="dlStatus" cssclass="entryfld2"  runat="server" Width="250px">
                                            </asp:DropDownList>
                                        </td>
                                        </tr>
                                         <tr>
                                        <td align="right" width="150px"  class="labelFreeForm"  style="padding-left:10">
                                            Classification:
                                            
                                        </td>
                                        <td>

                                            <asp:DropDownList ID="dlClassification" runat="server" Visible="true" AutoPostBack="false" 
                                                cssclass="entryfld2" Width="210px">
                                                <asp:ListItem Text="" Value="" Selected="True">All</asp:ListItem>
                                                <asp:ListItem Text="External" Value="0" Selected="False"></asp:ListItem>
                                                <asp:ListItem Text="Internal" Value="1" Selected="False"></asp:ListItem>                                                
                                            </asp:DropDownList>
                                        </td>
                                        <td align="right" width="150px"  class="labelFreeForm"  style="padding-left:10">
                                        Status:
                                            
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="dlDocStatus" cssclass="entryfld2"  runat="server" Width="250px">
                                                <asp:ListItem Text="Open" Value="A">-All-</asp:ListItem>
                                                <asp:ListItem Text="Open" Value="O"></asp:ListItem>
                                                <asp:ListItem Text="Completed/Closed" Value="C"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                     
                                     <td align="right" width="150px"  class="labelFreeForm"  style="padding-left:10">
                                        Request Type:
                                            
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="dlRequestType" cssclass="entryfld2"  runat="server" Width="350px">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="right"   class="labelFreeForm"  style="padding-left:10">
                                           <%-- Assigned Date Range:--%>
                                           Subject/Title:
                                        </td>
                                        <td align="left">
                                        <asp:TextBox ID="tbSubject" runat="server" visible="true" cssclass="entryfld"  Width="250px"></asp:TextBox>
                                            
                                        </td>
                                    
                                        </tr>
                                        <tr>
                                            
                                                <td align="right"   class="labelFreeForm"  style="padding-left:10" title="Upload Date">
                                                    Sender:
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="tbSender" runat="server" cssclass="entryfld" maxlength="100" Width="350px"></asp:TextBox>
                                                    
<%--
                                                    -
                                                    <asp:TextBox ID="tbEmailTo" runat="server" cssclass="entryfld"  Width="67px"></asp:TextBox>--%>
                                                    
                                             </td>       
                                             <td align="right" class="labelFreeForm"  style="padding-left:10">
                                               <asp:Literal ID="labelUser" runat="server" Text="User: " Visible="false"></asp:Literal>
                                            </td>
                                            <td align="left" >
                                                    <asp:DropDownList ID="dlUser" runat="server" cssclass="entryfld2" Width="250px" visible="false" AutoPostBack="false">
                                                    </asp:DropDownList>
                                                    
                                                </td>                                 
                                        </tr>
                                        <tr>                                        
                                            <td align="left" style="padding-left:10;font-style:italic;font-size:9pt;background-color:#CCCCCC" colspan="4" >
                                                Sort Option
                                            </td>                                        
                                    </tr>     
                                     <tr>
                                        <td align="right" width="150px"  class="labelFreeForm"  style="padding-left:10">
                                            Column Sort:                                            
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="dlColumns" runat="server" Visible="true" AutoPostBack="false" 
                                                cssclass="entryfld2" Width="210px">
                                                <asp:ListItem Text="Document No" Value="dl.refno" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Title" Value="dl.Title" Selected="False"></asp:ListItem>
                                                <asp:ListItem Text="Classification" Value="Classification" Selected="False"></asp:ListItem>
                                                <asp:ListItem Text="Sender" Value="SenderName" Selected="False"></asp:ListItem>
                                                <asp:ListItem Text="Status" Value="DStatus" Selected="False"></asp:ListItem>
                                                <asp:ListItem Text="Received Date" Value="dr.AssignedDate" Selected="False"></asp:ListItem>
                                                <asp:ListItem Text="Action Date" Value="dr.ActionDate" Selected="False"></asp:ListItem>
                                                <asp:ListItem Text="Action" Value="UserAction" Selected="False"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        
                                    </tr>
                                    <tr>
                                        <td align="right" width="150px"  class="labelFreeForm"  style="padding-left:10">
                                            Sort Order:
                                            
                                        </td>
                                        <td>

                                            <asp:DropDownList ID="dlSortOption" runat="server" Visible="true" AutoPostBack="false" 
                                                cssclass="entryfld2" Width="210px">
                                                <asp:ListItem Text="Ascending" Value="asc" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Descending" Value="desc" Selected="False"></asp:ListItem>
                                                
                                            </asp:DropDownList>
                                        </td>
                                        
                                    </tr>
                                    <tr>
                                    <td colspan="4" align="right" ><asp:Button ID="btSearch" runat="server" CssClass="btn" Text="Preview" style="margin:3px 3px 3px 3px"/></td></tr>
                                </table>
                            </td>
                         </tr>
                         
                         <tr>
                            <td style="text-align:CENTER;">
                                <asp:Label ID="msg" runat="server" Text="" CssClass="msg_red"></asp:Label></td>
                         </tr>
                    </table>        
                
                              
               
              
                
            </div>
        </td>        
    </tr>
    </table>
    
   
        </asp:Panel>
    
    </div>
</asp:Content>
