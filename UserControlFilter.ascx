<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlFilter.ascx.vb" Inherits="dms.UserControlFilter" %>
        <%--<div style="border:solid 1px #9DB5CD;background-color: #F5F5F5; width: 98%; margin-top: 8px; margin-left: 1px">--%>
        <div style="border-style: solid; border-width: 1px; border-color: #F1F4F8 #CFDBE7 #81A0C0 #CEDAE8; background-color: #FFFFFF; width: 98%; margin-top: 8px; margin-left: 1px">
                    <asp:UpdatePanel ID="pnlFilter" runat="server" UpdateMode="Conditional">
                       <ContentTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" class="newtblheader" >
                            <tr height="25px">
                                <td align="left"  class="tableHead27">
                                    <img alt="" width="20px" Height="24px" src="images/searchdoc2_h.png" />&nbsp;&nbsp;<asp:Label ID="lbBookMrk" runat="server" style="color:#537598;font-family:arial;font-size:8pt;font-weight:bold">FILTER RECORDS</asp:Label></td>
                                    <td width="50px" align="right" valign="top" class="tableHead27" >
                                        <asp:ImageButton ID="imgBk" runat="server" imageurl="images/hidepanel.png"/></td>
                            </tr>
                        </table>
                    
                    <asp:Panel runat="server" ID="pFilter"> 
                    <table border="0" cellpadding="4" cellspacing="0" width="100%" style="border:solid 1px gray">
                            <tr>
                                <td align="left" colspan="2" class="brdrhdr2">&nbsp;&nbsp;FILTER CRITERIA</td>                                      
                                <td align="right" class="brdrhdr2"><img src="images/close.png" width="22px" Height="22px" alt="Close Filter" onmouseover="this.src='images/close_h.png'"  onclick="showMenu2('pSearchCriteria')" onmouseout="this.src='images/close.png'" style="cursor:hand"/></td>
        
                            </tr>        
                            
                           
                            <tr>
                                <td align="left" class="labelFreeForm">
                                    Document Type:</td>
                                <td align="left">                                    
                                    <asp:DropDownList ID="dlFilterDocType" runat="server" cssclass="entryfld2" Width="250px"></asp:DropDownList>
                                </td>
            
                                <td align="left">
                                    &nbsp;</td>
            
                            </tr>
                            <tr>
                                <td align="left"  class="labelFreeForm">
                                    Document Status:</td>
                                <td align="left">                                    
                                    <asp:DropDownList ID="dlFilterDocStatus" runat="server" cssclass="entryfld2" Width="250px"></asp:DropDownList>
                                </td>
            
                                <td align="left">
                                    &nbsp;</td>
            
                            </tr>         
                             <tr>
                                <td align="left"  class="labelFreeForm">
                                    Title:</td>
                                <td align="left">
                                    <asp:TextBox ID="tbFilterTitle" runat="server"  MaxLength="100" Width="245px"  cssclass="entryfld"></asp:TextBox>
                                </td>
            
                                <td align="left">
                                    &nbsp;</td>
            
                            </tr>                   
                            <tr>
                                <td align="left"  class="labelFreeForm">
                                    Author:</td>
                                <td align="left">
                                    <asp:TextBox ID="txAuthor" runat="server"  MaxLength="100" Width="245px"  cssclass="entryfld"></asp:TextBox>
                                </td>
            
                                <td align="left">
                                    &nbsp;</td>
            
                            </tr>  
                            <tr>
                                <td align="left"  class="labelFreeForm">
                                    Date Created:</td>
                                <td align="left">
                                    <asp:TextBox ID="txDateCreated" runat="server"  MaxLength="100" Width="67px"  cssclass="entryfld"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txDateCreated" />
                                </td>
            
                                <td align="left">
                                    &nbsp;</td>
            
                            </tr>  

                            <tr>
                                <td align="left">
                                    &nbsp;</td>
                                <td align="right">
                                    <asp:Button ID="btSearch" runat="server" CssClass="btn" Text="Filter" />
                                </td>
            
                                <td align="left">
                                    &nbsp;</td>
            
                            </tr>                            
                        </table>
                    </asp:Panel>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                </div>