<%@ Page Title="User Maintenance" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false" 
    CodeBehind="user.aspx.vb" Inherits="dms._user" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register src="ucHr.ascx" tagname="ucHr" tagprefix="uc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register src="UserControlAdminMenuH.ascx" tagname="UserControlAdminMenuH" tagprefix="uc" %>    
<%@ Register src="UserControlPager.ascx" tagname="UserControlPager" tagprefix="uc" %>
<%@ Register src="ucButton.ascx" tagname="ucButton" tagprefix="uc" %>    
<%--menu content start--%>

<%--main content end--%>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <title>User Maintenance</title>
</asp:Content>

<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="HeaderMenuContent">
    <table cellpadding="0" cellspacing="0" border="0" width="100%" class="tableheaderGreen">
                   <tr >
                        <td valign="middle">
                        </td>
                        </tr>
                        </table>
</asp:Content>
<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="AddContent">
    <table cellpadding="0" cellspacing="0" border="0" width="100%" class="tableheaderGreen">
                   <tr >
                        <td valign="middle">
                        </td>
                        </tr>
                        </table>
</asp:Content>
<asp:Content ID="Content5" runat="server" ContentPlaceHolderID="MainFooterContent">
    <table cellpadding="0" cellspacing="0" border="0" width="100%" class="tableheaderGreen">
                   <tr >
                        <td valign="middle">&nbsp;
                        </td>
                        </tr>
                        </table>
</asp:Content>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="AdminMenu">
    <uc:UserControlAdminMenuH id="UserControlAdminMenuH1" runat="server"></uc:UserControlAdminMenuH>                                       
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="MainHeaderContent">

                <table border="0" width="100%" class="tableheaderGreen">
                   <tr >
                        <td valign="middle">
                            <img  alt="" src="images/user2.png" height="20px" width="24px" style="vertical-align:middle" />&nbsp;System Users
                        </td>                 
                        <td>
                        <asp:Label ID="lMsg"  visible="false" runat="server" cssclass="msg_red" Text="No records found. Please try another search criteria."></asp:Label>
                        </td>
                        <td align="right">
                                <asp:UpdatePanel ID="pPager" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                <asp:HiddenField ID="hfCurrent" runat="server" Value="1"/>
                                            <asp:HiddenField ID="hfTotalRows" runat="server" Value="0"/>
                                            <asp:HiddenField ID="hfSortCol" runat="server" Value=""/>
                                            <asp:HiddenField ID="hfSortOrder" runat="server" Value=""/>
                                            <uc:UserControlPager ID="ucPager" runat="server" />
                                </ContentTemplate>
                                </asp:UpdatePanel>
                        </td>
                   </tr>
                    </table>                               
</asp:Content>
<asp:Content ID="mContent" runat="server" ContentPlaceHolderID="menuContent">
                                
                                <table cellpadding="0" cellspacing="0" border="0" style="margin-bottom:5px;margin-left:2px">
                    <tr>
                        <td  class="addbl">
                        </td>
                        <td  class="addbm" valign="middle">
                            <asp:LinkButton ID="lbAddUser" runat="server" class="menu4" ><asp:Image ID="ImageButton1" visible="true" runat="server" ImageUrl="images/userid.png" style="vertical-align:middle;height:12px;width:12px;margin-right:4px;"/>Add New User</asp:LinkButton></td>
                        <td class="addbr">
                        </td>
                    </tr>
                </table>
                <uc:ucButton id="ucMerge" runat="server" pText="Merge Users" pImage="images/copydate.png"></uc:ucButton>

                <div style="border-radius:5px;border-style: solid; border-width: 1px; border-color: #F1F4F8 #CFDBE7 #81A0C0 #CEDAE8; background-color: #FFFFFF; width: 98%; margin-top: 8px; margin-left: 1px">
                                <asp:UpdatePanel ID="pnlFilter" runat="server" UpdateMode="Conditional">
                                   <ContentTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="tableheaderGreen" >
                                        <tr height="25px">
                                            <td align="left"  class="tableHead27" style="padding-left:3px">
                                                <img alt="" width="24px" Height="20px" src="images/find.png" />&nbsp;&nbsp;<asp:Label ID="lbUser" runat="server" style="color:#EEEEEE;font-family:Arial;font-size:10pt;font-weight:bold;font-style:normal;color:#CCCCCC">Filter Users</asp:Label></td>
                                                <td width="50px" align="right" valign="top" class="tableHead27" >
                                                    <asp:ImageButton ID="imgUser" runat="server" imageurl="images/showpanel.png"/></td>
                                        </tr>
                                    </table>
                    
                                <asp:Panel runat="server" ID="pFilter" Visible="false" DefaultButton="btSearch"> 
                                <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                        <tr>
                                            <td align="left" class="labelFreeForm" style="padding-left:5px">
                                                User Login:</td>
                                            <td align="left">                                    
                                                <asp:TextBox ID="UserLogin" runat="server" CssClass="entryfld" maxlength="100" ></asp:TextBox>
                                            </td>
            
                                            <td align="left">
                                                &nbsp;</td>
            
                                        </tr>                                          
                                        <tr>
                                            <td align="left"  class="labelFreeForm"  style="padding-left:5px">
                                                First Name:</td>
                                            <td align="left">
                                                <asp:TextBox ID="FirstName" runat="server"  CssClass="entryfld" maxlength="100" ></asp:TextBox>
                                            </td>
            
                                            <td align="left">
                                                &nbsp;</td>
            
                                        </tr>  
                                        <tr>
                                            <td align="left"  class="labelFreeForm"  style="padding-left:5px">
                                                Last Name:</td>
                                            <td align="left">
                                                <asp:TextBox ID="LastName" runat="server"  CssClass="entryfld" maxlength="100" ></asp:TextBox>
                                            </td>
            
                                            <td align="left">
                                                &nbsp;</td>
            
                                        </tr>  
                                        <tr>
                                            <td align="left"  class="labelFreeForm"  style="padding-left:5px">
                                                Email:</td>
                                            <td align="left">
                                                <asp:TextBox ID="txEmail" runat="server"  CssClass="entryfld" maxlength="100" ></asp:TextBox>
                                            </td>
            
                                            <td align="left">
                                                &nbsp;</td>
            
                                        </tr>  
                                        <tr>
                                            <td align="left"  class="labelFreeForm"  style="padding-left:5px">
                                                Group:</td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlGroup" runat="server" CssClass="entryfld2" Width="300px">
                                                </asp:DropDownList>
                                            </td>
            
                                            <td align="left">
                                                &nbsp;</td>
            
                                        </tr>  
                                        <tr>
                                            <td align="left"  class="labelFreeForm"  style="padding-left:5px">
                                                Role:</td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlRole" runat="server"  CssClass="entryfld2"  Width="300px">
                                        <asp:ListItem Value=""></asp:ListItem>
                                        <asp:ListItem Value="U">User</asp:ListItem>                                        
                                        <asp:ListItem Value="D">Super User</asp:ListItem>
                                        <asp:ListItem Value="R">Report Generator</asp:ListItem>
                                        <asp:ListItem Value="L">Archiving</asp:ListItem>
                                        <asp:ListItem Value="G">Group Officer</asp:ListItem>
                                        <asp:ListItem Value="A">Admin</asp:ListItem>                                                                                            
                                    </asp:DropDownList>
                                            </td>
            
                                            <td align="left">
                                                &nbsp;</td>
            
                                        </tr>  
                                        <tr>
                                            <td align="left">
                                                &nbsp;</td>
                                            <td align="right">
                                                <asp:Button ID="btSearch" runat="server" CssClass="btnsmall" Text="Filter" />
                                            </td>
            
                                            <td align="left">
                                                &nbsp;</td>
            
                                        </tr>                            
                                    </table>
                                </asp:Panel>
                                </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <br />
                                
                                

                                
</asp:content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    
    <div class="mainDiv_" align="left">
    
    <!-- start - resultset //-->                                                                
    <asp:UpdatePanel ID="pnlRepeater" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    <asp:Panel id="pRepeater" runat="server" visible="true">  
    <table border="0" class="codetbl" cellspacing="0" cellpadding="0" style="border-collapse:collapse;z-index:900;width:100%;border:#D4D4D4 solid 1px">
                    <tr >             
                        <td class="newtblheader" style="width:15px"></td>           
                        <td class="newtblheader">
                        <asp:LinkButton ID="lbSort1" runat="server" class="sortcol" tooltip="Sort by User Login" OnClick="sortColumnHeader">User Login</asp:LinkButton><asp:Image ID="imgSort1" imageurl="images/asc.png" runat="server" visible="true"/></td>
                        <td class="newtblheader">
                        <asp:LinkButton ID="lbSort2" runat="server" class="sortcol" tooltip="Sort by First Name" OnClick="sortColumnHeader">First Name</asp:LinkButton><asp:Image ID="imgSort2" imageurl="images/asc.png" runat="server" visible="false"/></td>
                        <td class="newtblheader">
                        <asp:LinkButton ID="lbSort3" runat="server" class="sortcol" tooltip="Sort by Last Name" OnClick="sortColumnHeader">Last Name</asp:LinkButton><asp:Image ID="imgSort3" imageurl="images/asc.png" runat="server" visible="false"/></td>
                        <td class="newtblheader">
                        <asp:LinkButton ID="lbSort4" runat="server" class="sortcol" tooltip="Sort by Email" OnClick="sortColumnHeader">Email</asp:LinkButton><asp:Image ID="imgSort4" imageurl="images/asc.png" runat="server" visible="false"/></td>                        
                        <td class="newtblheader">
                        <asp:LinkButton ID="lbSort5" runat="server" class="sortcol" tooltip="Sort by Group" OnClick="sortColumnHeader">Group</asp:LinkButton><asp:Image ID="imgSort5" imageurl="images/asc.png" runat="server" visible="false"/></td>
                        <td class="newtblheader">
                        <asp:LinkButton ID="lbSort6" runat="server" class="sortcol" tooltip="Sort by Role" OnClick="sortColumnHeader">Role</asp:LinkButton><asp:Image ID="imgSort6" imageurl="images/asc.png" runat="server" visible="false"/></td>
    <asp:Repeater ID="Repeater1" visible="true" runat="server" >

            <HeaderTemplate>
                
                        <td class="newtblheader" align="center" style="width:23px"><asp:ImageButton ID="imgDelete" runat="server" imageurl="images/del.png"/></td>
                    </tr>            
                    
            </HeaderTemplate>
            <ItemTemplate>                
                    <tr >
                        <td style="padding-left:4px;padding-right:4px">
                            <asp:ImageButton ID="imgClose" runat="server" ImageUrl="images/plus.jpg" OnClick="imgPlus"/>
                            <asp:ImageButton ID="imgOpen" runat="server" ImageUrl="images/minus.jpg" Visible="false" OnClick="imgPlus"/></td>
                        <td style="padding-left:2px"><asp:Literal ID="UserId" Visible="false" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "UserId"))%>'></asp:Literal>
                        <asp:TextBox ID="tbUserLogin" runat="server"  cssclass="entryfld" Text='<%#DataBinder.Eval(Container.DataItem, "UserLogin")%>' Visible="false" maxlength="100" Width="99%"></asp:TextBox>
                        <asp:Literal ID="UserLogin" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "UserLogin"))%>'></asp:Literal></td>
                        <td style="padding-left:2px"><asp:Literal ID="FirstName" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "FirstName"))%>'></asp:Literal>
                            <asp:TextBox ID="tbFirstName" runat="server"  cssclass="entryfld" Text='<%#DataBinder.Eval(Container.DataItem, "FirstName")%>' Visible="false" maxlength="100" Width="160px"></asp:TextBox>
                        </td>
                        
                        <td style="padding-left:2px"><asp:Literal ID="LastName" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "LastName"))%>'></asp:Literal>
                            <asp:TextBox ID="tbLastName" runat="server"   cssclass="entryfld" Text='<%#DataBinder.Eval(Container.DataItem, "LastName")%>' Visible="false"  maxlength="100" Width="160px"></asp:TextBox>
                        </td>
                        <td style="padding-left:2px"><asp:Literal ID="Email" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "email"))%>'></asp:Literal>
                            <asp:TextBox ID="tbEmail" runat="server"  cssclass="entryfld"  Text='<%#DataBinder.Eval(Container.DataItem, "email")%>' Visible="false"  maxlength="100" Width="160px"></asp:TextBox>
                        </td>
                        <td style="padding-left:2px"><asp:Literal ID="Group" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "GroupName"))%>'></asp:Literal>
                            <asp:Literal ID="lGroup" runat="server" Visible="false" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "usergroup"))%>' ></asp:Literal>
                            <asp:DropDownList ID="ddlGroupEdit" runat="server"  Visible="false" style="border:solid 1px #C0C0C0;width:180px"  cssclass="entryfld2">                                
                            </asp:DropDownList>
                        </td>
                        <td style="padding-left:2px"><asp:Literal ID="Role" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "urole"))%>'></asp:Literal>
                            <asp:Literal ID="lRole" runat="server" Visible="false" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "userrole"))%>'></asp:Literal>                            
                            
                            <asp:DropDownList ID="ddlRoleEdit" runat="server"  Visible="false"  style="border:solid 1px #C0C0C0;width:100px;"  cssclass="entryfld2">
                                <asp:ListItem Value=""></asp:ListItem>
                                <asp:ListItem Value="U">User</asp:ListItem>
                                <asp:ListItem Value="D">Super User</asp:ListItem>
                                <asp:ListItem Value="R">Report Generator</asp:ListItem>
                                <asp:ListItem Value="L">Archiving</asp:ListItem>
                                <asp:ListItem Value="G">Group Officer</asp:ListItem>
                                <asp:ListItem Value="A">Admin</asp:ListItem>
                                <asp:ListItem Value="B">Bidder</asp:ListItem>
                                <asp:ListItem Value="H">HelpDesk</asp:ListItem>
                            </asp:DropDownList>
                        </td>                                                
                        <td align="center"><asp:CheckBox ID="cbxDelete" runat="server" /></td>
                    </tr>                
                    <tr >
                        <td colspan="8" align="center">
                        
                            <asp:Panel ID="userDetails" runat="server" Visible="false" DefaultButton="btSave">
                            
                            
                            <table border="0" cellpadding="3" style="text-align:left;border: solid 1px #CCCCCC;background-color:#F1F0ED;margin-top:2px;" >
                                <tr>
                                    <td colspan="5" style="font-weight:bold">Security Information</td>
                                    <td><asp:Button ID="btSave" runat="server" Text="Save" cssclass="btnsmall2" OnClick="UpdateUser"/></td>
                                </tr>
                                
                                <tr>
                                    <td colspan="6"><center><hr style="width:99%" /></center></td>
                                </tr>
                                <tr>
                                    <td>Password Expiration:</td>
                                   <td>
                                        <asp:Literal ID="PassWordExpiration" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "PassExpirationDate"))%>'></asp:Literal>
                                        <asp:TextBox ID="tbPassWordExpiration" width="70px"  cssclass="entryfld" runat="server"  maxlength="10" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "PassExpirationDate"))%>' Visible="false"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="tbPassWordExpiration" />
                                   </td>
                                   <td>
                                        Lockout Attempt:
                                   </td>
                                   <td>                                   
                                        <asp:Literal ID="Lockout" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "lockoutattempts"))%>' ></asp:Literal>
                                        <asp:DropDownList ID="ddlLockout" runat="server" Visible="false"  style="border:solid 1px #C0C0C0" cssclass="entryfld2">
                                                <asp:ListItem Value="0">No Lockout</asp:ListItem>
                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                <asp:ListItem Value="6">6</asp:ListItem>
                                                <asp:ListItem Value="7">7</asp:ListItem>
                                                <asp:ListItem Value="8">8</asp:ListItem>
                                                <asp:ListItem Value="9">9</asp:ListItem>
                                                <asp:ListItem Value="10">10</asp:ListItem>
                                            </asp:DropDownList>
                            
                                        </td>
                                        <td>
                                        </td>
                                        <td rowspan="4" ><asp:Image ID="imgPic" runat="server" Height="123px" Width="106px" 
                                    ImageAlign="Middle" style="border:solid 1px #eeeeee" ImageUrl='<%# "images/avatar/" & Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "profilepic"))%>' /></td>
                                   </tr>
                                   <tr>
                                        <td>Can Change Password:</td>
                                        <td><asp:Literal ID="CanChange" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CanChangePass"))%>'></asp:Literal>
                                            <asp:CheckBox ID="cbCanChange" runat="server"  Visible="false" />
                                        </td>   
                                        <td>Locked:</td>                     
                                        <td><asp:Literal ID="Locked" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Locked"))%>'></asp:Literal>
                                            <asp:CheckBox ID="cbLocked" runat="server"  Visible="false"/>
                                            <asp:Literal ID="Email1" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Email"))%>' Visible="false"></asp:Literal>
                                            <asp:TextBox ID="tbEmail1" runat="server"  Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Email"))%>' Visible="false"  style="width:160px" cssclass="entryfld" maxlength="150"></asp:TextBox>
                                        </td>                        
                                        <td></td>                     
                                        <%--<td>
                                        </td>--%>                        
                                </tr>    
                                <tr>
                                <td>Title:</td>
                                        <td><asp:Literal ID="Title" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Title"))%>'></asp:Literal>
                                            <asp:TextBox ID="tbTitle" runat="server"   cssclass="entryfld" Text='<%#DataBinder.Eval(Container.DataItem, "Title")%>' Visible="false" style="width:160px"  maxlength="150"></asp:TextBox>
                                        </td>
                                        <td>Active</td>
                                        <td><asp:Literal ID="lActive" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "dDate"))%>'></asp:Literal>
                                            <asp:CheckBox ID="cbActive" runat="server"  Visible="false"/></td>
                                        <td></td>
                                        <%--<td>
                                        </td>--%>
                                </tr>
                                <tr>
                                    <td>New Password:</td>
                                    <td>
                                        <asp:TextBox ID="tbNewPassword" runat="server"  Text=''  TextMode="Password" Visible="false"  style="width:160px" cssclass="entryfld"  maxlength="50"></asp:TextBox>
                                    </td>
                                    <td>Confirm Password:</td>
                                    <td>
                                        <asp:TextBox ID="tbConfirmPassword" runat="server"  Text='' TextMode="Password"  Visible="false"  style="width:160px" cssclass="entryfld"  maxlength="50"></asp:TextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <%--<td>
                                        </td>--%>
                                </tr>                            
                                <tr>
                                    <td>Created By:</td>
                                    <td>
                                        <asp:Literal ID="CreatedBy" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CBy"))%>'></asp:Literal>                                        
                                    </td>
                                    <td>Created Date:</td>
                                    <td>
                                        <asp:Literal ID="CreatedDate" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CDate"))%>'></asp:Literal>
                                    </td>
                                    <td>
                                    </td>
                                    <td align="center">
                                        Profile Picture
                                    </td>
                                </tr>
                                <tr>
                                        <td colspan="6" align="center">
                                            <asp:Label ID="msg" runat="server" Text="" CssClass="msg_red"></asp:Label></td>
                                </tr>
                            </table>
                            
                            </asp:Panel>
                        
                        </td>
                    </tr>
                    <tr>
                        <td colspan="8" class="tbldashed"></td>
                    </tr>
            </ItemTemplate>
            <FooterTemplate>
            <tr>
                                    <td class="dashremover" colspan="8"></td>
                                </tr>
                 </table>                                
            </FooterTemplate>
        </asp:Repeater>
        </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>
    <!-- end - resultset //-->     
    <!-- end - search criteria //-->
    </div>
</asp:Content>
<asp:Content ID="cntPopup" runat="server" ContentPlaceHolderID="PopupMenu">
    
    <asp:UpdatePanel ID="pnlAddUser" runat="server" UpdateMode="Conditional">
    <ContentTemplate>  
    <asp:Panel id="pAddUser" runat="server" Visible="false" Width="700px" DefaultButton="btSave">
    <!-- start - search criteria //-->
    <center>
    
    
        <table border="0" class="popuphdrbox" cellspacing="0" cellpadding="0" style="border: solid 1px #3A5671;border-collapse:collapse;width:100%">

            <tr>
               <td align="center">
                  <table cellspacing="0" class="popuphdr" cellpadding="0" border="0" style="width:100%">
                        <tr height="30px">
                        <td align="left" valign="middle" colspan="2">&nbsp;<img height="25px" width="20px" src="images/userid.png" />&nbsp;System Users - Add</td>
                                            
                        <td  align="right" valign="top">
                            <asp:ImageButton ID="imgClose" runat="server" imageurl="images/close_window.gif" onmouseover="this.src='images/close_window.gif'"  onmouseout="this.src='images/close_window.gif'" width="18px" Height="18px"/>
                        </td>
                    </tr>
                  </table>
               </td>
            </tr>
            <tr>
            <td style="padding-left:15px">
            <fieldset class="notes" style="margin-right:15px;margin-top:0px">
            <legend>* - Required Field</legend>
            <table align="left">
            
                
            <tr>
                <td align="left" class="labelFreeForm">* User Login:</td>
                <td align="left"><asp:TextBox ID="tbUserID" CssClass="entryfld" runat="server"  width="200px" MaxLength="100"></asp:TextBox>
                </td>
                <td >&nbsp;</td>            
            </tr>
            <tr>
                <td align="left" style="padding-right:10px;" class="labelFreeForm">* First Name:</td>
                <td align="left"><asp:TextBox ID="tbFirstName"  CssClass="entryfld" runat="server"  width="300px" MaxLength="100"></asp:TextBox></td>
                <td align="left">&nbsp;</td>                        
            </tr>
            <tr>
                <td align="left" class="labelFreeForm">* Last Name:</td>        
                <td align="left"><asp:TextBox ID="tbLastName" runat="server" CssClass="entryfld"  width="300px" MaxLength="100"></asp:TextBox></td>
                <td>&nbsp;</td>                
            </tr>
            <tr>
                <td align="left" class="labelFreeForm">&nbsp;&nbsp;Title:</td>
                <td align="left"><asp:TextBox ID="tbTitle" runat="server" CssClass="entryfld"   width="300px" MaxLength="150"></asp:TextBox></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td align="left" class="labelFreeForm">* Email:</td>
                <td align="left"><asp:TextBox ID="tbEmail" runat="server" CssClass="entryfld"  width="300px" MaxLength="150"></asp:TextBox></td>
                <td>&nbsp;</td>                
            </tr>
            <tr>
                <td align="left" class="labelFreeForm">* Role:</td>
                <td align="left">
                    <asp:DropDownList ID="ddlRoleAdd" runat="server" cssclass="entryfld2">
                        <asp:ListItem Value=""></asp:ListItem>
                        <asp:ListItem Value="U">User</asp:ListItem>
                        <asp:ListItem Value="D">Super User</asp:ListItem>
                        <asp:ListItem Value="R">Report Generator</asp:ListItem>
                        <asp:ListItem Value="L">Archiving</asp:ListItem>
                        <asp:ListItem Value="A">Admin</asp:ListItem>
                        <asp:ListItem Value="B">Bidder</asp:ListItem>
                        <asp:ListItem Value="H">Helpdesk</asp:ListItem>                        
                    </asp:DropDownList>
                </td>
                <td>&nbsp;</td>                
            </tr>
            <tr>
                <td align="left" class="labelFreeForm">
                    * Group:</td>
                <td align="left">
                    <asp:DropDownList ID="ddlGroupAdd" runat="server"  cssclass="entryfld2">
                    </asp:DropDownList>
                </td>
                <td>&nbsp;</td>                
            </tr>
            <tr>
                <td align="left" colspan="3">
                        <asp:DropDownList ID="ddlAttempt" runat="server" cssclass="entryfld2" Visible="False">
                            <asp:ListItem Value="1">1</asp:ListItem>
                            <asp:ListItem Value="2">2</asp:ListItem>
                            <asp:ListItem Selected="True" Value="3">3</asp:ListItem>
                            <asp:ListItem Value="4">4</asp:ListItem>
                            <asp:ListItem Value="5">5</asp:ListItem>
                            <asp:ListItem Value="6">6</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                            <asp:ListItem Value="8">8</asp:ListItem>
                            <asp:ListItem Value="9">9</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            </fieldset>
            <fieldset class="notes" style="margin-right:15px">
            <legend>Note: Password should be atleast 8 characters.</legend>
            
            <table align="left">
                
                <tr>
                    <td align="left" class="labelFreeForm">
                      *  Password:</td>
                    <td align="left" colspan="2">
                        <asp:TextBox ID="tbPassword" runat="server" CssClass="entryfld" TextMode="Password" maxlength="50"></asp:TextBox>
                    </td>
                    <td class="notes" style="width:100px" rowspan="3"></td>
                </tr>
                <tr>
                        <td align="left" class="labelFreeForm">
                           * Confirm Password:</td>
                        <td align="left" colspan="2">
                            <asp:TextBox ID="tbConfirmPassword" runat="server" CssClass="entryfld" 
                                TextMode="Password"  maxlength="50"></asp:TextBox>
                        </td>
                        
                </tr>
                <tr>
                        <td align="left" class="labelFreeForm">
                            &nbsp;&nbsp;Password Expiration:<img src="images/question.PNG"  title="By default, password expiration is set to 30 days from current date."/></td>
                        <td align="left" colspan="2">
                            <asp:TextBox ID="tbExpiration" runat="server" width="70px" MaxLength="10" CssClass="entryfld"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="tbExpiration" />
                        </td>
                        
                </tr>
                <tr>
                        <td align="left" class="labelFreeForm">
                            &nbsp;&nbsp;Can Change Password:</td>
                        <td align="left" colspan="2">
                            <asp:CheckBox ID="cbCanChangePass" runat="server" />
                        </td>
                        
                    </tr>
                    <%--<tr>
                        <td align="left"  class="labelFreeForm">
                            &nbsp;&nbsp;Registration Key:</td>
                        <td align="left" colspan="2">
                            <asp:Textbox ID="regKey" runat="server" CssClass="entryfld" Width="200px" />
                        </td>
                        
                    </tr>--%>
                    </table>
                    </fieldset>
                    <table align="left">
                <tr>
                      
                        
                        <td align="left">
                            &nbsp;<asp:Button ID="btSave" runat="server" CssClass="btn" Text="Save" />&nbsp;<asp:Button ID="btClear" runat="server" CssClass="btn" visible="false" Text="Clear" /><asp:Button ID="btClose" runat="server" CssClass="btn" Text="Close" />
                        </td>                        
                </tr>
                
                <tr>
                    <td align="left">
                        <asp:UpdatePanel ID="pnlMsg" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Label ID="msg" runat="server" cssclass="msg_red">&nbsp;</asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                </table>
                
                </td>
                </tr>
                </table>
                
                </center>
    </asp:Panel>
    <cc1:DropShadowExtender ID="dse2" runat="server" TargetControlID="pAddUser" Opacity=".5" Rounded="false" TrackPosition="False"  />
    </ContentTemplate>
    </asp:UpdatePanel>  

    <!-- start - resultset delete//-->                                                                
    <asp:UpdatePanel ID="pnlDeleteUser" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    <asp:Panel id="pDeleteUser" runat="server" visible="false" width="700px">
    <table border="0" class="popuphdrbox" cellspacing="0" cellpadding="0" style="border: solid 1px #3A5671;border-collapse:collapse;width:100%">

            <tr>
               <td align="center">
                  <table cellspacing="0" class="popuphdr" cellpadding="0" border="0" style="width:100%">
                        <tr height="30px">
                        <td align="left" valign="middle" colspan="2">&nbsp;<img height="25px" width="20px" src="images/userid.png" />&nbsp;System Users - Delete</td>
                                            
                        <td  align="right" valign="top">
                            <asp:ImageButton ID="imgClose2" runat="server" imageurl="images/close_window.gif" onmouseover="this.src='images/close_window.gif'"  onmouseout="this.src='images/close_window.gif'" width="18px" Height="18px"/>
                        </td>
                    </tr>
                  </table>
               </td>
            </tr>
            <tr>
            <td align="left" style="padding:2px">
                <table border="0" class="codetbl" cellspacing="0" cellpadding="0" style="border-collapse:collapse;background-color:white;width:100%;border:solid 1px #D4D4D4">
    <asp:Repeater ID="Repeater2" visible="true" runat="server" >
            <HeaderTemplate>
                
                    <tr><td colspan="6" style="background-color:Gray;color:White;padding:2px;">Click on Delete button to confirm deleting of selected records.</td></tr>
                    <tr >                        
                        <td class="newtblheader">User Login</td>
                        <td class="newtblheader">First Name</td>
                        <td class="newtblheader">Last Name</td>
                        <td class="newtblheader">Title</td>
                        <td class="newtblheader">Group</td>
                        <td class="newtblheader">Role</td>                        
                    </tr>            
            </HeaderTemplate>
            <ItemTemplate>                
                    <tr>
                        <td class="tbldashed"><asp:Literal ID="UserId" runat="server" visible="false" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "UserId"))%>'></asp:Literal><asp:Literal ID="UserLogin" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "UserLogin"))%>'></asp:Literal></td>
                        <td class="tbldashed"><asp:Literal ID="FirstName" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "FirstName"))%>'></asp:Literal>
                        
                        </td>
                        <td class="tbldashed"><asp:Literal ID="LastName" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "LastName"))%>'></asp:Literal>
                            
                        </td>
                        <td class="tbldashed"><asp:Literal ID="Title" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Title"))%>'></asp:Literal>
                            
                        </td>
                        <td class="tbldashed"><asp:Literal ID="Group" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Group"))%>'></asp:Literal>
                            
                        </td>
                        <td class="tbldashed"><asp:Literal ID="Role" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Role"))%>'></asp:Literal>
                            
                        </td>                                                
                    </tr>     
                    
            </ItemTemplate>
            <FooterTemplate>
            <tr>
               <td style="border-top:solid 1px #ffffff" colspan="6"></td>
            </tr>
                 
            </FooterTemplate>
                 
        </asp:Repeater>
        </table>
        <table style="margin-top:5px;">
        
        <tr>
                <td colspan="6">
                     <asp:Button ID="btDelete" runat="server" Text="Delete" cssclass="btn" />
                     <asp:Button ID="btCancel" runat="server" Text="Cancel" cssclass="btn" visible="false"/>
                </td>
            </tr>
            <tr>
                   <td colspan="6" align="center">                        
                                <asp:Label ID="msgdelete" runat="server" text="" cssclass="msg_red"></asp:Label>                        
                    </td>
                </tr>
        </table>      
        </td>
            </tr>
            </table>                                          
        
    </asp:Panel>
    <cc1:DropShadowExtender ID="dse3" runat="server" TargetControlID="pDeleteUser" Opacity=".5" Rounded="false" TrackPosition="False"  />
    </ContentTemplate>
    </asp:UpdatePanel>
   
    <!-- end - resultset delete//-->  
    <asp:UpdatePanel ID="pnlCopy" runat="server" UpdateMode="Conditional">
    <ContentTemplate>  
    <asp:Panel id="pCopy" runat="server" Visible="False" Width="700px" DefaultButton="btMerge">
    <!-- start - search criteria //-->
    <center>
    
    
        <table border="0" class="popuphdrbox" cellspacing="0" cellpadding="0" style="border: solid 1px #3A5671;border-collapse:collapse;width:100%">

            <tr>
               <td align="center">
                  <table cellspacing="0" class="popuphdr" cellpadding="0" border="0" style="width:100%">
                        <tr height="30px">
                        <td align="left" valign="middle" colspan="2">&nbsp;<img height="25px" width="20px" src="images/user.png" />&nbsp;Merge Users</td>
                                            
                        <td  align="right" valign="top">
                            <asp:ImageButton ID="ImageButton2" runat="server" imageurl="images/close_window.gif" onmouseover="this.src='images/close_window.gif'"  onmouseout="this.src='images/close_window.gif'" width="18px" Height="18px"/>
                        </td>
                    </tr>
                  </table>
               </td>
            </tr>
            <tr>
            <td style="padding-left:15px">
            <table>
            <tr>
                <td colspan="2" align="left">
                <p class="helpnotes" ><b>Notes:</b><br />
                                                                    1. Existing holidays will be deleted from the destination group before copying from the soure group.<br />                                                                    
                                                                    2. Different groups should be selected in order to copy holidays. <br />                                                                    
                                                                    
                                                                </p>                      
                    
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left">
                    <hr />
                </td></tr>
            
            <tr>
                <td align="right"  class="labelFreeForm" nowrap>Source User</td>
                <td align="left">
                    <asp:DropDownList ID="dlSourceUser" runat="server" cssclass="entryfld2" Visible="True"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                
                <td align="right"  class="labelFreeForm" nowrap>Merge User</td>
                <td align="left">
                    <asp:DropDownList ID="dlMergeUser" runat="server" cssclass="entryfld2" Visible="True"></asp:DropDownList>
                </td>
            </tr>
            
                <tr>
                        
                        <td colspan="2" align="left">
                            <div id="prc" style="visibility: hidden ; font-style: italic;font-size:10px;color:Blue;">
                            <img src="images/processing.gif" /> Processing...
                        </div>
                        <div id="pbtn" style="visibility: visible">
                            <asp:Button ID="btMerge" runat="server" CssClass="btn" Text="Merge"  OnClientClick="document.getElementById('pbtn').style.visibility='hidden';document.getElementById('prc').style.visibility='visible';return true;" UseSubmitBehavior="true"/>&nbsp;<asp:Button ID="btCancelMerge" runat="server" CssClass="btn" Text="Cancel" OnClientClick="document.getElementById('pbtn').style.visibility='hidden';document.getElementById('prc').style.visibility='visible';return true;" UseSubmitBehavior="true"/>
                            </div>
                        </td>                        
                </tr>
                
                <tr>
                    <td align="left" colspan="2">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Label ID="mergemsg" runat="server" cssclass="msg_red">&nbsp;</asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                </table>
                </td>
                </tr>
                </table>
                
                </center>
    </asp:Panel>
    <cc1:DropShadowExtender ID="DropShadowExtender1" runat="server" TargetControlID="pCopy" Opacity=".5" Rounded="false" TrackPosition="True"  />
    </ContentTemplate>
    </asp:UpdatePanel>  
</asp:Content>
    
