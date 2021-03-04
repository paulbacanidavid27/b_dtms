<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucDashboardGraph.ascx.vb" Inherits="dms.ucDashboardGraph" %>
<%@ Register src="UserControlPager.ascx" tagname="UserControlPager" tagprefix="uc2" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<asp:UpdatePanel ID="pDB" runat="server" UpdateMode="Conditional">
<ContentTemplate>
<div style="width:100%;border:solid 1px #222222;color:#000000;background-color:#D5D5D5;padding:2px;margin-bottom:3px;">
Total Documents Received:<asp:Literal ID="ltdr" runat="server"></asp:Literal>
</div>
<div style="width:100%;border:solid 1px #222222;color:#000000;background-color:#D5D5D5;padding:2px;margin-bottom:3px;">
Action Documents Required:<asp:Literal ID="ladr" runat="server"></asp:Literal> 
</div>    
<div style="width:95%;border:solid 1px #222222;color:#000000;background-color:#D5D5D5;padding:2px;margin-bottom:3px;">
No Action Documents Required:<asp:Literal ID="lnadr" runat="server"></asp:Literal>
</div>
<div style="width:100%;border:solid 1px #222222;color:#000000;background-color:#D5D5D5;padding:2px;margin-bottom:3px;">
Pending Documents:<asp:Literal ID="lpd" runat="server"></asp:Literal>
</div>    
<div style="width:100%;border:solid 1px #222222;color:#000000;background-color:#D5D5D5; padding:2px;margin-bottom:3px;">
Completed Documents:<asp:Literal ID="lcd" runat="server"></asp:Literal>
</div>
 </ContentTemplate>
</asp:UpdatePanel>

<asp:Chart ID="Chart1" runat="server">
    <series>
        <asp:Series Name="Series1">
        </asp:Series>
    </series>
    <chartareas>
        <asp:ChartArea Name="ChartArea1">
        </asp:ChartArea>
    </chartareas>
</asp:Chart>


