<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Monitoring.Master" CodeBehind="ReleasingMonitoringImport.aspx.vb" Inherits="dms.ReleasingMonitoringImport" %>
<%@ MasterType VirtualPath="~/Monitoring.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Document Management System</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    CRD Document Monitoring - Releasing Section
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="MenuContent" runat="server">
    <a href="DocMonitoring.aspx" style="color:Blue">Back</a>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="imprt" runat="server" DefaultButton="btUpload">
<table style="text-align:left;margin-top:10px;margin-bottom:10px;">
<tr>
  <td class="labelFreeForm" colspan="2" style="border-bottom:solid 1px gray"><b>
             STEP 1</b> - Select a file to be imported. Only '.csv' file is accepted.</td>         
</tr>
<tr>
   <td class="labelFreeForm"> File to import:</td>
   <td>
    <asp:FileUpload ID="uploadFileName"  CssClass="entryfld2" runat="server" width="350px"/>
   </td>
</tr>         
<tr>
    <td colspan="2" class="labelFreeForm">Import File Name:  &nbsp;&nbsp;<asp:Label ID="lImportFileName" runat="server" Text="" style="font-style:italic"></asp:Label>
   </td>         
</tr>
 <tr>
    <td class="labelFreeForm" colspan="2" style="border-bottom:solid 1px gray"><b>STEP 2</b> - Select document status. This will set the status of all the document to be imported.</td>         
 </tr>
 <tr>
    <td class="labelFreeForm" colspan="2" style="border-bottom:solid 1px gray">
 <table>
 <tr>
    <td>Document Status:</td>
    <td >
        <asp:DropDownList ID="dlStatus" runat="server" cssclass="entryfld2" Visible="true"></asp:DropDownList>
    </td>
 </tr>
 </table>
 </td>
 </tr>
 <tr>
    <td class="labelFreeForm" colspan="2" style="border-bottom:solid 1px gray"><b>STEP 5</b> - Click Process button. This will display the content of the file to be imported.</td>         
 </tr>

 <tr>
    <td colspan="2">
        <asp:Button ID="btUpload" runat="server" CssClass="btn" Text="Process" visible="true"/>
        <asp:Button ID="btUploadFileName" runat="server" CssClass="btn" Text="Process" visible="false"/>        
    </td>
 </tr>
 <tr>
    <td class="labelFreeForm" colspan="2" style="border-bottom:solid 1px gray"><b>STEP 6</b> - Click Import button to import the documents. Only the records with 'OK to import' will be processed.</td>         
 </tr>         
 <tr>
    <td colspan="2">
        <asp:Panel ID="step5" runat="server" Visible="false">
        <table>        
         <tr>
            <td colspan="2">
                <asp:Button ID="btImport" runat="server" CssClass="btn" Text="Import" visible="false"/>
                <asp:Button ID="btCancel" runat="server" CssClass="btn" Text="Cancel" visible="false"/>
            </td>
        </tr>
        </table>
        </asp:Panel>
    </td>
 </tr>
 <tr>
    <td colspan="2">
        <asp:UpdatePanel ID="pnlMsg" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                
                 <asp:Repeater ID="rptDocIndex" runat="server" Visible="false">    
                   <ItemTemplate>
                    <asp:Literal ID="lDocType" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DocType"))%>'></asp:Literal>
                    <asp:Literal ID="lColSeq" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ColSeq"))%>'></asp:Literal>
                    <asp:Literal ID="lColId" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ColumnId"))%>'></asp:Literal>
                   </ItemTemplate>     
                 </asp:Repeater>
                 <asp:GridView ID="rptUpload" runat="server" AllowSorting="false" Visible="false"
                  AlternatingRowStyle-BackColor="#AEE4FF"  
                       HeaderStyle-Font-Names="tahoma" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White"
                       HeaderStyle-Font-Size="11px" RowStyle-Font-Names="tahoma" RowStyle-Font-Size="11px" RowStyle-VerticalAlign="Top" >            
                 </asp:GridView>
        </ContentTemplate>
        </asp:UpdatePanel>
             
    </td>
 </tr>
      <tr><td colspan="2"><asp:Literal ID="lRecordCount" runat="server"></asp:Literal></td></tr>
        
 </table>
 </asp:Panel>
</asp:Content>


<asp:Content ID="Content10" ContentPlaceHolderID="cntntUpload" runat="server">
</asp:Content>
<asp:Content ID="Content11" ContentPlaceHolderID="PopupMenu" runat="server">
</asp:Content>
