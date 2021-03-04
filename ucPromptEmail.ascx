<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucPromptEmail.ascx.vb" Inherits="dms.ucPromptEmail" %>
<%@ Register src="UserControlEmail.ascx" tagname="ucEmail" tagprefix="uc" %>



                 <table width="100%" height="100%" border="1" cellspacing="0" cellpadding="0" style="z-index:20002;">
                 <tr>
                 <td align="center" valign="middle">
                 <center>
                 
                    <div id="div1" style="padding-bottom:10px;background-color:white;width:510px;border-radius:4px;box-shadow: 1px 1px 1px 1px #808080;">
                    <div style="border-radius:4px 4px 0px 0px;background-color:#000080; padding:3px;border-bottom:solid 1px gray;">
                    <table border="0" cellpadding="2" cellspacing="2" style="color: #EAEAEA;font-family: Arial,Calibri;font-weight: bold;font-size: 12pt;background-color:#000080;width: 100%;border-collapse:collapse;border-radius:4px;">
                    <tr >
                                <td align="left" style="color:White;font-weight:bold;">
                                    <asp:Literal ID="lsTitle" runat="server" Text=""></asp:Literal>
                                </td>
                                <td valign="bottom" align="right">
                                    <asp:ImageButton ID="imgClose" runat="server" ImageUrl="images/close_window.gif" /> 
                                </td>
                            </tr>
                            </table>
                            </div>
                        <table border="0" cellpadding="5" cellspacing="2" style="width: 100%;border-collapse:collapse;margin-top:10px;margin-bottom:10px">
                            <tr>
                                <td align="left" style="font-size:10pt;font-weight:bold;font-family:Arial;">
                                    From: <asp:Literal ID="lFrom" runat="server" Visible="true"></asp:Literal>
                                </td>
                            </tr>                       
                            <tr>
                                <td align="left" style="font-size:10pt;font-weight:bold;font-family:Arial;">
                                    Recipient(s):<asp:Literal ID="lMsg" runat="server" Visible="false"></asp:Literal>
                                </td>
                            </tr>                            
                            <tr>
                                <td align="left">                                    
                                <asp:UpdatePanel ID="pEmail" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div style="height:20px;max-height:50px;overflow:auto;">
                                    <asp:Repeater ID="Repeater1" runat="server">
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <span style="border-radius:4px;border:solid 1px #CCCCCC;margin:2px;font-size:8pt;font-weight:normal;font-family:Arial;padding:2px;">                                            
                                            <asp:Label ID="lEmail" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "recipient"))%>'  ToolTip='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "email"))%>'></asp:Label><asp:ImageButton ID="imgDel" ImageUrl="images/deletemail.png" runat="server" onclick="fDelete" style="vertical-align:middle"/>
                                        </span>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                    </FooterTemplate>
                                    </asp:Repeater>
                                    </div>
                                    </ContentTemplate></asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <uc:ucEmail runat="server" id="uEmail" pHideCloseButton="False"></uc:ucEmail>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">                                    
                                    <asp:Button ID="btOK" runat="server" Text="OK" CssClass="btn" />&nbsp;<asp:Button ID="btClose" runat="server" Text="Cancel" CssClass="btn" /></td>
                            </tr>
                        </table>
                    </div>
                  </center>
                 </td>
                 </tr>
                 </table>

