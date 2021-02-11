<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MenuLocal.ascx.cs"
    Inherits="UserControls_MenuLocal" %>
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
<asp:TreeView ID="MenuLocal" SkinID="MenuLocal" runat="server">    
    <Nodes>
        <asp:TreeNode Text="Bandeja de entrada" Value="Bandeja de entrada" NavigateUrl="~/BandejaEntrada.aspx">
        </asp:TreeNode>
        <asp:TreeNode Text="Subir Avalúo" Value="Subir Avalúo" NavigateUrl="~/SubirAvaluo.aspx">
        </asp:TreeNode>
        <asp:TreeNode Text="Descargar Avalúo inicial" Value="Descargar Avalúo inicial" NavigateUrl="~/DescargarAvaluo.aspx">
        </asp:TreeNode>
        <asp:TreeNode Text="Informes" Value="Informes">
                    <asp:TreeNode Text="Investigación de mercado" Value="Investigación de mercado" NavigateUrl="~/InvMercado.aspx"></asp:TreeNode>
                    <asp:TreeNode Text="Cuentas duplicadas" Value="Cuentas duplicadas" NavigateUrl="~/CuentasDuplicadas.aspx"></asp:TreeNode>
         </asp:TreeNode>
                
      <%--  <asp:TreeNode Text="Cambiar mi contraseña" Value="Cambiar mi clave" NavigateUrl="">
        </asp:TreeNode>--%>
    </Nodes>
</asp:TreeView>
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