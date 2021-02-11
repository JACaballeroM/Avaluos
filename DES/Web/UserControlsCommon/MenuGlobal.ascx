<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MenuGlobal.ascx.cs" Inherits="UserControlsCommon_MenuGlobal" %>
&nbsp &nbsp<asp:LinkButton  ID="LinkButton2" SkinID="Global" Text="Avalúos" runat="server" PostBackUrl="~/Home.aspx"></asp:LinkButton>
<%--<asp:Menu ID="MenuGlobalFuentes" runat="server" SkinID="Global">
    <Items>
        <asp:MenuItem Text="Inicio" Value="Home" NavigateUrl="~/Home.aspx"></asp:MenuItem>
        <asp:MenuItem Text="Avaluos" Value="Avaluos" NavigateUrl="~/SUBAVALUOS/BandejaEntrada.aspx">
        </asp:MenuItem>
        <asp:MenuItem Text="ISAI" Value="ISAI" NavigateUrl="~/SUBISAI/BandejaEntrada.aspx">
        </asp:MenuItem>
        <asp:MenuItem Text="Dictamenes" Value="Dictamenes"></asp:MenuItem>
        <asp:MenuItem Text="Licencias" Value="Licencias"></asp:MenuItem>
        <asp:MenuItem Text="Fiscalización" Value="Fiscalización"></asp:MenuItem>
    </Items>
    <StaticItemTemplate>
        <%# Eval("Text") %>
    </StaticItemTemplate>
</asp:Menu>--%>