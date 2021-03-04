<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucTrackStatus.ascx.vb" Inherits="dms.ucTrackStatus" %>
<%@ Register src="ucTrackStatusUsers.ascx" tagname="ucTrackStatusUsers" tagprefix="uc" %>
<uc:ucTrackStatusUsers id="ucSTU" runat="server" visible="False"></uc:ucTrackStatusUsers>
<asp:Repeater ID="rptTrackStatus" runat="server">
<HeaderTemplate>
    <table cellpadding="0" cellspacing="6" border="0" >
        
</HeaderTemplate>
<ItemTemplate>
        <tr id="trow" runat="server">
            <td style="padding:4px" valign="top" height="110px" align="left" runat="server">            
                <table >
                    <tr>
                        <td  height="100px" width="100px" align="center" style='padding-right:2px;padding-left:2px;border:solid 1px <%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "BorderColor1"))%>;background-color:<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TrackingColor1"))%>;'>
                            <asp:LinkButton ID="lIGroup1" runat="server" ValidationGroup='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Group1"))%>'  style='<%# "font-size:15pt;color:" & Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TextColor1")) %>' Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "GroupName1"))%>'  OnClick="ShowUsers"></asp:LinkButton>
                            <asp:Literal ID="lGrp1" visible="false" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Group1"))%>'></asp:Literal>
                            <asp:Literal ID="lSSeqNo1" visible="false" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "StartSeqNo1"))%>'></asp:Literal>
                            <asp:Literal ID="lESeqNo1" visible="false" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "EndSeqNo1"))%>'></asp:Literal>
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
                
                <table >
                    <tr>
                        <td  height="100px" width="100px" align="center" style='padding-right:2px;padding-left:2px;border:solid 1px <%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "BorderColor2"))%>;background-color:<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TrackingColor2"))%>'>
                            <asp:LinkButton ID="lIGroup2" runat="server"  validationGroup ='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Group2"))%>' style='<%# "font-size:15pt;color:" & Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TextColor2"))%>' Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "GroupName2"))%>'  OnClick="ShowUsers"></asp:LinkButton>
                            <asp:Literal ID="lGrp2" visible="false" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Group2"))%>'></asp:Literal>
                            <asp:Literal ID="lSSeqNo2" visible="false" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "StartSeqNo2"))%>'></asp:Literal>
                            <asp:Literal ID="lESeqNo2" visible="false" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "EndSeqNo2"))%>'></asp:Literal>
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
                <table >
                    <tr>
                        <td  height="100px" width="100px" align="center" style='padding-right:2px;padding-left:2px;border:solid 1px <%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "BorderColor3"))%>;background-color:<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TrackingColor3"))%>;'>
                            <asp:LinkButton ID="lIGroup3" runat="server" validationGroup ='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Group3"))%>' style='<%# "font-size:15pt;color:" & Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TextColor3"))%>' Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "GroupName3"))%>' OnClick="ShowUsers"></asp:LinkButton>
                            <asp:Literal ID="lGrp3" visible="false" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Group3"))%>'></asp:Literal>
                            <asp:Literal ID="lSSeqNo3" visible="false" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "StartSeqNo3"))%>'></asp:Literal>
                            <asp:Literal ID="lESeqNo3" visible="false" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "EndSeqNo3"))%>'></asp:Literal>
                        </td>                        
                    </tr>
                </table>                
            </td>       
            <td valign="top">
                <table >
                    <tr>
                        <td  height="100px" valign="middle">
                            <asp:Image ID="imgIArrow3" runat="server" imageurl="images/track_rarrow.png" />
                        </td>                        
                    </tr>
                </table>                                
            </td> 
            <td style="padding:4px" valign="top" height="110px" align="left">            
                <table>
                    <tr>
                        <td  height="100px" width="100px" align="center" style='padding-right:2px;padding-left:2px;border:solid 1px <%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "BorderColor4"))%>;background-color:<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TrackingColor4"))%>;'>
                            <asp:LinkButton ID="lIGroup4" runat="server" validationGroup ='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Group4"))%>' style='<%# "font-size:15pt;color:" & Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TextColor4"))%>' Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "GroupName4"))%>'  OnClick="ShowUsers"></asp:LinkButton>
                            <asp:Literal ID="lGrp4" visible="false" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Group4"))%>'></asp:Literal>
                            <asp:Literal ID="lSSeqNo4" visible="false" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "StartSeqNo4"))%>'></asp:Literal>
                            <asp:Literal ID="lESeqNo4" visible="false" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "EndSeqNo4"))%>'></asp:Literal>
                        </td>                        
                    </tr>
                    <tr>
                        <td width="60px" align="center" runat="server">
                            <asp:Image ID="imgIArrow4" runat="server" imageurl="images/track_darrow.png" />
                        </td>                        
                    </tr>
                </table>                
            </td>       
            
        </tr>
       
</ItemTemplate>
<AlternatingItemTemplate>
        <tr id="trow" runat="server">
            <td style="padding:4px" valign="top" height="110px" align="left">            
                <table >
                    <tr>
                        <td  height="100px" width="100px" align="center" style='padding-right:2px;padding-left:2px;border:solid 1px <%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "BorderColor1"))%>;background-color:<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TrackingColor1"))%>;'>
                            <asp:LinkButton ID="lIGroup1" runat="server" validationGroup ='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Group1"))%>' style='<%# "font-size:15pt;color:" & Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TextColor1"))%>' Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "GroupName1"))%>'  OnClick="ShowUsers"></asp:LinkButton>
                            <asp:Literal ID="lGrp1" visible="false" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Group1"))%>'></asp:Literal>
                            <asp:Literal ID="lSSeqNo1" visible="false" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "StartSeqNo1"))%>'></asp:Literal>
                            <asp:Literal ID="lESeqNo1" visible="false" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "EndSeqNo1"))%>'></asp:Literal>
                        </td>                        
                    </tr>
                    <tr>
                        <td width="60px" align="center">
                            <asp:Image ID="imgIArrow1" runat="server" imageurl="images/track_darrow.png" />
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
                <table >
                    <tr>
                        <td  height="100px" width="100px" align="center" style='padding-right:2px;padding-left:2px;border:solid 1px <%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "BorderColor2"))%>;background-color:<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TrackingColor2"))%>'>
                            <asp:LinkButton ID="lIGroup2" runat="server" validationGroup ='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Group2"))%>' style='<%# "font-size:15pt;color:" & Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TextColor2"))%>' Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "GroupName2"))%>'  OnClick="ShowUsers"></asp:LinkButton>
                            <asp:Literal ID="lGrp2" visible="false" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Group2"))%>'></asp:Literal>
                            <asp:Literal ID="lSSeqNo2" visible="false" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "StartSeqNo2"))%>'></asp:Literal>
                            <asp:Literal ID="lESeqNo2" visible="false" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "EndSeqNo2"))%>'></asp:Literal>
                        </td>                        
                    </tr>
                </table>                
            </td>                    
            <td valign="top">
                <table >
                    <tr>
                        <td  height="100px" valign="middle">
                            <asp:Image ID="imgIArrow3" runat="server" imageurl="images/track_larrow.png" />
                        </td>                        
                    </tr>
                </table>                                
            </td> 
            <td style="padding:4px" valign="top" height="110px" align="left">            
                <table >
                    <tr>
                        <td  height="100px" width="100px" align="center" style='padding-right:2px;padding-left:2px;border:solid 1px <%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "BorderColor3"))%>;background-color:<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TrackingColor3"))%>;'>
                            <asp:LinkButton ID="lIGroup3" runat="server" validationGroup ='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Group3"))%>' style='<%# "font-size:15pt;color:" & Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TextColor3"))%>' Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "GroupName3"))%>'  OnClick="ShowUsers"></asp:LinkButton>
                            <asp:Literal ID="lGrp3" visible="false" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Group3"))%>'></asp:Literal>
                            <asp:Literal ID="lSSeqNo3" visible="false" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "StartSeqNo3"))%>'></asp:Literal>
                            <asp:Literal ID="lESeqNo3" visible="false" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "EndSeqNo3"))%>'></asp:Literal>
                        </td>                        
                    </tr>
                </table>                
            </td>       
            
            <td valign="top">
                <table >
                    <tr>
                        <td  height="100px" valign="middle">
                            <asp:Image ID="imgIArrow4" runat="server" imageurl="images/track_larrow.png"/>
                        </td>                        
                    </tr>
                </table>                                
            </td> 
            <td style="padding:4px" valign="top" height="110px" align="left">            
                <table >
                    <tr>
                        <td  height="100px" width="100px" align="center" style='padding-right:2px;padding-left:2px;border:solid 1px <%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "BorderColor4"))%>;background-color:<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TrackingColor4"))%>;'>
                            <asp:LinkButton ID="lIGroup4" runat="server" validationGroup ='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Group4"))%>' style='<%# "font-size:15pt;color:" & Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TextColor4"))%>' Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "GroupName4"))%>'  OnClick="ShowUsers"></asp:LinkButton>
                            <asp:Literal ID="lGrp4" visible="false" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Group4"))%>'></asp:Literal>
                            <asp:Literal ID="lSSeqNo4" visible="false" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "StartSeqNo4"))%>'></asp:Literal>
                            <asp:Literal ID="lESeqNo4" visible="false" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "EndSeqNo4"))%>'></asp:Literal>
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

