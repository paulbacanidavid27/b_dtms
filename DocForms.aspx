<%@ Page Title="Forms" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="DocForms.aspx.vb" Inherits="dms.DocForms" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="UserControlBookMark.ascx" TagName="UserControlBk" TagPrefix="uc" %>
<%@ Register Src="UserControlUpload.ascx" TagName="UserControlDocumentUpload" TagPrefix="uc" %>
<%@ Register Src="ucFormAttach.ascx" TagName="uAttach" TagPrefix="uc" %>
<%@ Register Src="UserControlPager.ascx" TagName="UserControlPager" TagPrefix="uc" %>
<%@ Register Src="ucHr.ascx" TagName="ucHr" TagPrefix="uc1" %>
<%@ Register Src="ucButton.ascx" TagName="ucButton" TagPrefix="uc" %>
<%@ Register Src="ucConfirm.ascx" TagName="ucConfirm" TagPrefix="uc" %>
<%@ Register Src="UserControlCheckBox.ascx" TagName="UserControlCheckBox" TagPrefix="uc" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <title>Forms</title>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="MainHeaderContent">

    <table border="0" width="100%">
        <tr>
            <td class="tableheader_" valign="middle">

                <img src="images/doclist.png" tooltip="Goto Inbox" height="20px" width="20px" />
                Forms
                            
            </td>
            <td></td>
            <td align="right">
                <%--pager: step 2--%>

                <asp:UpdatePanel ID="pPager" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:HiddenField ID="hfCurrent" runat="server" Value="1" />
                        <asp:HiddenField ID="hfTotalRows" runat="server" Value="0" />
                        <asp:HiddenField ID="hfSortCol" runat="server" Value="Uploaded Date" />
                        <asp:HiddenField ID="hfSortOrder" runat="server" Value="Desc" />
                        <uc:UserControlPager ID="ucPager" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">


    <div class="mainDiv1" align="left">


        <!-- end - search criteria //-->
        <!-- start - resultset //-->
        <asp:UpdatePanel ID="pnlCon2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <uc:ucConfirm ID="ucCon2" runat="server" Visible="false" pText="Are you sure you want to move the selected document(s)? Please click OK to proceed."></uc:ucConfirm>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="pnlRepeater" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                

                    <uc:ucConfirm ID="ucCon" runat="server" Visible="false" pText="Are you sure you want to delete the selected document(s)? Please click OK to proceed."></uc:ucConfirm>
                     
        <asp:Panel ID="pRepeater" runat="server">
                    <table border="0" class="codetbl" cellspacing="0" cellpadding="0" width="100%" style="border-collapse: collapse; z-index: 900; border: #D4D4D4 solid 1px">

                        <tr>
                            <td class="newtblheader"></td>
                            <td class="newtblheader">
                                <asp:LinkButton ID="lbSort1" runat="server" class="sortcol" ToolTip="Sort by File Name" OnClick="sortColumnHeader">File Name</asp:LinkButton><asp:Image ID="imgSort1" ImageUrl="images/asc.png" runat="server" Visible="false" Style="vertical-align: middle;" /></td>
                            <td class="newtblheader">
                                <asp:LinkButton ID="lbSort2" runat="server" class="sortcol" ToolTip="Sort by Description" OnClick="sortColumnHeader">Description</asp:LinkButton><asp:Image ID="imgSort2" ImageUrl="images/asc.png" runat="server" Visible="false" Style="vertical-align: middle;" /></td>
                            <td class="newtblheader">
                                <asp:LinkButton ID="lbSort3" runat="server" class="sortcol" ToolTip="Sort by Uploaded By" OnClick="sortColumnHeader">Uploaded By</asp:LinkButton><asp:Image ID="imgSort3" ImageUrl="images/desc.png" runat="server" Visible="false" Style="vertical-align: middle;" /></td>
                            <td class="newtblheader">
                                <asp:LinkButton ID="lbSort4" runat="server" class="sortcol" ToolTip="Sort by Uplodated Date" OnClick="sortColumnHeader">Updloaded Date</asp:LinkButton><asp:Image ID="imgSort4" ImageUrl="images/asc.png" runat="server" Visible="true" Style="vertical-align: middle;" /></td>


                            <asp:Repeater ID="rptRecordList" Visible="true" runat="server">
                                <HeaderTemplate>
                                    <td class="newtblheader">
                                        <asp:ImageButton ID="imgDelete" runat="server" Height="20px" Width="15px" ImageUrl="images/del.png" OnClick="imgDelete_Click" /></td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>

                                        <td class="tbldashed" valign="top" align="center" nowrap style="padding-top: 3px;">
                                            <asp:UpdatePanel runat="server" ID="pAT" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:ImageButton ID="imgUpd" runat="server" Height="14px" ImageUrl="images/update.png" />
                                                    <asp:ImageButton ID="iD" runat="server" ToolTip="Download Form" Height="14px" ImageUrl="images/download_doc.png" OnClick="fDownload" />
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="iD" />
                                                    <asp:PostBackTrigger ControlID="imgUpd" />
                                                </Triggers>
                                            </asp:UpdatePanel>

                                            <asp:Literal ID="lFormId" Visible="false" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "FormId"))%>'></asp:Literal>
                                        </td>
                                        <td class="tbldashed" valign="top" width="12px" align="left" style="padding-top: 3px;">
                                            <div class="xxx" style="width: 200px">
                                                <asp:LinkButton ID="lbtnDoc" runat="server" onmouseover="this.style.textDecoration = 'underline',this.style.color='#00005E'" onmouseout="this.style.textDecoration = 'none',this.style.color='blue'" Style="color: blue; font-family: arial; font-size: 8pt;" ToolTip='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "FormFileName"))%>'>
                                                    <asp:Literal ID="lFormFileName" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "FormFileName"))%>'></asp:Literal>
                                                </asp:LinkButton>

                                            </div>

                                        </td>
                                        <td class="tbldashed" valign="top" style="padding-top: 3px;" nowrap>
                                            <asp:Literal ID="lDescription" runat="server" Visible="true" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Description"))%>'></asp:Literal>
                                            <asp:TextBox ID="tbDescription" runat="server" Visible="false" MaxLength="100"></asp:TextBox>
                                            <asp:ImageButton ID="imgSave" runat="server" Visible="false" Height="14px" Width="15px" ImageUrl="images/save.png" OnClick="imgSave_Click" />
                                        </td>
                                        <td class="tbldashed" valign="top" style="padding-top: 3px;" nowrap>
                                            <asp:Literal ID="lUploadedBy" runat="server" Visible="false" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "UploadedBy"))%>'></asp:Literal>
                                            <asp:Literal ID="lUploadedByName" runat="server" Visible="true" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "UploadedByName"))%>'></asp:Literal>

                                        </td>
                                        <td class="tbldashed" valign="top" style="padding-top: 3px;" nowrap>
                                            <asp:Literal ID="lUploadedDate" runat="server" Visible="true" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "UploadedDate"))%>'></asp:Literal>

                                        </td>

                                        <td class="tbldashed">
                                            <asp:CheckBox ID="cbxDelete" runat="server" Visible="true" /></td>

                                    </tr>

                                </ItemTemplate>

                                <FooterTemplate>
                                    <tr>
                                        <td style="border-top: solid 1px #ffffff" colspan="6"></td>
                                    </tr>


                                </FooterTemplate>
                            </asp:Repeater>
                    </table>
                </asp:Panel>
           </ContentTemplate>

        </asp:UpdatePanel>
        <!-- end - resultset //-->
    </div>
</asp:Content>
<asp:Content ID="mContent" runat="server" ContentPlaceHolderID="menuContent">

    <uc:ucButton ID="ucAddDoc" runat="server" pText="Upload Form" pImage="images/upload2.png"></uc:ucButton>
    <table border="0" width="100%" cellpadding="0" cellspacing="0" style="border-collapse: collapse">
        <tr>
            
            <td align="left">
                <table border="0" width="95%">

                    <tr>
                        <td align="left">

                            <!-- start - search criteria //-->

                            <div style="border-style: solid; border-width: 1px; border-color: #F1F4F8 #CFDBE7 #81A0C0 #CEDAE8; background-color: #FFFFFF; width: 98%; margin-top: 8px; margin-left: 1px">
                                <asp:UpdatePanel ID="pnlFilter" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" class="newtblheader2">
                                            <tr height="25px">
                                                <td align="left" class="tableHead27" style="padding-left: 3px">
                                                    <img alt="" width="24px" height="20px" src="images/find.png" />&nbsp;&nbsp;<asp:Label ID="lbBookMrk" runat="server" Style="color: #EEEEEE; font-family: Arial; font-size: 10pt; font-weight: bold; font-style: normal; color: #CCCCCC">Filter Documents</asp:Label></td>
                                                <td width="50px" align="right" valign="top" class="tableHead27">
                                                    <asp:ImageButton ID="imgBk" runat="server" ImageUrl="images/showpanel.png" /></td>
                                            </tr>
                                        </table>

                                        <asp:Panel runat="server" ID="pFilter" Visible="false" DefaultButton="btSearch">
                                            <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                                <tr>
                                                    <td align="left" class="labelFreeForm">File Name:</td>
                                                    <td align="left">
                                                        <asp:TextBox ID="tbFileName" CssClass="entryfldw" runat="server" MaxLength="100" Width="245px"></asp:TextBox>
                                                    </td>

                                                    <td align="left">&nbsp;</td>

                                                </tr>
                                                <tr>
                                                    <td align="left" class="labelFreeForm">Description:</td>
                                                    <td align="left">
                                                        <asp:TextBox ID="tbDescription" CssClass="entryfldw" runat="server" MaxLength="100" Width="245px"></asp:TextBox>
                                                    </td>

                                                    <td align="left">&nbsp;</td>

                                                </tr>

                                                <tr>
                                                    <td align="left" class="labelFreeForm">Uploaded By:</td>
                                                    <td align="left">
                                                        <asp:TextBox ID="tbUploadedBy" runat="server" CssClass="entryfldw" AutoComplete="off" Width="245px"> </asp:TextBox>
                                                        <cc1:AutoCompleteExtender runat="server" ID="acomplete" TargetControlID="tbUploadedBy"
                                                            ServiceMethod="getUsers" ServicePath="getUser.asmx" CompletionInterval="800" EnableCaching="true"
                                                            MinimumPrefixLength="1" FirstRowSelected="false"
                                                            CompletionSetCount="25" />
                                                    </td>

                                                    <td align="left">&nbsp;</td>

                                                </tr>



                                                <tr>
                                                    <td align="left" class="labelFreeForm" nowrap>Uploaded Date:</td>
                                                    <td align="left">
                                                        <asp:TextBox ID="tbUploadedFromDate" CssClass="entryfldw" runat="server" MaxLength="100" Width="67px"></asp:TextBox>-
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="tbUploadedfromDate" />
                                                        <asp:TextBox ID="tbUploadedToDate" CssClass="entryfldw" runat="server" MaxLength="100" Width="67px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="tbUploadedToDate" />
                                                    </td>

                                                    <td align="left">&nbsp;</td>

                                                </tr>
                                                <tr>
                                                    <td align="left">&nbsp;</td>
                                                    <td align="right">
                                                        <asp:Button ID="btSearch" runat="server" CssClass="btnsmall" Text="Filter" />
                                                    </td>

                                                    <td align="left">&nbsp;</td>

                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                            <!-- end - search criteria //-->
                            <uc:UserControlBk ID="ucb" runat="server"></uc:UserControlBk>
                            <br />

                        </td>

                    </tr>

                </table>



            </td>


        </tr>

    </table>
</asp:Content>

<asp:Content ID="cntUpload" runat="server" ContentPlaceHolderID="cntntUpload">
    <uc:uAttach runat="server" ID="ucAtt" Visible="False" />
    <uc:UserControlDocumentUpload runat="server" ID="ucUpload" Visible="False" />

</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cpPeriod">
</asp:Content>
