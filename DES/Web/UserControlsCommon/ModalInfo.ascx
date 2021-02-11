<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ModalInfo.ascx.cs" Inherits="UserControlsCommon_ModalInfo" %>
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

<asp:UpdatePanel ID="PnlTextoInformacion" runat="server" UpdateMode="Conditional" RenderMode="Inline"> 
<ContentTemplate>
  
<table class="TablaCaja" width="100%">
    <tr id="trCabecera" runat="server" class="TablaCabeceraCaja">
        <td colspan="2">
            <div style="float: left" align="left">
                <asp:Label ID="lblTextoTitulo" runat="server" SkinID="None" Style="float: left;">Información</asp:Label></div>
            <div style="float: right">
                <asp:ImageButton ID="btnCancelar" runat="server" ImageUrl="~/Images/x.gif" OnClick="btnCancelar_Click" />
            </div>
        </td>
    </tr>
    <tr class="TablaCeldaCaja">
        <td style="width:48px">
            <asp:Image runat="server" ID="imgAlert" ImageUrl="~/Images/alert.jpg"  AlternateText="Se necesita confirmación" />
        </td>
        <td align="left">
  
                <asp:Label ID="lblTextoInformacion" runat="server">Información</asp:Label>
   
        </td>
    </tr>
    <tr class="TablaCeldaCaja">
        <td colspan="2" style="text-align: center">
            <asp:Button ID="btnOk" runat="server" OnClick="btnCancelar_Click" CausesValidation="false" Text="Aceptar"></asp:Button>
        </td>
    </tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>

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
