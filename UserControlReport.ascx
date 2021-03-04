<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlReport.ascx.vb" Inherits="dms.UserControlReport" %>
 <br />

           <div style="border:solid 1px gray;font-family:Verdana;font-size:9pt;font-weight:bold; background-color: #264E75; color: #FFFFFF; width:70%;padding-left:1px;padding-bottom:1px;padding-top:3px;">List of Reports
           
            <asp:Panel ID="pDocumentStatus" runat="server" Visible="true" cssclass="hfReportTd">
                <asp:HyperLink ID="HyperLink1" class="hlReportLink" onmouseover="m_over(this)" onmouseout="m_out(this)" runat="server" NavigateUrl="~/DocumentStatusCriteria.aspx">
                    <img alt="" src="images/report_icon.png" /> Document Status
                </asp:HyperLink>
           </asp:Panel>         
           <asp:Panel ID="pGroupStatus" runat="server" Visible="true" cssclass="hfReportTd">
                <asp:HyperLink ID="HyperLink12" cssclass="hfReportLink" onmouseover="m_over(this)" onmouseout="m_out(this)" runat="server" NavigateUrl="~/GroupStatusCriteria.aspx">
                <img alt="" src="images/report_icon.png" /> Group Status</asp:HyperLink>
           </asp:Panel>    
           <asp:Panel ID="pDocumentsUploaded" runat="server" Visible="true" cssclass="hfReportTd">
                <asp:HyperLink ID="HyperLink9" cssclass="hfReportLink" onmouseover="m_over(this)" onmouseout="m_out(this)" runat="server" NavigateUrl="~/DocumentUploadedCriteria.aspx">
                <img alt="" src="images/report_icon.png" /> Documents Uploaded</asp:HyperLink>
           </asp:Panel>           
           <asp:Panel ID="pDocumentsUploadedHourly" runat="server" Visible="true" cssclass="hfReportTd">
                <asp:HyperLink ID="HyperLink2" cssclass="hfReportLink" onmouseover="m_over(this)" onmouseout="m_out(this)" runat="server" NavigateUrl="~/DocumentUploadedhourlyCriteria.aspx">
                <img alt="" src="images/report_icon.png" /> Documents Uploaded Hourly</asp:HyperLink>
           </asp:Panel>   
           <asp:Panel ID="pDocumentChanges" runat="server" Visible="true" cssclass="hfReportTd">
                <asp:HyperLink ID="HyperLink10" cssclass="hfReportLink" onmouseover="m_over(this)" onmouseout="m_out(this)" runat="server" NavigateUrl="~/criteria_dochistory.aspx">
                <img alt="" src="images/report_icon.png" /> Document Changes (Audit Trail)</asp:HyperLink>
           </asp:Panel>           
           <asp:Panel ID="pDocumentIndex" runat="server" Visible="true" cssclass="hfReportTd">
                <asp:HyperLink ID="HyperLink11" cssclass="hfReportLink" onmouseover="m_over(this)" onmouseout="m_out(this)" runat="server" NavigateUrl="~/DocumentIndexCriteria.aspx">
                <img alt="" src="images/report_icon.png" /> Document Index</asp:HyperLink>
           </asp:Panel>           
           <asp:Panel ID="pDocumentIndexStatus" runat="server" Visible="true" cssclass="hfReportTd">
                <asp:HyperLink ID="HyperLink4" cssclass="hfReportLink" onmouseover="m_over(this)" onmouseout="m_out(this)" runat="server" NavigateUrl="~/DocumentIndexStatusCriteria.aspx">
                <img alt="" src="images/report_icon.png" /> Document Index Status</asp:HyperLink>
           </asp:Panel>  
                    
           <asp:Panel ID="pDocumentRoutingHistory" runat="server" Visible="true" cssclass="hfReportTd">
                <asp:HyperLink ID="HyperLink14" cssclass="hfReportLink" onmouseover="m_over(this)" onmouseout="m_out(this)" runat="server" NavigateUrl="~/DocumentRoutingHistoryCriteria.aspx">
                <img alt="" src="images/report_icon.png" /> Document Routing History</asp:HyperLink>
           </asp:Panel>           
              <asp:Panel ID="pDocumentReceived" runat="server" Visible="true" cssclass="hfReportTd">
                <asp:HyperLink ID="HyperLink6" cssclass="hfReportLink" onmouseover="m_over(this)" onmouseout="m_out(this)" runat="server" NavigateUrl="~/DocumentReceivedCriteria.aspx">
                <img alt="" src="images/report_icon.png" /> Document Received</asp:HyperLink>
          </asp:Panel> 
           
       <asp:Panel ID="pDocumentListPerUser" runat="server" Visible="true" cssclass="hfReportTd">
                <asp:HyperLink ID="HyperLink5" cssclass="hfReportLink" onmouseover="m_over(this)" onmouseout="m_out(this)" runat="server" NavigateUrl="~/DocumentListPerUserCriteria.aspx">
                <img alt="" src="images/report_icon.png" /> Document List Per User</asp:HyperLink>
           </asp:Panel>               
       <asp:Panel ID="pDocumentArchived" runat="server" Visible="true" cssclass="hfReportTd">
                <asp:HyperLink ID="HyperLink8" cssclass="hfReportLink" onmouseover="m_over(this)" onmouseout="m_out(this)" runat="server" NavigateUrl="~/criteria_userdocarchived.aspx">
                <img alt="" src="images/report_icon.png" /> Document Archived Report</asp:HyperLink>
           
       </asp:Panel>
           <asp:Panel ID="pDocumentArchivedPerUser" runat="server" Visible="true" cssclass="hfReportTd">
                <asp:HyperLink ID="HyperLink13" cssclass="hfReportLink" onmouseover="m_over(this)" onmouseout="m_out(this)" runat="server" NavigateUrl="~/DocumentArchivedPerUserCriteria.aspx">
                <img alt="" src="images/report_icon.png" /> Document Archived Per User</asp:HyperLink>
           </asp:Panel>          

           <asp:Panel ID="pDeletedDocuments" runat="server" Visible="true" cssclass="hfReportTd">
                <asp:HyperLink ID="HyperLink15" cssclass="hfReportLink" onmouseover="m_over(this)" onmouseout="m_out(this)" runat="server" NavigateUrl="~/DeletedDocumentCriteria.aspx">
                <img alt="" src="images/report_icon.png" /> Deleted Document Report</asp:HyperLink>
           </asp:Panel>     
                    
           <asp:Panel ID="pTotalAmount" runat="server" Visible="true" cssclass="hfReportTd">
                <asp:HyperLink ID="HyperLink3" cssclass="hfReportLink" onmouseover="m_over(this)" onmouseout="m_out(this)" runat="server" NavigateUrl="~/criteria_release.aspx">
                <img alt="" src="images/report_icon.png" /> Total Amount Released Per Bureau</asp:HyperLink>
           </asp:Panel>           
           <asp:Panel ID="pRecordsDisposition" runat="server" Visible="true" cssclass="hfReportTd">
                <asp:HyperLink ID="HyperLink7" cssclass="hfReportLink" onmouseover="m_over(this)" onmouseout="m_out(this)" runat="server" NavigateUrl="~/criteria_recordsdisposition.aspx">
                <img alt="" src="images/report_icon.png" /> Records Disposition Schedule</asp:HyperLink>
           </asp:Panel>           
           
                 
           <asp:Panel ID="pTotalFiles" runat="server" Visible="true" cssclass="hfReportTd">
                <asp:HyperLink ID="HyperLink16" cssclass="hfReportLink" onmouseover="m_over(this)" onmouseout="m_out(this)" runat="server" NavigateUrl="~/criteria_docsize.aspx">
                <img alt="" src="images/report_icon.png" /> Total Files and Bytes Uploaded Report</asp:HyperLink>                     
            </asp:Panel>
            </div>