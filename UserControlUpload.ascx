<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlUpload.ascx.vb" Inherits="dms.UserControlUpload" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register src="UserControlDocRouting.ascx" tagname="UserControlDocRouting" tagprefix="uc" %>    
<%@ Register src="UserControlDocumentIndex.ascx" tagname="UserControlDocumentIndex" tagprefix="uc" %>    
<%@ Register src="UserControlImgViewer2.ascx" tagname="UserControlImgViewer" tagprefix="uc" %>
<%@ Register src="UserControlPDFViewer2.ascx" tagname="UserControlPDFViewer" tagprefix="uc" %>
<%@ Register src="ucHr.ascx" tagname="ucHr" tagprefix="uc1" %>
<%@ Register src="UserControlDocViewer2.ascx" tagname="UserControlDocViewer" tagprefix="uc" %>
<%@ Register src="ucReceiptRoute.ascx" tagname="ucReceiptRoute" tagprefix="uc" %>

<asp:Panel ID="pUpload1"  cssclass="Div1" runat="server" Visible="true">
</asp:Panel>
<asp:Panel ID="pUpload2" cssclass="Div2" runat="server" Visible="True" >
            
                <table border="0" cellspacing="0" cellpadding="0" style="width:100%;height:100%;border-collapse:collapse" >
                <tr>
                  <td valign="middle" align="center" style="height:100%">                
                        <asp:Panel id="pAddDoc"  ClientIDMode="Static" runat="server" Visible="True" valign="top" style="border:solid 1px gray;width:90%;height:90%;background-color:White" defaultbutton="btSave">
                            <%--<table border="0" cellspacing="0" class="popuphdrbox" cellpadding="0" style="border-collapse:collapse;width:100%;">
        
                                    <tr>
                                    <td style="width:100%" valign="top">--%>
                            <table cellspacing="0" class="popuphdr" cellpadding="0" border="0" style="width:100%;margin-bottom:3px;">
                                            <tr height="30px">
                                            <td align="left" valign="middle" colspan="2"><asp:HyperLink ID="hlAgency" runat="server" Target="_blank"  enabled="false" onclientclick="return false" ToolTip="Open In New Window">
                       
                   </asp:HyperLink><img height="20px" width="18px" src="images/upload.png" />
                                            &nbsp;Upload Document</td>
                                            
                                            <td  align="right" valign="top">
                                                <asp:ImageButton ID="imgClose" runat="server" imageurl="images/close_window.gif" onmouseover="this.src='images/close_window.gif'"  onmouseout="this.src='images/close_window.gif'" width="18px" Height="18px"/>
                                            </td>
                                            </tr>
                                       </table>
                                <%--</td></tr></table>--%>
                                    
                                    <table width="99.5%" height="85%" cellspacing="0" cellpadding="0" border="0" style="background-color:White;">
                                    <tr>            
                                        <td class="tableHead"  style="border-top:solid 1px gray;border-right:solid 1px gray;border-left:solid 1px gray;padding-left:4px" align="left">Document Information</td> 
                                        <td width="4px"></td>           
                                        <td class="tableHead" width="55%" style="border-top:solid 1px gray;border-right:solid 1px gray;border-left:solid 1px gray;padding-left:4px"  align="left">Document Preview                                    
                                        <asp:UpdateProgress runat="server" ID="updPrgx">
    <ProgressTemplate>
    
        <asp:Image ID="Image2" runat="server" src="images/loading.gif"/>
    
    </ProgressTemplate>
</asp:UpdateProgress>
                                        </td>            
                                    </tr>
                                    <tr>
            
                                        <td valign="top" style="border-bottom:solid 1px gray;border-right:solid 1px gray;border-left:solid 1px gray;padding-left:2px;padding-right:2px;height:100%">
                                        <div id="autoflow" style="height:300px;overflow:auto;overflow-x:hidden" >    
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" >
                                    <tr>
                                            <td align="left" class="notes">&nbsp;</td>                
                                    </tr>                        
                                    
                                    <tr>
                                        <td align="left"  class="labelFreeForm" width="150px">
                                            Select File:</td>
                                    </tr>
                                    <tr>                    
                                        <td align="left">
                                            <asp:FileUpload ID="FileUpload1" runat="server" cssclass="entryfld2"  Width="350px"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button
                                                ID="btPreview" runat="server" Text="Preview" cssclass="btnsmall2"/>
                                        </td>                
                                    </tr>

                                    <tr>
                                        <td  align="left" class="labelFreeForm" style="padding:2px;">
                                        <asp:UpdatePanel ID="fileup" runat="server" UpdateMode="conditional">
                                        <ContentTemplate>
                                        <asp:Panel id="pnlUploadFile" runat="server" Visible="false">
                                        <table cellpadding="0" cellspacing="0" width="100%" style="border-collapse:collapse;border:solid 1px #999999;background-color:#CCCCCC">
                                        <tr>
                                        <td align="left" >
                                            <asp:Label ID="lbFTBU" runat="server" Text="File to be uploaded:" Visible="true"></asp:Label>&nbsp;<asp:LinkButton ID="lbFileUploaded" runat="server" Text=""  Visible="true" style="color:Blue"></asp:LinkButton>
                                            <asp:HiddenField ID="hfFileUploaded" runat="server" />
                                            <asp:HiddenField ID="hfFileSize" runat="server" />
                                            <asp:HiddenField ID="hfDocId" runat="server" />
                                        </td>
                                        <td align="center" width="15px">
                                            <asp:ImageButton ID="imgDeleteFile" runat="server" visible="true" ImageUrl="images/button/close.jpg"/>
                                        </td>
                                        </tr>
                                        </table>
                                            </asp:Panel>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        </td>
                                    </tr>            
                                    
                                    <tr>
                                    <td>
                                    <asp:UpdatePanel ID="dtDI" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
             
                                    <table width="100%" cellpadding="0" border="0" cellspacing="0">                                    
                                    <tr>
                                        <td align="left" class="labelFreeForm"  width="150px">Document Classification:</td>            
                                        
                                        <td align="left"  style="padding-bottom:10px">
                                        <table border="0" width="100%" cellpadding="0" cellspacing="0" style="border-collapse:collapse">
                                        <tr>
                                        <td>
                                        
                                            <asp:RadioButton ID="rbExt" runat="server" GroupName="DocCat" Text="External" Checked="true" />
                                            <asp:RadioButton ID="rbInt" runat="server" GroupName="DocCat" Text="Internal" />
                                            </td>
                                            
                                        <td align="right" style="padding-right:50px"><asp:CheckBox ID="cbConfidential" runat="server"  style="font-weight:bold" Text="Confidential" />
                                        </td>
                                        </tr>
                                        </table>
                                        </td>
                                    </tr>
                                    
                                        <tr>
                                            <td align="left" class="labelFreeForm" width="150px">
                                                Document Title: *
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="tbDocTitle" runat="server" cssclass="entryfld" MaxLength="300" 
                                                    Width="310px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    <tr>
                                        <td align="left"  class="labelFreeForm" width="145px">Document Type: * &nbsp;
                                        </td>
                                    
                                        <td align="left" ><asp:DropDownList ID="dlDocType" runat="server" cssclass="entryfld2" Width="316px" AutoPostBack="true">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td align="left"  class="labelFreeForm" width="145px"><asp:Literal ID="ltypeofrequest" runat="server" Text="Type of Request:"></asp:Literal>
                                        </td>
                                    
                                        <td align="left" ><asp:DropDownList ID="dlRequestType" runat="server" cssclass="entryfld2" Width="316px" AutoPostBack="true">
                                            </asp:DropDownList></td>
                                    </tr>
                                        <tr>
                                            <td align="left" class="labelFreeForm" width="145px">
                                                Total No of Copies: &nbsp;
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="tbNoCopies" runat="server" cssclass="entryfld" Width="310px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    <tr>
                                        <td align="left"  class="labelFreeForm" width="145px"><asp:Literal ID="lTotalNoPages" runat="server" Text="Total No of Pages:"></asp:Literal>
                                        </td>
                                    
                                        <td align="left" ><asp:TextBox ID="tbNoPages" runat="server" cssclass="entryfld"  Width="310px"></asp:TextBox></td>
                                    </tr>
                                    <%--'internal/remarks
                                    <tr>
                                        <td align="left"  class="labelFreeForm" width="145px">Additional Remarks:
                                        </td>
                                    
                                        <td align="left" ><asp:TextBox ID="tbRemarks" runat="server" cssclass="entryfld"  Width="205px"></asp:TextBox></td>
                                    </tr>
                                    --%>
                                    <tr>
                                        <td align="left"  class="labelFreeForm" width="145px"><asp:Literal ID="lMannerofReceipt" runat="server" Text="Manner of Receipt:"></asp:Literal> 
                                        </td>
                                    
                                        <td align="left" ><table style="border-collapse:collapse" border="0" cellpadding="0" cellspacing="0"><tr><td><asp:DropDownList ID="dlReceipt" runat="server" autopostback="true" cssclass="entryfld2" Width="205px">
                                            </asp:DropDownList></td><td>
                                            <asp:UpdatePanel ID="pRetCard" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                            <asp:TextBox ID="tbRetCard" placeholder="Return Card" visible="false" runat="server" cssclass="entryfld" maxlength="20" Width="103px" style="margin-left:2px;"></asp:TextBox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="dlReceipt" EventName="SelectedIndexChanged"/>
                                            </Triggers>
                                            </asp:UpdatePanel>
                                            </td></tr></table>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td align="left"  class="labelFreeForm" width="145px">Sender:&nbsp;
                                        </td>
                                    
                                        <td align="left" >
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:TextBox ID="tbDocSender" runat="server" cssclass="entryfld" maxlength="100" text="" Width="310px"></asp:TextBox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="dlReceipt" EventName="SelectedIndexChanged"/>
                                            </Triggers>
                                            </asp:UpdatePanel>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td align="left"  class="labelFreeForm" width="145px" valign="top">Notes:&nbsp;
                                        </td>                                    
                                        <td align="left" ><asp:TextBox ID="tbNotes" runat="server" cssclass="entryfld" TextMode="MultiLine" maxlength="300" Rows="3" Width="310px"></asp:TextBox>
                                            <asp:HiddenField ID="hfNoteId" runat="server" Value=""/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left"  class="labelFreeForm" width="145px" valign="top"><asp:Literal ID="lArchiveLabel" runat="server" Text="Archive:"></asp:Literal>&nbsp;
                                        </td>                                    
                                        <td align="left" >
                                            <asp:CheckBox ID="cbArchive" runat="server" autopostback="true" /></td>
                                    </tr>
                                    </table>
                                   </ContentTemplate>
                                   <Triggers><asp:PostBackTrigger ControlID="cbArchive" /></Triggers>
                                   </asp:UpdatePanel>
                                    </td>
                                    </tr>
                                    </table>
                                    <table width="99%" style="margin-bottom:4px;border-collapse:collapse" >       
                                    <tr>
                                    <td>
                                    <asp:UpdatePanel ID="pnDR" runat="server" UpdateMode="Conditional">
                    
                                    <ContentTemplate>
                                    <%--<fieldset style="margin: 0 0 0 0;padding:0"><legend style="margin:0;padding:0">Document Index</legend>--%>
                                    <div style="max-height:300px; overflow:auto;">
                                            <uc:UserControlDocumentIndex id="di" runat="server"></uc:UserControlDocumentIndex>
                                            </div>
                                            <%--</fieldset>--%>
                                            </ContentTemplate>                    
                                                </asp:UpdatePanel>
                                    </td>
                                    </tr>   
                                            <tr>
                                        <td class="tableHead" style="height:20px;border-top:solid 1px gray;border-bottom:solid 1px gray;border-right:solid 1px gray;border-left:solid 1px gray;padding-left:4px"  align="left">
                                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                <tr>
                                                    <td>Document Routing</td>
                                                    <td align="right">
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <asp:ImageButton ID="imgHelp" runat="server" ImageUrl="images/question.png" />
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                     </td>
                                                </tr>
                                            </table>
                                        </td>
                                        
                                    </tr>   
                                    <tr>
                                        <td style="border-bottom:solid 1px gray;border-right:solid 1px gray;border-left:solid 1px gray;padding-left:2px;">
                                                
                                                    <table width="100%">
                                                        <tr>
                                                        <td align="left" width="140px" class="labelFreeForm">
                                                            Office:
                                                        </td>
                                                        <td align="left">
                                                        <asp:UpdatePanel ID="pnlOfcCode" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="dlOfficeCode" runat="server" cssclass="entryfld2" Width="350px" AutoPostBack="true">
                                                            </asp:DropDownList>
                                                            </ContentTemplate>
                                                            </asp:UpdatePanel>


                                                        </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" colspan="2">                               
                                                                <uc:UserControlDocRouting ID="ucDocRouting" runat="server" />
                                                            </td>
                        
                                                        </tr>                
                                                    </table>
                                                   
                                        </td>
                                      </tr>
                                    </table>
                                        </div>  
                                        </td>
                                        
                                            <td>
                                                &nbsp;</td>
                                            <td style="border-bottom:solid 1px gray;border-right:solid 1px gray;border-left:solid 1px gray;padding-left:2px;height:100%" 
                                                valign="top">
                                                <%-- old palce of document routing --%>
                                                <uc:UserControlPDFViewer ID="ucPDFViewer" runat="server" visible="false" />
                                                <uc:UserControlDocViewer ID="ucDocViewer" runat="server" visible="False" />
                                                <uc:UserControlImgViewer ID="ucViewer" runat="server" visible="False" />
                                            </td>
                                        
                                    </tr>
                                    </table>
                                    <%--<asp:UpdatePanel ID="plButtons" runat="server" UpdateMode="Conditional" >
                                                    <ContentTemplate>--%>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%" style="border-collapse:collapse; background-color:White;">
                                    <tr>
                                            <td align="left" style="padding: 2px 2px 2px 2px">
                                                                    <div id="prc" style="visibility: hidden ; font-style: italic;font-size:10px;color:Blue;">
                            <img src="images/processing.gif" /> Processing...
                        </div>
                        <div id="pbtn" style="visibility: visible">
                                                <asp:Button ID="btSave" runat="server" CssClass="btnsmall2" OnClientClick="document.getElementById('pbtn').style.visibility='hidden';document.getElementById('prc').style.visibility='visible';return true;"
                                                    style="margin-right:4px" Text="Upload" UseSubmitBehavior="true" />
                                                <asp:Button ID="btCancel" runat="server" CssClass="btnsmall2" Text="Cancel" Visible="false" />
                                                <asp:Button ID="btUpdate" runat="server" CssClass="btnsmall2" OnClientClick="document.getElementById('pbtn').style.visibility='hidden';document.getElementById('prc').style.visibility='visible';return true;"
                                                    style="margin-right:4px" Text="Update" UseSubmitBehavior="true" 
                                                    visible="false" />
                                                    
                                                <asp:Label ID="lMsg2" runat="server" cssclass="msg_red" Visible="True"></asp:Label>
                        
                        
                        </div> 
                                            </td>
                                        </tr>
                              </table>
                            <%--</ContentTemplate>
                                                </asp:UpdatePanel>--%>
                        </asp:Panel>
                        <cc1:DropShadowExtender ID="dse" runat="server" TargetControlID="pAddDoc" Opacity=".5" Rounded="false"/>
     <asp:Panel id="pMsg" runat="server" Visible="false" height="100%" width="800px" style="margin-top:45px">
                            <table border="0" cellspacing="0" class="popuphdrbox" cellpadding="0" style="border-collapse:collapse;width:100%;border:solid 1px #27468A">
                            <tr>
                             <td class="tableheader">
                                <table cellspacing="0" cellpadding="0" border="0" style="width:100%">
                                            <tr height="30px">
                                            <td align="left" valign="middle" colspan="2">&nbsp;<img height="20px" width="18px" src="images/print.png" />&nbsp;Print Acknowledgement Receipt</td>
                                            
                                            <td  align="right" valign="top">
                                                <asp:ImageButton ID="imgCloseAck" runat="server" imageurl="images/close_window.gif" onmouseover="this.src='images/close_window.gif'"  onmouseout="this.src='images/close_window.gif'" width="18px" Height="18px"/>
                                            </td>
                                            </tr>
                                </table>                                   
                                    
                             </td>
                             </tr>
            <tr>
               <td align="center">
               <asp:UpdatePanel ID="pnlMsg" runat="server" UpdateMode="Conditional" >
                            <ContentTemplate>
                            <asp:Panel ID="pWarning" runat="server" Visible="false" style="overflow:auto;max-height:550px;">
                                <table>
                                    <tr>
                                        <td style="font-size:15pt">
                                            Note: Acknowledgement Receipt will not be available since this document was not routed to any user. 
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                                <asp:Panel ID="ackreceipt" runat="server" Visible="false" style="overflow:auto;max-height:550px;">
                                <br />
                                <table id="printArea" style="background-image:url('images/logo/watermark.png');background-repeat:no-repeat;background-position:center;height:100%;width:100%;font-family:Arial;font-size:10pt">
                                <tr>
                                <td colspan="2" align="center">
                                <table cellpadding="0" cellspacing="0" style="width:100%">
                                <tr>
                                <td align="center">
                                    <table width="100%">
                                    <tr>
                                    <td width="40%"></td>
                                    <td align="center" width="100px"><%--<img alt="" src="images/logo/dbm.png" height="90px" width="90px"/>--%>
                                    <asp:Image ID="imgLogo" runat="server" Height="90px" Width="90px"/>
                                    </td>
                                    <td width="40%" align="center" valign="top"><div style="border:solid 1px #222222;font-size:8pt;width:200px;">In following-up, pls. cite DMS ref #<br /><asp:Label ID="lrefno" runat="server" style="font-size:12pt;font-family: Arial Black"></asp:Label></div></td>
                                    </tr>
                                    </table>                                    
                                    

                                    
                                </td>
                                </tr>
                                <tr>
                                    <td align="center" style="font-weight:normal;font-family:Book Antiqua;font-size:11pt;">REPUBLIC OF THE PHILIPPINES</td>
                                </tr>
                                <tr>
                                    <td align="center" style="font-weight:bold;font-family:Book Antiqua;font-size:13pt;padding:1px;"><asp:Literal ID="lTitle" runat="server" Text="DEPARTMENT OF BUDGET AND MANAGEMENT"></asp:Literal></td>
                                </tr>
                                <tr>
                                    <td align="center" style="font-weight:normal;font-family:Cambria;font-size:10pt;"><asp:Literal ID="lAddress" runat="server" Text="General Solano St, San Miguel, Manila"></asp:Literal></td>
                                </tr>
                                <tr>
                                    <td align="center" style="font-size:14px;font-weight:bold;padding:15px;">ACKNOWLEDGEMENT RECEIPT</td>
                                </tr>
                                </table>
                                </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="left" style="padding-top:5px;padding-bottom:15px;"><p style="text-indent:50px;text-align:justify; line-height:20px;">The <b><asp:Literal ID="lTitle2" runat="server" Text="Department of Budget and Management"></asp:Literal></b> hereby acknowledges the receipt of
your letter/request which has been uploaded to the DBM-Document Management System
and routed to the appropriate office/s with the following information:</p></td>
                                </tr>
                                <tr>
                                    <td align="left" style="padding-left:50px;padding-top:10px;" valign="top">Sender: </td>
                                    <td align="left" style="padding-top:10px;" valign="top">
                                        <asp:Literal ID="lSender" runat="server"></asp:Literal></td>
                                </tr>
                                <tr>
                                    <td align="left" style="padding-left:50px;padding-top:10px;" valign="top">Document Title: </td>
                                    <td align="left" style="padding-top:10px;">
                                        <div style="width:500px;word-wrap:break-word;border:0px;">
                                        <asp:Literal ID="lackTitle" runat="server"></asp:Literal>
                                        </div></td>
                                </tr>
                                <tr>
                                    <td align="left" style="padding-left:50px;padding-top:10px;" nowrap valign="top">Document Reference No: </td>
                                    <td align="left" style="padding-top:10px;" valign="top">
                                        <asp:Literal ID="lackFilename" runat="server"></asp:Literal></td>
                                </tr>
                                <tr>
                                    <td align="left" style="padding-left:50px;padding-top:10px;" valign="top">Date and Time Uploaded: </td>
                                    <td align="left" style="padding-top:10px;" valign="top">
                                        <asp:Literal ID="lackDate" runat="server"></asp:Literal></td>
                                </tr>
                                <tr>
                                    <td align="left" style="padding-left:50px;padding-top:10px;" valign="top">Uploaded By: </td>
                                    <td align="left" style="padding-top:10px;" valign="top">
                                        <asp:Literal ID="lCreatedBy" runat="server"></asp:Literal></td>
                                </tr>
                                <tr>
                                    <td align="left" style="padding-left:50px;padding-top:10px;" valign="top">Routed To: </td>
                                    <td align="left" style="padding-top:10px;" valign="top">
                                        <asp:Literal ID="lRoutedTo" runat="server"></asp:Literal></td>
                                </tr>
                                <tr>
                                    <td align="left" style="padding-left:50px;padding-top:10px;" valign="top">&nbsp;</td>
                                    <td align="left" style="padding-top:10px;" valign="top">
                                        CC: <asp:Literal ID="lCarbon" runat="server"></asp:Literal></td>
                                </tr>
                                <tr>
                                    <td align="left" style="padding-left:50px;padding-top:10px;" valign="top">Total no of pages received: </td>
                                    <td align="left" style="padding-top:10px;" valign="top">
                                        <asp:Literal ID="lPages" runat="server"></asp:Literal></td>
                                </tr>
                                    <asp:Panel ID="pNotes" runat="server" Visible="false">
                                    <tr>
                                    <td align="left" style="padding-left:50px;padding-top:10px;" valign="top">Notes: </td>
                                    <td align="left" style="padding-top:10px;" valign="top">
                                        <asp:Literal ID="lRemarks" runat="server"></asp:Literal></td>
                                </tr>

                                    </asp:Panel>
                                <%--'internal/remarks
                                <tr>
                                    <td align="left" style="padding-left:50px;padding-top:10px;" valign="top">Additional Remarks: </td>
                                    <td align="left" style="padding-top:10px;" valign="top">
                                        <asp:Literal ID="lRemarks" runat="server"></asp:Literal></td>
                                </tr>
                                --%>
                                <tr>
                                    <%--<td colspan="2" align="left" style="padding-top:5px;padding-bottom:5px; line-height:20px;">--%>
                                    <td colspan="2" align="left" style="padding-top:20px;padding-bottom:30px; line-height:20px;">
<p style="text-indent:50px; text-align:justify; line-height:20px;">The determination of the completeness of the documentary requirements
submitted, if any, is subject to the evaluation of the technical person in
charge.</p>
<p style="text-indent:50px; line-height:20px;">This receipt is system generated and does not require signature.</p></td>
                                </tr>
                                <tr>
                                <td colspan="2" align="left" style="padding-top:5px">Received by:</td>
                                </tr>
                                <tr>
                                <td colspan="2" align="left" style="padding-top:10px">                                    
                                    <asp:Image ID="imgBottomLogo" runat="server" ImageUrl="images/logo/logo.png" /></td>
                                </tr>
                                </table>
                                
                                </asp:Panel>
                                </ContentTemplate>
                        </asp:UpdatePanel>
                                </td>
                                </tr>
                                <tr>
                                <td align="center" style="background-color: #D4D4D4;padding:5px;">
                                <asp:Button ID="btPrint" runat="server" OnClientClick="printAck('printArea')" Text="Print Receipt" ToolTip="Print acknowledgement receipt"   CssClass="btnsmall2" style="margin-right:4px"/>
                                    <asp:Button ID="btRoute" runat="server" Text="Print Route Slip" ToolTip="Print Route Slip"   CssClass="btnsmall2" style="margin-right:4px"/>
                                    <asp:Button ID="btAdd" runat="server" Text="Upload Another"   CssClass="btnsmall2" ToolTip="Upload Another document" style="margin-right:4px"/>
                                    <asp:Button ID="btBack" runat="server" Text="Back"   CssClass="btnsmall2" ToolTip="Update document" style="margin-right:4px"/>
                                    <asp:Button ID="btView" runat="server" Text="View Document"   CssClass="btnsmall2" ToolTip="View document" style="margin-right:4px"/>
                                </td></tr>
                                </table>
                    </asp:Panel>        
                  </td>
                </tr>                
                </table>
            
            </asp:Panel>
<uc:ucReceiptRoute id="uReceiptRoute" runat="server" Visible="false"></uc:ucReceiptRoute>           
           
