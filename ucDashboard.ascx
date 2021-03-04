<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucDashboard.ascx.vb" Inherits="dms.ucDashboard" %>
<%--<%@ Register src="UserControlPager.ascx" tagname="UserControlPager" tagprefix="uc2" %>--%>
<asp:UpdatePanel ID="pDB" runat="server" UpdateMode="Conditional">
<ContentTemplate>

    

<table border="0" class="codetbl" cellspacing="0" cellpadding="0" width="100%" style="border-collapse:collapse;z-index:900;border:#D4D4D4 solid 1px">

        <tr>
            <td class="newtblheader" align="center" width="90px" >
            <asp:LinkButton ID="lbSort1" runat="server" class="sortcol" tooltip="Sort by Date Received" OnClick="sortColumnHeader" style="vertical-align:middle;">Received</asp:LinkButton>
            <asp:Image ID="imgSort1" imageurl="images/asc.png" runat="server" visible="True"  style="vertical-align:middle;margin-left:0px;margin-right:0px"/>
            </td>
            <td class="newtblheader" align="center" width="90px" >
            <asp:LinkButton ID="lbSort2" runat="server" class="sortcol" tooltip="Sort by Due Date" OnClick="sortColumnHeader" style="vertical-align:middle;">Due Date</asp:LinkButton>
            <asp:Image ID="imgSort2" imageurl="images/asc.png" runat="server" visible="false"  style="vertical-align:middle;"/>
            </td>
            <td class="newtblheader" align="left" nowrap>
            <asp:LinkButton ID="lbSort3" runat="server" class="sortcol" tooltip="Sort by Reference No" OnClick="sortColumnHeader" style="vertical-align:middle;">Reference No</asp:LinkButton>
            <asp:Image ID="imgSort3" imageurl="images/asc.png" runat="server" visible="false"  style="vertical-align:middle;"/>
            </td>
            <td class="newtblheader" align="left" width="250px" >
            <asp:LinkButton ID="lbSort4" runat="server" class="sortcol" tooltip="Sort by Subject" OnClick="sortColumnHeader" style="vertical-align:middle;">Subject</asp:LinkButton>
            <asp:Image ID="imgSort4" imageurl="images/asc.png" runat="server" visible="false"  style="vertical-align:middle;"/></td>
            <td class="newtblheader" align="left">
            <asp:LinkButton ID="lbSort5"  width="120px" runat="server" class="sortcol" tooltip="Sort by Status" OnClick="sortColumnHeader" style="vertical-align:middle;">Last Action</asp:LinkButton>
            <asp:Image ID="imgSort5" imageurl="images/asc.png" runat="server" visible="false"  style="vertical-align:middle;margin:0px;"/></td>
            <td class="newtblheader" align="left" width="120px">
            <asp:LinkButton ID="lbSort6" runat="server" class="sortcol" tooltip="Sort by Personnel In-Charge" OnClick="sortColumnHeader" style="vertical-align:middle;" title="Personnel In-Charge">In-Charge</asp:LinkButton>
            <asp:Image ID="imgSort6" imageurl="images/asc.png" runat="server" visible="false"  style="vertical-align:middle;margin:0px;"/>
            </td>
            <td class="newtblheader" align="left">
            <asp:LinkButton ID="lbSort7" runat="server" class="sortcol" tooltip="Sort by Bureau/Office" OnClick="sortColumnHeader" style="vertical-align:middle;">Bureau/Office</asp:LinkButton>
            <asp:Image ID="imgSort7" imageurl="images/asc.png" runat="server" visible="false"  style="vertical-align:middle;"/>
            </td>
            <td class="newtblheader" align="left">
            <asp:LinkButton ID="lbSort8" runat="server" class="sortcol" tooltip="Sort by Age(Days)" OnClick="sortColumnHeader" style="vertical-align:middle;">Age</asp:LinkButton>
            <asp:Image ID="imgSort8" imageurl="images/asc.png" runat="server" visible="false"  style="vertical-align:middle;"/>
            </td>
         </tr>
<asp:Repeater ID="rptDashboard" runat="server">
<HeaderTemplate>    
</HeaderTemplate>
<ItemTemplate>
        <tr>
            <td class="tbldashed" valign="top"  align="center"  style="padding:2px;">            
                <asp:Literal ID="ldr" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "cdate"))%>'></asp:Literal>
            </td>                
            <td class="tbldashed" valign="top"  align="center"  style="padding-top:2px;">            
                <asp:Literal ID="ldd" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "duedt"))%>'></asp:Literal>
            </td>                
            <td class="tbldashed" valign="top"  align="left"  style="padding-top:2px;">            
                <asp:Literal ID="lDocId" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "docid"))%>' Visible="false"></asp:Literal>                          
                <asp:LinkButton ID="lbView" runat="server" OnClick="ViewDoc" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "refno"))%>'
                onmouseover="this.style.textDecoration = 'underline',this.style.color='#00005E'" onmouseout="this.style.textDecoration = 'none',this.style.color='blue'" style="color:blue;font-family:arial;font-size:8pt;"></asp:LinkButton>                                          
            </td>                
            <td class="tbldashed" valign="top"  align="left"  style="padding-top:2px;">            
                <asp:Literal ID="lsu" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "title"))%>'></asp:Literal>
            </td>                
            <td class="tbldashed" valign="top"  align="left"  style="padding-top:2px;">            
                <asp:Literal ID="lst" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "docstat"))%>'></asp:Literal>
            </td>                
            <td class="tbldashed" valign="top"  align="left"  style="padding-top:2px;">            
                <asp:Literal ID="lpic" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "assignedto"))%>'></asp:Literal>
            </td>                
            <td class="tbldashed" valign="top"  align="left"  style="padding-top:2px;">            
                <asp:Literal ID="lbu" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ofc"))%>'></asp:Literal>
            </td>                
            <td class="tbldashed" valign="top"  align="left"  style="padding-top:2px;">            
                <asp:Label ID="lage" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "age"))%>' ToolTip='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "datecompleted"))%>'></asp:Label>
            </td>                
        </tr>
       
</ItemTemplate>
<FooterTemplate>           
</FooterTemplate>
</asp:Repeater>
    <asp:Panel ID="pNo" runat="server" Visible="false">
    <tr>
        <td colspan="8" align="center">No Records Retrieved.</td>
    </tr>
    </asp:Panel>
 </table>
 </ContentTemplate>
</asp:UpdatePanel>

