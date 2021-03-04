<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlAdminMenuH.ascx.vb" Inherits="dms.UserControlAdminMenuH" %>
<asp:Panel ID="pAdminMenu_" runat="server" Visible="true" style="padding-right:10px;margin:10px 10px 10px 10px;text-align:left;">
       


           
<div id="div_1" runat="server" class="adminmenu222">

<table cellpadding="0" cellspacing="0" border="0" style="margin-bottom:5px">
                    <tr>
                        <td  class="abl">
                            </td>
                        <td  class="abm" valign="middle" nowrap>
                            <asp:HyperLink ID="HyperLink1" cssclass="amenu4" runat="server" NavigateUrl="~/user.aspx" onmouseover="ufHL(this,'abm.png','abl.png','abr.png')" onmouseout="ufHLR(this)">
    <asp:Image ID="Image4" runat="server" width="16px" Height="16px" imageurl="images/user2.png" />&nbsp;System Users
                            </asp:HyperLink>
                            </td>
                        <td class="abr">
                            </td>
                    </tr>
                </table>
</div>
           
<div id="div_2" runat="server" class="adminmenu222">       

<table cellpadding="0" cellspacing="0" border="0" style="margin-bottom:5px">
                    <tr>
                        <td  class="abl">
                            </td>
                        <td  class="abm" valign="middle" nowrap>
                            <asp:HyperLink ID="HyperLink2" cssclass="amenu4" onmouseover="ufHL(this,'abm.png','abl.png','abr.png')" onmouseout="ufHLR(this)" runat="server" NavigateUrl="~/usergroup.aspx">
    <asp:Image ID="Image22" runat="server" width="16px" Height="16px" imageurl="images/group.png" />
    &nbsp;User Groups
</asp:HyperLink>
                            </td>
                        <td class="abr">
                            </td>
                    </tr>
                </table>
</div>
           
<div id="div_3" runat="server" class="adminmenu222">       

<table cellpadding="0" cellspacing="0" border="0" style="margin-bottom:5px">
                    <tr>
                        <td  class="abl">
                            </td>
                        <td  class="abm" valign="middle" nowrap>
                            <asp:HyperLink ID="HyperLink3" cssclass="amenu4" onmouseover="ufHL(this,'abm.png','abl.png','abr.png')" onmouseout="ufHLR(this)" 
     
     runat="server" NavigateUrl="~/doctype.aspx">
    <asp:Image ID="Image5" runat="server" width="16px" Height="16px" imageurl="images/doctype.png" />
    &nbsp;Document Types
</asp:HyperLink>
                            </td>
                        <td class="abr">
                            </td>
                    </tr>
                </table>
</div>
<div id="div_4" runat="server" class="adminmenu222">       
    
    <table cellpadding="0" cellspacing="0" border="0" style="margin-bottom:5px">
                    <tr>
                        <td  class="abl">
                            </td>
                        <td  class="abm" valign="middle" nowrap>
    <asp:HyperLink ID="HyperLink4" cssclass="amenu4" onmouseover="ufHL(this,'abm.png','abl.png','abr.png')" onmouseout="ufHLR(this)" 
     
     runat="server" NavigateUrl="~/docStatus.aspx">
    <asp:Image ID="Image1" runat="server" width="16px" Height="16px" imageurl="images/import.png" />&nbsp;Document Action</asp:HyperLink>
                            </td>
                        <td class="abr">
                            </td>
                    </tr>
                </table>
</div>
<div id="Td1" runat="server" class="adminmenu222">       
    
    <table cellpadding="0" cellspacing="0" border="0" style="margin-bottom:5px">
                    <tr>
                        <td  class="abl">
                            </td>
                        <td  class="abm" valign="middle" nowrap>
    <asp:HyperLink ID="HyperLink10" cssclass="amenu4" onmouseover="ufHL(this,'abm.png','abl.png','abr.png')" onmouseout="ufHLR(this)" 
     
     runat="server" NavigateUrl="~/docReceipt.aspx">
    <asp:Image ID="Image2" runat="server" width="16px" Height="16px" imageurl="images/import.png" />&nbsp;Manner of Receipt</asp:HyperLink>
                            </td>
                        <td class="abr">
                            </td>
                    </tr>
                </table>
</div>
<div id="Td2" runat="server" class="adminmenu222">       
    
    <table cellpadding="0" cellspacing="0" border="0" style="margin-bottom:5px">
                    <tr>
                        <td  class="abl">
                            </td>
                        <td  class="abm" valign="middle" nowrap>
    <asp:HyperLink ID="HyperLink11" cssclass="amenu4" onmouseover="ufHL(this,'abm.png','abl.png','abr.png')" onmouseout="ufHLR(this)" 
     
     runat="server" NavigateUrl="~/docRequestType.aspx">
    <asp:Image ID="Image3" runat="server" width="16px" Height="16px" imageurl="images/import.png" />&nbsp;Request Type</asp:HyperLink>
                            </td>
                        <td class="abr">
                            </td>
                    </tr>
                </table>
</div>
<div id="Div1" runat="server" class="adminmenu222">       
    
    <table cellpadding="0" cellspacing="0" border="0" style="margin-bottom:5px">
                    <tr>
                        <td  class="abl">
                            </td>
                        <td  class="abm" valign="middle" nowrap>
    <asp:HyperLink ID="HyperLink12" cssclass="amenu4" onmouseover="ufHL(this,'abm.png','abl.png','abr.png')" onmouseout="ufHLR(this)" 
     
     runat="server" NavigateUrl="~/office.aspx">
    <asp:Image ID="Image9" runat="server" width="16px" Height="16px" imageurl="images/import.png" />&nbsp;List of Offices</asp:HyperLink>
                            </td>
                        <td class="abr">
                            </td>
                    </tr>
                </table>
</div>
<div id="div_5" runat="server" class="adminmenu222">       
    
    <table cellpadding="0" cellspacing="0" border="0" style="margin-bottom:5px">
                    <tr>
                        <td  class="abl">
                            </td>
                        <td  class="abm" valign="middle" nowrap>
    <asp:HyperLink ID="HyperLink5" cssclass="amenu4" onmouseover="ufHL(this,'abm.png','abl.png','abr.png')" onmouseout="ufHLR(this)" 
     
     runat="server" NavigateUrl="~/purge.aspx">
    <asp:Image ID="Image6" runat="server" width="16px" Height="16px" imageurl="images/delete_doc.png" />&nbsp;Delete Documents</asp:HyperLink>
                            </td>
                        <td class="abr">
                            </td>
                    </tr>
                </table>
</div>
<div id="div_6" runat="server" class="adminmenu222">       
    
    <table cellpadding="0" cellspacing="0" border="0" style="margin-bottom:5px">
                    <tr>
                        <td  class="abl">
                            </td>
                        <td  class="abm" valign="middle" nowrap>
    <asp:HyperLink ID="HyperLink6" cssclass="amenu4" onmouseover="ufHL(this,'abm.png','abl.png','abr.png')" onmouseout="ufHLR(this)" 
     
     runat="server" NavigateUrl="~/changelog.aspx">
    <asp:Image ID="Image7" runat="server" width="16px" Height="16px" imageurl="images/changelog_icon.png" />&nbsp;Track Changes</asp:HyperLink>
                            </td>
                        <td class="abr">
                            </td>
                    </tr>
                </table>
</div>
<div id="div_7" runat="server" class="adminmenu222">       
    <asp:Panel ID="bup" runat="server">
    <table cellpadding="0" cellspacing="0" border="0" style="margin-bottom:5px">
                    <tr>
                        <td  class="abl">
                            </td>
                        <td  class="abm" valign="middle" nowrap>
    <asp:HyperLink ID="HyperLink7" cssclass="amenu4" onmouseover="ufHL(this,'abm.png','abl.png','abr.png')" onmouseout="ufHLR(this)" 
     
     runat="server" NavigateUrl="~/backupdb.aspx">
    <asp:Image ID="Image8" runat="server" width="16px" Height="16px" imageurl="images/backup.png" />&nbsp;Backup Database</asp:HyperLink>
                            </td>
                        <td class="abr">
                            </td>
                    </tr>
                </table>
                </asp:Panel>
</div>
<div id="div_8" runat="server" class="adminmenu222">       
    
    <table cellpadding="0" cellspacing="0" border="0" style="margin-bottom:5px">
                    <tr>
                        <td  class="abl">
                            </td>
                        <td  class="abm" valign="middle" nowrap>
    <asp:HyperLink ID="HyperLink8" cssclass="amenu4" onmouseover="ufHL(this,'abm.png','abl.png','abr.png')" onmouseout="ufHLR(this)" 
     
     runat="server" NavigateUrl="~/OfficeHoliday.aspx">
    <asp:Image ID="Image13" runat="server" width="12px" Height="12px" style="vertical-align:middle" imageurl="images/holiday_icon.png" />&nbsp;Holidays</asp:HyperLink>
                            </td>
                        <td class="abr">
                            </td>
                    </tr>
                </table>
</div>
     <div id="div_9" runat="server" class="adminmenu222">       
    
    <table cellpadding="0" cellspacing="0" border="0" style="margin-bottom:5px">
                    <tr> 
                        <td  class="abl">
                            </td>
                        <td  class="abm" valign="middle" nowrap>
    <asp:HyperLink ID="HyperLink9" cssclass="amenu4" onmouseover="ufHL(this,'abm.png','abl.png','abr.png')" onmouseout="ufHLR(this)" 
     
     runat="server" NavigateUrl="~/DocSettings.aspx">
    <asp:Image ID="Image14" runat="server" width="12px" Height="12px" style="vertical-align:middle" imageurl="images/advancedsearch_h.png" />&nbsp;Settings</asp:HyperLink>
                            </td>
                        <td class="abr">
                            </td>
                    </tr>
                </table>
</div>  
<div id="div2" runat="server" class="adminmenu222">       
    
    <table cellpadding="0" cellspacing="0" border="0" style="margin-bottom:5px">
                    <tr> 
                        <td  class="abl">
                            </td>
                        <td  class="abm" valign="middle" nowrap>
    <asp:HyperLink ID="HyperLink13" cssclass="amenu4" onmouseover="ufHL(this,'abm.png','abl.png','abr.png')" onmouseout="ufHLR(this)" 
     
     runat="server" NavigateUrl="~/rdsprocess.aspx">
    <asp:Image ID="Image10" runat="server" width="12px" Height="12px" style="vertical-align:middle" imageurl="images/advancedsearch_h.png" />&nbsp;RDS Process</asp:HyperLink>
                            </td>
                        <td class="abr">
                            </td>
                    </tr>
                </table>
</div>  

       
       </asp:Panel>
