<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlWorkSched.ascx.vb" Inherits="dms.UserControlWorkSched" %>
<%@ Register src="UserControlCheckBox.ascx" tagname="UserControlCheckBox" tagprefix="uc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<table style="border:solid 1px #D4D4D4;border-collapse:collapse" cellspacing="0" cellpadding="0" border="0">
    <tr>
            <td colspan="4" align="left" style="font-size:10px;font-weight:bold;padding:3px;background-color:#CCCCCC; ">Work Schedule</td>
    </tr>
    <tr>
            <td class="tableHead" colspan="4" align="left">
                <table width="100%">
                <tr>
                    <td width="15px">
                        <asp:Literal ID="lGroupId" runat="server" Visible="false"></asp:Literal>
                    <uc1:UserControlCheckBox ID="ucAlwaysAllowed" runat="server" />
                    </td>
                    <td style="font-weight:normal;font-size:10px">Always Allowed
                    </td>
                    <td align="right">
                    <asp:ImageButton ID="imgSave" runat="server" imageurl="images/saveicon.png" ToolTip="Save Changes"/></td>
                
                </tr>
                </table> 
                </td>
                
                    
        </tr>
        <tr>
            <td class="newtblheader"></td><td class="newtblheader">Week Days</td><td align="center"  class="newtblheader">Start Time</td><td  class="newtblheader" align="center">End Time</td>
        </tr>
<asp:Repeater ID="rptWeekdays" runat="server">
    <HeaderTemplate>
    
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
        <td style="padding:2px">
            <asp:Literal ID="lwd" runat="server" Visible="false" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "WeekDay"))%>'></asp:Literal>
            <asp:Literal ID="lchk" runat="server" Visible="false" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "chk"))%>'></asp:Literal>
            <uc1:UserControlCheckBox ID="ucCheck" runat="server" One_check2="enableTime" Visible="true"/>            
        </td>
        <td style="padding:2px" align="left"><asp:Label ID="lDayName" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DayName"))%>'></asp:Label>
            </td>
        <td style="padding:2px">
            
            <asp:TextBox ID="txtStart" runat="server" width="60px" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ST"))%>' Enabled="false"></asp:TextBox>
            <cc1:MaskedEditExtender id="MeExt" runat="server" TargetControlID="txtStart"  AcceptAMPM="true" MaskType="Time" Mask="99:99" InputDirection="LeftToRight" AcceptNegative="None"/>
        </td>
        <td style="padding:2px">
            <asp:TextBox ID="txtEnd" runat="server" width="60px"  Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ET"))%>'  Enabled="false"></asp:TextBox>
            <cc1:MaskedEditExtender id="MaskedEditExtender1" runat="server" TargetControlID="txtEnd"  AcceptAMPM="true" MaskType="Time"  Mask="99:99" InputDirection="LeftToRight" AcceptNegative="None"/>
        </td>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
        </table>
    </FooterTemplate>
</asp:Repeater>
<asp:UpdatePanel ID="pnlMsg" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Label ID="msg" runat="server" cssclass="msg_red">&nbsp;</asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
<%--</ContentTemplate>
</asp:UpdatePanel>--%>

    
    
