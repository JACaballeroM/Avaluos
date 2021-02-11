<%@ Page Language="C#" MasterPageFile="~/MasterPageFE/MasterDetalleFE.master" AutoEventWireup="true"
    CodeFile="SubirAvaluo.aspx.cs" Inherits="SubirAvaluo" Title="Subir avalúo" %>

<%@ Register Src="UserControls/MenuLocal.ascx" TagName="MenuLocal" TagPrefix="uc1" %>
<%@ Register Src="UserControls/Navegacion.ascx" TagName="Navegacion" TagPrefix="uc2" %>
<%@ Register Src="UserControlsCommon/ModalAvaluoError.ascx" TagName="ModalAvaluoError"
    TagPrefix="uc3" %>
<%@ Register Src="UserControlsCommon/Progreso.ascx" TagName="Progreso" TagPrefix="uc4" %>
<%@ Register Src="UserControlsCommon/ModalInfo.ascx" TagName="ModalErrorInfo" TagPrefix="uc5" %>
<%@ Register Src="UserControlsCommon/ModalInfo.ascx" TagName="ModalInfo" TagPrefix="uc6" %>
<%@ Register Src="UserControlsCommon/ModalConfirmarcion.ascx" TagName="ModalConfirmar"
    TagPrefix="uc7" %>
<%@ Register Src="UserControlsCommon/ModalInfoExcepcion.ascx" TagName="ModalInfoExcepcion"
    TagPrefix="uc11" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderDImagen" runat="Server">
    <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/caracter_detalle.jpg" />

    <script type="text/javascript">

        function ocultarModalConfirmar() {
            window.document.getElementById('pnlModalConfirmar').style.visibility = "hidden";
        }

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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderDRuta" runat="Server">
    <uc2:Navegacion ID="Navegacion1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderDContenido" runat="Server">
    <asp:Panel ID="PnlRegAvaluo" Style="display: none;" runat="server" SkinID="Modal">
        <table cellpadding="0" cellspacing="0">
            <tr id="trCabecera" runat="server" class="TablaCabeceraCaja">
                <td class="TextLeftTop">
                    <asp:Label ID="lblTextoTitulo" runat="server" SkinID="None" Style="float: left;">Información</asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 500px; padding: 10px;">
                    <asp:UpdatePanel ID="uppMensajeExito" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                        <ContentTemplate>
                            <asp:Label ID="lblTextoInformacion" runat="server"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td style="padding-left: 5px; padding-right: 5px; padding-bottom: 5px; text-align: center;">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 50%; text-align: center">
                                <asp:Button ID="lnkOk" runat="server" OnClick="lnkOk_Click" Text="Aceptar" CausesValidation="false" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:HiddenField runat="server" ID="HiddenConfirmarModal" />
    <cc1:ModalPopupExtender ID="confirmar_ModalPopupExtender" runat="server" Enabled="True"
        TargetControlID="HiddenConfirmarModal" PopupControlID="pnlModalConfirmar" BackgroundCssClass="PanelModalBackground"
        DropShadow="false" />
    <asp:UpdatePanel ID="UpdatePnlModalConfirmar" runat="server" UpdateMode="Conditional"
        RenderMode="Inline">
        <ContentTemplate>
            <asp:Panel ID="pnlModalConfirmar" SkinID="Modal" Style="display: none;" runat="server">
                <uc7:ModalConfirmar ID="ModalConfirmacion" runat="server" OnClientClick="javascript:ocultarModalConfirmar()"
                    OnConfirmClick="confirmar_ConfirmClick" />
                <uc4:Progreso ID="Progreso1" runat="server" AssociatedUpdatePanelID="UpdatePnlModalConfirmar"
                    DisplayAfter="0" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="uppErrorTareas" runat="server" UpdateMode="Conditional" RenderMode="Inline">
        <ContentTemplate>
            <cc1:ModalPopupExtender ID="mpeErrorTareas" runat="server" Enabled="True" TargetControlID="hlnErrorTareas"
                PopupControlID="panErrorTareas" DropShadow="false" BackgroundCssClass="PanelModalBackground" />
            <asp:HyperLink ID="hlnErrorTareas" runat="server" Style="display: none" />
            <asp:Panel ID="panErrorTareas" runat="server" Style="display: none" SkinID="Modal">
                <uc11:ModalInfoExcepcion ID="errorTareas" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <fieldset class="formulario">
        <legend class="formulario">Información</legend>
        <asp:Label ID="lblInformacion" runat="server" Text="El formato de fichero a subir será en XML y con el formato correcto especificado por la secretaría de finanzas"></asp:Label>
        <br />
    </fieldset>
    <br />
    <fieldset class="formulario">
        <legend class="formulario">Caso del avalúo</legend>
        <asp:Panel ID="PanelBusqueda" runat="server">
            <table class="TextLeftMiddle">
                <tr>
                    <td style="width: 100%">
                        <asp:RadioButton ID="rbNormal" runat="server" GroupName="rbCasoAvaluo" Text="Normal"
                            AutoPostBack="false" Checked="True" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%">
                        <asp:RadioButton ID="rbEspecial" runat="server" GroupName="rbCasoAvaluo" Text="Especial"
                            AutoPostBack="false" Checked="False" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </fieldset>
    <br />
    <fieldset class="formulario">
        <legend class="formulario">Seleccione el fichero XML</legend>
        <br />
        <asp:UpdatePanel ID="UpdatePanelSubir" runat="server" RenderMode="Inline" UpdateMode="Conditional">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnGuardar" />
            </Triggers>
            <ContentTemplate>
                <div>
                    <asp:UpdatePanel ID="UpdatePanelFileUpload" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:FileUpload ID="fileAvaluoXML" runat="server" Width="100%" />
                            <uc4:Progreso ID="ProgresoSubirAvaluo" runat="server" AssociatedUpdatePanelID="UpdatePanelFileUpload"
                                DisplayAfter="0" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <asp:UpdateProgress ID="UpdateProgressSubir" runat="server" AssociatedUpdatePanelID="UpdatePanelSubir">
                    <ProgressTemplate>
                        <div id="progressBackgroundFilter">
                        </div>
                        <div id="processMessage">
                            <asp:Label ID="lblMensaje" runat="server" Text="Subiendo datos..."></asp:Label><br />
                            <br />
                            <asp:Image ID="imagenUpr" runat="server" ImageUrl="~/images/actualizando.gif" />
                            <asp:Button ID="btnCancelarUpdateProgress" runat="server" Text="Cancelar" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <br />
                <div>
                    <asp:UpdatePanel ID="UpdatePanelBotonGuardar" runat="server" RenderMode="Inline"
                        UpdateMode="Conditional">
                        <ContentTemplate>
                            <uc4:Progreso ID="ProgresoBotonGuardar" runat="server" AssociatedUpdatePanelID="UpdatePanelBotonGuardar"
                                DisplayAfter="0" />
                            <table style="width: 100%">
                                <tr>
                                    <td class="TextRigthMiddle">
                                        <asp:Button ID="btnGuardar" Text="Subir" OnClick="btnGuardar_Click" OnClientClick="javascript:ShowWait();"
                                            runat="server" />
                                        <asp:HiddenField ID="HiddenRegAvaluo" runat="server" />
                                        <cc1:ModalPopupExtender ID="btnGuardar_ModalPopupExtenderRegistrado" runat="server"
                                            DynamicServicePath="" Enabled="True" TargetControlID="HiddenRegAvaluo" BackgroundCssClass="PanelModalBackground"
                                            PopupControlID="PnlRegAvaluo">
                                        </cc1:ModalPopupExtender>
                                        <cc1:ModalPopupExtender ID="ModalPopupExtenderError" runat="server" DynamicServicePath=""
                                            Enabled="true" TargetControlID="HiddenError" BackgroundCssClass="PanelModalBackground"
                                            PopupControlID="PanelError">
                                        </cc1:ModalPopupExtender>
                                        <asp:HiddenField runat="server" ID="HiddenError" />
                                        <asp:Panel ID="PanelError" Style="display: none; text-align: center; width: 280px;"
                                            runat="server" SkinID="Modal">
                                            <uc5:ModalErrorInfo ID="ModalErrorInfo" runat="server" OnConfirmClick="ModalErrorInfo_Ok_Click" />
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanelErroresValidacion" runat="server" RenderMode="Inline"
            UpdateMode="Conditional">
            <ContentTemplate>
                <cc1:ModalPopupExtender ID="btnGuardar_ModalPopupExtender" runat="server" DynamicServicePath=""
                    Enabled="true" TargetControlID="HiddenAvaluoError" BackgroundCssClass="PanelModalBackground"
                    PopupControlID="pnlInfoModal">
                </cc1:ModalPopupExtender>
                <asp:HiddenField runat="server" ID="HiddenAvaluoError" />
                <asp:Panel ID="pnlInfoModal" runat="server" SkinID="Modal" Width="750px" HorizontalAlign="Left"
                    Style="display: none;">
                    <uc3:ModalAvaluoError ID="ModalAvaluoError" runat="server" OnConfirmClick="ModalAvaluoError_Ok_Click"
                        BackgroundCssClass="PanelModalBackground" />
                </asp:Panel>
                <asp:HiddenField runat="server" ID="HiddenTokenModal" />
                <cc1:ModalPopupExtender ID="extenderPnlInfoTokenModal" runat="server" Enabled="True"
                    TargetControlID="HiddenTokenModal" PopupControlID="pnlTokenModal" BackgroundCssClass="PanelModalBackground"
                    DropShadow="True" />
                <asp:Panel ID="pnlTokenModal" SkinID="Modal" Style="display: none; width: 280px;"
                    runat="server">
                    <uc6:ModalInfo ID="ModalInfoToken" runat="server" OnConfirmClick="ModalInfoToken_Ok_Click" />
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </fieldset>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderDMenuLocal" runat="Server">
    <uc1:MenuLocal ID="MenuLocal1" runat="server" />
</asp:Content>
