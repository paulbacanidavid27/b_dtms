<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlIssuanceFolder.ascx.vb" Inherits="dms.UserControlIssuanceFolder" %>
<div style="border-radius:5px;border-style: solid; border-width: 1px; border-color: #F1F4F8 #CFDBE7 #81A0C0 #CEDAE8; background-color: #FFFFFF; width: 98%; margin-top: 8px; margin-left: 1px">
 <asp:HiddenField ID="hfFolderId" runat="server" />
                                     <asp:HiddenField ID="hfFolderDesc" runat="server" />
 <asp:UpdatePanel ID="pnlFolder" runat="server" UpdateMode="Conditional">

                                     <ContentTemplate>
                                     
<asp:Repeater ID="rptFolder" runat="server" Visible="True">
                <HeaderTemplate>            
                           
                    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="newtblheader2">
                            <tr height="25px">
                                <td align="center" width="30px">
                                    <asp:imagebutton runat="server" ID="imgShowInbox" onclientClick="ShowProgress(this)" imageurl="images/folder_icon.png" width="20px" height="20px" ToolTip="Goto Inbox"/></td>
                                    <td style="color:#EEEEEE;font-family:Arial;font-size:10pt;font-weight:bold;font-style:normal;color:#CCCCCC">Folders</td>
                                    <td align="right" style="padding-right:15px"><asp:ImageButton ID="imgAddFolder"  height="16px" Width="18px" tooltip="Add Folder" runat="server"   ImageUrl="images/add_folder.png" onclick="AddFolder"/></td>
                                    <td width="50px" align="right" valign="middle" class="tableHead27" >
                                        <asp:ImageButton ID="imgDelFolder"  height="15px" Width="14px" tooltip="Delete Folder." runat="server"   ImageUrl="images/del.png" onclick="DeleteFolders"/></td>
                            </tr>
                        </table>
                    <table  border="0" width="100%" cellpadding="0" cellspacing="0" style="border-collapse: collapse;">                            
                   
                </HeaderTemplate>
                <ItemTemplate>
                     
                     <tr>
                        <td style="padding:5px;font-size:13px;color:#5B5B5B">
                                    
                        
                                    <div  class="xxx" style="width:350px;">
                                        <asp:ImageButton ID="Image2"  ClientIDMode="Static" runat="server" style="vertical-align:middle;margin-right:3px" imageurl="images/fClose.png" ToolTip="Edit Folder Name"/>
                                        <asp:LinkButton ID="lbFolder" onclientClick="ShowProgressInChild(this,'#Image2')" runat="server" OnClick="SearchDoc" style="color:#6671E7"  onmouseover="this.style.textDecoration = 'underline',this.style.color='BLUE'" onmouseout="this.style.textDecoration = 'none',this.style.color='#6671E7'" >
                                            
                                            <asp:Literal ID="lFolder" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "FolderDesc"))%>'></asp:Literal>
                                            <asp:Literal ID="lCnt" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Cnt"))%>' Visible="true"></asp:Literal>
                                        </asp:LinkButton>
                                        <asp:TextBox ID="tbFolderName" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "FolderDesc"))%>' visible="false" style="width:320px;" autopostback="true" OnTextChanged="SaveChanges"></asp:TextBox>
                                    </div> 
                                    <asp:Literal ID="lFolderId" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "FolderId"))%>' Visible="false"></asp:Literal>
                                    
                        </td>
                        
                        <td align="right"><%--<asp:ImageButton ID="imgSelect" runat="server"  ImageUrl="images/box.png"  OnClick="fSelect" visible="false"/>
                        <asp:ImageButton ID="ImgSelected" runat="server"  ImageUrl="images/checkbox.png" OnClick="fUnSelect" visible="false"/>--%>
                        <asp:CheckBox ID="cbDelete" runat="server" visible="false" />
                        </td>                                                                        
                    </tr>                                                                                        
                </ItemTemplate>
                <FooterTemplate>
                            <tr>
                                    <td style="border-top:solid 1px #ffffff" colspan="2"></td>
                                </tr>
                                </table>
                </FooterTemplate>

            </asp:Repeater>
            <asp:Panel ID="FolderAdd" runat="server" DefaultButton="lbSave" Visible="false">
            <table>
                                                          <tr>
                                                                        <td><asp:TextBox ID="txtFolder" maxlength="50" runat="server" ClientIDMode="Static" Width="300px" onfocus="tclear(this,'Add New Folder')" onblur="tblur(this,'Add New Folder')" cssclass="entryfld" style="color:#C0C0C0">Add New Folder</asp:TextBox></td>
                                                                        <td>
                                                                            <asp:Button ID="lbSave" runat="server" CssClass="btnsmall2" Text="Save" /></td>
                                                                    </tr>
                                                                    <tr>
        <td colspan="2"><asp:Label ID="lcheckmsg" cssclass="msg_green" runat="server" Text=""></asp:Label></td>
        </tr>
                                                         </table>
                                                         </asp:Panel>
                                                         </ContentTemplate>
                                                         </asp:UpdatePanel>
                                                         </div>