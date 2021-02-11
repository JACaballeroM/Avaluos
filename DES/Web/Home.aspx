<%@ Page Language="C#" MasterPageFile="~/MasterPageFE/MasterPrincipalFE.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" Title="FUENTES EXTERNAS - Avalúo -" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPDestacados" Runat="Server">
    
    <asp:LinkButton ID="HyperLink1" runat="server" 
        PostBackUrl="~/BandejaEntrada.aspx">Bandeja de entrada</asp:LinkButton>
    
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderPActualidad" Runat="Server">
    <asp:Label ID="lblTituloActualidad" runat="server" SkinID="Titulo2" 
        Text="Actualidad"></asp:Label>
    <br />
    <br />
    
    <div style="language:es-MX;margin-top:0pt;margin-bottom:0pt;margin-left:.09in;
text-indent:-.09in;text-align:left;direction:ltr;unicode-bidi:embed;vertical-align:
baseline">
        <span style="font-size:10.0pt"><span style="mso-special-format:bullet">•</span></span><span style="font-size:10.0pt;font-family:Arial;mso-ascii-font-family:Arial;
mso-fareast-font-family:+mn-ea;mso-bidi-font-family:+mn-cs;mso-fareast-theme-font:
minor-fareast;mso-bidi-theme-font:minor-bidi;color:#5F5F5F;mso-font-kerning:
12.0pt;language:es;mso-style-textfill-type:solid;mso-style-textfill-fill-color:
#5F5F5F;mso-style-textfill-fill-alpha:100.0%">Este módulo permite presentar avalúos 
        (para declaraciones ISAI, actualizaciones catastral, dictamen, etc.), 
        proporcionando la funcionalidad necesaria para el registro y gestión de avalúos. 
        Destinado a los peritos valuadores y sociedades, para registrar avalúos en un<span 
            style="mso-spacerun:yes">&nbsp; </span>formato unificado. Estos pueden ser 
        avalúos comerciales o catastrales.<br />
        </span>
    </div>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPPromoOvica" Runat="Server">
    <asp:Label ID="lblTituloPromoOvica" runat="server" SkinID="Titulo2" 
        Text="Destacados"></asp:Label>
    <br />
        
        <li>
            <asp:HyperLink ID="LinkOvica" runat="server"  NavigateUrl="http://www.finanzas.df.gob.mx/" Font-Size="Small"> 
            Secretaría de Finanzas del Distrito Federal </asp:HyperLink>
        </li>
        
        <li>
            <asp:HyperLink ID="HyperLink3" runat="server"  NavigateUrl="http://www.finanzas.df.gob.mx/servicios/" Font-Size="Small"> 
            Servicios al contribuyente</asp:HyperLink>
        </li>
        
        <li>
            <asp:HyperLink ID="HyperLink2" runat="server"  NavigateUrl="http://ovica.finanzas.df.gob.mx/" Font-Size="Small"> 
            Oficina Virtual del Catastro </asp:HyperLink>
        </li>
        

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderPPromoInstitucional" Runat="Server">
    </asp:Content>

