﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlAddHoliday.ascx.vb" Inherits="dms.UserControlAddHoliday" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
  
<asp:Panel ID="imprt" runat="server" DefaultButton="lbSave">
<div style="width:100%;" align="center">

<table style="border:solid 0px #D4D4D4;width:98%;text-align:left;margin-bottom:5px;">
    <tr><td class="tblHeader2" colspan="3">Add Holiday</td></tr>
    <tr><td class="labelFreeForm">Select Group</td>
            <td>
                
                <asp:DropDownList ID="dlGroup" runat="server" AutoPostBack="true">
                </asp:DropDownList>
    </td>
            <td>
                <asp:Button ID="lbSave" runat="server" CssClass="btnsmall" Text="Save" />
                <asp:Button ID="lbCancel" runat="server" CssClass="btnsmall" Text="Cancel" />
        </td>
        </tr>

    <tr><td class="labelFreeForm">Holiday</td>
            <td>
                <asp:TextBox ID="txHoliday" maxlength="10" runat="server" 
                    ClientIDMode="Static" Width="60px"  cssclass="entryfldw"></asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" 
                    TargetControlID="txHoliday" PopupPosition="BottomLeft"/>
    </td>
            <td>
                &nbsp;</td>
        </tr>

    <tr><td class="labelFreeForm" >Description</td>
            <td colspan="2">
                <asp:TextBox ID="txDesc" runat="server" cssclass="entryfldw" width="400px" 
                    maxlength="200" ></asp:TextBox>
    </td>
            
        </tr>
        <tr>
        <td colspan="3">        
            <asp:Label ID="lMsg"  visible="false" cssclass="msg_red" runat="server" Text=""></asp:Label>        
        </td>
        </tr>
</table>

</div>
</asp:Panel>
                                                             

