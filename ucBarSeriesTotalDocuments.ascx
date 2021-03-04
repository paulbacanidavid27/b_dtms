<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucBarSeriesTotalDocuments.ascx.vb" Inherits="docuvu.ucBarSeriesTotalDocuments" %>

<asp:UpdatePanel ID="pTotalDocuments" runat="server" EnableViewState="true" UpdateMode="Conditional">
<ContentTemplate>

    <asp:chart id="Chart1" runat="server" BackColor="WhiteSmoke"   EnableViewState="true" BackSecondaryColor="White" BackGradientStyle="TopBottom" BorderlineDashStyle="Solid" Palette="BrightPastel" BorderColor="26, 59, 105" Height="296px" Width="412px" BorderWidth="2" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)">
    
							<legends>
								<asp:Legend IsTextAutoFit="False" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold" ></asp:Legend>
							</legends>
                            <Titles>
                                <asp:Title Font="Calibri, 11pt, style=Bold" Name="Title1" 
                                    Text="Total Documents">
                                </asp:Title>
                            </Titles>
							<borderskin SkinStyle="Emboss"></borderskin>
                            <Series>
                                <asp:Series Name="Documents" IsValueShownAsLabel="true" LabelBackColor="White" LabelForeColor="Blue" >                
                                </asp:Series>
                            </Series>
							<chartareas>
								<asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" BackSecondaryColor="White" BackColor="Gainsboro" ShadowColor="Transparent" BackGradientStyle="TopBottom">
									<area3dstyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="False" WallWidth="0" IsClustered="False" />
									<axisy LineColor="64, 64, 64, 64" IsLabelAutoFit="False">
										<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
										<MajorGrid LineColor="64, 64, 64, 64" />
									</axisy>
									<axisx LineColor="64, 64, 64, 64">
										<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" IntervalType="Auto" />
										<MajorGrid Interval="Auto" IntervalType="Auto" LineColor="64, 64, 64, 64" />
										<MajorTickMark IntervalType="Auto" />
									</axisx>
								</asp:ChartArea>
							</chartareas>
						</asp:chart>
</ContentTemplate>
</asp:UpdatePanel>

