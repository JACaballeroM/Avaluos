<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ModalEstado.ascx.cs" Inherits="UserControls_Confirmarcion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControlsCommon/ModalInfoExcepcion.ascx" TagName="ModalInfoExcepcion" TagPrefix="uc11" %>
  <script type="text/javascript">
          function ocultarPanelError(boton, panel) {
              if (panel.style.display == "block") {
                  panel.style.display = "none";
                  boton.value = "Mostrar Detalles";
              }
              else {
                  panel.style.display = "block";
                  boton.value = "Ocultar Detalles";
              }
          }
    </script>
<style type="text/css">
    .style1
    {
        text-align: left;
        vertical-align: top;
        width: 73%;
    }
</style>
<table style="width: 100%">
    <tr id="trCabecera" runat="server" class="TablaCabeceraCaja">
        <td class="style1">
            <asp:Label ID="lblTextoTitulo" runat="server" SkinID="None" CssClass="TextLeftTop">Cambiar 
            estado al avalúo</asp:Label>
        </td>
        <td style="width: 5%">
            <asp:ImageButton ID="btnBuscarPersonaModalCancelar" runat="server" SkinID="BotonBarraCerrar"
                ImageUrl="~/Images/x.gif" />
        </td>
    </tr>
    <tr class="TextCenterMiddle">
        <td colspan="2">
            <asp:Label ID="lblTextoNuevoEstado" runat="server" SkinID="Titulo2">Nuevo 
            Estado</asp:Label>
            &nbsp;
            <asp:Label ID="LblCancelado" runat="server" Text="Cancelado"></asp:Label>
            <asp:DropDownList ID="ddlEstado" runat="server" Width="50%" Enabled="False" 
                Visible="False">
            </asp:DropDownList>
        </td>
    </tr>
</table>
<table style="width: 100%">
    <tr>
        <td style="width: 50%" class="TextCenterMiddle">
            <asp:Button ID="lnkConfirmar" runat="server" OnClick="lnkConfirmar_Click" Text="Aceptar"/>
        </td>
        <td style="width: 50%" class="TextCenterMiddle ">
            <asp:Button ID="lnkCancelar" runat="server" OnClick="lnkCancelar_Click" Text="Cancelar"/>
        </td>
    </tr>
</table>
<asp:UpdatePanel ID="uppErrorTareas" runat="server" UpdateMode="Conditional" RenderMode="Inline"> 
    <ContentTemplate> 
        <cc1:modalpopupextender ID="mpeErrorTareas" runat="server"  Enabled="True" 
        TargetControlID="hlnErrorTareas" PopupControlID="panErrorTareas" 
        Dropshadow="false" BackgroundCssClass="PanelModalBackground" />
        <asp:HyperLink ID="hlnErrorTareas" runat="server" Style="Display:none" />
        <asp:Panel ID="panErrorTareas" runat="server" Style="Display:none" SkinID="Modal">
            <uc11:ModalInfoExcepcion ID="errorTareas" runat="server" /> 
        </asp:Panel>
    </ContentTemplate> 
</asp:UpdatePanel>