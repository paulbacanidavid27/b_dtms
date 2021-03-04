<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucDailyStorageConsumption.ascx.vb" Inherits="dms.ucDailyStorageConsumption" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<%--<%@ Register src="ucTableDocUploaded.ascx" tagname="uctabledocuploaded" tagprefix="uc" %>--%>

<asp:UpdatePanel ID="pTotalDocuments" class="panel panel-default" runat="server" EnableViewState="true" UpdateMode="Conditional">
<ContentTemplate>
            <asp:UpdatePanel runat="server" ID="pnlCriteria" class="col-lg-5 col-md-6 col-x2-12 col-sm-12"  UpdateMode="Conditional">
    <ContentTemplate>
                
                    <table>
                    <tr>
                        <td>Report Type:</td><td colspan="4">
                            <asp:DropDownList ID="dlType" runat="server" AutoPostBack="true" cssclass="entryfld2">
                            <asp:ListItem Value="D">Daily</asp:ListItem>
                            <asp:ListItem Value="M">Monthly</asp:ListItem>
                            <%--<asp:ListItem Value="Y">Year</asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr><td>Date Range: </td><td>

                    <asp:TextBox ID="tbDCFrom" runat="server"   Width="100px" cssclass="entryfld2" Placeholder="Start Date"></asp:TextBox>
                    <asp:DropDownList ID="dlMonthFrom" runat="server" Visible="false" cssclass="entryfld2">
                            <asp:ListItem Value="1">January</asp:ListItem>
                            <asp:ListItem Value="2">February</asp:ListItem>
                            <asp:ListItem Value="3">March</asp:ListItem>
                            <asp:ListItem Value="4">April</asp:ListItem>
                            <asp:ListItem Value="5">May</asp:ListItem>
                            <asp:ListItem Value="6">June</asp:ListItem>
                            <asp:ListItem Value="7">July</asp:ListItem>
                            <asp:ListItem Value="8">August</asp:ListItem>
                            <asp:ListItem Value="9">Spetember</asp:ListItem>
                            <asp:ListItem Value="10">October</asp:ListItem>
                            <asp:ListItem Value="11">November</asp:ListItem>
                            <asp:ListItem Value="12">December</asp:ListItem>
                            </asp:DropDownList>
                    <asp:TextBox ID="tbYearFrom" runat="server"  Visible="false" cssclass="entryfld" Width="50px" place="Year"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="tbDCFrom" />
                                                </td><td>
                                                -
                                                </td><td>
                                                <asp:TextBox ID="tbDCTo" runat="server"   cssclass="entryfld" Width="100px" Placeholder="End Date"></asp:TextBox>
                                                <asp:DropDownList ID="dlMonthTo" runat="server" cssclass="entryfld2" Visible="false"> 
                            <asp:ListItem Value="1">January</asp:ListItem>
                            <asp:ListItem Value="2">February</asp:ListItem>
                            <asp:ListItem Value="3">March</asp:ListItem>
                            <asp:ListItem Value="4">April</asp:ListItem>
                            <asp:ListItem Value="5">May</asp:ListItem>
                            <asp:ListItem Value="6">June</asp:ListItem>
                            <asp:ListItem Value="7">July</asp:ListItem>
                            <asp:ListItem Value="8">August</asp:ListItem>
                            <asp:ListItem Value="9">Spetember</asp:ListItem>
                            <asp:ListItem Value="10">October</asp:ListItem>
                            <asp:ListItem Value="11">November</asp:ListItem>
                            <asp:ListItem Value="12">December</asp:ListItem>
                            </asp:DropDownList>
                                                <asp:TextBox ID="tbYearTo" cssclass="entryfld" runat="server"  Visible="false"  Width="50px" place="Year"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="tbDCTo"/>
                    </td><td>
                    <asp:Button ID="btSearch" runat="server" CssClass="btn" Text="Preview" style="margin:3px 3px 3px 3px"/>
                        <asp:Button ID="btUpdate" runat="server" CssClass="btn" Text="Update" style="margin:3px 3px 3px 3px"/>

                        </td></tr></table>
                
                </ContentTemplate>
                
                </asp:UpdatePanel>
                    <hr />
                
        <table style="border:solid 1px #222222">
        <tr>
        <td>Total Files For the Selected Criteria:</td>
        <td align="right">
            <b><asp:Label ID="lFiles" runat="server" Text=""></asp:Label></b>
        </td>
        </tr>
        <tr>
        <td>Average Files Uploaded For the Selected Criteria:</td>
        <td align="right">
            <b><asp:Label ID="lAveFiles" runat="server" Text=""></asp:Label></b>
        </td>
        </tr>
        <tr>
        <td>Total File Size For the Selected Criteria:</td>
        <td align="right">
            <b><asp:Label ID="lTotalBytes" runat="server" Text=""></asp:Label></b>
        </td>
        </tr>
        
        
        <tr>
        <td>Average File Size Uploaded For the Selected Criteria:</td>
        <td align="right">
            <b><asp:Label ID="lAveBytes" runat="server" Text=""></asp:Label></b>
        </td>
        </tr>
        <tr>
        <td>Current Disk Space:</td>
        <td align="right">
            <b><asp:Label ID="lFreeSpace" runat="server" Text=""></asp:Label></b>
        </td>
        </tr>
        <tr>
        <td>Days Until Disk Space Run-out:</td>
        <td align="right">
            <b><asp:Label ID="lDaysLeft" runat="server" Text=""></asp:Label></b>
        </td>
        </tr>
        </table>
    <asp:UpdatePanel runat="server" ID="pnlGraphStatus" class="col-lg-5 col-md-6 col-x2-12 col-sm-12"  UpdateMode="Conditional">
    <ContentTemplate>
        <asp:chart id="Chart1" runat="server" BackColor="WhiteSmoke"   EnableViewState="true" BackSecondaryColor="White" BackGradientStyle="TopBottom" BorderlineDashStyle="Solid" Palette="BrightPastel" BorderColor="26, 59, 105" Height="296px" Width="900px" BorderWidth="2" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)">
    
							<legends>
								<asp:Legend IsTextAutoFit="False" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold" ></asp:Legend>
							</legends>
                            <Titles>
                                <asp:Title Font="Calibri, 11pt, style=Bold" Name="Title1" 
                                    Text="Total Files and Bytes Uploaded ">
                                </asp:Title>
                            </Titles>
							<borderskin SkinStyle="Emboss"></borderskin>
                            <Series>
                                <asp:Series Name="File Size (KB)" IsValueShownAsLabel="true" LabelBackColor="White" LabelForeColor="Blue" >                
                                </asp:Series>
                                <asp:Series Name="No of Files" IsValueShownAsLabel="true" LabelBackColor="White" LabelForeColor="Green" >                
                                </asp:Series>
                            </Series>
                            
							<chartareas>
								<asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" BackSecondaryColor="White" BackColor="Gainsboro" ShadowColor="Transparent" BackGradientStyle="TopBottom">
									<area3dstyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="False" WallWidth="0" IsClustered="False" />
									<axisy LineColor="64, 64, 64, 64" IsLabelAutoFit="False">
										<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
										<MajorGrid LineColor="64, 64, 64, 64" />
									</axisy>
									<axisx LineColor="64, 64, 64, 64" interval="1" >
										<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" IntervalType="Auto" />
										<MajorGrid Interval="Auto" IntervalType="Auto" LineColor="64, 64, 64, 64" />
										<MajorTickMark IntervalType="Auto"/>
									</axisx>
								</asp:ChartArea>

							</chartareas>
						</asp:chart>
    </ContentTemplate>
    </asp:UpdatePanel>
    
      
    
</ContentTemplate>
</asp:UpdatePanel>

