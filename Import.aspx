<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Import.aspx.vb" Inherits="dms.Import" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register src="UserControlAdminMenuH.ascx" tagname="UserControlAdminMenuH" tagprefix="uc" %>    
<%@ Register src="ucUploadFiles.ascx" tagname="ucUploadFiles" tagprefix="uc1" %>
<%@ Register src="ucViewFiles.ascx" tagname="ucViewFiles" tagprefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeaderMenuContent">
    <table cellpadding="0" cellspacing="0" border="0" width="100%" class="tableheaderGreen">
                   <tr >
                        <td valign="middle">
                        </td>
                        </tr>
                        </table>
</asp:Content>
<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="AddContent">
    <table cellpadding="0" cellspacing="0" border="0" width="100%" class="tableheaderGreen">
                   <tr >
                        <td valign="middle">
                        </td>
                        </tr>
                        </table>
</asp:Content>
<asp:Content ID="Content5" runat="server" ContentPlaceHolderID="MainFooterContent">
    <table cellpadding="0" cellspacing="3" border="0" width="100%" class="tableheaderGreen">
                   <tr >
                        <td valign="middle"><div class="notes2">&nbsp;<asp:Literal ID="lRecordCount" runat="server"></asp:Literal></div>
                        </td>
                        </tr>
    </table>
</asp:Content>

<%--<asp:Content ID="Content12" runat="server" ContentPlaceHolderID="AdminMenu">
    <uc:UserControlAdminMenuH id="UserControlAdminMenuH1" runat="server"></uc:UserControlAdminMenuH>                                       
</asp:Content>--%>

<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="MainHeaderContent">
    <table border="0" width="100%" class="tableheaderGreen">
        <tr>
            <td class="tableheader_1"><img  alt="" src="images/import.png" Width="20px" height="20px" style="vertical-align:middle" />&nbsp;Import Documents</td>
            <td align="right">
                <table width="100%">
                  <tr>                    
                    <td width="373px">&nbsp;
                    </td>
                    <td>
                        
                    </td>
                    <td>
                        
                    </td>            
                    <td>
                        <asp:ImageButton ID="imgAddDoc" runat="server" ImageUrl="images/question.png" visible="false"/>
                    </td>
                  </tr>
                  
                 </table>
            </td>
        </tr>        
    </table>    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mainDiv_"  align="center">
<uc1:ucUploadFiles ID="ucUploadFiles1" runat="server" visible="false"/>
<uc1:ucViewFiles ID="ucViewFiles1" runat="server" visible="false"/>
<asp:Panel ID="imprt" runat="server" DefaultButton="btUpload">
<table style="text-align:left;margin-top:10px;margin-bottom:10px;">
<tr>
         <td class="labelFreeForm" colspan="2" style="border-bottom:solid 1px gray"><b>
             STEP 1</b> - Select a file to be imported. Only '.csv' file is accepted.</td>         
</tr>
<tr>
    <td colspan="2" class="labelFreeForm">     Import File Contains Only File name:  &nbsp;&nbsp;<asp:CheckBox ID="cbFileName" runat="server" AutoPostBack="true" />
   </td>         
</tr>
<tr>
   <td class="labelFreeForm"> File to import:</td>
   <td>
    <asp:FileUpload ID="uploadFileName"  CssClass="entryfld2" runat="server" width="350px"/>
   </td>
</tr>         
<tr>
    <td colspan="2" class="labelFreeForm">     Import File Name:  &nbsp;&nbsp;<asp:Label ID="lImportFileName" runat="server" Text="" style="font-style:italic"></asp:Label>
   </td>         
</tr>

<tr>
    <td class="labelFreeForm" colspan="2" style="border-bottom:solid 1px gray"><b>STEP 2</b> - Define the columns of the file to be imported.</td>         
</tr>
<tr>
    <td colspan="2">
        <asp:UpdatePanel ID="pnlDocType" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

           <table border="0">
                     <tr>
                         <td class="labelFreeForm" >Document Type already included in the Import File </td>
                         <td>
                             <asp:RadioButtonList ID="rbDocType" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                                <asp:ListItem Selected="True">Yes</asp:ListItem>
                                <asp:ListItem>No</asp:ListItem>
                             </asp:RadioButtonList>
                             <asp:CheckBox ID="cbDocType" runat="server" checked="false" AutoPostBack="true" Visible="false"/>
                         </td>
                    </tr>
                    <tr>
                       <td >
                           <asp:Label ID="lSelectDoc" cssclass="labelFreeForm" runat="server"  Visible="false" Text="Select a Document Type of the documents to be imported:"></asp:Label>
                       </td>
                       <td>
                           <asp:DropDownList ID="dlDocType" runat="server" cssclass="entryfld2" Width="210px" Visible="false"></asp:DropDownList>
                       </td>
                    </tr>
                     <tr>
                        <td>
                            <asp:Label ID="lDocTypeColNumber" runat="server" cssclass="labelFreeForm" Text="Specify column number of Document Type in the Import File:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="tbDocTypeColNumber" runat="server" Text = "1" cssclass="entryfldint" Visible="true"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td>
                            <asp:Label ID="lOfficeColumn" runat="server" cssclass="labelFreeForm" Text="Specify column number of Office/Agency in the Import File:" ToolTip="Specify in what column number the Office/Agency can be found in the import file."></asp:Label>
                            <asp:Label ID="lOfficeCode" cssclass="labelFreeForm" runat="server"  Visible="false" Text="Select an Office/Agency of the documents to be imported:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="tbOfficeColumnNumber" runat="server" Text = "2" cssclass="entryfldint" Visible="true"></asp:TextBox>
                            <asp:DropDownList ID="dlOffice" runat="server" cssclass="entryfld2" Width="210px" Visible="false"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2"><i>(Office/Agency will be used in generating Reference Number. Set it to blank if you want to skip generating Reference Number. You can also put here the Office Code to be used in generating reference no.)</i></td>
                    </tr>

                    <tr>
                        <td class="labelFreeForm">Specify in what column number the file name can be found in the import file:
                        </td>
                        <td><asp:TextBox ID="tbFileNameColNumber" runat="server"  ReadOnly="false" Text = "3" cssclass="entryfldint"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2"><i>(File name should be in the last column to generate the index properly.)</i></td>
                    </tr>
         
            </table>
        </ContentTemplate>
        </asp:UpdatePanel>
    </td>
 </tr>      
 <tr>
    <td class="labelFreeForm" colspan="2" style="border-bottom:solid 1px gray"><b>STEP 3</b> - Set the location of the documents to be imported (can be changed in config file).</td>         
 </tr>

 <tr>
    <td class="labelFreeForm">Current location of files to be imported in the server:
                             <asp:RadioButtonList ID="rbDocLoc" runat="server" RepeatDirection="Horizontal" 
                                 AutoPostBack="true">
                                <asp:ListItem Selected="True">Default</asp:ListItem>
                                <asp:ListItem>Upload</asp:ListItem>
                             </asp:RadioButtonList>
                             </td>
    <td><asp:TextBox ID="tbLocation" runat="server"  ReadOnly="true" Text = "" width="300px" cssclass="entryflddisabled"></asp:TextBox><asp:Button ID="btUploadFiles" cssclass="btnsmall2" runat="server"
            Text="Upload Files" visible="false"/><asp:ImageButton ID="imgViewFiles" runat="server" ImageUrl="images/bullets.png" style="vertical-align:middle;height:26px;width:26px;" tooltip="View Files"/></td>
 </tr>
 <tr>
    <td class="labelFreeForm" colspan="2" style="border-bottom:solid 1px gray"><b>STEP 4</b> - Select document status. This will set the status of all the document to be imported.</td>         
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
      
        
 </table>
 </asp:Panel>
 </div>

</asp:Content>
