<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DetalleAvaluo.ascx.cs"
    Inherits="UserControls_DetalleAvaluo" %>
    
<%@ Register Src="ModalEstado.ascx" TagName="ModalEstado" TagPrefix="uc3" %>
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
<fieldset class="formulario">
<legend class="formulario">Detalle del avaluo</legend>
<%--<asp:Label ID="lblTituloAvaluo" runat="server" SkinID="Titulo2" class="TextLeftTop"
    Text="Detalle del avaluo"></asp:Label>--%>
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
<asp:Panel ID="PanelBotones" runat="server">
    <asp:ImageButton ID="btnCambiarEstado" runat="server" ImageUrl="~/Images/back-forth.gif"
        AlternateText="Cambiar estado del avalúo" />        
    <cc1:ModalPopupExtender ID="btnCambiarEstado_ModalPopupExtender" runat="server" DynamicServicePath=""
     Enabled="True" TargetControlID="btnCambiarEstado" PopupControlID="PnlModalCambiarEstado"
     BackgroundCssClass="PanelModalBackground"></cc1:ModalPopupExtender>     
    &nbsp;&nbsp;
    <asp:ImageButton ID="btnGenerarAcuse0" runat="server" AlternateText="Ver avalúos de inmuebles próximos"
        ImageUrl="~/Images/camera.gif" Width="16px" />
    &nbsp;&nbsp;
    <asp:ImageButton ID="btnGenerarAcuse" runat="server" AlternateText="Acuse de recibo"
        ImageUrl="~/Images/two-docs.gif" />
    &nbsp;&nbsp;
    <asp:ImageButton ID="btnNotario" runat="server" AlternateText="Asignar notario" 
        ImageUrl="~/Images/user_p.gif" />
        &nbsp;&nbsp;
    <asp:LinkButton ID="lnkEnValidacion" runat="server" 
        onclick="lnkEnValidacion_Click">En validación</asp:LinkButton>
    &nbsp;&nbsp;<asp:LinkButton ID="lnkAceptado" runat="server" 
        onclick="lnkAceptado_Click">Aceptado</asp:LinkButton>
    &nbsp;&nbsp;<asp:LinkButton ID="lnkRechazado" runat="server">Rechazado</asp:LinkButton>
&nbsp;<asp:HiddenField ID="HiddenIdAvaluo" runat="server" />
   </asp:Panel>
<br />
<asp:Panel ID="panelDetalleAvaluo" runat="server" >
    <table style="width: 100%">
        <tr>
            <td style="width: 20%">
                <asp:Label ID="lblIdAvaluo" runat="server" SkinID="Titulo2" Text="Nº avalúo"></asp:Label>
            </td>
            <td style="width: 30%">
                <asp:Label ID="lblIdAvaluoDato" runat="server" Text="1"></asp:Label>
            </td>
            <td style="width: 20%">
                <asp:Label ID="lblFecha" runat="server" SkinID="Titulo2" Text="Fecha"></asp:Label>
            </td>
            <td style="width: 30%">
                <asp:Label ID="lblFechaDato" runat="server" Text="11-06-2008"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblTipo" runat="server" SkinID="Titulo2" Text="Tipo"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblTipoDato" runat="server" Text="Comercial"></asp:Label>
            </td>
            <td class="TextLeftTop">
                <asp:Label ID="lblEstado" runat="server" SkinID="Titulo2" Text="Estado"></asp:Label>
            </td>
            <td class="TextLeftTop">
                <asp:Label ID="lblEstadoDato" runat="server" Text="Asignado"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblValorComercial" runat="server" SkinID="Titulo2" Text="Valor comercial"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblValorComercialDato" runat="server" Text="$ 13.000"></asp:Label>
            </td>
            <td class="TextLeftTop">
                <asp:Label ID="lblVigente" runat="server" SkinID="Titulo2" Text="Vigente"></asp:Label>
            </td>
            <td class="TextLeftTop">
                <asp:Label ID="lblVigenteDato" runat="server" Text="Si"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblValorCatastral" runat="server" SkinID="Titulo2" Text="Valor Catastral"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblValorCatastralDato" runat="server" Text="$ 13.000"></asp:Label>
            </td>
            <td class="TextLeftTop">
                <asp:Label ID="lblObjeto" SkinID="Titulo2" runat="server" Text="Objeto"></asp:Label>
            </td>
            <td class="TextLeftTop">
                <asp:Label ID="lblObjetoDato" runat="server" Text="Objeto"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblValorReferido" runat="server" SkinID="Titulo2" Text="Valor Referido"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblValorReferidoDato" runat="server" Text="$ 13.000"></asp:Label>
            </td>
            <td class="TextLeftTop">
                <asp:Label ID="lblProposito" runat="server" SkinID="Titulo2" Text="Proposito"></asp:Label>
            </td>
            <td class="TextLeftTop">
                <asp:Label ID="lblPropositoDato" runat="server" Text="Proposito"></asp:Label>
            </td>
        </tr>
        <tr runat="server" id="trNotario">
            <td>
                <asp:Label ID="lblNotario" runat="server" SkinID="Titulo2" Text="Notario"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblNotarioDato" runat="server" Text="234 - Pedro Salas Gómez"></asp:Label>
            </td>
            <td class="TextLeftTop">
                &nbsp;
            </td>
            <td class="TextLeftTop">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Panel>
 <asp:Panel ID="PnlModalCambiarEstado" runat="server" Style="width: 400px; display: none;"
             SkinID="Modal">
            <uc3:ModalEstado ID="ModalEstado1" runat="server" OnConfirmClick="modalEstado_ConfirmClick" />
</asp:Panel>
                    <asp:HiddenField ID="HiddenNumUniAv" runat="server" />
                 </fieldset>