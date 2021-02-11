<%@ Page Language="C#" MasterPageFile="~/MasterPageFE/MasterDetalleFE.master" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="Error" Title="Error" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<%@ Register src="UserControls/MenuLocal.ascx" tagname="MenuLocal" tagprefix="uc1" %>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderDImagen" Runat="Server">
    <asp:Image ID="Image1" runat="server" 
        ImageUrl="~/Images/caracter_detalle.jpg" />
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderDRuta" Runat="Server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderDContenido" Runat="Server">
    <asp:Label ID="LabelError" runat="server" CssClass="TextoNormalError" 
        Text="Error en la pagina, si el problema persiste pongase en contacto con la secretaria de finanazas"></asp:Label>
    <br />
        <a class="TextoNormal" href="mailto:bacalzada@finanzas.df.gob.mx" > Contactar con el Administrador </a>
    <br />
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderDMenuLocal" Runat="Server">
    
    <uc1:MenuLocal ID="MenuLocal1" runat="server" />
    
</asp:Content>
