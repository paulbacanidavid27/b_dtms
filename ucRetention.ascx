<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucRetention.ascx.vb" Inherits="dms.ucRetention" %>
<table width="100%" style="color:Black">
<tr>
<td align="left" colspan="2" style="font-size:10pt;">
   A. <asp:Literal ID="lRetentionSetup" runat="server"></asp:Literal>
</td>
</tr>

<tr><td colspan="2">&nbsp;</td></tr>

<tr>
<td align="left" width="25%" class="labelFreeForm2">Active Period:</td><td  align="left" class="labelFreeForm"><asp:Literal ID="ap" runat="server"></asp:Literal></td>
</tr>
<tr><td colspan="2">&nbsp;</td></tr>
<tr>
<td align="left" class="labelFreeForm2">Storage Period:</td><td align="left" class="labelFreeForm"><asp:Literal ID="sp" runat="server"></asp:Literal></td>
</tr>
<tr><td colspan="2">&nbsp;</td></tr>
<tr>
<td colspan="2" align="left" style="font-size:10pt;">
    B. <asp:Literal ID="lRetentionStatus" runat="server"></asp:Literal>
</td>
</tr>
<tr>
<td align="left" width="25%" class="labelFreeForm2">Current Status:</td><td  align="left" class="labelFreeForm"><asp:Literal ID="lad" runat="server" Text="Not Applicable"></asp:Literal></td>
</tr>
<tr>
<td align="left" width="25%" class="labelFreeForm2">Date Received By Agency:</td><td  align="left" class="labelFreeForm"><asp:Literal ID="ldrba" runat="server"  Text="Not Applicable"></asp:Literal></td>
</tr>
<tr><td colspan="2">&nbsp;</td></tr>
<tr>
<td align="left" width="25%" class="labelFreeForm2">Document Active Period Range:</td><td  align="left" class="labelFreeForm"><asp:Literal ID="lAP" runat="server"></asp:Literal></td>
</tr>
<tr><td colspan="2">&nbsp;</td></tr>
<tr>
<td align="left" class="labelFreeForm2">Document Storage Period Range:</td><td align="left" class="labelFreeForm"><asp:Literal ID="lSP" runat="server"></asp:Literal></td>
</tr>
<tr>
<td colspan="2" align="left">
    <small>Notes: <br />
    1. Storage Period will start after the active period. 
    <br />2. Once the document has been purged, the document will 
    not be viewable from the System.)
    <br />3. Active Period and Storage Period is computed based on the RDS setup of the document type.
    <br />4. Active Period and Storage Period automatically computed once the document was set to archive and the date received by agency has been filled-up.
    <br />5. Changing the document status from Archived to another status will not reset the Retention Disposable Schedule.
    <br />6. You need to reset it manually by clicking on Refresh <img src="images/refresh.png" />or Reset <img src="images/img_delete.jpg" />.</small></td>
</tr>
</table>
