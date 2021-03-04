<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlDocLink.ascx.vb" Inherits="dms.UserControlDocLink" %>
<%--pager: step 1--%>
<%@ Register src="UserControlPager.ascx" tagname="UserControlPager" tagprefix="uc2" %>
<%--<%@ Register src="UserControlImgViewer.ascx" tagname="UserControlImgViewer" tagprefix="uc" %>
<%@ Register src="UserControlPDFViewer.ascx" tagname="UserControlPDFViewer" tagprefix="uc" %>
<%@ Register src="UserControlDocViewer.ascx" tagname="UserControlDocViewer" tagprefix="uc" %>
<uc:UserControlPDFViewer runat="server" id="ucPDFViewer" visible="False"/>
    <uc:UserControlDocViewer runat="server" id="ucDocViewer" visible="False"/>
    <uc:UserControlImgViewer runat="server" id="ucViewer" visible="False"/> --%>           
                <asp:UpdatePanel ID="pnlDocLinks" runat="server" UpdateMode="Conditional" >
                                     <ContentTemplate>
            <asp:HiddenField ID="hfCount" runat="server" />
                <asp:Repeater ID="rptLinks" runat="server" Visible="True">
                    <HeaderTemplate>                                                                   
                        <table  border="0" width="100%" cellpadding="0" cellspacing="0" style="border:solid 1px #D4D4D4;background-color:white;border-collapse:collapse">
                            <tr>
                               <td class="newtblheader" width="100%"><img src="images/link_icon.png" />&nbsp;Document Link</td> <td class="newtblheader" align="right" width="30px"><asp:ImageButton ID="imgSelect"  tooltip="Delete selected links." runat="server"  ImageUrl="images/del.png" onclick="DeleteLinks"/></td>
                            </tr>    
                     
                    </HeaderTemplate>
                    <ItemTemplate>                                                                                           
                     <tr>
                        <td style="padding:5px" ><asp:LinkButton ID="lbDoc" runat="server" cssclass="nb" OnClick="ViewDoc" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "refno"))%>'></asp:LinkButton> - <asp:Literal ID="ldoctitle" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "title"))%>'  Visible="true"></asp:Literal>
                        <asp:Literal ID="lTB" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CreatedBye"))%>'  Visible="false"></asp:Literal>
                        </td>
                        
                        <td align="right" width="30px"><asp:ImageButton ID="imgSelect" runat="server"  ImageUrl="images/box.png"  OnClick="fSelect"/>
                        <asp:ImageButton ID="ImgSelected" runat="server"  ImageUrl="images/checkbox.png" OnClick="fUnSelect" visible="false"/>
                        <asp:Literal ID="lLinkDocId" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "LinkDocId"))%>' Visible="false"></asp:Literal>
                        </td>
                                                                        
                    </tr>                                                                    
                    <tr><td class="tbldashed-gray" colspan="2" >By: <asp:Literal ID="Literal1" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "username"))%>'></asp:Literal>&nbsp; (<asp:Label ID="lCreateDate" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CreatedDate"))%>'></asp:Label>)</td>
                     </tr>
                </ItemTemplate>
                <FooterTemplate>
                            <tr>
                                    <td style="border-top:solid 1px #ffffff" colspan="2"></td>
                                </tr>
                                </table>
                </FooterTemplate>

                </asp:Repeater>
                <asp:Panel ID="linkSearch" runat="server" DefaultButton="imgSearch">
                <table style="background-color:white;text-align:left">
                <tr>
                    <td>
                        <asp:TextBox ID="txtLinks" maxlength="150" runat="server" ClientIDMode="Static" Width="300px" onfocus="tclear(this,'Search Document to Link')" onblur="tblur(this,'Search Document to Link')" cssclass="entryfld">Search Document to Link</asp:TextBox>
                    </td>
                    <td align="left">
                        <asp:ImageButton ID="imgSearch" runat="server" imageurl="images/magni.png"/><asp:Button ID="btSaveLinks" visible="false" runat="server" CssClass="btnsmall2" Text="Save" />
                    </td>
                </tr>
                <tr>
        <td colspan="2"><asp:Label ID="lcheckmsg" cssclass="msg_green" runat="server" Text=""></asp:Label></td>
        </tr>
                </table>
                </asp:Panel>
                <table border="0" width="100%" cellpadding="0" cellspacing="0" style="color:#5D5D5D; background-color:white;">
                <tr>
                <td align="left">
                        <table border="0" cellpadding="0" cellspacing="0" style="background-color:white;">
                                            <tr >
                                            <td align="center">&nbsp;<asp:Button ID="btSave" runat="server" Text="Save" CssClass="btnsmall" Visible="false" /><asp:ImageButton Visible="false" ID="imgSave" runat="server" imageurl="images/link_doc.png" ToolTip ="Link selected document" />&nbsp;</td>                                            
                                                        </tr>
                                                        </table>
                                                        </td>
                                                        <td align="right">
                                                        <table>
                                                        <tr>
                                                    <td align="right">
                                                        <%--pager: step 2--%>
                                                        <asp:HiddenField ID="hfCurrent" runat="server" Value="1" />
                                                        <asp:HiddenField ID="hfTotalRows" runat="server" Value="0" />
                                                        <uc2:UserControlPager ID="ucPager" runat="server" pTextColor="gray" />
                                                    </td>
                                                    <td align="right">
                                                        <asp:ImageButton ID="imgClose" runat="server" imageurl="images/img_delete.jpg" 
                                                            Visible="false" />
                                                    </td>
                                                </tr>
                                                </table>
                                                </td>
                                                </tr>
                                   </table>             
                                            <div style="max-height:200px;border:solid 1px #D4D4D4;overflow:auto">
                                            <asp:Repeater ID="rptDocList" runat="server" Visible="False">
                                                <HeaderTemplate>
                                                <table width="100%" border="0"  cellpadding="0" cellspacing="0" style="background-color:white;">
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td align="center" width="15px">
                                                            <asp:ImageButton ID="imgSelect" runat="server" ImageUrl="images/box.png" 
                                                                OnClick="fSelect" />
                                                            <asp:ImageButton ID="ImgSelected" runat="server" ImageUrl="images/checkbox.png" 
                                                                OnClick="fUnSelect" visible="false" />
                                                        </td>
                                                        
                                                        <td align="left">
                                                            <asp:HyperLink ID="hlNewTab" runat="server" CssClass="menu5"  tooltip="Reference No" Target="_blank"   onclientclick="return false" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "refno"))%>'>                                
                                                            </asp:HyperLink>
                                                            <%--<asp:LinkButton ID="lbOpenDoc" tooltip="Reference No" runat="server" CssClass="menu5" 
                                                                visible="false"><asp:Literal ID="lref" visible="True" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "refno"))%>'></asp:Literal></asp:LinkButton> - --%>
                                                             <asp:Literal ID="lbDoc" runat="server" 
                                                                Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "title"))%>' 
                                                                visible="true"></asp:Literal>
                                                            <asp:Literal ID="lIsLocal" runat="server" 
                                                                Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "IsLocal"))%>' 
                                                                visible="false"></asp:Literal>
                                                            <asp:Literal ID="lnkDocId" runat="server" 
                                                                Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DocId"))%>' 
                                                                visible="false"></asp:Literal>
                                                                <asp:Literal ID="lCDate" runat="server" 
                                                                Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CreatedDate"))%>' 
                                                                visible="false"></asp:Literal>
                                                            <asp:Literal ID="lVersion" runat="server" 
                                                                Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "FileVersion"))%>' 
                                                                visible="false"></asp:Literal>
                                                            <asp:Literal ID="lFileName" runat="server" 
                                                                Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "FileName"))%>' 
                                                                visible="false"></asp:Literal>
                                            <%--                <asp:HyperLink ID="hlNewWindow" runat="server" cssclass="menu4" 
                                                                NavigateUrl="viewfile.aspx?d_id=" onclientclick="return false" Target="_blank" 
                                                                ToolTip="Open in New Window" Visible="false">
                            <asp:Literal ID="lbDoc" visible="false" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Title"))%>'></asp:Literal>

                            </asp:HyperLink>--%>
                                                            
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr style="background-color:#FFEAF5">
                                                        <td align="center" width="15px">
                                                            <asp:ImageButton ID="imgSelect" runat="server" ImageUrl="images/box.png" 
                                                                OnClick="fSelect" />
                                                            <asp:ImageButton ID="ImgSelected" runat="server" ImageUrl="images/checkbox.png" 
                                                                OnClick="fUnSelect" visible="false" />
                                                        </td>
                                                        <td align="left">
                                                            <asp:HyperLink ID="hlNewTab" runat="server"  CssClass="menu5"   onclientclick="return false" tooltip="Reference No" Target="_blank"   Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "refno"))%>'>                                
                                                            </asp:HyperLink>
                                                            <%--<asp:LinkButton ID="lbOpenDoc" tooltip="Reference No" runat="server" CssClass="menu5" 
                                                                OnClick="openDoc" visible="true"><asp:Literal ID="lref" visible="True" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "refno"))%>'></asp:Literal></asp:LinkButton> - --%>
                                                             <asp:Literal ID="lbDoc" runat="server" 
                                                                Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "title"))%>' 
                                                                visible="true"></asp:Literal>
                                                            <asp:Literal ID="lIsLocal" runat="server" 
                                                                Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "IsLocal"))%>' 
                                                                visible="false"></asp:Literal>
                                                            <asp:Literal ID="lnkDocId" runat="server" 
                                                                Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DocId"))%>' 
                                                                visible="false"></asp:Literal>
                                                             <asp:Literal ID="lCDate" runat="server" 
                                                                Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CreatedDate"))%>' 
                                                                visible="false"></asp:Literal>
                                                            <asp:Literal ID="lVersion" runat="server" 
                                                                Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "FileVersion"))%>' 
                                                                visible="false"></asp:Literal>
                                                            <asp:Literal ID="lFileName" runat="server" 
                                                                Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "FileName"))%>' 
                                                                visible="false"></asp:Literal>
                                            <%--                <asp:HyperLink ID="hlNewWindow" runat="server" cssclass="menu4" 
                                                                NavigateUrl="viewfile.aspx?d_id=" onclientclick="return false" Target="_blank" 
                                                                ToolTip="Open in New Window" Visible="True">
                            <asp:Literal ID="lbDoc" visible="False" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Title"))%>'></asp:Literal>

                            </asp:HyperLink>--%>
                                                            <%--<asp:HyperLink ID="hlNewWindow" runat="server" cssclass="menu4"  onclientclick="return false" Visible="True" NavigateUrl='viewfile.aspx?d_id=' Target="_blank"  ToolTip="Open in New Window">--%>

                                                            
                                                            <%--</asp:HyperLink>--%>
                                                        </td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                                <FooterTemplate>
                                                    </table>      
                                                </FooterTemplate>
                                            </asp:Repeater>                                             
                                                    </div>
                                            
                                                                          
                         
                                                                     
            </ContentTemplate>
                </asp:UpdatePanel>