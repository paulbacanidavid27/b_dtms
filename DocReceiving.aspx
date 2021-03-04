<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Monitoring.Master" CodeBehind="DocReceiving.aspx.vb" Inherits="dms.DocReceiving" %>
<%@ MasterType VirtualPath="~/Monitoring.Master" %>
<%@ Register src="UserControlPager.ascx" tagname="UserControlPager" tagprefix="uc" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Document Management System</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    CRD Document Monitoring - Receiving Section
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MenuContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="pSearch" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <table width="100%">
        <tr>
        <td>
        <asp:ImageButton ID="imgDownload"  imageurl="images/download_doc.png" runat="server" />&nbsp;<asp:ImageButton ID="imgUpload" imageurl="images/upload2.png" runat="server" />
                                <asp:UpdatePanel ID="upnlUpload" runat="server" UpdateMode="Conditional"><ContentTemplate>
                                <asp:Panel ID="pnlUpload" runat="server" Visible="false">
                                    <asp:FileUpload ID="fUpload" runat="server"/><asp:Button ID="btUpload" runat="server" Text="Upload" cssclass="btnsmall" />
                                </asp:Panel>
                                </ContentTemplate>
                                <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="imgUpload" EventName="click"/>
                                <asp:PostBackTrigger ControlID="btUpload" />
                                </Triggers>
                                </asp:UpdatePanel>
        </td>
        <td>
               
                                          
                                        <div id="prc5" style="display: none ; font-style: italic;font-size:10px;color:Blue;">
                                            <img src="images/processing.gif" />
                                        </div>
                                        <div id="pbtn5" style=" display: inline;margin-right:10px;">
                                            <asp:ImageButton ID="imgHoliday" runat="server" Width="20px" Height="20px" tooltip="Holiday" imageurl = "images/button/holiday.png"   onmouseover="this.src='images/button/holiday_h.png'" onmouseout="this.src='images/button/holiday.png'" OnClientClick="document.getElementById('pbtn5').style.display='none';document.getElementById('prc5').style.display='inline';return true;"/> Holiday                                                                    
                                        </div> 
                                        <div id="prc4" style="display: none ; font-style: italic;font-size:10px;color:Blue;">
                                            <img src="images/processing.gif" />
                                        </div>
                                        <div id="pbtn4" style=" display: inline;margin-right:10px;">
                                            <asp:ImageButton ID="imgSettings" runat="server" Width="20px" Height="20px" tooltip="Settings" imageurl = "images/button/msettings.png"   onmouseover="this.src='images/button/msettings_h.png'" onmouseout="this.src='images/button/msettings.png'" OnClientClick="document.getElementById('pbtn4').style.display='none';document.getElementById('prc4').style.display='inline';return true;"/> Settings                                                                    
                                        </div> 
        </td>
        <td align="center">
        <asp:Panel ID="pSearchPanel" runat="server" DefaultButton="imgSearch">
                <table border="0" cellspacing="2" cellpadding="1" style="margin-top:5px">
                                <tr>     
                                    <td align="right">
                                    <asp:TextBox ID="tbSelectedDate" runat="server" Width="67px" AutoPostBack="true" style="padding:4px;border: solid 1px #9DB5CD;border-collapse:collapse;color:#222222;font-size:12px"></asp:TextBox><cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="tbSelectedDate" />
                                    </td>                               
                                    <td nowrap>
                                        <asp:TextBox width="600px" ID="tbSearch" runat="server" maxlength="250" style="padding:4px;border: solid 1px #9DB5CD;border-collapse:collapse;color:#222222;font-size:12px" placeholder="Enter reference no or search criteria here ..."></asp:TextBox>
                                    </td>                                    
                                    <td  nowrap valign="top">
                                        <div id="prc" style="display: none ; font-style: italic;font-size:10px;color:Blue;">
                                            <img src="images/processing.gif" />
                                        </div>
                                        <div id="pbtn" style=" display: inline">
                                            <asp:ImageButton ID="imgSearch" runat="server" Width="20px" Height="20px" tooltip="Search" imageurl = "images/button/mfind.png"   onmouseover="this.src='images/button/mfind_h.png'" onmouseout="this.src='images/button/mfind.png'" OnClientClick="document.getElementById('pbtn').style.display='none';document.getElementById('prc').style.display='';return true;"/>                                                                        
                                        </div>                                                                          
                                    </td>                                
                                </tr>
                            </table>
                            </asp:Panel>
                            </td>
        </tr>
        </table>
        </ContentTemplate>
        <Triggers><asp:PostBackTrigger ControlID="imgDownload" /><asp:PostBackTrigger ControlID="btUpload" /></Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upHoliday" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    <asp:Repeater ID="rptHolidays" visible="false" runat="server">    
                            <HeaderTemplate>
                            <div width="100%" align="left">
                             <table style="border:solid 1px #C0C0C0;border-collapse:collapse;"><tr><td style="font-weight:bold; border-bottom:solid 1px #999999;">Holidays/Weekends</td><td style="font-weight:bold; border-bottom:solid 1px #999999;" align="right">
                                 <asp:ImageButton ID="imgRefresh" runat="server"  ImageUrl="images/refresh.png" onclick="RefreshHolidays"/></td></tr>
                            </HeaderTemplate>        
                                <ItemTemplate>   
                                <tr><td style="padding-left:3px;padding-right:3px">                              
                                    <asp:Literal ID="lDate" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Holidate"))%>' Visible="True"></asp:Literal>
                                    </td><td style="padding-left:3px;padding-right:3px">
                                    <asp:Literal ID="lHoliday" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Description")%>' Visible="True"></asp:Literal>                                
                                    </td></tr>
                                </ItemTemplate>  
                                <FooterTemplate>
                            
                            </table></div></FooterTemplate>
                            </asp:Repeater>
    <asp:Literal ID="lsMsg" runat="server" Text="No records retrieved." Visible="false"></asp:Literal>
    </ContentTemplate>
        
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upSettings" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <asp:Panel ID="pSettings" runat="server" Visible="false" style="z-index:1000;border:solid 1px #CCCCCC">
            <table>
            <tr>
            <td colspan="7" style="font-weight:bold; border-bottom:solid 1px #999999;">Default Settings</td>
            <td align="right" style="font-weight:bold; border-bottom:solid 1px #999999;">
                <asp:ImageButton ID="imgSave" runat="server" ImageUrl="images/saveicon.png" /></td></tr>
            <tr>
            <td nowrap>Cut-off Time (Morning)
            </td>                
            <td><asp:TextBox ID="tbCutOffMorning" runat="server" Width="60px"></asp:TextBox>            
                            <cc1:MaskedEditExtender id="MaskedEditExtender1" runat="server" TargetControlID="tbCutOffMorning"  AcceptAMPM="true" MaskType="Time"  Mask="99:99" InputDirection="LeftToRight" AcceptNegative="None"/>
            </td>            
            <td nowrap>Background Image
            </td>                
            <td><asp:TextBox ID="tbBackgroundImage" runat="server" Width="295px"></asp:TextBox>
            </td>
            <td nowrap>No. of Rows To Display</td>
            <td><asp:TextBox ID="tbRows" runat="server" Width="25px"></asp:TextBox>
            </td>
            <td>Separator
            </td>
            <td><asp:TextBox ID="tbSeparator" runat="server" Width="25px" Text=";"></asp:TextBox>
            </td>
            </tr>
            <tr>
            <td nowrap>Cut-off Time (Afternoon)
            </td>                
            <td><asp:TextBox ID="tbCutOffAfternoon" runat="server" Width="60px"></asp:TextBox>            
                            <cc1:MaskedEditExtender id="MaskedEditExtender2" runat="server" TargetControlID="tbCutOffAfternoon"  AcceptAMPM="true" MaskType="Time"  Mask="99:99" InputDirection="LeftToRight" AcceptNegative="None"/>
            </td>   
            <td nowrap>Month
            </td>                
            <td><asp:DropDownList ID="dlMonth" runat="server" Visible="true" Width="102px">
                <asp:ListItem Text="January" Value="1" />
                <asp:ListItem Text="February" Value="2" />
                <asp:ListItem Text="March" Value="3" />
                <asp:ListItem Text="April" Value="4" />
                <asp:ListItem Text="May" Value="5" />
                <asp:ListItem Text="June" Value="6" />
                <asp:ListItem Text="July" Value="7" />
                <asp:ListItem Text="August" Value="8" />
                <asp:ListItem Text="September" Value="9" />
                <asp:ListItem Text="October" Value="10" />
                <asp:ListItem Text="November" Value="11" />
                <asp:ListItem Text="December" Value="12" />
              </asp:DropDownList>
            </td>
            <td nowrap>Year</td>
            <td><asp:TextBox ID="tbYearFrom" runat="server" placeholder="From" Width="42px"></asp:TextBox>
            </td>
            <td></td><td></td>
            </tr>
            <tr>
            <td nowrap></td>
            <td></td>
            <td nowrap></td>
            <td>
            </td>
            <td nowrap></td>
            <td>
            </td>
            </tr>
            <tr>
            <td nowrap>Cut-off (Lunch Break)
            </td>                
            <td><asp:TextBox ID="tbLunchFrom" runat="server" Width="60px"></asp:TextBox>-            
                            <cc1:MaskedEditExtender id="MaskedEditExtender3" runat="server" TargetControlID="tbLunchFrom"  AcceptAMPM="true" MaskType="Time"  Mask="99:99" InputDirection="LeftToRight" AcceptNegative="None"/>
                            <asp:TextBox ID="tbLunchTo" runat="server" Width="60px"></asp:TextBox>            
                            <cc1:MaskedEditExtender id="MaskedEditExtender4" runat="server" TargetControlID="tbLunchTo"  AcceptAMPM="true" MaskType="Time"  Mask="99:99" InputDirection="LeftToRight" AcceptNegative="None"/>
            </td>  
            <td nowrap>Default Sort Order</td>
            <td><asp:DropDownList ID="dlDefaultSort" runat="server" Visible="true" Width="240px">
                    <asp:ListItem Text="Status" Value="MainStatusDesc"></asp:ListItem>
                    <asp:ListItem Text="Reference No" Value="cm.RefNo"></asp:ListItem>
                    <asp:ListItem Text="Received Date" Selected="True" Value="cm.ReceivedDate"></asp:ListItem>
                    <asp:ListItem Text="Received By" Value="rbu.FirstName+' '+rbu.LastName"></asp:ListItem>
                    <asp:ListItem Text="Requesting Office" Value="o.Description"></asp:ListItem>
                    <asp:ListItem Text="Description" Value="cm.Description"></asp:ListItem>
                    
                      </asp:DropDownList> - 
                      <asp:DropDownList ID="dlDefaultSortOrder" runat="server" Visible="true" Width="55px">
                    <asp:ListItem Value="Desc" Text="Descending"></asp:ListItem>
                    <asp:ListItem Value="Asc" Text="Ascending"></asp:ListItem>
                    
                    
                      </asp:DropDownList>
            </td>
            <td nowrap>Group Code</td>
            <td><asp:TextBox ID="tbGroupCode" runat="server"></asp:TextBox>
            </td>
            <td></td><td></td>
            </tr>
            <tr>
            <td>Delivery Time (min)</td>
            <td><asp:TextBox ID="tbDeliveryTime" runat="server" width="60px" Text="60"></asp:TextBox></td>
            <td colspan="6"></td></tr>
            <tr>
            <td colspan="8"><hr /></td></tr>
            <tr>
            <td colspan="8"><b>Search Keys</b></td></tr>
            <tr>
            <td colspan="8"><table style="border-collapse:collapse" border="1"><tr><td><b>refno:</b> - Searching Reference No</td>
            <td><b>[enter date]</b> - Searching Date Received</td>
            <td><b>rcvby:</b> - Searching Received By</td>
            <td><b>ofc:</b> - Searching Office</td>
            <td><b>desc:</b> - Searching Description</td>
            <td><b>rem:</b> - Searching Remarks</td></tr>
            <tr><td><b>loc:</b> - Searching Location</td>
            <td><b>cour:</b> - Searching Courrier Name</td>
            <td><b>due:</b> - Searching Due Date</td>
            <td><b>dol:</b> - Searching Date of Letter</td>
            <td><b>drba:</b> - Searching Date Received By Agency</td>
            <td><b>duration:</b> - Searching Duration</td></tr>
            <tr><td><b>ds:</b> - Searching Sort Date Completed</td>
            <td><b>dd:</b> - Searching Date Delivered</td>
            <td><b>dm:</b> - Searching Date Mailed</td>
            <td><b>sby:</b> - Searching Sorted By</td>
            <td><b>dby:</b> - Searching Delivered By</td>
            <td><b>mby:</b> - Searching Mailed By</td></tr>
            </table></td></tr>
            </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <table border="0" cellspacing="0" cellpadding="0" style="border-collapse:collapse;background-color:White;width:100%;">
    <tr>
        <td style="font-family:arial;font-weight:bold">
        
        <asp:UpdatePanel ID="upSelected" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
            View Summary:  
            <asp:Literal ID="lmonthyear" runat="server" Text=""></asp:Literal> 
            
                <asp:ImageButton ID="imgShow" runat="server" imageurl="images/button/down.png" />
            
            </ContentTemplate></asp:UpdatePanel></td>   
        <td align="right">
            <asp:UpdatePanel ID="pPager" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
            <asp:HiddenField ID="hfSortCol" runat="server" Value=""/>
            <asp:HiddenField ID="hfSortOrder" runat="server" Value="Desc"/>
            <asp:HiddenField ID="hfCurrent" runat="server" Value="1"/>
            <asp:HiddenField ID="hfTotalRows" runat="server" Value="0"/>
            <uc:UserControlPager ID="ucPager" runat="server" />
            </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        </tr>
        </table>
    <asp:UpdatePanel ID="upProcess" runat="server" UpdateMode="Conditional" align="left">
    <ContentTemplate>
        <asp:Panel id="processtbl" visible="false" runat="server" >
        
   <table  cellpadding="4" border="1" style="border-collapse:collapse;background-color:White;width:40%;margin-bottom:15px;border:solid 1px #C0C0C0">
   <tr>
   <td ><b>Summary - <asp:Literal ID="lSummaryMonthYear" runat="server" Text=''></asp:Literal></b></td><td colspan="2" align="center"><asp:Button ID="btGenReport" runat="server" Text="Generate Report" cssclass="btnsmall"/></td></tr>
   <asp:Repeater ID="rptSummary" visible="true" runat="server">
            <HeaderTemplate>               
                    
                   
            </HeaderTemplate>
            <ItemTemplate>                
                    <tr style="background-color:#F8F8F8" >
                        <td>
                            <asp:Literal ID="lRem" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "remarks"))%>'></asp:Literal>
                            <asp:Literal ID="lOrder" runat="server" visible="false" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "OrderNo"))%>'></asp:Literal>
                        </td>
                                              
                        <td align="right"><asp:Literal ID="lsubtotal" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "subtotalrecv"))%>'></asp:Literal>                       
                            <asp:Literal ID="ltotal" runat="server" Visible="false" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "totalrcv"))%>'></asp:Literal>  
                        </td>                        
                        <td nowrap  align="right">
                                <asp:Literal ID="lpercentage" runat="server" Text='' Visible="true"></asp:Literal>
                                                       
                        </td>
                       
                    </tr>      
                            
            </ItemTemplate>
            
            <FooterTemplate>
            <tr style="background-color:#F0F0F0" >
                    <td>Total Docs Delivered by CRD</td>
                   
                    <td align="right"><b><asp:Literal ID="ltotaldelivered" runat="server" Text=''></asp:Literal></b></td>
                    <td align="right"><b><asp:Literal ID="ltotaldeliveredpercentage" runat="server" Text='100'></asp:Literal>%</b></td>
                    </tr>
                    <tr style="background-color:#DFDFDF" >
                    <td>Total Docs Generated By System</td>
                    
                    <td align="right"><b><asp:Literal ID="lTotalDocs" runat="server" Text=''></asp:Literal></b></td>
                    <td align="right"><b><asp:Literal ID="lTotalDocsPercentage" runat="server" Text='100'></asp:Literal>%</b></td>
                    </tr>
                  
            </FooterTemplate>
                 
        </asp:Repeater>
   
   
   
   </table>
   </asp:Panel>
    </ContentTemplate>
        <Triggers><asp:PostBackTrigger ControlID="btGenReport" /></Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="pData" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        
    <table border="1" cellspacing="0" cellpadding="2" style="border-collapse:collapse;width:100%;border:solid 1px #D4D4D4">
        
    <asp:Repeater ID="rptMonitoring" visible="true" runat="server">
            <HeaderTemplate>
                
                    
                    <tr style="border-bottom:solid 1px #C0C0C0">                        
                        <td style="background-color:#CACACA; color:#222222;" width="15px" align="center">No</td>
                        
                        <td class="tableHead2"  width="60px">Status</td>                        
                        <td class="tableHead2"  width="120px">Reference No</td>   
                        <td class="tableHead2"  width="120px">Subject</td>  
                        <td class="tableHead2"  width="150px">Received By</td>                     
                        <td class="tableHead2" width="150px">Uploaded Date/Time</td>
                        
                        <td class="tableHead2"  width="150px">Cut-off Date/Time</td>
                        <td class="tableHead2"  width="150px">Received Date/Time</td>
                        <td class="tableHead2" width="40px" title="HH:MM">Duration(HH:MM)</td>
                        <td class="tableHead2" width="15px">Delivery</td>
                    </tr>            
            </HeaderTemplate>
            <ItemTemplate>                
                    <tr style="background-color:#F0F0F0"  valign="top">
                        <td style="background-color:#CACACA; color:#222222;" align="right">
                            <asp:Literal ID="lCtr" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "rn"))%>'></asp:Literal><asp:Literal ID="lReceivingId" runat="server" visible="false" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ReceivingId"))%>'></asp:Literal>.
                        </td>
                                              
                        <td><asp:Literal ID="lStatusDesc" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "StatusDesc"))%>'></asp:Literal>                       
                            <asp:Literal ID="lStatus" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "StatusId"))%>' Visible="false"></asp:Literal> 
                            <asp:Literal ID="lDocId" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DocId"))%>' Visible="false"></asp:Literal>                       
                        </td>                        
                        <td nowrap>
                            
                            <asp:HyperLink ID="hlOpenDoc" runat="server"  Style="color:Blue"  Target="_blank"   onclientclick="return false" ToolTip="Open In New Window">
                                <asp:Literal ID="lRefNo" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "RefNo"))%>' Visible="true"></asp:Literal>
                            </asp:HyperLink>                            
                            
                        </td>
                        <td style="color:Black"><asp:Literal ID="lSubject" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Subject"))%>' Visible="true"></asp:Literal>
                        
                        </td> 
                        <td><asp:Literal ID="lReceivedBy" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ReceivedBy"))%>' Visible="true"></asp:Literal>
                        
                        </td>   
                        
                        <td title='Uploader Group: <%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "UploaderGrp"))%>'  style="color:Black">
                            <asp:Literal ID="lCreatedDate" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CreatedDate"))%>' Visible="true"></asp:Literal></b>
                            
                        </td>   
                        <td>
                            <asp:Literal ID="lCuttoff" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CutoffDate"))%>' Visible="true"></asp:Literal>
                            
                        </td>     
                        <td style="color:Black">
                           <asp:Literal ID="lReceivedDate" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ReceivedDate"))%>' Visible="true"></asp:Literal>
                            
                        </td>                        
                                             
                                             
                        
                        <td align="right"><asp:Literal ID="lDuration" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Duration"))%>'></asp:Literal><asp:Literal ID="lMin" runat="server" Visible="false" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Duration"))%>'></asp:Literal></td>
                        <td width="15px" style="color:Black"><asp:Literal ID="lRemarks" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Remarks"))%>'></asp:Literal></td>               
                                  
                    </tr>      
                            
            </ItemTemplate>
            <AlternatingItemTemplate>                
                    <tr valign="top">
                        <td style="background-color:#CACACA; color:#222222;" align="right">
                            <asp:Literal ID="lCtr" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "rn"))%>'></asp:Literal><asp:Literal ID="lReceivingId" runat="server" visible="false" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ReceivingId"))%>'></asp:Literal>.
                        </td>
                                              
                        <td><asp:Literal ID="lStatusDesc" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "StatusDesc"))%>'></asp:Literal>                       
                            <asp:Literal ID="lStatus" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "StatusId"))%>' Visible="false"></asp:Literal> 
                            <asp:Literal ID="lDocId" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DocId"))%>' Visible="false"></asp:Literal>                       
                        </td>                        
                        <td nowrap>
                            
                            <asp:HyperLink ID="hlOpenDoc" runat="server"  Style="color:Blue"  Target="_blank"   onclientclick="return false" ToolTip="Open In New Window">
                                <asp:Literal ID="lRefNo" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "RefNo"))%>' Visible="true"></asp:Literal>
                            </asp:HyperLink>                            
                            
                        </td>
                        <td style="color:Black"><asp:Literal ID="lSubject" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Subject"))%>' Visible="true"></asp:Literal>
                        
                        </td> 
                        <td><asp:Literal ID="lReceivedBy" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ReceivedBy"))%>' Visible="true"></asp:Literal>
                        
                        </td>   
                        
                        <td title='Uploader Group: <%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "UploaderGrp"))%>'  style="color:Black">
                            <asp:Literal ID="lCreatedDate" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CreatedDate"))%>' Visible="true"></asp:Literal></b>
                            
                        </td>   
                        <td>
                            <asp:Literal ID="lCuttoff" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CutoffDate"))%>' Visible="true"></asp:Literal>
                            
                        </td>     
                        <td style="color:Black">
                           <asp:Literal ID="lReceivedDate" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ReceivedDate"))%>' Visible="true"></asp:Literal>
                            
                        </td>                        
                                             
                                             
                        
                        <td align="right"><asp:Literal ID="lDuration" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Duration"))%>'></asp:Literal><asp:Literal ID="lMin" runat="server" Visible="false" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Duration"))%>'></asp:Literal></td>
                        <td width="15px" style="color:Black"><asp:Literal ID="lRemarks" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Remarks"))%>'></asp:Literal></td>               
                                  
                    </tr>     
                    
                               
            </AlternatingItemTemplate>
            <FooterTemplate>
            <tr>
                    <td  colspan="10">
                    <table style="font-size:10pt">
                    <tr>
                    <td colspan="6"><b>Summary</b></td></tr>
                 <tr>
                    <td style="padding-left:10px;">On Time:</td>
                    <td><b><asp:Literal ID="lOntime" runat="server" Text=''></asp:Literal></b></td>
                    
                    <td style="padding-left:10px;">Late:</td>
                    <td><b><asp:Literal ID="lLate" runat="server" Text=''></asp:Literal></b></td>
                    <td style="padding-left:10px;">Others:</td>
                    <td><b><asp:Literal ID="lOthers" runat="server" Text=''></asp:Literal></b></td>

                    </tr>
                    </table>
                    </td>
                    </tr>
            </FooterTemplate>
                 
        </asp:Repeater>
        </table>
        </ContentTemplate>
   </asp:UpdatePanel>
   <asp:UpdatePanel ID="pnlView" runat="server" UpdateMode="Conditional"><ContentTemplate>
                    <asp:Panel ID="pView" runat="server" Visible="false" style="width:100%;max-height:400px;border:solid 1px #CCCCCC;overflow:auto">
                        <b>Import Results</b>
                <asp:GridView ID="GridView1" runat="server" ShowHeader="false">
                </asp:GridView>
                </asp:Panel>
                </ContentTemplate>
                
                </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cntntUpload" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="PopupMenu" runat="server">
    <asp:Panel ID="pDeleteMonitoringConfirmation" runat="server" Visible="false" Width="600px" style="z-index:1000;border:solid 1px #CCCCCC" BackColor="#FFFFFF">
                <table align="left" style="background-color:#FFFFFF" width="100%">
                <tr>
                        
                        <td align="left" style="background-color:#003399; color: #FFFFFF;font-weight:normal;padding:5px;">
                            Delete Confirmation
                        </td>                        
                </tr>  
                <tr>
                    <td align="left" style="font-family:Tahoma;font-size:11pt;color:#003399; padding:12px;">
                        <asp:Literal ID="ltext" runat="server" Text="Are you sure you want to delete this record? Please click OK to proceed."></asp:Literal>
                        <asp:Literal ID="lSelectedReceivingId" runat="server" Text="" Visible="false"></asp:Literal>
                    </td>               
                    </tr>  <tr>
                        
                        <td align="right">
                            <asp:Button ID="btDeleteMonitoring" runat="server" CssClass="btnsmall2" Text="OK" Width="40px" />&nbsp;<asp:Button ID="btDeleteCancel" runat="server" CssClass="btnsmall2" Text="Cancel" />
                        </td>                        
                </tr>               
                
                </table>
                </asp:Panel>
</asp:Content>
