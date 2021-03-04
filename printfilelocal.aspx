<%@ Page Title="" Language="vb" AutoEventWireup="false"  CodeBehind="printfilelocal.aspx.vb" Inherits="dms.printfilelocal"  EnableEventValidation="False" %>
<html>
<head></head>
<body oncontextmenu="return false;" style="background-image:url('images/bg.png'); background-repeat:repeat;margin:0px;padding:0px" scroll="no">
<form id="frm" runat="server" autocomplete="off">
<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="false" > 
</asp:ScriptManager>
    
<%--<div class="mainDiv">--%>
<iframe id="docprint" clientidmode="static" src="" border="0"  visible="true" style="border:ridge 0px #D4D4D4;height:0px" runat="server" width="0px"></iframe>

    <asp:Literal ID="lMsg" runat="server" Visible="false" Text=""></asp:Literal>

                <asp:Panel ID="pnlImageDisp" runat="server" width="100%" height="100%" style="overflow:auto;border:solid 1px gray;text-align:left" visible="false">                
                    <asp:Image ID="imgHandler2" ClientIDMode="Static"  visible="false"  runat="server" />
                </asp:Panel>
                <iframe id="docvw2" src="" border="0"  visible="false" style="border:solid 1px gray" runat="server" width="100%" height="100%"></iframe>                                 
</form>             
<%--</div>--%>
</body>

</html>







    

