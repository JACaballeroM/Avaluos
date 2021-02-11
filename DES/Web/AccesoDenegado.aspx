<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterBase.master" AutoEventWireup="true" CodeFile="AccesoDenegado.aspx.cs" Inherits="AccesoDenegado" Title="Untitled Page" %>

<%@ Register src="UserControlsCommon/MenuCabeceraLogo.ascx" tagname="MenuCabeceraLogo" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            font-size: xx-small;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMenuCabecera" Runat="Server">
    <p>
        <uc1:MenuCabeceraLogo ID="MenuCabeceraLogo1" runat="server" />
    </p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderContentBase" Runat="Server">
<p style="text-align:center">
    <br />
    <br />
    <br />
    <br />
    <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/alert.jpg" />
    <br />
    ACCESO RESTRINGIDO<br />
    <span class="style1">Su usuario no tiene acceso al modulo solicitado, diculpe 
    las molestias. </span>
    <br />
    <br />
    <br />
    </p>
&nbsp; 
</asp:Content>

