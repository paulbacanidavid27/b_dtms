<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucUserList.ascx.vb" Inherits="dms.ucUserList" %>
<asp:UpdatePanel ID="pnl" runat="server" UpdateMode="Conditional">
     
            <ContentTemplate>
<asp:Panel ID="pCopyFurnish" Visible="true" runat="server" style="width:100%;max-height:200px;overflow:auto">
                                                                     <table border="0" cellpadding="0" cellspacing="0" 
                                                                         style="width:100%;margin-bottom:2px;">
                                                                    <tr>
                                                                        
                                                                        <td align="left" colspan="3"  class="labelFreeForm" >Select Personnel:</td>
                                                                        
                                                                    </tr>                                                                    
                                                                    
                                                                    <asp:Repeater ID="rptCopy" runat="server" visible="false">
                                                                    <HeaderTemplate>                                                                        
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                    <tr >
                                                                        <td><asp:ImageButton ID="imgSelect" runat="server"  ImageUrl="images/box.png"  visible="false"/>
                                                                        <asp:ImageButton ID="ImgSelected" runat="server"  ImageUrl="images/del.png" OnClick="fUnSelect2" ToolTip="Remove user from the list"/>
                                                                        </td>
                                                                        <td align="left" class="labelFreeForm">
                                                                        <asp:Literal ID="lApprover" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Approver"))%>'></asp:Literal>
                                                                        </td>
                                                                        <td align="left" class="labelFreeForm">
                                                                        <asp:Literal ID="lUserId" visible="false" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "UserId"))%>'></asp:Literal>
                                                                        <asp:Literal ID="lEmail" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Email"))%>'></asp:Literal>
                                                                        <asp:Literal ID="lExist" runat="server" Text='' Visible="false"></asp:Literal>
                                                                        </td>                                                                        
                                                                    </tr>                                                                                                  
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>                                                                                                                                        
                                                                    </FooterTemplate>
                                                                    </asp:Repeater>
                                                                    </table>
                                                                    </asp:Panel>          
                                                          <asp:Panel ID="pnlCC" Visible="True" runat="server" DefaultButton="imgCCSearch">
                                                          
                                                         <table cellpadding="0" cellspacing="0" 
                                                                  style="width:100%;background-color:#99CCFF; border:solid 1px gray; " >
                                                         <tr><td align="left">
                                                         <table cellpadding="0" cellspacing="0" border="0" >
                                                            
                                                            <tr><td class="labelFreeForm" align="left" style="padding-left:2px;">Search 
                                                                <asp:DropDownList ID="dlCCSearchType" runat="server" AutoPostBack="true"  
                                                                    CssClass="entryfld2">
                                                                <asp:ListItem Value="u">Personnel</asp:ListItem>
                                                                <asp:ListItem Value="g">Group</asp:ListItem>
                                                                </asp:DropDownList> </td>
                                                                <td  align="left" style="padding-left:2px;">
                                                                    <asp:TextBox ID="txtCCBx" maxlength="150" runat="server" ClientIDMode="Static"  
                                                                        Width="300px" placeholder="Enter Personnel Name..." cssclass="entryfld"></asp:TextBox>
                                                                    <asp:DropDownList ID="dlCCGroups" runat="server" Visible="false" 
                                                                        CssClass="entryfld2">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td  align="left" style="padding-left:2px;">
                                                                    <asp:ImageButton ID="imgCCSearch" runat="server" imageurl="images/magni.png" 
                                                                        ToolTip="Search Users"/>
                                                                </td>               
                                                                <td></td>                                                 
                                                  
                                                                <td align="right">
                                                                </td></table>
                                                         </td><td><asp:ImageButton ID="imgCloseSearch" runat="server" imageurl="images/button/close.jpg" 
                                                                        ToolTip="Close Search Result" visible="false"/></td>
                                                         </tr>
                                                         </table>
                                                         
                                                         <div style="width:100%;max-height:100px;overflow:auto;background-color:#99CCFF; margin:0;padding:0">
                                                             <center><asp:Label ID="lCCSearchMsg" cssclass="helpnotes" runat="server" 
                                                                     Text="Label" Visible="False"></asp:Label></center>                                                          
                                                             <asp:Repeater ID="rptCCSub" runat="server" visible="false">
                                                                <HeaderTemplate>
                                                                    
                                                                    <table border="0"  cellpadding="0" cellspacing="0" style="width:100%;background-color:#DFDFDF; border-left:solid 1px gray; border-right:solid 1px gray;border-bottom:solid 1px gray;margin-bottom:2px;">
                                                                     <tr>
                                                                        <td class="tableHead2">
                                                                        <asp:ImageButton ID="imgSelecth" runat="server"  Width="16px" Height="16px" ImageUrl="images/selectall.png" OnClick="fSelectAll"  visible="true" ToolTip="Select All"/>
                                                                        <asp:ImageButton ID="ImgSelectedh" runat="server"  ImageUrl="images/selectall.png" OnClick="fUnSelectAll" ToolTip="Deselect All" visible="false"/>
                                                                        </td>
                                                                        <td align="left" class="tableHead2">Personnel Name</td>
                                                                        <td align="left" class="tableHead2">Email</td>
                                                                        </tr>
                                                                    
                                                                </HeaderTemplate>
                                                               
                                                                <ItemTemplate>
                                                                    <tr >
                                                                        <td><asp:ImageButton ID="imgSelect" runat="server"  ImageUrl="images/box.png"  OnClick="fSelectCC" tooltip="Select users"/>                                                                        
                                                                        </td>
                                                                        <td align="left" class="labelFreeForm">
                                                                        <asp:Literal ID="lApprover" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "username"))%>'></asp:Literal>
                                                                        </td>
                                                                        <td align="left"  class="labelFreeForm">
                                                                        <asp:Literal ID="lUserId" visible="false" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "UserId"))%>'></asp:Literal>
                                                                        <asp:Literal ID="lEmail" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Email"))%>'></asp:Literal>
                                                                        </td>
                                                                    </tr>       
                                                                                                                                
                                                                </ItemTemplate>
                                                                <FooterTemplate>                                                                    
                                                                    </table>
                                                                    </div>
                                                                </FooterTemplate>

                                                          </asp:Repeater>
                                                          </div>
                                                       </asp:Panel>          
</ContentTemplate>
</asp:UpdatePanel>