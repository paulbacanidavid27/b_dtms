<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlBookMark.ascx.vb" Inherits="dms.UserControlBookMark" %>
            <div style="border-radius:5px;border-style: solid; border-width: 1px; border-color: #F1F4F8 #CFDBE7 #81A0C0 #CEDAE8; background-color: #FFFFFF; width: 98%; margin-top: 8px; margin-left: 1px">
                    <asp:UpdatePanel ID="pnlBk" runat="server" UpdateMode="Conditional">
                       <ContentTemplate>
                       
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" class="newtblheader2">
                            <tr height="25px">
                                <td align="left" width="30px">
                                    <img alt="" src="images/bookmark_icon.png" /></td><td><asp:LinkButton ID="lbBookMrk" runat="server" onmouseover="this.style.textDecoration = 'underline',this.style.color='#EEEEEE'" onmouseout="this.style.textDecoration = 'none',this.style.color=''" STYLE="color:#EEEEEE;font-family:Arial;font-size:10pt;font-weight:bold;font-style:normal;color:#CCCCCC">Bookmarks</asp:LinkButton></td>
                                    <td width="50px" align="right" valign="top" class="tableHead27" >
                                        <asp:ImageButton ID="imgBk" runat="server" imageurl="images/hidepanel.png"/></td>
                            </tr>
                        </table>
                       
                       <asp:Panel runat="server" ID="Pbk"> 
                    <asp:Repeater ID="rBookmark" runat="server" visible="true">
                        <HeaderTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" 
                                style="border-collapse: collapse;margin-top:8px;" width="100%">
                            
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                
                                
                                <td valign="top" style="padding-left:2px;" colspan="2">
                                    <asp:Image ID="imgUpd" visible="false" runat="server"  width="15px" imageurl=''/>
                                    <asp:ImageButton ID="ImgUnbookmark" ToolTip='Remove your bookmark for this document' runat="server" imageUrl="images/bookmark_h.png" onmouseover="this.src='images/bookmark.png'" onmouseout="this.src='images/bookmark_h.png'" Visible="false"/>
                                    <asp:ImageButton ID="ImgBookmark" ToolTip='Bookmark this document' runat="server" imageUrl="images/bookmark.png" onmouseover="this.src='images/bookmark_h.png'" onmouseout="this.src='images/bookmark.png'" Visible="false"/>
                                    
                                    <asp:Literal ID="lDocId" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "docid"))%>' Visible="false"></asp:Literal>
                                    <asp:Literal ID="lDocType" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "docType"))%>' Visible="false"></asp:Literal>
                                    <asp:Literal ID="lGroupAccessId" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "GroupAccessId"))%>' Visible="false"></asp:Literal>
                                
                                   <div  class="xxx" style="width:350px;">
                                       <asp:LinkButton ID="lbBM" onclientClick="ShowProgressInChild(this,'#Image2')" runat="server" OnClick="ViewDoc" style="color:#6671E7"  onmouseover="this.style.textDecoration = 'underline',this.style.color='BLUE'" onmouseout="this.style.textDecoration = 'none',this.style.color='#6671E7'" >
                                       <asp:Image ID="Image2"  ClientIDMode="Static" runat="server" style="vertical-align:middle;margin-right:3px" height="18px" width="15px" imageurl='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "doctypeimg"))%>'/>
                                        <asp:Label  ID="Literal2" runat="server"  
                                        Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "title"))%>' ></asp:Label>
                                        </asp:LinkButton>
                                        </div> 
                                </td>
                                
                            </tr>
                            <tr>
                                <td align="center">
                                </td>
                                <td style="font-size: 8pt;padding-left:10px; font-family: helvetica,arial; color: green">
                                    Uploaded By:
                                    <span style="color:Black"><asp:Literal ID="Literal3" runat="server" 
                                        Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "originator"))%>'></asp:Literal></span>
                                        Ref No:
                                    <span style="color:Black"><asp:Literal ID="lRefNo" runat="server" 
                                        Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "refno"))%>'></asp:Literal></span>
                                </td>                                
                            </tr>                           
                            
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <span >&nbsp;&nbsp;<asp:LinkButton ID="lbMoreBookmark" runat="server" visible="false" onmouseover="this.style.textDecoration = 'underline',this.style.color='#00005E'" onmouseout="this.style.textDecoration = 'none',this.style.color='blue'" style="color:blue;font-style:italic;font-family:arial;font-size:8pt;">More...</asp:LinkButton></span>
                    </asp:Panel>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                </div>