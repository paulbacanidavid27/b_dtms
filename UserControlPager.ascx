<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlPager.ascx.vb" Inherits="dms.UserControlPager" %>
<table cellpadding="0" cellspacing="0" border="0" style="border-collapse:collapse">
<tr>
           <%-- <td colspan="2"><div class="totalno"><asp:Literal ID="lRecordCount" runat="server" ></asp:Literal>
            
        </div></td>--%>
            
            <td valign="top" align="right">
                <table border="0" cellspacing="0" cellpadding="0" style="border-collapse:collapse">
                        <tr><td valign="middle" style="padding-left:2px;padding-right:2px;font-family:arial; font-size:12px;font-style:normal;color:inherit">
                                            <asp:Label ID="lPageCount" runat="server" Text="" ></asp:Label></td>
                        <td style="padding-left:2px;padding-right:2px;"><asp:ImageButton ID="imgFirst" runat="server"  ToolTip="Go to First page" ImageUrl="images/nav/first.png"  onmouseover="this.src='images/nav/first_h.png'"  onmouseout="this.src='images/nav/first.png'" Visible="False"/>
                                            <asp:ImageButton ID="imgFirstD" runat="server"  style=" cursor:default" ImageUrl="images/nav/first_d.png" Visible="False"  Enabled="false"/></td>
                            <td style="padding-left:2px;padding-right:2px;"><asp:ImageButton ID="imgLess" runat="server"  ToolTip="Retrieve previous page" ImageUrl="images/nav/prev.png"  onmouseover="this.src='images/nav/prev_h.png'"  onmouseout="this.src='images/nav/prev.png'" Visible="False"/>
                                            <asp:ImageButton ID="imgLessD" runat="server"  style=" cursor:default" ImageUrl="images/nav/prev_d.png" Visible="False"  Enabled="false"/></td>
                            
                            <td style="padding-left:2px;padding-right:2px;"><asp:ImageButton ID="imgGreater" runat="server" 
                                    ToolTip="Retrieve next page" ImageUrl="images/nav/next.png" 
                                    onmouseover="this.src='images/nav/next_h.png'"  
                                    onmouseout="this.src='images/nav/next.png'" Visible="False"/>                                            
                                            <asp:ImageButton ID="imgGreaterD" runat="server" style=" cursor:default" ImageUrl="images/nav/next_d.png"  Visible="False" Enabled="False"/>
                            </td>
                            <td style="padding-left:2px;padding-right:2px;"><asp:ImageButton ID="imgLast" runat="server" 
                                    ToolTip="Go to Last page" ImageUrl="images/nav/last.png" 
                                    onmouseover="this.src='images/nav/last_h.png'"  
                                    onmouseout="this.src='images/nav/last.png'" Visible="False"/>                                            
                                            <asp:ImageButton ID="imgLastD" runat="server"  style=" cursor:default" ImageUrl="images/nav/last_d.png"  Visible="False" Enabled="False"/>
                            </td>
                        </tr>
                    </table>  
            </td>
            </tr>
            </table>
