<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucTrackStatusUsers.ascx.vb" Inherits="dms.ucTrackStatusUsers" %>

<asp:Panel ID="pTrackuser1"  cssclass="Div1" runat="server" Visible="True">
</asp:Panel>
<asp:Panel ID="pTrackuser2" cssclass="Div2" runat="server" Visible="True" >
 <table border="0" cellspacing="0" cellpadding="0" style="width:100%;height:100%;border-collapse:collapse" >
                <tr>
                  <td valign="middle" align="center" style="height:100%">  

<table cellpadding="0" cellspacing="6" width="100%" border="0" style="background-color:White;width:450px;" >
        <tr>
            <td colspan="5">
                
                <table cellspacing="0" class="popuphdr" cellpadding="0" border="0" style="width:100%;margin-bottom:3px;">
                                            <tr height="30px">
                                            <td align="left" valign="middle" colspan="3">&nbsp;
                                            <asp:Literal ID="lHdr" runat="server" Text=""></asp:Literal></td>
                                            <td  align="right" valign="top">
                                                <asp:ImageButton ID="imgClose" runat="server" imageurl="images/close_window.gif" onmouseover="this.src='images/close_window.gif'"  onmouseout="this.src='images/close_window.gif'" width="18px" Height="18px" OnClick="fClose"/>
                                            </td>
                                            </tr>
                                       </table>
            </td>
        </tr>
        </table>
        <asp:Panel ID="pStatus" runat="server" Visible="True" style="background-color:White;width:450px;max-height:470px;overflow:auto;">                    
<asp:Repeater ID="rptTrackStatus" runat="server">
<HeaderTemplate>
    <table cellpadding="0" cellspacing="6" border="0" style="background-color:White" >
</HeaderTemplate>
<ItemTemplate>
<tr>
                        <td id="Td2" width="100px" align="center" runat="server">
                            <asp:Image ID="imgIArrow3" runat="server" imageurl="images/track_darrow.png" visible="false"/>
                        </td>                        
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
        <tr id="trow" runat="server">
            <td style="padding:4px" valign="top" height="110px" align="left" id="td1" runat="server">            
                <table  id="tbl1" runat="server" style="border:solid 1px #CCCCCC;border-collapse:collapse">
                    <tr>
                        <td  height="100px" width="100px" align="center">                            
                            <asp:Image  ID="imgUser1" style="max-height:100%;max-width:90%" runat="server" imageurl='<%#  "images/avatar/" & Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "profilePic1"))%>' ToolTip='<%# "Assigned Date: " & Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "assigneddate1"))%>'/>
                        </td>                        
                    </tr>
                    <tr>                        
                        <td align="left" style="font-size:8pt;border:solid 1px #CCCCCC;border-collapse:collapse;background-color:#EEEEEE" nowrap>
                            <asp:Literal ID="lName1" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "userName1"))%>'></asp:Literal>
                        </td>
                    </tr>
                    <tr>                        
                        <td align="left" style="font-size:8pt;border:solid 1px #CCCCCC;border-collapse:collapse;background-color:#EEEEEE" nowrap>
                           <b>Action:</b> <asp:Literal ID="lAct1" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "description1"))%>'></asp:Literal>
                        </td>
                    </tr>
                    <tr>                        
                        <td style="font-size:8pt;border:solid 1px #CCCCCC;border-collapse:collapse;background-color:#EEEEEE"  title='<%# "Action Date: " & Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "actiondate1"))%>'>
                            <b>Duration:</b> <asp:Literal ID="lDuration1" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Duration1"))%>'></asp:Literal>
                        </td>
                    </tr>
                </table>                
            </td>       
            <td valign="top">
                <table >
                    <tr>
                        <td  height="100px" valign="middle">
                            <asp:Image ID="imgIArrow1" runat="server" imageurl="images/track_rarrow.png" />
                        </td>                        
                    </tr>
                </table>                                
            </td> 
            <td style="padding:4px" valign="top" height="110px" align="left">            
                
                <table  id="tbl2" runat="server" style="border:solid 1px #CCCCCC;border-collapse:collapse">
                    <tr>
                        <td  height="100px" width="100px" align="center">                            
                            <asp:Image ID="imgUser2" style="max-height:100%;max-width:90%" runat="server" imageurl='<%#  "images/avatar/" & Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "profilePic2"))%>'  ToolTip='<%# "Assigned Date: " & Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "assigneddate2"))%>'/>
                        </td>                        
                    </tr>
                    <tr>
                        <td align="left" style="font-size:8pt;border:solid 1px #CCCCCC;border-collapse:collapse;background-color:#EEEEEE" nowrap>
                            <asp:Literal ID="lName2" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "userName2"))%>'></asp:Literal>
                        </td>
                    </tr>
                    <tr>                        
                        <td align="left" style="font-size:8pt;border:solid 1px #CCCCCC;border-collapse:collapse;background-color:#EEEEEE" nowrap>
                            <b>Action:</b> <asp:Literal ID="lAct2" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "description2"))%>'></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="font-size:8pt;border:solid 1px #CCCCCC;border-collapse:collapse;background-color:#EEEEEE"  title='<%# "Action Date: " & Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "actiondate2"))%>'>
                            <b>Duration:</b> <asp:Literal ID="lDuration2" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Duration2"))%>'></asp:Literal>
                        </td>
                    </tr>
                </table>              
            </td>       
            <td valign="top">
                <table >
                    <tr>
                        <td  height="100px" valign="middle">
                            <asp:Image ID="imgIArrow2" runat="server" imageurl="images/track_rarrow.png" />
                        </td>                        
                    </tr>
                </table>                                
            </td> 
            <td style="padding:4px" valign="top" height="110px" align="left">            
                <table  id="tbl3" runat="server" style="border:solid 1px #CCCCCC;border-collapse:collapse">
                    <tr>
                        <td  height="100px" width="100px" align="center">
                            <asp:Image ID="imgUser3" style="max-height:100%;max-width:90%" runat="server" imageurl='<%# "images/avatar/" & Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "profilePic3"))%>'  ToolTip='<%# "Assigned Date: " & Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "assigneddate3"))%>'/>
                        </td>                        
                    </tr>
                     <tr>                        
                        <td align="left" style="font-size:8pt;border:solid 1px #CCCCCC;border-collapse:collapse;background-color:#EEEEEE" nowrap>
                            <asp:Literal ID="lName3" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "userName3"))%>'></asp:Literal>
                        </td>
                    </tr>
                    <tr>                        
                        <td align="left" style="font-size:8pt;border:solid 1px #CCCCCC;border-collapse:collapse;background-color:#EEEEEE" nowrap>
                            <b>Action:</b> <asp:Literal ID="lAct3" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "description3"))%>'></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="font-size:8pt;border:solid 1px #CCCCCC;border-collapse:collapse;background-color:#EEEEEE"  title='<%# "Action Date: " & Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "actiondate3"))%>'>
                            <b>Duration:</b> <asp:Literal ID="lDuration3" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Duration3"))%>'></asp:Literal>
                        </td>
                    </tr>                    
                </table>                
            </td>       
            
        </tr>
        
</ItemTemplate>
<AlternatingItemTemplate>
<tr>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>        
        <td width="100px" align="center" runat="server">
            <asp:Image ID="imgIArrow3" runat="server" imageurl="images/track_darrow.png"  visible="false"/>
        </td>                        
        </tr>
        <tr id="trow" runat="server">
            <td style="padding:4px" valign="top" height="110px" align="left">            
                <table  id="tbl3" runat="server" style="border:solid 1px #CCCCCC;border-collapse:collapse">
                    <tr>
                        <td  height="100px" width="100px" align="center">
                            <asp:Image ID="imgUser3" style="max-height:100%;max-width:90%" runat="server" imageurl='<%#  "images/avatar/" & Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "profilePic3"))%>'  ToolTip='<%# "Assigned Date: " & Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "assigneddate3"))%>' />
                        </td>                        
                    </tr>
                     <tr>
                        <td align="left" style="font-size:8pt;border:solid 1px #CCCCCC;border-collapse:collapse;background-color:#EEEEEE" nowrap>
                            <asp:Literal ID="lName3" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "userName3"))%>'></asp:Literal>
                        </td>
                    </tr>
                    <tr>                        
                        <td align="left" style="font-size:8pt;border:solid 1px #CCCCCC;border-collapse:collapse;background-color:#EEEEEE" nowrap>
                           <b> Action:</b> <asp:Literal ID="lAct3" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "description3"))%>'></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="font-size:8pt;border:solid 1px #CCCCCC;border-collapse:collapse;background-color:#EEEEEE"  title='<%# "Action Date:  " & Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "actiondate3"))%>'>
                            <b>Duration:</b> <asp:Literal ID="lDuration3" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Duration3"))%>'></asp:Literal>
                        </td>
                    </tr>
                    
                </table>                
            </td>  

            <td valign="top">
                <table >
                    <tr>
                        <td  height="100px" valign="middle">
                            <asp:Image ID="imgIArrow2" runat="server" imageurl="images/track_larrow.png" />
                        </td>                        
                    </tr>
                </table>                                
            </td> 
            <td style="padding:4px" valign="top" height="110px" align="left">            
                
                <table  id="tbl2" runat="server" style="border:solid 1px #CCCCCC;border-collapse:collapse">
                    <tr>
                        <td  height="100px" width="100px" align="center">                            
                            <asp:Image ID="imgUser2" style="max-height:100%;max-width:90%" runat="server" imageurl='<%#  "images/avatar/" & Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "profilePic2"))%>'  ToolTip='<%# "Assigned Date: " & Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "assigneddate2"))%>'/>
                        </td>                        
                    </tr>
                    <tr>
                        <td align="left" style="font-size:8pt;border:solid 1px #CCCCCC;border-collapse:collapse;background-color:#EEEEEE" nowrap>
                            <asp:Literal ID="lName2" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "userName2"))%>'></asp:Literal>
                        </td>
                    </tr>
                    <tr>                        
                        <td align="left" style="font-size:8pt;border:solid 1px #CCCCCC;border-collapse:collapse;background-color:#EEEEEE" nowrap>
                           <b>Action:</b> <asp:Literal ID="lAct2" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "description2"))%>'></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="font-size:8pt;border:solid 1px #CCCCCC;border-collapse:collapse;background-color:#EEEEEE"  title='<%# "Action Date: " & Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "actiondate2"))%>'>
                            <b>Duration:</b> <asp:Literal ID="lDuration2" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Duration2"))%>'></asp:Literal>
                        </td>
                    </tr>
                </table>              
            </td>   
            <td valign="top">
                <table >
                    <tr>
                        <td  height="100px" valign="middle">
                            <asp:Image ID="imgIArrow1" runat="server" imageurl="images/track_larrow.png" />
                        </td>                        
                    </tr>
                </table>                                
            </td> 
            <td style="padding:4px" valign="top" height="110px" align="left" runat="server">            
                <table id="tbl1" runat="server" style="border:solid 1px #CCCCCC;border-collapse:collapse">
                    <tr>
                        <td height="100px" width="100px" align="center">                            
                            <asp:Image ID="imgUser1" style="max-height:100%;max-width:90%" runat="server" imageurl='<%#  "images/avatar/" & Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "profilePic1"))%>'  ToolTip='<%# "Assigned Date: " & Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "assigneddate1"))%>'/>
                        </td>                        
                    </tr>
                    <tr>                        
                        <td align="left" style="font-size:8pt;border:solid 1px #CCCCCC;border-collapse:collapse;background-color:#EEEEEE" nowrap>
                            <asp:Literal ID="lName1" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "userName1"))%>'></asp:Literal>
                        </td>
                    </tr>
                    <tr>                        
                        <td align="left" style="font-size:8pt;border:solid 1px #CCCCCC;border-collapse:collapse;background-color:#EEEEEE" nowrap>
                            <b>Action:</b> <asp:Literal ID="lAct1" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "description1"))%>'></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="font-size:8pt;border:solid 1px #CCCCCC;border-collapse:collapse;background-color:#EEEEEE"  title='<%# "Action Date: " & Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "actiondate1"))%>'>
                            <b>Duration:</b> <asp:Literal ID="lDuration1" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Duration1"))%>'></asp:Literal>
                        </td>
                    </tr>
                </table>                
            </td>                   
        </tr>
        
</AlternatingItemTemplate>


<FooterTemplate>        
</table>    
</FooterTemplate>
</asp:Repeater>

</asp:Panel>
</td>
</tr>
</table>
</asp:Panel>