<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ModalConfirmarcion.ascx.cs"
    Inherits="UserControlsCommon_ModalConfirmacion" %>    

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
        width: 50%;
        height: 18px;
    }
    #tablaModalConfirmacion
    {
        width: 376px;
    }
    .style2
    {
        width: 15px;
    }
</style>

<table cellpadding="0" cellspacing="0" id="tablaModalConfirmacion" 
    align="left"  >
    <tr id="trCabecera" runat="server" class="TablaCabeceraCaja">
        <td class="TextLeftTop" colspan="2">
            <asp:Label ID="lblTextoTitulo" runat="server" SkinID="None" Style="float: left;">Confirmación</asp:Label>
        </td>
    </tr>
    <tr  >
        <td style="padding: 10px;" class="style2" >
            <asp:Image runat="server" ID="imgAlert" ImageUrl="~/Images/alert.jpg" Width="48px"
                Height="48px" AlternateText="Se necesita confirmación" />
        </td>
        <td style="padding: 10px; text-align:general" valign="middle">
            <asp:Label ID="lblTextoConfirmacion" runat="server">¿Desea confirmar la acción?</asp:Label>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 5px; padding-right: 5px; padding-bottom: 5px; text-align: center;"
            colspan="2">
            <table  style="width:103%; height: 11px;">
                <tr>
                    <td  style="text-align:center" class="style1">
                        <%--<asp:LinkButton ID="lnkConfirmar" runat="server" OnClick="lnkConfirmar_Click" CausesValidation="false"
                            ToolTip="Pulsando aceptará la acción">Aceptar</asp:LinkButton>--%>
                        <asp:Button ID="lnkConfirmar" runat="server" OnClick="lnkConfirmar_Click" CausesValidation="false"
                            ToolTip="Pulsando aceptará la acción" Text="Aceptar">
                        </asp:Button>
                    </td>
                    <td style="text-align:center" class="style1">
                          <%-- <asp:LinkButton ID="lnkCancelar" runat="server" OnClick="lnkCancelar_Click" CausesValidation="false"
                            ToolTip="Pulsando cancelará la acción">Cancelar</asp:LinkButton>--%>
                            <asp:Button ID="lnkCancelar" runat="server" OnClick="lnkCancelar_Click" CausesValidation="false"
                            ToolTip="Pulsando cancelará la acción" Text="Cancelar"></asp:Button>
                    </td>
                </tr>
            </table>
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