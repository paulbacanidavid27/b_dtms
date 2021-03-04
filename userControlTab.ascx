<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="userControlTab.ascx.vb" Inherits="dms.userControlTab" %>

<table border="0" cellpadding="0" cellspacing="0" width="100%" style="border-collapse: collapse; background-color: #FFFFFF">
    <tr>
        <td align="left">
            <table cellspacing="0" cellpadding="0" border="0" style="border-collapse: collapse; font-family: Verdana; font-size: 12px; font-weight: normal; background-color: #FFFFFF">
                <tr align="center">
                    <td width="30px"></td>
                    <td id='t10' runat="server" class="l"></td>
                    <td id='t11' runat="server" class="m">
                        <asp:LinkButton ID="lbHome" runat="server" Visible="true" class="menu2">Home</asp:LinkButton>
                        <asp:LinkButton ID="lbHome2" runat="server" Visible="false" class="menu">Home</asp:LinkButton>
                        <asp:Label ID="lHome" runat="server" Text="Home" Visible="false"></asp:Label>
                    </td>
                    <td id='t12' runat="server" class="r"></td>
                    <asp:Panel ID="pDashboard" runat="server" Visible="false">
                    <td id='t20' runat="server" visible="true" class="l"></td>
                    <td id='t21' runat="server" class="m" visible="true">
                        <asp:LinkButton ID="lbDashBoard" runat="server" class="menu2" Visible="true">Dashboard</asp:LinkButton>
                        <asp:Label ID="lDashboard" runat="server" Text="Dashboard" Visible="false"></asp:Label>
                    </td>
                    <td id='t22' runat="server" class="r" visible="true"></td>
                    </asp:Panel>
                    <td id='t30' runat="server" class="l"></td>
                    <td id='t31' runat="server" class="m">
                        <asp:LinkButton ID="lbDocuments" runat="server" class="menu2">Documents</asp:LinkButton>
                        <asp:Label ID="lDocuments" runat="server" Text="Documents" Visible="false"></asp:Label>
                    </td>
                    <td id='t32' runat="server" class="r"></td>
                    <asp:Panel ID="pFormsIssuances" runat="server" Visible="false">
                        <td id='t40' runat="server" class="l" visible="true"></td>
                        <td id='t41' runat="server" class="m" visible="true">
                            <asp:LinkButton ID="lbBookmarked" runat="server" class="menu2">Forms</asp:LinkButton>
                            <asp:Label ID="lBookmarked" runat="server" Text="Forms" Visible="false"></asp:Label>
                        </td>
                        <td id='t42' runat="server" class="r" visible="true"></td>
                        <%--<td id='Td10' runat="server" class="l" visible="true"></td>
                        <td id='Td11' runat="server" class="m" visible="true">
                            <asp:LinkButton ID="lbIssuances" runat="server" class="menu2">Issuances</asp:LinkButton>
                            <asp:Label ID="lIssuances" runat="server" Text="Issuances" Visible="false"></asp:Label>
                        </td>
                        <td id='Td12' runat="server" class="r" visible="true"></td>--%>
                    </asp:Panel>
                    <asp:Panel ID="pReport" runat="server" Visible="true">
                        <td id='t50' runat="server" class="l"></td>
                        <td id='t51' runat="server" class="m">
                            <asp:LinkButton ID="lbReports" runat="server" class="menu2">Reports</asp:LinkButton>
                            <asp:Label ID="lReports" runat="server" Text="Reports" Visible="false"></asp:Label>
                        </td>
                        <td id='t52' runat="server" class="r"></td>
                    </asp:Panel>
                </tr>
            </table>
        </td>
        <td align="right" style="padding-right: 20px; background-color: #FFFFFF">
            <table cellspacing="0" cellpadding="0" border="0" style="font-family: Verdana; font-size: 12px; font-weight: normal; background-color: #FFFFFF">
                <tr align="center">
                    <asp:Panel ID="pAdmin" runat="server" Visible="false">
                        <td id='Td1' runat="server" class="lg"></td>
                        <td id='Td2' runat="server" class="mg">
                            <asp:LinkButton ID="lnkAdmin" runat="server" Visible="true" class="menu2">Admin</asp:LinkButton>
                            <asp:Label ID="lblAdmin" runat="server" Text="Admin" Visible="false"></asp:Label>
                        </td>
                        <td id='Td3' runat="server" class="rg"></td>
                    </asp:Panel>
                    <asp:Panel ID="pImport" runat="server" Visible="false">
                        <td id='Td7' runat="server" class="lg"></td>
                        <td id='Td8' runat="server" class="mg">
                            <asp:LinkButton ID="lbImport" runat="server" Visible="true" class="menu2">Import</asp:LinkButton>
                            <asp:Label ID="lImport" runat="server" Text="Import" Visible="false"></asp:Label>
                        </td>
                        <td id='Td9' runat="server" class="rg"></td>
                    </asp:Panel>                    
                    <asp:Panel ID="pSearch" runat="server" Visible="false">
                    <td id='Td4' runat="server" class="lg"></td>
                    <td id='Td5' runat="server" class="mg">
                        <asp:LinkButton ID="lnkSearch" runat="server" Visible="true" class="menu2">Search</asp:LinkButton>
                        <asp:Label ID="lblSearch" runat="server" Text="Search" Visible="false"></asp:Label>
                    </td>
                    <td id='Td6' runat="server" class="rg"></td>
                    </asp:Panel>
                    <asp:Panel ID="pTracking" runat="server" Visible="false">
                        <td id='Td16' runat="server" class="lg"></td>

                        <td id='Td17' runat="server" class="mg">
                            <asp:LinkButton ID="lnkTrack" runat="server" Visible="true" class="menu2">Tracking</asp:LinkButton>
                            <asp:Label ID="lblTrack" runat="server" Text="Tracking" Visible="false"></asp:Label>
                        </td>
                        <td id='Td18' runat="server" class="rg"></td>
                    </asp:Panel>
                    <asp:Panel ID="pHoliday" runat="server" Visible="false">
                        <td id='Td19' runat="server" class="lg"></td>

                        <td id='Td20' runat="server" class="mg">
                            <asp:LinkButton ID="lbHoliday" runat="server" Visible="true" class="menu2">Holidays</asp:LinkButton>
                            <asp:Label ID="lHoliday" runat="server" Text="Holidays" Visible="false"></asp:Label>
                        </td>
                        <td id='Td21' runat="server" class="rg"></td>
                    </asp:Panel>
                </tr>
            </table>

        </td>
    </tr>
</table>
