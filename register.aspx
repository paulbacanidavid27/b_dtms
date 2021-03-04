<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="register.aspx.vb" Inherits="dms.register" %>
<%@ Register src="UserControlCheckBox.ascx" tagname="ucBox" tagprefix="uc" %>    

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/docview.css" rel="stylesheet" type="text/css" />
    <style type="text/css" >
        .imgb 
        {
            height: 21px; width: 19px;
        }
          .txtbx 
          {
             width:300px; border:solid 1px #D4D4D4;color:Gray;background-color:#EEFFFF;font-family:Verdana;font-size:9pt;padding:5px 3px 5px 3px;
            margin-top: 0px;
        }
        
        
        .style1
        {
            height: 27px;
        }
        
        
    </style>
    </head>
<body style="background-color:#EEEEEE;margin:0px;font-family:Verdana;font-size:9pt" >
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" /> 
    <table cellpadding="0" cellspacing="0" border="0" width="100%" style="border-collapse:collapse;height:120px;background-image:url('images/tabs/header.png');background-repeat:repeat;">
        <tr>
            <td valign="middle">
            <table cellspacing="0" cellpadding="0" border="0" style="border-collapse:collapse;width:100%;height:100%">
                    <tr>
                                                                        
                        <td align="left" valign="top" colspan="2">
                        
                        </td>                       
                    </tr>  
                    <tr>
                    <td style="font-family:Verdana;color:#EEEEEE;font-size:18pt;padding-left:10px;vertical-align:middle; ">
                           <asp:Image ID="Image3" runat="server" Height="50px" Width="70px" imageurl="images/dnlogo.png" Visible="true" />DOCUMENT MANAGEMENT SYSTEM
                           
                        </td>
                        <td align="right" style="padding-right:5px;" valign="top">
             
                        </td>
                    </tr>                                     
                </table>
            
            </td>
        </tr>
        <tr>
            <td valign="bottom">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
<tr>
<td align="left">
<table cellspacing="0" cellpadding="0" border="0" style="font-family:Verdana;font-size:12px;font-weight:normal;">
        <tr align="center">        
        <td width="30px"></td>
        <td id='Td7' runat="server" class="ylts"></td>
        <td id='Td8' runat="server" class="ymts"></td>
        <td id='Td9' runat="server" class="yrts"></td>
        <td></td>
        </tr>
        <tr align="center">        
        <td width="30px"></td>
        <td id='t10' runat="server" class="ylbs">&nbsp;</td>
        <td id='t11' runat="server" class="ymbs" >            
            <asp:Label ID="lHome" runat="server" Text="Registration" Visible="true"></asp:Label>
        </td>
        <td id='t12' runat="server" class="yrbs">&nbsp;</td>
        </tr>
        
       </table>
</td>

</tr>
</table>
            </td>
        </tr>
        </table>
    <asp:UpdatePanel runat="server" id="upReg" UpdateMode="Conditional">
    <ContentTemplate>
    
    <asp:Panel ID="PanelRegistration" runat="server"  Visible="false">
    
    <div style="padding:15px;">
    
        Welcome to <b>Document Management System</b>. Please register in order to access the 
        system. Please fill-up the forms of your informantion if you are first time registering the product. If you are upgrading provide the old registration key. Then select registration type. Click on I agree with the terms and condition and click on Submit button. 
        <p ><li style="color:#0066CC"> For demo version, you only have 30 days to try our system.</li>
            <p>
            </p>
            <p>
            </p>
            <p>
            </p>
            <p>
            </p>
        </p>
    
    </div>
    <table style="width:100%;">
        <tr>
            <td>
                &nbsp;</td>
            <td colspan="3" style="font-size:11pt;color: Navy;font-weight:bold">
                REGISTRATION FORM</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td colspan="3" style="font-size:11pt;color: Navy;font-weight:bold">
                <hr /></td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td colspan="3" style="color:Gray;font-weight:bold">
                1. Please fill-up the following fields</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td colspan="3">
                <table >
                    <tr>
                        <td>
                            First Name</td>
                        <td>
                            <asp:TextBox ID="txFN" runat="server" CssClass="txtbx"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            Last Name</td>
                        <td>
                            <asp:TextBox ID="txLN" runat="server" CssClass="txtbx"></asp:TextBox></td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            Email Address</td>
                        <td>
                            <asp:TextBox ID="txEmail" runat="server" CssClass="txtbx"></asp:TextBox></td>
                        <td>
                            &nbsp;</td>
                    </tr>                    
                    <asp:panel id="pnlup" runat="server" Visible="false">
                    <tr>
                        <td>
                            Old Registration Key:</td>
                        <td>
                            <asp:TextBox ID="txReg" runat="server" CssClass="txtbx"></asp:TextBox></td>
                        <td>
                            &nbsp;</td>
                    </tr>           
                    </asp:panel>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td  style="color:Gray;font-weight:bold">
                2. Select Registration Type</td>
            <td>
                <asp:Panel ID="Panel1" runat="server">
                    <table style="width:100%;">
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:RadioButton ID="rbBasic" runat="server"  style="font-weight:bold;color:Blue;font-size:10pt;" Text="Basic" GroupName="Registration" autopostback="true"/>
                    
                
                </td>
            <td>
                <asp:RadioButton ID="rbStandard" runat="server" style="font-weight:bold;color:Blue;font-size:10pt;"  Text="Standard" GroupName="Registration"  AutoPostBack="true"/></td>
            <td>
                <asp:RadioButton ID="rbEnterprise" runat="server" style="font-weight:bold;color:Blue;font-size:10pt;"  Text="Enterprise" GroupName="Registration" autopostback="true"/></td>
        </tr>
        
        <tr>
            <td valign="top">
                &nbsp;</td>
            <td valign="top">
                <table style="width:100%;" id="tblBasic" runat="server">
                    <tr>
                        <td>
                            <li style="color:#003399"></li></td>
                        <td>
                            Limited to 2 users( 1 admin, 1 user)</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <li style="color:#003399"></li></td>
                        <td>
                            Upload/Download Documents</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <li style="color:#003399"></li></td>
                        <td>
                            Version Documents</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <li style="color:#003399"></li></td>
                        <td>
                            Index Documents</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <li style="color:#003399"></li></td>
                        <td>
                            Print Documents</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <li style="color:#003399"></li></td>
                        <td>
                            Share Documents</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <li style="color:#003399"></li></td>
                        <td>
                            Bookmark Documents</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <li style="color:#003399"></li></td>
                        <td>
                            Document Routing</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
            <td valign="top">
                <table style="width:100%;" id="tblStandard" runat="server">
                    <tr>
                        <td>
                            <li style="color:#003399"></li></td>
                        <td>
                            Limited to 5 users (including admin)</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <li style="color:#003399"></li></td>
                        <td>
                            Upload/Download Documents</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <li style="color:#003399"></li></td>
                        <td>
                            Version Documents</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <li style="color:#003399"></li></td>
                        <td>
                            Index Documents</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <li style="color:#003399"></li></td>
                        <td>
                            Print Documents</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <li style="color:#003399"></li></td>
                        <td>
                            Share Documents</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <li style="color:#003399"></li></td>
                        <td>
                            Bookmark Documents</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <li style="color:#003399"></li></td>
                        <td>
                            Document Routing</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <li style="color:#003399"></li></td>
                        <td>
                            Holiday Restriction</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
            <td valign="top">
                <table style="width:100%;" id="tblEnt" runat="server">
                    <tr>
                        <td>
                            <li style="color:#003399"></li></td>
                        <td>
                            Unlimited Users</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <li style="color:#003399"></li></td>
                        <td>
                            Upload/Download Documents</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <li style="color:#003399"></li></td>
                        <td>
                            Version Documents</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <li style="color:#003399"></li></td>
                        <td>
                            Index Documents</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <li style="color:#003399"></li></td>
                        <td>
                            Print Documents</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <li style="color:#003399"></li></td>
                        <td>
                            Share Documents</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <li style="color:#003399"></li></td>
                        <td>
                            Bookmark Documents</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <li style="color:#003399"></li></td>
                        <td>
                            Document Routing</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <li style="color:#003399"></li></td>
                        <td>
                            Holiday Restriction</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <li style="color:#003399"></li></td>
                        <td>
                            Track Changes</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <li style="color:#003399"></li></td>
                        <td>
                            Backup Database</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <li style="color:#003399"></li></td>
                        <td>
                            Purge/Archive Documents</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <li style="color:#003399"></li></td>
                        <td>
                            Import Documents</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <li style="color:#003399"></li></td>
                        <td>
                            Reports</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td valign="top">
                &nbsp;</td>
            <td valign="top" colspan="3">
                <asp:CheckBox ID="cbTerms" runat="server" /><asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="terms.pdf" Target="_blank" class="menu">I have read and agreed to the 
                terms and 
                conditions of this registration.</asp:HyperLink></td>
        </tr>
        <tr>
        <td colspan="4" align="center"><asp:Label ID="lbMsg" runat="server" Text="" style="color:Red;font-family:Verdana;font-size:9pt"></asp:Label></td>
        </tr>
        <tr>
            <td valign="top" colspan="4" align="center">&nbsp;
                <asp:HiddenField ID="HiddenField1" runat="server" Value="" /> <table id="tblupgrade" runat="server" cellpadding="0" cellspacing="0" border="0"  style="display:none">
                    <tr>
                        <td style="height:38px;width:12px;background-image:url('images/button/lb.png');">
                            &nbsp;</td>
                        <td style="height:38px;background-image:url('images/button/mb.png');">
                            <asp:LinkButton ID="lbUpgrade" runat="server" style="text-decoration:none;font-weight:bold; text-transform:uppercase"  class="menu">Upgrade Registration</asp:LinkButton></td>
                        <td style="height:38px;width:12px;background-image:url('images/button/rb.png');">
                            &nbsp;</td>
                    </tr>
                </table>
               
                
                <table id="tbllive" runat="server" cellpadding="0" cellspacing="0" border="0" style="display:none">
                    <tr>
                        <td style="height:38px;width:12px;background-image:url('images/button/lb.png');">
                            &nbsp;</td>
                        <td style="height:38px;background-image:url('images/button/mb.png');">
                            <asp:LinkButton ID="lbRegister" runat="server" style="text-decoration:none;font-weight:bold; text-transform:uppercase"   class="menu">Submit Registration</asp:LinkButton></td>
                        <td style="height:38px;width:12px;background-image:url('images/button/rb.png');">
                            &nbsp;</td>
                    </tr>
                </table>
                
                
                <table id="tbldemo" runat="server" cellpadding="0" cellspacing="0" border="0" style="display:none">
                    <tr>
                        <td style="height:38px;width:12px;background-image:url('images/button/lb.png');">
                            &nbsp;</td>
                        <td style="height:38px;background-image:url('images/button/mb.png');">
                            <asp:LinkButton ID="lbDemo" runat="server" style="text-decoration:none;font-weight:bold; text-transform:uppercase"  class="menu">Submit Registration</asp:LinkButton></td>
                        <td style="height:38px;width:12px;background-image:url('images/button/rb.png');">
                            &nbsp;</td>
                    </tr>
                </table>
               
            </td>
        </tr>
    </table>
    </asp:Panel>
    <asp:Panel ID="PanelSuccessful" runat="server" Visible="false">
        <table>
            <tr>
                <td colspan="2" style="font-size:16pt;padding-top:25px;padding-bottom:25px;color:#003366">
                    Registration Successful!
                </td>
            </tr>
            <tr>
                <td>Registration key</td><td style="font-size:14pt"><asp:Literal ID="lRegKey" runat="server" Text=""></asp:Literal></td>
            </tr>
            <tr>
                <td>Registration Type</td><td style="font-size:14pt"><asp:Literal ID="lRegType" runat="server" Text=""></asp:Literal></td>
            </tr>
            <tr>
                <td class="style1">Admin user name</td><td style="font-size:14pt" 
                    class="style1"><asp:Literal ID="lAdminUser" runat="server" Text=""></asp:Literal></td>
            </tr>
            <tr>
                <td>Password</td><td style="font-size:14pt"><asp:Literal ID="lPassword" runat="server" Text=""></asp:Literal></td>
            </tr>
            <tr>
                <td colspan="2" style="padding-top:20px">Keep your registration key. You will use this when you new create users.</td>
            </tr>
            <tr>
            <td colspan="2" style="padding-top:20px">Go to <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Default.aspx" style="color:Blue;text-decoration:underline">Login</asp:HyperLink>&nbsp;page to access the system.</td>
            </tr>
            <tr>
                <td colspan="2" style="font-size:16pt;padding-top:25px;">
                    Thank you for using our software!
                </td>
            </tr>
        </table>

    </asp:Panel>
    <asp:Panel ID="PanelUpgrade" runat="server" Visible="false">
        <table>
            <tr>
                <td colspan="2" style="font-size:16pt;padding-top:25px;padding-bottom:25px;color:#003366">
                    Registration Upgrade Successful!
                </td>
            </tr>
            <tr>
                <td>New Registration key</td><td style="font-size:14pt"><asp:Literal ID="lregkey2" runat="server" Text=""></asp:Literal></td>
            </tr>
            <tr>
                <td>Registration Type</td><td style="font-size:14pt"><asp:Literal ID="lregtype2" runat="server" Text=""></asp:Literal></td>
            </tr>
            <tr>
                <td class="style1" colspan="2">You will use the same admin user and password.</td>
            </tr>            
            <tr>
                <td colspan="2" style="padding-top:20px">Keep your registration key. You will use this when you create new users.</td>
            </tr>
            <tr>
            <td colspan="2" style="padding-top:20px">Go to <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/Default.aspx" style="color:Blue;text-decoration:underline">Login</asp:HyperLink>&nbsp;page to access the system.</td>
            </tr>
            <tr>
                <td colspan="2" style="font-size:16pt;padding-top:25px;">
                    Thank you for using our software!
                </td>
            </tr>
        </table>

    </asp:Panel>
    <asp:Panel ID="PanelAlreadyRegisted" runat="server" Visible="false">
        <table>
            <tr>
                <td style="font-size:16pt;padding-top:25px;">
                    You're already registered!
                </td>
            </tr>
            <tr>
                <td style="font-size:10pt;padding-top:25px;">Go to 
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Default.aspx" style="color:Blue;text-decoration:underline">Login</asp:HyperLink>&nbsp;page to access the system.</td>
            </tr>
        </table>
         
    </asp:Panel>
    <asp:Panel ID="PanelRegistrationForge" runat="server" Visible="false">
        <table>
            <tr>
                <td style="font-size:16pt;padding-top:25px;">
                    <asp:Literal ID="laltered" runat="server"></asp:Literal>
                </td>
            </tr>
            
            <tr>
                <td style="font-size:10pt;padding-top:25px;">Contact the software vendor.</td>
            </tr>
        </table>
         
    </asp:Panel>
    <asp:Panel ID="PanelRegistrationError" runat="server" Visible="false">
        <table>
            <tr>
                <td style="font-size:16pt;padding-top:25px;">
                    An error occurred while saving your registration!
                </td>
            </tr>
            <tr>
                <td style="font-size:10pt;padding-top:25px;">(<asp:Literal ID="lmsg" runat="server"></asp:Literal>)</td>
            </tr>
            <tr>
                <td style="font-size:10pt;padding-top:25px;">Please try 
                    <asp:LinkButton ID="lbAgain" runat="server">again</asp:LinkButton>.</td>
            </tr>
        </table>
         
    </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
