<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ModalAvaluoError.ascx.cs"
    Inherits="UserControlsCommon_ModalAvaluoError" %>
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

<table cellpadding="0" cellspacing="0" width="100%">
    <tr id="trCabecera" runat="server" class="TablaCabeceraCaja">
        <td class="TextLeftTop" align="left">
            <asp:Label ID="lblTextoTitulo" runat="server" SkinID="None">Errores de validación de los datos del avalúo</asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="gvErroresAvaluo" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" GridLines="Horizontal" EmptyDataText="No hay existen errores de validación"
                        OnPageIndexChanging="gvErroresAvaluo_PageIndexChanging">
                        <Columns>
                            <asp:BoundField DataField="tipoError" HeaderText="Tipo de error">
                                <FooterStyle Width="25%" />
                            <HeaderStyle Width="25%" />
                            <ItemStyle Width="25%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="descripcion" HeaderText="Descripción">
                                <FooterStyle Width="75%" />
                            <HeaderStyle Width="75%" />
                            <ItemStyle Width="75%" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td style="text-align: center;">
            <asp:Button ID="btnAceptar" runat="server" Class="TextoBoton" Text="Aceptar" OnClick="btnAceptar_Click">
            </asp:Button>
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