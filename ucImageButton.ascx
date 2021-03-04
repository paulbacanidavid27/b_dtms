<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucImageButton.ascx.vb" Inherits="dms.ucImageButton" %>

<img id="imgProc" style="visibility:hidden" alt="Processing..." src="images/processing.gif" />
<asp:ImageButton ID="imgBT" OnClientClick="document.getElementById('imgProc').style.visibility='hidden';document.getElementById('prc').style.visibility='visible';return true;" style="width:20px;Height:22px" runat="server" />