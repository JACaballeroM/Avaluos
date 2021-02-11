<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterBase.master" AutoEventWireup="true" CodeFile="InformeAvaluo.aspx.cs" Inherits="InformeAvaluo" Title="Informe - Avalúo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register Src="UserControlsCommon/ModalInfoExcepcion.ascx" TagName="ModalInfoExcepcion" TagPrefix="uc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        .style4
        {
            width: 90px;
        }
    </style>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMenuCabecera" runat="Server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenuGlobal" runat="Server">
    <h2>&nbsp;&nbsp;Avalúos - Justificante avalúo</h2>
    <p>&nbsp;</p>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderContentBase" runat="Server">

   <asp:Panel ID="Panel1" runat="server"></asp:Panel>
   
      <rsweb:ReportViewer ID="rpvAvaluo" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="700px" style="margin-left: 0px" Width="769px"  ShowPrintButton="False" ShowRefreshButton="False" ExportContentDisposition="OnlyHtmlInline">
        <LocalReport EnableExternalImages="True"></LocalReport>
    </rsweb:ReportViewer>
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
 
</asp:Content>

