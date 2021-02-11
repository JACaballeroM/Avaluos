<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Paginacion.ascx.cs" Inherits="UserControlsCommon_Paginacion" %>

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
        width: 1043px;
    }
</style>


<table style="width:100%;">
     <tr>
        <td>
                <asp:LinkButton ID="lnkFirtsPage" runat="server" ToolTip="Ir a Primera Pagina" Text="<<" onclick="lnkFirtsPage_Click"> </asp:LinkButton>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:LinkButton ID="lnkPrevPage" runat="server" ToolTip="Pág. anterior" Text="<" onclick="lnkPrevPage_Click"> </asp:LinkButton>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:LinkButton ID="lnkNextPage" runat="server" ToolTip="Sig. página" Text=">" onclick="lnkNextPage_Click"> </asp:LinkButton>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:LinkButton ID="lnkLastPage" runat="server" ToolTip="Ir a Ultima Pagina" Text=">>" onclick="lnkLastPage_Click"> </asp:LinkButton>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblira" runat="server" Text="Ir a:" />
                &nbsp;
                <asp:TextBox ID="txtIraPag" runat="server" AutoPostBack="true" OnTextChanged="txtIraPag_TextChanged" />
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtIraPag" EnableViewState="False"
                     Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ToolTip="El Campo es númerico" Type="Integer"
                    ValidationGroup="ValidarConfiguracion">*</asp:CompareValidator>
                &nbsp;
                &nbsp;
                <asp:Label ID="lblTotalNumberOfPages" runat="server" />
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
