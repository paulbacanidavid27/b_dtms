<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucDashboardCount.ascx.vb" Inherits="dms.ucDashboardCount" %>
<%@ Register src="UserControlPager.ascx" tagname="UserControlPager" tagprefix="uc2" %>
<asp:UpdatePanel ID="pDB" runat="server" UpdateMode="Conditional">
<ContentTemplate>
<asp:HiddenField ID="hfStartDate" runat="server" Value=""/>
<asp:HiddenField ID="hfEndDate" runat="server" Value=""/>
<asp:HiddenField ID="hfPersonnelInCharge" runat="server" Value=""/>
<asp:HiddenField ID="hfSubject" runat="server" Value=""/>
<asp:HiddenField ID="hfRefNo" runat="server" Value=""/>
<asp:HiddenField ID="hfDueDate" runat="server" Value=""/>
<asp:HiddenField ID="hfStatus" runat="server" Value=""/>
<asp:HiddenField ID="hfOfcCode" runat="server" Value=""/>
<asp:HiddenField ID="hfAction" runat="server" Value=""/>


<table border="0" class="codetbl" style="height:24px;width:100%;border:solid 1px #222222;border-collapse:collapse;margin-bottom:2px;" cellspacing="0">
<tr>
<td class="newtblheader">Total Documents Received:</td>
<td class="newtblheaderl" align="right"> <asp:Literal ID="ltdr" runat="server"></asp:Literal>
</td>
<td class="newtblheader" align="right">
    <asp:ImageButton ID="imgShowAction" runat="server" ImageUrl="images/showpanel.png" />
</td>
</tr>
    <asp:Panel ID="pnlAction" runat="server" Visible="false">
    <tr>
<td class="newtblheader2" style="padding-left:15px;">Action Documents Required:</td>
<td class="newtblheader2" align="right"><asp:Literal ID="ladr" runat="server"></asp:Literal></td>
<td class="newtblheader2"></td>
</tr>
<tr>
<td class="newtblheader2" style="padding-left:15px;">No Action Documents Required: </td>
<td class="newtblheader2" align="right"><asp:Literal ID="lnadr" runat="server"></asp:Literal>
</td>
<td class="newtblheader2"></td>
</tr>
    </asp:Panel>
</table>
<table border="0" class="codetbl" style="height:24px;width:100%;border:solid 1px #222222;border-collapse:collapse;margin-bottom:2px;" cellspacing="0">
<tr>
<td class="newtblheader">Pending</td>
<td class="newtblheaderl"><asp:Literal ID="lpd" runat="server"></asp:Literal></td>
<td class="newtblheader">Oldest</td>
<td class="newtblheaderl">
    <asp:LinkButton ID="lbopd" runat="server" style="color:#222222"><asp:Literal ID="lopd" runat="server"></asp:Literal></asp:LinkButton></td>
<td class="newtblheader">Newest</td>
<td class="newtblheaderl"><asp:LinkButton ID="lbnpd" runat="server" style="color:#222222"><asp:Literal ID="lnpd" runat="server"></asp:Literal></asp:LinkButton></td>
<td class="newtblheader">AVG</td>
<td class="newtblheaderl"><asp:Literal ID="lapd" runat="server"></asp:Literal></td>
</tr>
</table>
<table border="0" class="codetbl" style="height:24px;width:100%;border:solid 1px #222222;border-collapse:collapse;margin-bottom:2px;" cellspacing="0">
<tr>
<td class="newtblheader">Completed</td>
<td class="newtblheaderl"><asp:Literal ID="lcd" runat="server"></asp:Literal></td>
<td class="newtblheader">Longest</td>
<td class="newtblheaderl"><asp:LinkButton ID="lblcd" runat="server"  style="color:#222222"><asp:Literal ID="llcd" runat="server"></asp:Literal></asp:LinkButton></td>
<td class="newtblheader">Quickest</td>
<td class="newtblheaderl"><asp:LinkButton ID="lbqcd" runat="server" style="color:#222222"><asp:Literal ID="lqcd" runat="server"></asp:Literal></asp:LinkButton></td>
<td class="newtblheader">Avg</td>
<td class="newtblheaderl"><asp:Literal ID="lacd" runat="server"></asp:Literal></td>
</tr>
</table>
 </ContentTemplate>
</asp:UpdatePanel>
<asp:HiddenField ID="hfReset" runat="server" value="000000"/>
<asp:UpdatePanel ID="pChartDays" runat="server" UpdateMode="Conditional">
<ContentTemplate>
<table style="width:400px;border:solid 1px #C3C3C3;border-collapse:collapse;border-radius:3px;margin-top:10px;" cellpadding="3">
    <tr>
    <td align="left" style="padding:3px;background-color:#FFFFFF;font-size:10pt;font-weight:bold;">
        <img src="images/graph.png" height="18px" width="15px" style="margin:0px;"/> Ageing of Documents Per Type
    </td>
    <td align="right" valign="top" style="padding:3px;background-color:#FFFFFF;font-size:10pt;font-weight:bold;"><asp:ImageButton ID="imgDays" runat="server" imageurl="images/showpanel.png"/></td>
    </tr>
    <tr runat="server" visible="false" id="trChartDays">
    <td colspan="2">
    This graph shows the average days of completion of documents per document type. The records will be based on the dates defined on Period Covered criteria and selected Office. 
    </td></tr>
    </table>

<table width="100%" runat="server" visible="false" id="tblChartDaysPage">
<tr>
<td align="right" >
    <asp:HiddenField ID="hfCurrentChartDays" runat="server" value="5"/>
    <asp:ImageButton ID="imgChartDaysPrevious" ImageUrl="images/track_larrow.png" runat="server" visible="false" style="margin-right:5px"/><asp:Label ID="lTotalChartDays" runat="server" Text=""></asp:Label>
    <asp:ImageButton ImageUrl="images/track_rarrow.png" ID="imgChartDaysNext" runat="server"  visible="false"  style="margin-left:5px"/>
</td>
</tr>
</table>
<asp:Chart ID="ChartDays" runat="server" Palette="BrightPastel" BackColor="#D3DFF0"  BorderlineColor="#C3C3C3" Height="296px" Width="400px" 
 BorderlineDashStyle="Solid" BackGradientStyle="TopBottom" 
 BorderWidth="2" BorderColor="26, 59, 105" IsSoftShadows="False" visible="false" EnableViewState="true">
    <%--<Titles>
        <asp:Title Font="Calibri, 11pt, style=Bold" Name="Title1" 
            Text="Days of Completion">
        </asp:Title>
    </Titles>--%>
    
    <%--<borderskin   SkinStyle="Emboss" PageColor="#DDF0FA"></borderskin>--%>
    <Series>
        <asp:Series Name="Series1" IsValueShownAsLabel="true" LabelBackColor="White" LabelForeColor="Blue" >        
        
        </asp:Series>
    </Series>
    <ChartAreas>
        <asp:ChartArea  Name="ChartArea1">
            <AxisX LineColor="LightGray" LabelAutoFitStyle="LabelsAngleStep90" ></AxisX>
            <AxisY LineColor="LightGray" LabelAutoFitStyle="LabelsAngleStep90"></AxisY>
            <AxisX2 LineColor="LightGray" LabelAutoFitStyle="LabelsAngleStep90" Title="Days"></AxisX2>
            <AxisY2 LineColor="LightGray" LabelAutoFitStyle="LabelsAngleStep90" Title="Days"></AxisY2>                    
        </asp:ChartArea>
    </ChartAreas>
</asp:Chart>
</ContentTemplate>
<Triggers>
<asp:AsyncPostBackTrigger ControlID="imgDays" EventName="click" /></Triggers>
</asp:UpdatePanel>
<table style="width:400px;border:solid 1px #C3C3C3;border-collapse:collapse;border-radius:3px;margin-top:10px;" cellpadding="3">
    <tr>
    <td align="left" style="padding:3px;background-color:#FFFFFF;font-size:10pt;font-weight:bold;">
        <img src="images/graph.png" height="18px" width="15px" style="margin:0px;"/> Pending Document Count
    </td>
    <td align="right" valign="top" style="padding:3px;background-color:#FFFFFF;font-size:10pt;font-weight:bold;"><asp:ImageButton ID="imgPending" runat="server" imageurl="images/showpanel.png"/></td>
    </tr>
    <tr runat="server" visible="false" id="trChartPending">
    <td colspan="2">
    This graph shows the number of pending documents per document type. The records will be based on the dates defined on Period Covered criteria and selected Office. 
    </td></tr>
    </table>
<asp:UpdatePanel ID="pChartPending" runat="server" UpdateMode="Conditional">
<ContentTemplate>
<table width="100%" runat="server" visible="false" id="tblChartPending">
<tr>
<td align="right" >
    <asp:HiddenField ID="hfChartPending" runat="server" value="5"/>
    <asp:ImageButton ID="imgChartPendingPrevious" ImageUrl="images/track_larrow.png" runat="server" visible="false" style="margin-right:5px"/><asp:Label ID="lChartPending" runat="server" Text=""></asp:Label>
    <asp:ImageButton ImageUrl="images/track_rarrow.png" ID="imgChartPendingNext" runat="server"  visible="false"  style="margin-left:5px"/>
</td>
</tr>
</table>
<asp:Chart ID="ChartPending" runat="server" Palette="BrightPastel" BackColor="#D3DFF0" BorderlineColor="#C3C3C3" Height="296px" Width="400px" 
 BorderlineDashStyle="Solid" BackGradientStyle="TopBottom" 
 BorderWidth="2" BorderColor="26, 59, 105" IsSoftShadows="False" Visible="false" EnableViewState="true">
    <%--<Titles>
        <asp:Title Font="Calibri, 11pt, style=Bold" Name="Title1" 
            Text="PENDING DOCUMENT COUNT">
        </asp:Title>
    </Titles>
    --%>
    <%--<borderskin   SkinStyle="Emboss" PageColor="#DDF0FA"></borderskin>--%>
    <Series>
        <asp:Series Name="Series1" Color="Red" IsValueShownAsLabel="true" LabelBackColor="White" LabelForeColor="Blue" >        
        
        </asp:Series>
    </Series>
    <ChartAreas>
        <asp:ChartArea  Name="ChartArea1">
            <AxisX LineColor="LightGray"   LabelAutoFitStyle="LabelsAngleStep90" ></AxisX>
            <AxisY LineColor="LightGray" LabelAutoFitStyle="LabelsAngleStep90"></AxisY>
            <AxisX2 LineColor="LightGray" LabelAutoFitStyle="LabelsAngleStep90" Title="Days"></AxisX2>
            <AxisY2 LineColor="LightGray" LabelAutoFitStyle="LabelsAngleStep90" Title="Days"></AxisY2>                    
        </asp:ChartArea>
    </ChartAreas>
</asp:Chart>
</ContentTemplate>
<Triggers>
<asp:AsyncPostBackTrigger ControlID="imgPending" EventName="click" /></Triggers>
</asp:UpdatePanel>
<table id="gDistribution" runat="server" style="width:400px;border:solid 1px #C3C3C3;border-collapse:collapse;border-radius:3px;margin-top:10px;" cellpadding="3">
    <tr>
    <td align="left" style="padding:3px;background-color:#FFFFFF;font-size:10pt;font-weight:bold;">
        <img src="images/pie.png" height="18px" width="15px" style="margin:0px;"/> Distribution of Pending Request
    </td>
    <td align="right" valign="top" style="padding:3px;background-color:#FFFFFF;font-size:10pt;font-weight:bold;"><asp:ImageButton ID="imgDistribution" runat="server" imageurl="images/showpanel.png"/></td>
    </tr>
    </table>
<asp:UpdatePanel ID="pChartDistribution" runat="server" UpdateMode="Conditional">
<ContentTemplate>

<%--<asp:CHART id="Chart1" runat="server" Palette="BrightPastel"   BackColor="#D3DFF0" Height="296px" Width="400px" BorderlineDashStyle="Solid" BackGradientStyle="TopBottom" BorderWidth="2" BorderColor="26, 59, 105" IsSoftShadows="False" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" Visible="false"  EnableViewState="true">--%>
<asp:CHART id="ChartDistribution" runat="server" BorderlineColor="#C3C3C3" Palette="BrightPastel"   BackColor="#D3DFF0" Height="296px" Width="400px" BorderlineDashStyle="Solid" BackGradientStyle="TopBottom" BorderWidth="2" BorderColor="26, 59, 105" IsSoftShadows="False" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" Visible="false"  EnableViewState="true">
<%--<Titles>
        <asp:Title Font="Calibri, 11pt, style=Bold" Name="Title1" 
            Text="Distribution of Pending Request">
        </asp:Title>
    </Titles>
--%>							<legends>
								<asp:Legend TitleFont="Microsoft Sans Serif, 8pt, style=Bold" BackColor="Transparent" IsEquallySpacedItems="True" Font="Trebuchet MS, 8pt, style=Bold" IsTextAutoFit="False" Name="Default"></asp:Legend>                                
							</legends>
							<%--<borderskin SkinStyle="Emboss"></borderskin>--%>
                            <%--<borderskin   SkinStyle="Emboss" PageColor="#DDF0FA"></borderskin>--%>
							<series>
								<asp:Series ChartArea="Area1" XValueType="Double" Name="Series1" ChartType="Pie" Font="Trebuchet MS, 8.25pt, style=Bold" CustomProperties="DoughnutRadius=25, PieDrawingStyle=Concave, CollectedLabel=Other, MinimumRelativePieSize=20" MarkerStyle="Circle" BorderColor="64, 64, 64, 64" Color="180, 65, 140, 240" YValueType="Double" Label="#PERCENT{P1}">
									<%--<points>
										<asp:DataPoint LegendText="RUS" CustomProperties="OriginalPointIndex=0" YValues="39" />
										<asp:DataPoint LegendText="CAN" CustomProperties="OriginalPointIndex=1" YValues="18" />
										<asp:DataPoint LegendText="USA" CustomProperties="OriginalPointIndex=2" YValues="15" />
										<asp:DataPoint LegendText="PRC" CustomProperties="OriginalPointIndex=3" YValues="12" />
										<asp:DataPoint LegendText="DEN" CustomProperties="OriginalPointIndex=5" YValues="8" />
										<asp:DataPoint LegendText="AUS" YValues="4.5" />
										<asp:DataPoint LegendText="IND" CustomProperties="OriginalPointIndex=4" YValues="3.20000004768372" />
										<asp:DataPoint LegendText="ARG" YValues="2" />
										<asp:DataPoint LegendText="FRA" YValues="1" />
									</points>--%>
								</asp:Series>
							</series>
							<chartareas>
								<asp:ChartArea Name="Area1" BorderColor="64, 64, 64, 64" BackSecondaryColor="Transparent" BackColor="Transparent" ShadowColor="Transparent" BackGradientStyle="TopBottom">
									<axisy2>
										<MajorGrid Enabled="False" />
										<MajorTickMark Enabled="False" />
									</axisy2>
									<axisx2>
										<MajorGrid Enabled="False" />
										<MajorTickMark Enabled="False" />
									</axisx2>
									<area3dstyle PointGapDepth="900" Rotation="162" IsRightAngleAxes="False" WallWidth="25" IsClustered="False" />
									<axisy LineColor="64, 64, 64, 64">
										<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
										<MajorGrid LineColor="64, 64, 64, 64" Enabled="False" />
										<MajorTickMark Enabled="False" />
									</axisy>
									<axisx LineColor="64, 64, 64, 64">
										<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
										<MajorGrid LineColor="64, 64, 64, 64" Enabled="False" />
										<MajorTickMark Enabled="False" />
									</axisx>
								</asp:ChartArea>
							</chartareas>
						</asp:CHART>
</ContentTemplate>
<Triggers>
<asp:AsyncPostBackTrigger ControlID="imgDistribution" EventName="click" /></Triggers>
</asp:UpdatePanel>
<table id="gTotalAmount" runat="server" style="width:400px;border:solid 1px #C3C3C3;border-collapse:collapse;border-radius:3px;margin-top:10px;" cellpadding="3">
    <tr>
    <td align="Left" colspan="2" style="padding:3px;background-color:#FFFFFF;font-size:10pt;font-weight:bold;">
        <img src="images/index.png" height="18px" width="15px" style="margin:0px;"/> Total Amount Released by Operation Group
    </td>
    <td align="right" valign="top" style="padding:3px;background-color:#FFFFFF;font-size:10pt;font-weight:bold;"><asp:ImageButton ID="imgBk" runat="server" imageurl="images/showpanel.png"/></td>
    </tr>
    </table>
<asp:UpdatePanel ID="pnlTotalAmount" runat="server" UpdateMode="Conditional">
<ContentTemplate>

<asp:Repeater ID="rptTotalAmount" runat="server" Visible="false">
<HeaderTemplate>
    <table style="width:400px;border:solid 1px #C3C3C3;border-collapse:collapse;border-radius:3px;margin-top:0px;" cellpadding="3">
    <tr style="background-color:#82ACAC; font-size:10pt;font-weight:bold;">
    <td>#</td><td>Bureau</td><td align="right">Amount</td>
    </tr>
</HeaderTemplate>
<ItemTemplate>
<tr>
<td>
<asp:Literal ID="lno" runat="server" Visible="true" Text=''></asp:Literal>
</td>
<td>
<%--<asp:LinkButton ID="lbBureau" runat="server" onClick="fSelectComplete" style="color: inherit" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "description"))%>' ToolTip='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "bureau"))%>'></asp:LinkButton>--%>
<asp:Literal ID="lOfficeCode" runat="server" Visible="True" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "description"))%>'></asp:Literal>
</td>
<td align="right" style="padding-right:2px">
    <asp:Literal ID="lAmount" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "amount"))%>'></asp:Literal>
</td>    
</tr>
</ItemTemplate>
<AlternatingItemTemplate>
<tr style="background-color: #CBE4E3">
<td>
<asp:Literal ID="lno" runat="server" Visible="true" Text=''></asp:Literal>
</td>
<td>
<%--<asp:LinkButton ID="lbBureau" runat="server" onClick="fSelectComplete" style="color: inherit" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "description"))%>' ToolTip='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "bureau"))%>'></asp:LinkButton>--%>
<asp:Literal ID="lOfficeCode" runat="server" Visible="True" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "description"))%>'></asp:Literal>
</td>
<td  align="right" style="padding-right:2px">
    <asp:Literal ID="lAmount" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "amount"))%>'></asp:Literal>
</td>    
</tr>
</AlternatingItemTemplate>
<FooterTemplate>
</table>
</FooterTemplate>
</asp:Repeater>
</ContentTemplate>
<Triggers>
<asp:AsyncPostBackTrigger ControlID="imgBk"  EventName="click" /></Triggers>
</asp:UpdatePanel>    

<table id="gAged" runat="server" style="width:400px;border:solid 1px #C3C3C3;border-collapse:collapse;border-radius:3px 3px 3px 3px;margin-top:10px" cellpadding="3">
    <tr>
    <td align="left" colspan="2" style="padding:3px;background-color:#FFFFFF;font-size:10pt;font-weight:bold;">
        <img src="images/index.png" height="18px" width="15px" style="margin:0px;"/> Top 10 Aged Pending Bureau
    </td>
    <td align="right" valign="top" style="padding:3px;background-color:#FFFFFF;font-size:10pt;font-weight:bold;"><asp:ImageButton ID="imgAged" runat="server" imageurl="images/showpanel.png"/></td>
    </tr>
</table>
<asp:UpdatePanel ID="pnlAged" runat="server" UpdateMode="Conditional">
<ContentTemplate>
<asp:Repeater ID="rptAged" runat="server" Visible="false">
<HeaderTemplate>
    <table style="width:400px;border:solid 1px #C3C3C3;border-collapse:collapse;border-radius:3px 3px 3px 3px;" cellpadding="3">
    <tr style="background-color:#77BBFF; border:solid 1px #C3C3C3;font-size:10pt;font-weight:bold;">
    <td>#</td><td>Bureau</td><td align="right">Age(<span style="font-size:7pt">Days</span>)</td>
    </tr>
</HeaderTemplate>
<ItemTemplate>
<tr>
<td>
<asp:Literal ID="lno" runat="server" Visible="true" Text=''></asp:Literal>
</td>
<td>
<asp:LinkButton ID="lbBureau" runat="server" onClick="fSelectPending" style="color: inherit" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "bureau"))%>' ToolTip='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "bureau"))%>'></asp:LinkButton>
<asp:Literal ID="lOfficeCode" runat="server" Visible="false" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "officecode"))%>'></asp:Literal>
</td>
<td align="right" style="padding-right:2px">
    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "docage"))%>
</td>    
</tr>
</ItemTemplate>
<AlternatingItemTemplate>
<tr style="background-color:#C4E1FF;font-size:10pt;font-weight:normal;">
<td>
<asp:Literal ID="lno" runat="server" Visible="true" Text=''></asp:Literal>
</td>
<td>
<asp:LinkButton ID="lbBureau" runat="server" onClick="fSelectPending" style="color: inherit" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "bureau"))%>' ToolTip='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "bureau"))%>'></asp:LinkButton>
<asp:Literal ID="lOfficeCode" runat="server" Visible="false" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "officecode"))%>'></asp:Literal>
</td>
<td align="right" style="padding-right:1px">
    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "docage"))%>
</td>    
</tr>
</AlternatingItemTemplate>
<FooterTemplate>
</table>
</FooterTemplate>
</asp:Repeater>
</ContentTemplate>
<Triggers>
<asp:AsyncPostBackTrigger ControlID="imgAged" EventName="click" /></Triggers>
</asp:UpdatePanel>
<table id="gQuick" runat="server" style="width:400px;border:solid 1px #C3C3C3;border-collapse:collapse;border-radius:3px;margin-top:10px;" cellpadding="3">
    <tr>
    <td align="left" colspan="2" style="padding:3px;background-color:#FFFFFF;font-size:10pt;font-weight:bold;">
       <img src="images/index.png" height="18px" width="15px" style="margin:0px;"/> Top 10 Quickest Bureau
    </td>
    <td align="right" valign="top" style="padding:3px;background-color:#FFFFFF;font-size:10pt;font-weight:bold;"><asp:ImageButton ID="imgQuick" runat="server" imageurl="images/showpanel.png"/></td>
    </tr>
</table>
<asp:UpdatePanel ID="pnlQuick" runat="server" UpdateMode="Conditional">
<ContentTemplate>
<asp:Repeater ID="rptQuick" runat="server" Visible="false">
<HeaderTemplate>
    <table style="width:400px;border:solid 1px #C3C3C3;border-collapse:collapse;border-radius:3px;" cellpadding="3">
    <tr style="background-color:#C0C0C0;font-size:10pt;font-weight:bold;">
    <td>#</td><td>Bureau</td><td align="right">Age(<span style="font-size:7pt">Days</span>)</td>
    </tr>
</HeaderTemplate>
<ItemTemplate>
<tr>
<td>
<asp:Literal ID="lno" runat="server" Visible="true" Text=''></asp:Literal>
</td>
<td>
<asp:LinkButton ID="lbBureau" runat="server" onClick="fSelectComplete" style="color: inherit" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "bureau"))%>' ToolTip='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "bureau"))%>'></asp:LinkButton>
<asp:Literal ID="lOfficeCode" runat="server" Visible="false" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "officecode"))%>'></asp:Literal>
</td>
<td align="right" style="padding-right:2px">
    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "docage"))%>
</td>    
</tr>
</ItemTemplate>
<AlternatingItemTemplate>
<tr style="background-color: #E1E1E1">
<td>
<asp:Literal ID="lno" runat="server" Visible="true" Text=''></asp:Literal>
</td>
<td>
<asp:LinkButton ID="lbBureau" runat="server" onClick="fSelectComplete" style="color: inherit" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "bureau"))%>' ToolTip='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "bureau"))%>'></asp:LinkButton>
<asp:Literal ID="lOfficeCode" runat="server" Visible="false" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "officecode"))%>'></asp:Literal>
</td>
<td  align="right" style="padding-right:2px">
    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "docage"))%>
</td>    
</tr>
</AlternatingItemTemplate>
<FooterTemplate>
</table>
</FooterTemplate>
</asp:Repeater>
</ContentTemplate>

<Triggers>
<asp:AsyncPostBackTrigger ControlID="imgQuick" EventName="click" /></Triggers>
</asp:UpdatePanel>